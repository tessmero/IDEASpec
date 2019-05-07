
/**
 *
 * @author Chris Hall
 * <p>
 * This program is a DLL designed to allow for other programs to control the
 * ChrisBlaster FPGA timing controller. It uses DPCUTIL.DLL from Digilent Inc.
 * for the USB I/O.
 *
 * */

#using <mscorlib.dll>

#pragma unmanaged

#if defined (_WIN32) || defined (__WIN32__) || defined (WIN32)
#define LIBEXPORT __declspec(dllexport)
#define LIBCALL __stdcall
#else
#define LIBEXPORT
#define LIBCALL
#endif

#if defined (_WIN32) || defined (__WIN32__) || defined (WIN32)
#include <Windows.h>
#define CROSSPLATSLEEP(w) Sleep(w)
#else
#include <unistd.h>
#define CROSSPLATSLEEP(w) usleep(w * 1000)
#endif

// Note: HANDLE defined in winnt.h as "typedef void *HANDLE;"

//// includes
#include "chrisblaster_ChrisBlasterManager.h"
#include "ChrisBlasterHost.h"
#include "CBlasterOS.h"
#include "dpcdefs.h"
#include "dpcutil.h"
#include <stdio.h>
#include <stdlib.h>
#include <string>
using namespace std;

/*
LIBEXPORT return_type LIBCALL function_name(parameters)
 * */

#ifdef __cplusplus
extern "C" {
#endif
    // Function Declarations go here:
    LIBEXPORT void LIBCALL getErrorMessage(char* msgbuff, int length);
    LIBEXPORT string LIBCALL errorCodeToMessage(int code);
    LIBEXPORT bool LIBCALL initDrivers();
    LIBEXPORT bool LIBCALL openChrisBlasterConnection(char* name, HANDLE* devpointer);
    LIBEXPORT bool LIBCALL closeChrisBlasterConnection(HANDLE devpointer);

    LIBEXPORT long LIBCALL getClockFrequency(HANDLE devpointer);
    LIBEXPORT bool LIBCALL getSlowIOlimits(unsigned long* numouts, unsigned long* numins, HANDLE devpointer);
    LIBEXPORT bool LIBCALL getBoardStatus(BYTE* bstat, HANDLE devpointer);
    LIBEXPORT BYTE LIBCALL castIntToByte(long in);
    LIBEXPORT long LIBCALL castByteToInt(BYTE in);

    LIBEXPORT bool LIBCALL allocateNewProtocol(long pindex, long numloops, long numbitmasks, HANDLE devpointer);
    LIBEXPORT bool LIBCALL repeatLoop(long pindex, long lindex, long numreps,HANDLE devpointer);
    LIBEXPORT bool LIBCALL addBitmask(long pindex, long lindex, unsigned long bitmask, unsigned long cycles,HANDLE devpointer);
    LIBEXPORT bool LIBCALL setTerminalBitmask(long pindex, unsigned long bitmask,HANDLE devpointer);
    LIBEXPORT bool LIBCALL freeMemory(long pindex, HANDLE devpointer);
    LIBEXPORT bool LIBCALL runProtocol(long pindex, HANDLE devpointer); // doesn't return until finished
    LIBEXPORT bool LIBCALL startProtocol(long pindex, HANDLE devpointer); // returns immediately, need to you checkIfDone() to determine when protocol is done
    LIBEXPORT bool LIBCALL checkIfDone(HANDLE devpointer);
    LIBEXPORT bool LIBCALL killProtocol(HANDLE devpointer);
    LIBEXPORT bool LIBCALL changeOutputBitmask(unsigned long bitmask,HANDLE devpointer);
    LIBEXPORT bool LIBCALL slowIOreadByte(BYTE address, BYTE* getbyte, HANDLE devpointer);
    LIBEXPORT bool LIBCALL slowIOwriteByte(BYTE address,BYTE setbyte, HANDLE devpointer);
    LIBEXPORT bool LIBCALL refreshBoard(HANDLE devpointer);

    LIBEXPORT bool LIBCALL sendCommand(BYTE command,HANDLE devpointer);
    LIBEXPORT bool LIBCALL sendCommandWithParameters(BYTE command,long p1, long p2, long p3, long p4, HANDLE devpointer);
    LIBEXPORT bool LIBCALL writeInt32(unsigned long number, BYTE startindex, HANDLE devpointer);
    LIBEXPORT bool LIBCALL readInt32(unsigned long* number, BYTE startindex, HANDLE devpointer);
    
#ifdef __cplusplus
}
#endif

