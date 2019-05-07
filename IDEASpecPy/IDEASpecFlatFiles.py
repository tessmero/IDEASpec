from scipy.signal import savgol_filter
import matplotlib.pyplot as plt
import numpy as np
import pandas as pd
from scipy.signal import savgol_filter
from scipy.optimize import curve_fit #see the following link for info on curve_fit
import matplotlib.cm as cm


wavelengths = [475,488,505,520,535,545]
ecs=np.array([-0.86,-0.62,0.23,1.,0.51, 0.2])
qE=np.array([-.01,-.5,-.2,2.9,5.2,3.2])
qE=qE/np.max(qE)
Zx=np.array([-0.45,-0.24,1.3,0.45,0.31,0.16])
#Zx=Zx-.3
scatter=np.array([1.1,.6,.25,.1,.07,.05])
scatter = scatter + .3
drift=np.array([.2,.4,.6,.75,.65,.55])

#recalculate the Abs data using a particular set of I0 points
# input the dataframe, and optionally a list of measuring_light_names and the suffix to use, 
# and a list of points to average for the I0 value to use in the calculation.

# for example, 

# recalcAbsUsingI0 (sample_df, ['475', '488', '505', '520', '535', '545'], I_suffix='_I', [0,1]):
# the '_I' will be attached to the end of the wavelength when searching the dataframe
#in this 


def recalcAbsUsingI0 (df, list_of_measuring_lights=df['measuring_light_names'][0], I_suffix='_I', I0_points=[0,1], trim_points=[0,-1], newSufficx='_rDA'):
    if (I0_points[0] == I0_points[1]):
        I0_points[1]=I0_points[0]+1
    for wl in df['measuring_light_names'][0]:
        df[wl + newSufficx] = 0
        df[wl + newSufficx] = df[wl + newSufficx].astype('object')
    
    for t in range(len(df)): #['475']:
        for wl in df['measuring_light_names'][t]:
            
            I0=np.mean(np.array(df[wl+ '_I'][t][I0_points[0]:I0_points[1]]))
            daTrace = -1*np.log10(np.array(df[wl+ '_I'][t])/I0) #np.array(df[wl+ '_I'][t][0]))
            df[wl + newSufficx][t] = daTrace #[trim_points[0]:trim_points[1]]
            


# The subtraceBaseline funciton adds a set of traces where the baseline is subtracted. The baseline MUST BE
# the same trace EXCEPT that no catinic was changed AND it is saved with a specific label, e.g. "baseline". 
# The wavelengths paramter is a list of the wavelengths upon which traces to subtract.
# The suffix paramter is the suffix for the traces of interest (e.g. '_calc') 

def subtractBaseline(sample_df, wavelengths, suffix, newSuffix, baselineLabel = 'baseline'):
    baseline=sample_df[sample_df['trace_label']==baselineLabel]
    traces=sample_df[sample_df['trace_label']=='fluct']
    
    for  wli in range(len(wavelengths)):
        
        wl=str(wavelengths[wli])
        sample_df[str(wl) + newSuffix] = 0
        sample_df[str(wl) + newSuffix] = sample_df[str(wl) + newSuffix].astype('object')
        for i in range(0, len(sample_df[wl+suffix])): # cyclie through the traces for each wl
            print(i)
            sample_df[str(wl) + newSuffix][i] = np.array(sample_df[wl + suffix][i]) - np.array(baseline[wl + suffix][0])
        
#len(sample_df['475_time'][0]), len(sample_df['475_I'][0])

from scipy.optimize import curve_fit #see the following link for info on curve_fit


def fit_burn(x,a,b,c):
    
    # Expecting absorbance data over time for the baseline, during which there will be a
    #burn in artifact from changes in LED intensity and color. 
    #I will fit the baseline with a hyperbolic or exponential
    # a is the amplitude of the burn-in
    # b is the time constant
    # c is the offset
    fit_burn = c + a/(1+x/b)
    return (fit_burn)
