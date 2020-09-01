import sys
import os
sys.path.append("Lib\site-packages")
sys.path.append(os.getcwd())

import threading
import time

from NormalizedDB import *
from Flat_DB_Get import Flat_DB_Get
from NormalizedDB.Maphawks_Db_Handler import Maphawks_Db_Handler
from MapHawks_DB_Get import MapHawks_DB_Get


'''
Checks if field is empty
param: field (string)
return: True or False
'''
def _is_empty(field):
    if len(field) == 0: return True
    return False

class update_Tables_thread(threading.Thread):

    def __init__(self, zipcode):
        threading.Thread.__init__(self) # must be called in class init
        self.zipcode = zipcode

    def run(self):
        temp=self.zipcode.strip()
        flat_DB = Flat_DB_Get(temp)
        flatDB_locations = flat_DB.run(temp)
        normDB_locations = MapHawks_DB_Get()
        locationsNoUNoD, locationsNoUD, locationsUD, locationsUNoD = normDB_locations.run(temp)

        # Insert records
        maphawks_db.insert_into_db(flatDB_locations,locationsNoUNoD, locationsNoUD, locationsUD, locationsUNoD)

        # Set delete parameter for deletions
        normalizedDB_locations.set_delete_(temp)

def time_convert(sec):
    mins = sec // 60
    sec = sec % 60
    hours = mins // 60
    mins = mins % 60
    print("Time Lapsed for Normalized DB = {0}:{1}:{2}".format(int(hours),int(mins),sec))

## Execute insert/update contacts table
def execute_DB_commands_(fileName):
    executeCommands = MapHawks_DB_Get()
    try:
        File = open(str(fileName), 'r')
        sqlCommands=""
        for line in File:
            sqlCommands= sqlCommands + line.strip() +"\n" 
            if len(sqlCommands.split(';')) == 100:
                executeCommands.execute_command_(sqlCommands)
                sqlCommands = ""

        if len(sqlCommands) > 0:
            executeCommands.execute_command_(sqlCommands)
        File.close()
        os.remove(str(fileName))
    except (FileNotFoundError) as e:
        print ("no new records found to insert or update in table:", str(fileName))
        pass

if __name__ == "__main__":
    # Create Tables
    maphawks_db = Maphawks_Db_Handler()
    maphawks_db.create_tables()
    normalizedDB_locations = MapHawks_DB_Get()
    normalizedDB_locations.get_all_zipCodes()
    
    with open("ZipCode_List_FlatDB.txt",'r') as myfile:
        zipcodeList = myfile.readlines()

    start = time.time()
    threads = [update_Tables_thread(zipcode) for zipcode in zipcodeList]
    [thread.start() for thread in threads] # begins running the thread. this calls api_call_thread.run()
    [thread.join() for thread in threads] # joins threads for an accurate measure of concurrency
    # normDB_locations = MapHawks_DB_Get()
    # for zipcode in zipcodeList:
    #     temp=zipcode.strip()
    #     flat_DB = Flat_DB_Get(temp)
    #     flatDB_locations = flat_DB.run(temp)

    #     locationsNoUNoD, locationsNoUD, locationsUD, locationsUNoD = normDB_locations.run(temp)

    #     # Insert records
    #     maphawks_db.insert_into_db(flatDB_locations,locationsNoUNoD, locationsNoUD, locationsUD, locationsUNoD)

    #     # Set delete parameter for deletions
    #     normalizedDB_locations.set_delete_(temp)

    execute_DB_commands_('sql_cmds.txt')


    execute_DB_commands_('sql_cmds_locaions.txt')
    execute_DB_commands_('sql_cmds_contacts.txt')
    execute_DB_commands_('sql_cmds_special_qualitites.txt')
    execute_DB_commands_('sql_cmds_hours.txt')

    os.remove('ZipCode_List_FlatDB.txt')
    end = time.time()
    time_convert(end-start)