// Global Variables
string error_message = "No errors yet.";

HANDLE device_table[256]; // for JNI functions
const long device_limit = 256; // for JNI functions

/*
 * bool initDrivers()
 * <p>
 * returns true if successful, false otherwise
 * <p>
 * Must be called before anything else will work
 * */
LIBEXPORT bool LIBCALL initDrivers(){
    for(int i = 0; i < 256; i++){
        device_table[i] = NULL;
    }
    ERC code = 0;
    bool success = true;
    success = success && DpcInit(&code);
    error_message = errorCodeToMessage(code);
    return success;
}

/**
 * bool openChrisBlasterConnection(char* name, HANDLE* devpointer)
 * <p>
 * returns true if connection was successful, false otherwise
 * <p>
 * Before using any data transfer functions, the application must connect to a
 * communication device using DpcOpenData. The first parameter is a pointer to
 * an interface handle (hif). If the function returns successfully, this handle
 * will be used to connect to the device in all proceeding data transfer calls.
 * The device is specified by its assigned name in the device table and passed
 * as the second parameter in the DpcOpenData function.
 * */
LIBEXPORT bool LIBCALL openChrisBlasterConnection(char* name, HANDLE* devpointer){
    ERC code = 0;
    bool success = true;
    success = success && DpcOpenData(devpointer, name, &code, NULL);
    error_message = errorCodeToMessage(code);
    return success;
}
/**
 * bool closeChrisBlasterConnection(HANDLE devpointer)
 * <p>
 * returns true if connection was successfully closed, false otherwise
 * */
LIBEXPORT bool LIBCALL closeChrisBlasterConnection(HANDLE devpointer){
    ERC code = 0;
    bool success = true;
    success = success && DpcCloseData(devpointer,&code);
    error_message = errorCodeToMessage(code);
    return success;
}

/**
 * long getClockFrequency(HANDLE devpointer)
 * <p>
 * returns the FPGA clock frequency in Hz. This should be used to calibrate 
 * the conversion of times into clock cycles. Returns 0 if the connection fails.
 * */
LIBEXPORT long LIBCALL getClockFrequency(HANDLE devpointer){
    bool success = true;
    unsigned long freq = 0;
    success = success && sendCommand((BYTE) GET_CLOCK, devpointer);
    success = success && readInt32(&freq,(BYTE) REGISTER_DATA,devpointer);
    if(success){
        return freq;
    } else {
        return 0;
    }
}
/**
 * bool getSlowIOlimits(long* numouts, long* numins, HANDLE devpointer)
 * <p>
 * stores the number of slow output bytes in numouts and the number of input
 * bytes in numins. Returns true if the connection was successful and false
 * otherwise.
 * */
LIBEXPORT bool LIBCALL getSlowIOlimits(unsigned long* numouts, unsigned long* numins, HANDLE devpointer){
    bool success = TRUE;
    success = success && sendCommand((BYTE) SLOW_IO_GET_READ_SIZE, devpointer);
    success = success && readInt32(numins,(BYTE) REGISTER_DATA,devpointer);
    success = success && sendCommand((BYTE) SLOW_IO_GET_WRITE_SIZE, devpointer);
    success = success && readInt32(numouts,(BYTE) REGISTER_DATA,devpointer);
    return success;
}
/**
 *  bool  getBoardStatus(BYTE* bstat, HANDLE devpointer)
 * <p>
 * gets the current status of the board and stores it in bstat.
 * <p>
 * returns true if the connection was successful
 * */
LIBEXPORT bool LIBCALL getBoardStatus(BYTE* bstat, HANDLE devpointer){
    ERC code = 0;
    bool success = TRUE;
    success = success && DpcGetReg(devpointer, (BYTE) REGISTER_STATUS, bstat, &code, NULL);
    error_message = errorCodeToMessage(code);
    return success;
}
/**
 *  BYTE  castIntToByte(long in)
 * <p>
 * returns the BYTE conversion of in [0 , 256)
 * */
LIBEXPORT BYTE LIBCALL castIntToByte(long in){
    return (BYTE) (in % 256);
}
/**
 * long  castByteToInt(BYTE in)
 * <p>
 * returns the 32-bit integer conversion of in
 * */
LIBEXPORT long LIBCALL castByteToInt(BYTE in){
    return (long) in;
}