def burnCorrection(sample_df,wavelengths,suffix,baselineArray,newSuffix = '_bcor'):
    fit_b=baselineArray[0]
    fit_e=baselineArray[1]
    
    for i in range(len(wavelengths)):
        wl = str(wavelengths[i])
        sample_df[str(wl) + newSuffix] = 0
        sample_df[str(wl) + newSuffix] = sample_df[str(wl) + newSuffix].astype('object')
        tName = wl + suffix
        xName = wl + '_time'
        #corTraces=[]
        #print(len(sample_df[tName]))
        for traceNum in range (0, len(sample_df[tName])):
            #print(tName)
            y_data=sample_df[tName][traceNum]
            x_data=np.array(sample_df[xName][traceNum])
            x_data=x_data-x_data[0]
            popt, pcov = curve_fit(fit_burn, x_data[fit_b:fit_e], y_data[fit_b:fit_e], p0=[.001,1,.001], bounds=([-np.inf, 0, -np.inf], np.inf))
            corTrace=[]
            #print(len(y_data))
            for i, x in enumerate(x_data):
                vc = y_data[i] - fit_burn(x, popt[0],popt[1],popt[2])
                corTrace.append(vc)
            #print(str(wl) + newSuffix)
            sample_df[str(wl) + newSuffix][traceNum]=corTrace
                            
from scipy.signal import savgol_filter

def smoothTraces(sample_df,wavelengths,suffix,smooth_window=50,newSuffix = '_smooth'):

    for i in range(len(wavelengths)):
        
        wl = wavelengths[i]
        tName = str(wl) + suffix
        newColName= tName + newSuffix
        sample_df[newColName] = 0
        sample_df[newColName] = sample_df[newColName].astype('object')
        for traceNum in range (0, len(sample_df[tName])):  # cycle through each trace in experiment 
            subtrace=sample_df[tName][traceNum]
            smoothedTrace=savgol_filter(subtrace , smooth_window, 3)
            sample_df[newColName][traceNum]=smoothedTrace


def generateDAS(sample_df, wavelengths=df['measuring_light_names'][0], suffix='_rDA', newColName='das'):

    #sample_df[colName]= pd.Series([0]*len(sample_df))
    #sample_df[colName] = [[0]*len(sample_df)]
    sample_df[newColName] = 0
    sample_df[newColName] = sample_df[newColName].astype('object')

    #wavelengths=['475', '488', '505', '520', '535', '545']
    for t in range(0,len(sample_df)):
        dass=[]
        wl = str(wavelengths[0]) #start with the first wl
        for pt in range(len(sample_df[wl + suffix][t])): #cycle through all points in the trace
            das=[]
            for wl in range(len(wavelengths)):
                das.append(sample_df[str(wavelengths[wl]) + suffix][t][pt])
            dass.append(das)
        sample_df[newColName][t] = dass

        from scipy.signal import savgol_filter

def fit_spec_5(x,a,b,c,d,e): #function to fit the spectroscopic data
    global ecs
    global qE
    global Zx
    global scatter
    global drift
    
    # Expecting absorbance data from 5 wavelengths per time point
    # The component spectra also have 7 wavelength
    #print(len(components[0]),len(components[1]),len(components[2]),len(components[3]))
    fit_spectrum = a*ecs + b*qE + c*Zx + d*scatter + e*drift
    return (fit_spectrum)

def fitDAS_5(sample_df, wavelengths, dasColName, newSuffix=''):
    components = ['ecs', 'qE', 'Zx', 'scatter', 'drift']
    for c in components:
        newColName= c + newSuffix
        sample_df[newColName] = 0
        sample_df[newColName] = sample_df[newColName].astype('object')

    for traceNum in range (0, len(sample_df[dasColName])):  # cycle through each trace in experiment 
        comp={}
        for c in components:
            comp[c]=[]
       
        for time_index in range(len(sample_df[dasColName][0])):
            y_data=sample_df[dasColName][traceNum][time_index]
            popt, pcov = curve_fit(fit_spec, wavelengths, y_data, p0=[0,0,0,0,0])
            for i, c in enumerate(components):
                comp[c].append(popt[i])
            
        for c in components:
            newColName= c + newSuffix
            sample_df[newColName][traceNum] = comp[c]


