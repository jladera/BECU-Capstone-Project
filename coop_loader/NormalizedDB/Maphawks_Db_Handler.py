import pypyodbc as pyodbc # python SQL Server database integration library

'''
Below are Python Class imports. Each class corresponds to a table in the normalized DB schema.
'''
from NormalizedDB.Locations_Table import Locations_Table
from NormalizedDB.Contacts_Table import Contacts_Table
from NormalizedDB.SpecialQualities_Table import SpecialQualities_Table
from NormalizedDB.DailyHours_Table import DailyHours_Table
import threading


'''
Enums for SQL Commands (CREATE and INSERT) and SQL Tables (same tables as above) 
'''
from Sql_Enums import Sql_Commands, Sql_Tables


import time # used in insert_into_db() so we have some time to see printed message on console

class Maphawks_Db_Handler(threading.Thread):

    '''
    Constructor
    SUMMARY:
    Reads db_connection_str.txt (txt file with one line: the db connection string). Creates instances
    of all SQL Table Classes.
    '''
    def __init__(self):
        with open('./NormalizedDb/db_connection_str.txt') as file:
            self.connection_str = file.read().strip()
        self.locations_table = Locations_Table()
        self.contacts_table = Contacts_Table()
        self.special_qualities_table = SpecialQualities_Table()
        self.daily_hours = DailyHours_Table()
        self.dbWriteFileLock = threading.Lock()
    
    '''
    SUMMARY:
    Calls execute_sql_command() helper method. Drives the creation of tables in SQL DB.
    '''
    def create_tables(self):
        # Create tables in DB
        self.execute_sql_command(Sql_Commands.CREATE, Sql_Tables.Locations)
        self.execute_sql_command(Sql_Commands.CREATE, Sql_Tables.Contacts)
        self.execute_sql_command(Sql_Commands.CREATE, Sql_Tables.SpecialQualities)
        self.execute_sql_command(Sql_Commands.CREATE, Sql_Tables.DailyHours)
    

    '''
    SUMMARY:
    Calls execute_sql_command() helper method. Drives the insertion of location records into the DB.
    '''
    def insert_into_db(self, flat_db_locations,locationsNoUNoD, locationsNoUD, locationsUD, locationsUNoD):
        flatArray=flat_db_locations.split(";")

        for flocation in flatArray:
            locationExists=self.get_ReferenceID(flocation,locationsNoUNoD)
            if locationExists==False:
                locationExists=self.get_ReferenceID(flocation,locationsNoUD)
                if locationExists==False:
                    locationExists=self.get_ReferenceID(flocation,locationsUD)
                    if locationExists != False and locationExists != "noUpdate":                        
                        #print("Update is required", locationExists)
                        self.execute_sql_command(Sql_Commands.UPDATE, None, flocation,locationExists)
                    elif locationExists == False:
                        locationExists=self.get_ReferenceID(flocation,locationsUNoD)
                        if locationExists != False and locationExists != "noUpdate":                           
                            #print("Update is required", locationExists)
                            self.execute_sql_command(Sql_Commands.UPDATE, None, flocation,locationExists)
                        elif locationExists == False:
                            if flocation != "":
                                self.execute_sql_command(Sql_Commands.INSERT, None, flocation)


    def get_ReferenceID(self,flatLocation,normalizedLocation):
        flatArray=flatLocation.split(",")
        rID=""
        normLocationEntry=""
        updateNeeded=""
        for values in flatArray:
            value=values.split(":")
            if value[0] == "referenceID":
                rID=value[1]
                if normalizedLocation.find(rID) != -1:
                    updateNeeded="False"
                    normValues=normalizedLocation.split(";")
                    for eachRecord in normValues:
                        if eachRecord.find(rID) != -1:
                            normLocationEntry = eachRecord

        for values in flatArray:
            value=values.split(":")
            try:
                if updateNeeded == "False":
                    normArray=normLocationEntry.split(",")
                    for normValue in normArray:
                        splitNormValue=normValue.split(":")
                        if value[0]==splitNormValue[0]:
                            # To troubleshoot update value scenario unhash the below two lines
                            #if rID=="1555551287E001180":
                                #print (value[0],"=",splitNormValue[0])
                            if value[1]!=splitNormValue[1]:
                                updateNeeded="True"
            except (SyntaxError, IndexError) as e:
                pass

        if updateNeeded=="True":
            return rID 
        elif updateNeeded == "False": 
            return "noUpdate"
        else:  
            return False  


    '''
    SUMMARY:
    Determines type of sql command and calls corresponding method.
    In case of SELECT command, table is an array, but in all other cases table parameter can be assumed to 
    refer to only 1 table.

    **SELECT functionality has yet to be implemented**
    '''
    def execute_sql_command(self, cmd_type, table=None, location=None, columns=None):
        if cmd_type is Sql_Commands.CREATE: self.execute_create(table) # Create a new table
        elif cmd_type is Sql_Commands.INSERT: self.execute_insert(location) # Insert new row into table
        elif cmd_type is Sql_Commands.SELECT: self.execute_sql_command(table, columns)
        elif cmd_type is Sql_Commands.UPDATE: self.execute_update(location,columns)
        else: print("SQL Command: {} is not supported".format(str(cmd_type)))

    '''
    SUMMARY:
    CREATES tables in DB.
        Step 1: Call table class instance to return table creation SQL statement
        Step 2: Call commit_command() helper method. commit_command() parameter 'cmd' must be passed as a
                LIST. This ensures method will work for both single statements as well as with
                multiple statements.
        Step 3: Retry table creation if success is not initially achieved.
    '''
    def execute_create(self, table):
        commit_status = None
        while commit_status is not True:
            if table is Sql_Tables.Locations: cmd = self.locations_table.get_create_table()
            elif table is Sql_Tables.Contacts: cmd = self.contacts_table.get_create_table()
            elif table is Sql_Tables.SpecialQualities: cmd = self.special_qualities_table.get_create_table()
            elif table is Sql_Tables.DailyHours: cmd = self.daily_hours.get_create_table()
            else: 
                print("TABLE {} DOES NOT EXIST".format(str(table)))
                return
            commit_status = self.commit_command([cmd])    

    '''
    SUMMARY:
    Inserts a row for location (parameter) into all Sql_Tables. 
        Step 1: Gets SQL INSERT statement from Locations Table Class. Returns the SQL statement and the
                BECU LocationId (GUID)
        Step 2: Gets SQL INSERT statement from other Table Classes.
        Step 3: Calls commit_command() to commit commands to DB
        Step 4: Retry until success.
    '''
    def execute_insert(self, location):
        commit_status = None
        while commit_status is not True:
            cmd_locations, locationId = self.locations_table.get_insert_row(location)
            cmd_contacts = self.contacts_table.get_insert_row(location, locationId)
            cmd_special_qualities = self.special_qualities_table.get_insert_row(location, locationId)
            cmd_hours = self.daily_hours.get_insert_row(location, locationId)
            self.dbWriteFileLock.acquire()
            commit_status = self.commit_command_insert_([cmd_locations, cmd_contacts, cmd_special_qualities, cmd_hours])
            self.dbWriteFileLock.release()

    def execute_update(self, location, refID):
        commit_status = None
        while commit_status is not True:
            cmd_locations = self.locations_table.get_update_row(location, refID)
            cmd_contacts = self.contacts_table.get_update_row(location,refID)
            cmd_special_qualities = self.special_qualities_table.get_update_row(location,refID)
            cmd_hours = self.daily_hours.get_update_row(location,refID)
            commit_status = self.commit_command_insert_([cmd_locations, cmd_contacts, cmd_special_qualities, cmd_hours])

    '''
    SUMMARY:
    Commits SQL statements to DB
    '''
    def commit_command(self, cmds):
        with open(r'sql_cmds.txt', 'a') as myfile:
            for cmd in cmds:
                myfile.write(cmd)
        myfile.close()
        return True

    def commit_command_insert_(self, cmds):
        with open(r'sql_cmds_locaions.txt', 'a') as myfile1:
            myfile1.write(cmds[0])
        myfile1.close()
        test=cmds[1]
        with open(r'sql_cmds_contacts.txt', 'a') as myfile2:
            myfile2.write(cmds[1])
        myfile2.close()
        with open(r'sql_cmds_special_qualitites.txt', 'a') as myfile3:
            myfile3.write(cmds[2])
        myfile3.close()
        with open(r'sql_cmds_hours.txt', 'a') as myfile4:
            myfile4.write(cmds[3])
        myfile4.close()
        return True

    def commit_command_update_(self, cmds):
        with open('sql_cmds_locaions.txt', 'a') as myfile:
            myfile.write(cmds[0])
        with open('sql_cmds_contacts.txt', 'a') as myfile:
            myfile.write(cmds[1])
        with open('sql_cmds_special_qualitites.txt', 'a') as myfile:
            myfile.write(cmds[2])
        with open('sql_cmds_hours.txt', 'a') as myfile:
            myfile.write(cmds[3])
        return True

        '''
        with pyodbc.connect(self.connection_str) as conn:
            cursor = conn.cursor()
            try:
                for cmd in cmds:
                    if cmd == '': continue #if no data to insert into db, skip to next loop iteration
                    cursor.execute(cmd)
                    cursor.commit()
                return True
            # NB : you won't get an IntegrityError when reading
            except pyodbc.Error as e:
                error_state = e.args[0]
                if error_state == '42S01':
                    print("Object has already been created")
                    return True
                elif error_state == '0A000':
                    print('Database does not support this command at this time:\n{cmd}')
                elif error_state == '40002' or error_state[:2] == '23':
                    print('There was an integrity error on {cmd}')
                elif error_state[:2] == '22':
                    print('There was a data error (division by 0, etc.) in command:\n{cmd}')
                else: 
                    print("error: {}".format(e))
                    print("command: {}".format(cmd))
                    exit(1)
        '''