/**
 *  bool  allocateNewProtocol(long pindex, long numloops, long numbitmasks, HANDLE devpointer)
 * <p>
 * Tells the ChrisBlaster board to allocate memory for a protocol at index
 * numner pindex.
 * <p>
 * returns true on successful execution, false otherwise
 * */
LIBEXPORT bool LIBCALL allocateNewProtocol(long pindex, long numloops, long numbitmasks, HANDLE devpointer){
    return sendCommandWithParameters((BYTE) NEW_PROTOCOL,
            pindex, numloops, numbitmasks, 0, devpointer);
}
/**
 *  bool LIBCALL (long pindex, long lindex, long numreps,HANDLE devpointer)
 * <p>
 * Tells the ChrisBlaster board to how many times to loop through the bitmasks
 * of protocol number pindex, loop number lindex
 * <p>
 * returns true on successful execution, false otherwise
 * */
LIBEXPORT bool LIBCALL repeatLoop(long pindex, long lindex, long numreps,HANDLE devpointer){
    return sendCommandWithParameters((BYTE) SET_LOOP_REP,
            pindex,  lindex,  numreps, 0, devpointer);
}
/**
 *  bool  addBitmask(long pindex, long lindex, unsigned long bitmask, unsigned long cycles,HANDLE devpointer);
 * <p>
 * adds the specified bitmask to protocol number pindex, loop number lindex, and
 * sets that bitmask to persist for cycles clock cycles.
 * <p>
 * returns true on successful execution, false otherwise
 * */
LIBEXPORT bool LIBCALL addBitmask(long pindex, long lindex, unsigned long bitmask, unsigned long cycles,HANDLE devpointer){
    return sendCommandWithParameters((BYTE) ADD_BITMASK,
            pindex,  lindex,  bitmask, cycles, devpointer);
}
/**
 *  bool  setTerminalBitmask(long pindex, unsigned long bitmask,HANDLE devpointer)
 * <p>
 * Sets the bitmask that protocol number pindex should revert to after it
 * completes its execution
 * <p>
 * returns true on successful execution, false otherwise
 * */
LIBEXPORT bool LIBCALL setTerminalBitmask(long pindex, unsigned long bitmask,HANDLE devpointer){
    return sendCommandWithParameters((BYTE) SET_TERMINAL_BITMASK,
            pindex,  bitmask, 0, 0, devpointer);
}
/**
 *  bool  freeMemory(long pindex, HANDLE devpointer)
 * <p>
 * frees protocol number pindex from the board's memory and erases its program
 * <p>
 * returns true on successful execution, false otherwise
 * */
LIBEXPORT bool LIBCALL freeMemory(long pindex, HANDLE devpointer){
    return sendCommandWithParameters((BYTE) ERASE_PROTOCOL,
            pindex,  0, 0, 0, devpointer);
}
/**
 *  bool  runProtocol(long pindex, HANDLE devpointer)
 * <p>
 * Starts the indicated protocol and then does not return until that protocol is
 * finished or an error occures.
 * <p>
 * returns true after successful execution, false if an error occures
 * */
LIBEXPORT bool LIBCALL runProtocol(long pindex, HANDLE devpointer){
    ERC code = 0;
    bool success = TRUE;
    success = success && sendCommandWithParameters((BYTE) RUN_PROTOCOL,
            pindex,  0, 0, 0, devpointer);
    BYTE boardstat;
    do{
        CROSSPLATSLEEP(100);
        success = success && DpcGetReg(devpointer, (BYTE) REGISTER_STATUS, &boardstat, &code, NULL);
    } while(success && boardstat == STATUS_RUNNING);
    error_message = errorCodeToMessage(code);
    if(boardstat == STATUS_ERROR){
        return FALSE;
    }
    return success;
}
/**
 *  bool  startProtocol(long pindex, HANDLE devpointer)
 * <p>
 * Starts the indicated protocol and then returns. You should use checkIfDone()
 * to periodically poll the ChrisBlaster to know when the protocol is finished
 * <p>
 * returns true if the command was sent successfully, false if an error occures
 * */
LIBEXPORT bool LIBCALL startProtocol(long pindex, HANDLE devpointer){
    return sendCommandWithParameters((BYTE) RUN_PROTOCOL,
            pindex,  0, 0, 0, devpointer);
}
/**
 *  bool  checkIfDone(HANDLE devpointer)
 * <p>
 * returns true if the last protocol has finished, false if it is still running
 * */
