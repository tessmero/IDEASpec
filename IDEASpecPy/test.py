# Test the functions in IDEASpecPy
# These tests are designed to run as a part of the continuous integration pipeline
# These tests are designed to emulate the way the module is used in the example notebook
# These tests (and IDEASpecPy) do NOT follow the standard python package structure

import sys
import os

# "install" the module and import items
folder_that_contains_code="./IDEASpecPy/"
sys.path.append(os.path.abspath(folder_that_contains_code))
import IDEASpecPy as Ipy


# test the function generateCombinedJSON
# assert that outputs are compatible with pd.read_json
baseFileName = './example_data/col-0_3g-simple_1'
Ipy.generateCombinedJSON(baseFileName)
sample_df=pd.read_json(baseFileName + '_combined.json') 

burnInFileName = './example_data/card3_3g-simple_1'
Ipy.generateCombinedJSON(burnInFileName)
burn_in_profile=pd.read_json(burnInFileName + '_combined.json') 


# test the function smoothTraces
Ipy.smoothTraces(sample_df, wavelengths, '_I', 31, '_smooth')
Ipy.smoothTraces(burn_in_profile, wavelengths, '_I', 31, '_smooth')


# test the function 
label_for_new_averaged_row = "avg"
rows_to_average = [0,1,2,3]
columns_to_average = ["475_I_smooth","488_I_smooth", "505_I_smooth", "520_I_smooth", "535_I_smooth", "545_I_smooth"]
Ipy.compute_average_row( burn_in_profile, rows_to_average, columns_to_average, label_for_new_averaged_row)