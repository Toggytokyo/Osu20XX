
import os
import re
from zipfile import ZipFile

#Opening directory
dir_name = "C:/Users/Anthony DiGiovanna/Documents/Other Projects/Osu_Data/Raw_Maps"
dir_content = os.listdir(dir_name)

#Looking for beatmap packs
osz_count = 0
print("Extracting packs...")
for pack in dir_content:
    if pack.endswith(".zip"):
        with ZipFile(os.path.join(dir_name, pack), 'r') as zip:
            # extracting the files
            print('Extracting ', pack)
            zip.extractall("C:/Users/Anthony DiGiovanna/Documents/Other Projects/Osu_Data/Raw_Maps")
        #Refresh directory
        dir_content = os.listdir(dir_name)
        for osz in dir_content:
            if osz.endswith(".osz") and re.match("\d\d\d\d-\d*", osz) == None:
                new_name = pack[0:4] + "-" + str(osz_count) +  ".osz"
                os.rename(os.path.join(dir_name, osz), os.path.join(dir_name,new_name))
                osz_count+=1
print('Done!')

dir_content = os.listdir(dir_name)
#extracting any .osz files
print("Extracting .osz files...")
osu_count = 0
for osz in dir_content:
    if osz.endswith(".osz"):
        with ZipFile(os.path.join(dir_name, osz), 'r') as zip:
            # extracting the files
            print('Extracting .osu(s) from ', osz)
            osz_content = zip.namelist()
            for osu in osz_content:
                if osu.endswith(".osu"):
                    zip.extract(osu, dir_name)
        #Refresh directory
        dir_content = os.listdir(dir_name)
        for osu in dir_content:
            if osu.endswith(".osu") and re.match("\d\d\d\d-\d*", osu) == None:
                new_name = osz[0:4] + "-" + str(osu_count) + ".osu"
                os.rename(os.path.join(dir_name, osu), os.path.join(dir_name, new_name))
                osu_count+=1
print('Done!')

#Refreshing directory and deleting excess files
dir_content = os.listdir(dir_name)
print("Deleting excess files...")
del_count = 0
err_count = 0

for file in dir_content:
    if not file.endswith(".osu"):
        try:
            os.remove(os.path.join(dir_name, file))
            del_count+=1
        except PermissionError:
            err_count+=1
            print("Could not delete \'" + file + "\'! - You may need to manually delete this file.")
        
#Closing data
print(err_count, "files could not be deleted.")
print("Deleted", del_count, "files.")

input("Press Enter to continue...")