LIBEXPORT bool LIBCALL checkIfDone(HANDLE devpointer){
    BYTE status = 0;
    getBoardStatus(&status, devpointer);
    if(status == STATUS_RUNNING) {
        return FALSE;
    } else {
        return TRUE;
    }
}
/**
 *  bool  killProtocol(HANDLE devpointer)
 * <p>
 * Kills the currently running protocol and sets the bitmask to zeroes
 * <p>
 * returns true if the command was sent successfully, false if an error occures
 * */
LIBEXPORT bool LIBCALL killProtocol(HANDLE devpointer){
    return sendCommand(ABORT,devpointer);
}
/**
 *  bool  changeOutputBitmask(unsigned long bitmask,HANDLE devpointer)
 * <p>
 * changes the current non-protocol bitmask. This is primarily used for
 * controlling the actinic lights
 * <p>
 * returns true if the command was sent successfully, false if an error occures
 * */
LIBEXPORT bool LIBCALL changeOutputBitmask(unsigned long bitmask,HANDLE devpointer){
    return sendCommandWithParameters((BYTE) SET_CURRENT_BITMASK,
            bitmask,  0, 0, 0, devpointer);
}
/**
 *  bool  slowIOreadByte(BYTE address, BYTE* getbyte, HANDLE devpointer)
 * <p>
 * reads the slow input byte whose index is address into getbyte
 * <p>
 * returns true if the command was sent successfully, false if an error occures
 * */
LIBEXPORT bool LIBCALL slowIOreadByte(BYTE address, BYTE* getbyte, HANDLE devpointer){
    bool success = TRUE;
    success = success && sendCommandWithParameters((BYTE) SLOW_IO_READ_BYTE,
            address,  0, 0, 0, devpointer);
    unsigned long slowbyte;
    success = success && readInt32(&slowbyte, (BYTE) REGISTER_DATA, devpointer);
    *getbyte = (BYTE) (slowbyte % 256);
    return success;
}
/**
 *  bool  slowIOwriteByte(BYTE address,BYTE setbyte, HANDLE devpointer)
 * <p>
 * writes to the slow output byte whose index is address
 * <p>
 * returns true if the command was sent successfully, false if an error occures
 * */
LIBEXPORT bool LIBCALL slowIOwriteByte(BYTE address,BYTE setbyte, HANDLE devpointer){
    return sendCommandWithParameters((BYTE) SLOW_IO_WRITE_BYTE,
            address,  setbyte, 0, 0, devpointer);
}
/**
 *  bool  refreshBoard(HANDLE devpointer)
 * <p>
 * resets the board status, so if it was previously in an error state, it is now
 * back to the ready state
 * <p>
 * returns true if the command was sent successfully, false if an error occures
 * */
LIBEXPORT bool LIBCALL refreshBoard(HANDLE devpointer){
    return sendCommand(RESET_STATUS,devpointer);
}

LIBEXPORT bool LIBCALL sendCommand(BYTE command,HANDLE devpointer){
    ERC code;
    bool success = TRUE;
    success = success && DpcPutReg(devpointer, (BYTE) REGISTER_COMMAND, command, &code, NULL);
    success = success && DpcPutReg(devpointer, (BYTE) REGISTER_SWAP, SWAP_ACTIVE, &code, NULL);
    if(!success){ // errors occured
        error_message = errorCodeToMessage(code);
        return success;
    }
    BYTE swapvalue;
    do{
        success = success && DpcGetReg(devpointer, (BYTE) REGISTER_SWAP, &swapvalue, &code, NULL);
    } while(success && swapvalue == SWAP_ACTIVE);
    return success;
}
LIBEXPORT bool LIBCALL sendCommandWithParameters(BYTE command,long p1, long p2, long p3, long p4, HANDLE devpointer){
    ERC code;
    bool success = TRUE;
    // first, set the parameters
    success = success && writeInt32(p1, (BYTE) REGISTER_PARAM1, devpointer);
    success = success && writeInt32(p2, (BYTE) REGISTER_PARAM2, devpointer);
    success = success && writeInt32(p3, (BYTE) REGISTER_PARAM3, devpointer);
    success = success && writeInt32(p4, (BYTE) REGISTER_PARAM4, devpointer);
    // then send the command code and then set the swap to active
    success = success && DpcPutReg(devpointer, (BYTE) REGISTER_COMMAND, command, &code, NULL);
    success = success && DpcPutReg(devpointer, (BYTE) REGISTER_SWAP, SWAP_ACTIVE, &code, NULL);
    if(!success){ // errors occured
        error_message = errorCodeToMessage(code);
        return success;
    }
    BYTE swapvalue;
    do{
        success = success && DpcGetReg(devpointer, (BYTE) REGISTER_SWAP, &swapvalue, &code, NULL);
    } while(success && swapvalue == SWAP_ACTIVE);
    return success;
}
LIBEXPORT bool LIBCALL writeInt32(unsigned long number, BYTE startindex, HANDLE devpointer){
    ERC code;
    bool success = TRUE;
    success = success && DpcPutReg(devpointer, (BYTE) startindex, (BYTE) Int32ToByte0(number), &code, NULL);
    success = success && DpcPutReg(devpointer, (BYTE) (startindex + 1), (BYTE) Int32ToByte1(number), &code, NULL);
    success = success && DpcPutReg(devpointer, (BYTE) (startindex + 2), (BYTE) Int32ToByte2(number), &code, NULL);
    success = success && DpcPutReg(devpointer, (BYTE) (startindex + 3), (BYTE) Int32ToByte3(number), &code, NULL);
    if(!success) error_message = errorCodeToMessage(code);
    return success;
}
LIBEXPORT bool LIBCALL readInt32(unsigned long* number, BYTE startindex, HANDLE devpointer){
    ERC code;
    bool success = TRUE;
    BYTE bb[4];
    success = success && DpcGetReg(devpointer, (BYTE) startindex, &bb[0], &code, NULL);
    success = success && DpcGetReg(devpointer, (BYTE) (startindex + 1), &bb[1], &code, NULL);
    success = success && DpcGetReg(devpointer, (BYTE) (startindex + 2), &bb[2], &code, NULL);
    success = success && DpcGetReg(devpointer, (BYTE) (startindex + 3), &bb[3], &code, NULL);
    if(!success){ // errors occured
        error_message = errorCodeToMessage(code);
        return success;
    }
    *number = BytesToInt32(bb[0],bb[1],bb[2],bb[3]);
    return success;
}


/**
 * LIBEXPORT void LIBCALL getErrorMessage(char* msgbuff, int length)
 * <p>
 * Puts  the last error message in msgbuff
 * */
LIBEXPORT void LIBCALL getErrorMessage(char* msgbuff, int length){
    strncpy(msgbuff, error_message.c_str(), length);
}
/**
 * conversts the error code into a short message based on dpcdefs.h
 * */
LIBEXPORT string LIBCALL errorCodeToMessage(int code){
    string message;
    switch (code){
        case ercNoError:
            message = "No error";
            break;
        case ercConnReject:
            message = "ercConnReject";
            break;
        case ercConnType:
            message = "ercConnType";
            break;
        case ercConnNoMode:
            message = "ercConnNoMode";
            break;
        case ercInvParam:
            message = "ercInvParam";
            break;
        case ercInvCmd:
            message = "ercInvCmd";
            break;
        case ercUnknown:
            message = "ercUnknown";
            break;
        case ercJtagConflict:
            message = "ercJtagConflict";
            break;
        case ercNotImp:
            message = "ercNotImp";
            break;
        case ercNoMem:
            message = "ercNoMem";
            break;
        case ercTimeout:
            message = "ercTimeout";
            break;
        case ercConflict:
            message = "ercConflict";
            break;
        case ercBadPacket:
            message = "ercBadPacket";
            break;
        case ercInvOption:
            message = "ercInvOption";
            break;
        case ercAlreadyCon:
            message = "ercAlreadyCon";
            break;
        case ercConnected:
            message = "ercConnected";
            break;
        case ercNotInit:
            message = "ercNotInit";
            break;
        case ercCantConnect:
            message = "ercCantConnect";
            break;
        case ercAlreadyConnect:
            message = "ercAlreadyConnect";
            break;
        case ercSendError:
            message = "ercSendError";
            break;
        case ercRcvError:
            message = "ercRcvError";
            break;
        case ercAbort:
            message = "ercAbort";
            break;
        case ercTimeOut:
            message = "ercTimeOut";
            break;
        case ercOutOfOrder:
            message = "ercOutOfOrder";
            break;
        case ercExtraData:
            message = "ercExtraData";
            break;
        case ercMissingData:
            message = "ercMissingData";
            break;
        case ercTridNotFound:
            message = "ercTridNotFound";
            break;
        case ercNotComplete:
            message = "ercNotComplete";
            break;
        case ercNotConnected:
            message = "ercNotConnected";
            break;
        case ercWrongMode:
            message = "ercWrongMode";
            break;
        case ercWrongVersion:
            message = "ercWrongVersion";
            break;
        case ercDvctableDne:
            message = "ercDvctableDne";
            break;
        case ercDvctableCorrupt:
            message = "ercDvctableCorrupt";
            break;
        case ercDvcDne:
            message = "ercDvcDne";
            break;
        case ercDpcutilInitFail:
            message = "ercDpcutilInitFail";
            break;
        case ercUnknownErr:
            message = "ercUnknownErr";
            break;
        case ercDvcTableOpen:
            message = "ercDvcTableOpen";
            break;
        case ercRegError:
            message = "ercRegError";
            break;
        case ercNotifyRegFull:
            message = "ercNotifyRegFull";
            break;
        case ercNotifyNotFound:
            message = "ercNotifyNotFound";
            break;
        case ercOldDriverNewFw:
            message = "ercOldDriverNewFw";
            break;
        case ercInvHandle:
            message = "ercInvHandle";
            break;
        default:
            message = "Could not identify error code";
            break;

    }
    return message;
}

////////// JAVA-NATIVE INTERFACE FUNCTIONS //////////

/*
 * Class:     chrisblaster_ChrisBlasterManager
 * Method:    initializeDrivers
 * Signature: ()Z
 */
JNIEXPORT jboolean JNICALL Java_chrisblaster_ChrisBlasterManager_initializeDrivers
  (JNIEnv *env, jclass callingclass){
    jboolean success = initDrivers();
    return success;
}

/*
 * Class:     chrisblaster_ChrisBlasterManager
 * Method:    open
 * Signature: (Ljava/lang/String;)Lchrisblaster/ChrisBlasterManager/ChrisBlasterDevice;
 */
JNIEXPORT jboolean JNICALL Java_chrisblaster_ChrisBlasterManager_connect
  (JNIEnv *env, jclass callingclass, jstring jname, jobject device) {
    jboolean boo;
    jboolean success = JNI_TRUE;
    const char* cname = env->GetStringUTFChars(jname, &boo);
    int len = 512;
    char boardname[512];
    strncpy(boardname, cname, len);
    // find lowest NULL handle
    jint devindex = -1;
    for (int i = 0; i < device_limit; i++) {
        if (devindex < 0 && device_table[i] == NULL) {
            devindex = i;
            break;
        }
    }

    if (openChrisBlasterConnection(boardname, &device_table[devindex])) {
        // successful connection
        jclass device_class = env->GetObjectClass(device);
        jfieldID device_table_index_id = env->GetFieldID(device_class, "device_table_index", "I");
        env->SetIntField(device, device_table_index_id, devindex);
        jint clockspeed = getClockFrequency(device_table[devindex]);
        jfieldID clock_id = env->GetFieldID(device_class, "clock_frequency", "I");
        env->SetIntField(device, clock_id, clockspeed);
        BYTE status_byte = 0;
        getBoardStatus(&status_byte, device_table[devindex]);
        jint status = status_byte;
        jfieldID status_id = env->GetFieldID(device_class, "status", "I");
        env->SetIntField(device, status_id, status);
        jfieldID name_id = env->GetFieldID(device_class, "name", "Ljava/lang/String;");
        env->SetObjectField(device, name_id, jname);
    } else {
        // unsuccessful connection (e.g. wrong name)
        success = JNI_FALSE;
    }
    env->ReleaseStringUTFChars(jname, cname);
    return success;
}

/*
 * Class:     chrisblaster_ChrisBlasterManager
 * Method:    close
 * Signature: (Lchrisblaster/ChrisBlasterManager/ChrisBlasterDevice;)Z
 */
JNIEXPORT jboolean JNICALL Java_chrisblaster_ChrisBlasterManager_close
  (JNIEnv *env, jclass callingclass, jobject device){
    jclass device_class = env->GetObjectClass(device);
    jfieldID device_table_index_id = env->GetFieldID(device_class,"device_table_index","I");
    long devindex = env->GetIntField(device,device_table_index_id);
    if(closeChrisBlasterConnection(device_table[devindex])){
        device_table[devindex] = NULL;
        return JNI_TRUE;
    }else{
        return JNI_FALSE;
    }
}

/*
 * Class:     chrisblaster_ChrisBlasterManager
 * Method:    getChrisBlasterStatus
 * Signature: (Lchrisblaster/ChrisBlasterManager/ChrisBlasterDevice;)Z
 */
JNIEXPORT jboolean JNICALL Java_chrisblaster_ChrisBlasterManager_getChrisBlasterStatus
  (JNIEnv *env, jclass callingclass, jobject device){
    jclass device_class = env->GetObjectClass(device);
    jfieldID device_table_index_id = env->GetFieldID(device_class,"device_table_index","I");
    long devindex = env->GetIntField(device,device_table_index_id);
    BYTE status_byte = 0;
    bool success = getBoardStatus(&status_byte, device_table[devindex]);
    jint status = status_byte;
    jfieldID status_id = env->GetFieldID(device_class, "status", "I");
    env->SetIntField(device, status_id, status);
    if (success) {
        return JNI_TRUE;
    } else {
        return JNI_FALSE;
    }
}

/*
 * Class:     chrisblaster_ChrisBlasterManager
 * Method:    getLastErrorMessage
 * Signature: (Lchrisblaster/ChrisBlasterManager/ChrisBlasterDevice;)Z
 */
JNIEXPORT jstring JNICALL Java_chrisblaster_ChrisBlasterManager_getLastErrorMessage
  (JNIEnv *env, jclass callingclass){
    return env->NewStringUTF(error_message.c_str());

}

/*
 * Class:     chrisblaster_ChrisBlasterManager
 * Method:    allocateProtocol
 * Signature: (Lchrisblaster/ChrisBlasterManager/ChrisBlasterDevice;III)Z
 */
JNIEXPORT jboolean JNICALL Java_chrisblaster_ChrisBlasterManager_allocateProtocol
  (JNIEnv *env, jclass callingclass, jobject device, jint index, jint nloops, jint nmasks){
    jclass device_class = env->GetObjectClass(device);
    jfieldID device_table_index_id = env->GetFieldID(device_class,"device_table_index","I");
    long devindex = env->GetIntField(device,device_table_index_id);
    if(allocateNewProtocol(index,nloops,nmasks,device_table[devindex])){
        return JNI_TRUE;
    }else{
        return JNI_FALSE;
    }

}

/*
 * Class:     chrisblaster_ChrisBlasterManager
 * Method:    setLoopRepeat
 * Signature: (Lchrisblaster/ChrisBlasterManager/ChrisBlasterDevice;III)Z
 */
JNIEXPORT jboolean JNICALL Java_chrisblaster_ChrisBlasterManager_setLoopRepeat
  (JNIEnv *env, jclass callingclass, jobject device, jint pindex, jint lindex, jint reps){
jclass device_class = env->GetObjectClass(device);
    jfieldID device_table_index_id = env->GetFieldID(device_class,"device_table_index","I");
    long devindex = env->GetIntField(device,device_table_index_id);
    if(repeatLoop(pindex,lindex,reps,device_table[devindex])){
        return JNI_TRUE;
    }else{
        return JNI_FALSE;
    }
}

/*
 * Class:     chrisblaster_ChrisBlasterManager
 * Method:    addBitmask
 * Signature: (Lchrisblaster/ChrisBlasterManager/ChrisBlasterDevice;IIII)Z
 */
JNIEXPORT jboolean JNICALL Java_chrisblaster_ChrisBlasterManager_addBitmask
  (JNIEnv *env, jclass callingclass, jobject device, jint pindex, jint lindex, jint mask, jint cycles){
jclass device_class = env->GetObjectClass(device);
    jfieldID device_table_index_id = env->GetFieldID(device_class,"device_table_index","I");
    long devindex = env->GetIntField(device,device_table_index_id);
    if(addBitmask(pindex,lindex,mask,cycles,device_table[devindex])){
        return JNI_TRUE;
    }else{
        return JNI_FALSE;
    }

}

/*
 * Class:     chrisblaster_ChrisBlasterManager
 * Method:    setTerminalBitmask
 * Signature: (Lchrisblaster/ChrisBlasterManager/ChrisBlasterDevice;II)Z
 */
JNIEXPORT jboolean JNICALL Java_chrisblaster_ChrisBlasterManager_setTerminalBitmask
  (JNIEnv *env, jclass callingclass, jobject device, jint pindex, jint mask){
jclass device_class = env->GetObjectClass(device);
    jfieldID device_table_index_id = env->GetFieldID(device_class,"device_table_index","I");
    long devindex = env->GetIntField(device,device_table_index_id);
    if(setTerminalBitmask(pindex,mask,device_table[devindex])){
        return JNI_TRUE;
    }else{
        return JNI_FALSE;
    }

}

/*
 * Class:     chrisblaster_ChrisBlasterManager
 * Method:    deallocateProtocol
 * Signature: (Lchrisblaster/ChrisBlasterManager/ChrisBlasterDevice;I)Z
 */
JNIEXPORT jboolean JNICALL Java_chrisblaster_ChrisBlasterManager_deallocateProtocol
  (JNIEnv *env, jclass callingclass, jobject device, jint pindex){
jclass device_class = env->GetObjectClass(device);
    jfieldID device_table_index_id = env->GetFieldID(device_class,"device_table_index","I");
    long devindex = env->GetIntField(device,device_table_index_id);
    if(freeMemory(pindex,device_table[devindex])){
        return JNI_TRUE;
    }else{
        return JNI_FALSE;
    }

}

/*
 * Class:     chrisblaster_ChrisBlasterManager
 * Method:    startProtocol
 * Signature: (Lchrisblaster/ChrisBlasterManager/ChrisBlasterDevice;I)Z
 */
JNIEXPORT jboolean JNICALL Java_chrisblaster_ChrisBlasterManager_startProtocol
  (JNIEnv *env, jclass callingclass, jobject device, jint pindex){
jclass device_class = env->GetObjectClass(device);
    jfieldID device_table_index_id = env->GetFieldID(device_class,"device_table_index","I");
    long devindex = env->GetIntField(device,device_table_index_id);
    if(startProtocol(pindex,device_table[devindex])){
        return JNI_TRUE;
    }else{
        return JNI_FALSE;
    }

}

/*
 * Class:     chrisblaster_ChrisBlasterManager
 * Method:    isRunningProtocol
 * Signature: (Lchrisblaster/ChrisBlasterManager/ChrisBlasterDevice;)Z
 */
JNIEXPORT jboolean JNICALL Java_chrisblaster_ChrisBlasterManager_isRunningProtocol
  (JNIEnv *env, jclass callingclass, jobject device){
jclass device_class = env->GetObjectClass(device);
    jfieldID device_table_index_id = env->GetFieldID(device_class,"device_table_index","I");
    long devindex = env->GetIntField(device,device_table_index_id);
    if(checkIfDone(device_table[devindex])){
        // done, return false
        return JNI_FALSE;
    }else{
        // not done yet, return true
        return JNI_TRUE;
    }
// sorry the above code is confusing.
}

/*
 * Class:     chrisblaster_ChrisBlasterManager
 * Method:    abortProtocol
 * Signature: (Lchrisblaster/ChrisBlasterManager/ChrisBlasterDevice;)Z
 */
JNIEXPORT jboolean JNICALL Java_chrisblaster_ChrisBlasterManager_abortProtocol
  (JNIEnv *env, jclass callingclass, jobject device){
jclass device_class = env->GetObjectClass(device);
    jfieldID device_table_index_id = env->GetFieldID(device_class,"device_table_index","I");
    long devindex = env->GetIntField(device,device_table_index_id);
    if(killProtocol(device_table[devindex])){
        return JNI_TRUE;
    }else{
        return JNI_FALSE;
    }

}

/*
 * Class:     chrisblaster_ChrisBlasterManager
 * Method:    setCurrentBitmask
 * Signature: (Lchrisblaster/ChrisBlasterManager/ChrisBlasterDevice;I)Z
 */
JNIEXPORT jboolean JNICALL Java_chrisblaster_ChrisBlasterManager_setCurrentBitmask
  (JNIEnv *env, jclass callingclass, jobject device, jint mask){
jclass device_class = env->GetObjectClass(device);
    jfieldID device_table_index_id = env->GetFieldID(device_class,"device_table_index","I");
    long devindex = env->GetIntField(device,device_table_index_id);
    if(changeOutputBitmask(mask,device_table[devindex])){
        return JNI_TRUE;
    }else{
        return JNI_FALSE;
    }

}

/*
 * Class:     chrisblaster_ChrisBlasterManager
 * Method:    refresh
 * Signature: (Lchrisblaster/ChrisBlasterManager/ChrisBlasterDevice;)Z
 */
JNIEXPORT jboolean JNICALL Java_chrisblaster_ChrisBlasterManager_refresh
  (JNIEnv *env, jclass callingclass, jobject device){
jclass device_class = env->GetObjectClass(device);
    jfieldID device_table_index_id = env->GetFieldID(device_class,"device_table_index","I");
    long devindex = env->GetIntField(device,device_table_index_id);
    if(refreshBoard(device_table[devindex])){
        return JNI_TRUE;
    }else{
        return JNI_FALSE;
    }

}

////////// END OF JNI FUNCTIONS //////////
