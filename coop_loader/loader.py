# -*- coding: utf-8 -*-
import sys
import os
sys.path.append("Lib\site-packages")
sys.path.append(os.getcwd())

import requests
import urllib3 
import lxml
import pypyodbc
import threading
import time
import json # for processing api response
import string

'''
Define the global variables
'''
KEY = "" #this is a private a key used to get the location information from CO-OP 
checkTable="default" #this value is used as default to check if the table is already created. If not, sql command will create it.
normalized_json_thread_semaphore = threading.Semaphore(25)

'''
Gets Co-Op API key from local txt file. This file contains one line, which is the API key.
'''
def get_key():
    global KEY
    with open('key.txt', 'r') as file:
        KEY = file.read()

'''
Calls Co-Op API.
param: offset; offset as viewed by Co-Op. If offset = 0, result = [1,100]. If offset = 100, result = [101, 200]
return: BeautifulSoup list of locations, number of records if offset = 0, else just locations list.
'''
def _call_api(offset,theZipCode):
    r = requests.get('https://api.co-opfs.org/locator/proximitysearch', params={'zip':str(theZipCode), 'offset': str(offset)}, headers={'Accept':'application/json', 'Version':'1', 'Authorization': KEY})       
    response_as_json = json.loads(r.text)
    #REMOVE THIS Print BELOW
    #print("Records available in {}: {}".format(str(theZipCode), response_as_json['response']['resultInfo']['recordsAvailable'])+'\n')
    locations = response_as_json['response']['locations'] 
    records_avail=int(response_as_json['response']['resultInfo']['recordsAvailable'])
    return locations, records_avail
    

'''
Calls Co-Op API. For now only looks at all locations in one zipcode.
Calls insert_into_locations, insert_into_contact, insert_into_specialqualities.
'''
def _insert_driver(zipcodeNumber):
    
    offset = 0
    records_avail = -1
    sql_statements = []
    refIDs=""
    totalLocations=""

    # Call API
    locations, records_avail = _call_api(offset,zipcodeNumber)

    numberOfLoops=int(records_avail/100)

    #get all the locations per zip
    offset2=0
    while numberOfLoops>0:
        offset2=offset2+100
        tempLocations,tempNumOfRecs=_call_api(offset2,zipcodeNumber)
        locations += tempLocations
        numberOfLoops -= 1

    # Get all the ATM locations in the area "zipcode"
    conn2 = pypyodbc.connect('Driver={ODBC Driver 17 for SQL Server};Server=maphawks.database.windows.net;Database=maphawks_flat;uid=jLadera;pwd=6VxbvqwMazBseP')
    cursor2 = conn2.cursor()
    sqlCommand="SELECT distinct(ReferenceID) FROM [maphawks_flat].[dbo].[Locations] where PostalCode='"+zipcodeNumber+"';"

    try:
        cursor2.execute(sqlCommand)
        refIDs=cursor2.fetchall()
        # NB : If error is returned when reading from db, it will be logged
    except (pypyodbc.ProgrammingError, pypyodbc.Error)  as e:
        print("Found an error when trying to get select Reference IDs from the DB for zipcode:",zipcodeNumber," the error is:",e)
        pass

    # The following loop will check if the record fetched from CO-OP exists in the database:
    # If the record does not exist in the DB, it will be inserted.
    # If the record exists in the database, it will check if it is updated.
    # If it is not updated, then no action is taken on the record.
    # If the record in CO-OP does not match the record in the db, the db record will get updated.
    # A reverse check is also made, if a record exists in the db but does not exist in the list from CO-OP, the record will be deleted.
    while offset+1 < records_avail:
        for location in locations:
            #to be checked when performing a delete record
            totalLocations=str(totalLocations+location['institutionRtn']+location['terminalId']+",")
            
            #_get_ReferenceID is a method that check if the record fetched from CO-OP exists at BECU's Database
            referenceID=0
            offset += 1

            rID=str(location['institutionRtn']+location['terminalId'])

            if (str(checkTable).find(rID) == -1):
                referenceID=rID
            #If the record from CO-OP does not exist in the DB, then the reference ID is returned, otherwise a '0' is returned, indicating that the record exists
            if referenceID!=0: 
                sql_statements.append(_insert_into_locations(location) + '\n')
            else:
                    sqlCommand="SELECT * FROM [maphawks_flat].[dbo].[Locations] where ReferenceID = '"+location['institutionRtn']+location['terminalId']+"';"

                    type_name = location['locatorType']
                    if type_name == 'A':
                        typed_name = "ATM"
                    elif type_name == 'S':
                        typed_name ="Shared Branch"
                    else: 
                        typed_name = ""
                    locationString = ("[('"+location['institutionRtn']+location['terminalId']+"', '"+location['institutionName']+"', '"+location['address']+"', '"+location['city']+"', '"+location['county']+"', '"+location['state']+"', '"+location['zip']+"', '"+location['country']+"', '"+location['phone']+"', '"+location['fax']+"', '"+location['webAddress']+"', Decimal('"+location['latitude']+"'), Decimal('"+location['longitude']+"'), '"+location['hours_open_value']+"', '"+location['retailOutlet']+"', '"+location['restrictedAccess']+"', '"+location['acceptDeposit']+"', '"+location['acceptCash']+"', '"+location['envelopeRequired']+"', '"+location['onMilitaryBase']+"', '"+location['onPremise']+"', '"+location['surcharge']+"', '"+location['access']+"', '"+location['accessNote']+"', '"+location['installationType']+"', '"+location['handicapAccess']+"', '"+typed_name+"', '"+location['mondayOpen']+"', '"+location['mondayClose']+"', '"+location['tuesdayOpen']+"', '"+location['tuesdayClose']+"', '"+location['wednesdayOpen']+"', '"+location['wednesdayClose']+"', '"+location['thursdayOpen']+"', '"+location['thursdayClose']+"', '"+location['fridayOpen']+"', '"+location['fridayClose']+"', '"+location['saturdayOpen']+"', '"+location['saturdayClose']+"', '"+location['sundayOpen']+"', '"+location['sundayClose']+"', '"+location['mondayDriveThruOpen']+"', '"+location['mondayDriveThruClose']+"', '"+location['tuesdayDriveThruOpen']+"', '"+location['tuesdayDriveThruClose']+"', '"+location['wednesdayDriveThruOpen']+"', '"+location['wednesdayDriveThruClose']+"', '"+location['thursdayDriveThruOpen']+"', '"+location['thursdayDriveThruClose']+"', '"+location['fridayDriveThruOpen']+"', '"+location['fridayDriveThruClose']+"', '"+location['saturdayDriveThruOpen']+"', '"+location['saturdayDriveThruClose']+"', '"+location['sundayDriveThruOpen']+"', '"+location['sundayDriveThruClose']+"', '"+location['cashless']+"', '"+location['driveThruOnly']+"', '"+location['limitedTransactions']+"', '"+location['militaryIdRequired']+"', '"+location['selfServiceDevice']+"', '"+location['selfServiceOnly']+"')]")
                    try:
                        cursor2.execute(sqlCommand)
                        atmLocationRec=str(cursor2.fetchall())
                        atmLocationRec = atmLocationRec.replace("None","''")
                        #After confirming the record exists, the following command checks if there were any updates 
                        if locationString not in atmLocationRec: 
                           # If an update is required then the database is updated with the new CO-OP information
                           sql_statements.append(_update_sql_statement(location,"Locations")+ '\n')
                    except (pypyodbc.ProgrammingError, pypyodbc.Error)  as e:
                        print("Found an error when trying to get the record parameters from the DB. Record with issue has Reference ID=",location['institutionRtn']+location['terminalId'],". the error is:",e)
                        pass
        break

    #The following loop checks if there are records in the database that are not there in CO-OP. 
    #If so, the record in the db will be deleted.
    for refID in refIDs:
        refID=str(refID).strip().replace("('","").replace("',)","")
        if totalLocations.find(refID) == -1:
            statement = "delete from Locations where ReferenceID = '"+refID+"';"
            sql_statements.append(statement+ '\n')

    cursor2.close()
    conn2.close()

    return sql_statements
'''
Before performing an insert_statement, check to see if the record already exist
Note that the: "Reference ID" is the primary key and identifies the record
'''
def _get_ReferenceID(location,refIDs):
    rID=str(location['institutionRtn']+location['terminalId'])

    if (str(refIDs).find(rID) != -1):
        return 0
    
    return rID

'''
Parses location object to generate the "sql insert statements" into Locations table
param: location (a single location object)
return: sql insert statement (string)
'''
def _insert_into_locations(location):
    values = []
    values.append(field_formatting_helper(location['institutionRtn']+location['terminalId']))
    values.append(field_formatting_helper(location['institutionName']))
    values.append(field_formatting_helper(location['address']))
    values.append(field_formatting_helper(location['city']))
    values.append(field_formatting_helper(location['county']))
    values.append('WA') # We are only using Washington values, but this should obviously change for production
    values.append(field_formatting_helper(location['zip']))
    values.append(field_formatting_helper(location['country']))
    values.append(field_formatting_helper(location['phone']))
    values.append(field_formatting_helper(location['fax']))
    values.append(field_formatting_helper(location['webAddress']))
    values.append(field_formatting_helper(location['latitude']))
    values.append(field_formatting_helper(location['longitude']))
    values.append(field_formatting_helper(location['hours_open_value']))
    values.append(field_formatting_helper(location['retailOutlet']))
    values.append(field_formatting_helper(location['restrictedAccess']))
    values.append(field_formatting_helper(location['acceptDeposit']))
    values.append(field_formatting_helper(location['acceptCash']))
    values.append(field_formatting_helper(location['envelopeRequired']))
    values.append(field_formatting_helper(location['onMilitaryBase']))
    values.append(field_formatting_helper(location['onPremise']))
    values.append(field_formatting_helper(location['surcharge']))
    values.append(field_formatting_helper(location['access']))
    values.append(field_formatting_helper(location['accessNote']))
    values.append(field_formatting_helper(location['installationType']))
    values.append(field_formatting_helper(location['handicapAccess']))
    type_name = location['locatorType']
    if type_name == 'A': values.append('ATM')
    elif type_name == 'S': values.append('Shared Branch')
    else: values.append('')
    values.append(field_formatting_helper(location['mondayOpen']))
    values.append(field_formatting_helper(location['mondayClose']))
    values.append(field_formatting_helper(location['tuesdayOpen']))
    values.append(field_formatting_helper(location['tuesdayClose']))
    values.append(field_formatting_helper(location['wednesdayOpen']))
    values.append(field_formatting_helper(location['wednesdayClose']))
    values.append(field_formatting_helper(location['thursdayOpen']))
    values.append(field_formatting_helper(location['thursdayClose']))
    values.append(field_formatting_helper(location['fridayOpen']))
    values.append(field_formatting_helper(location['fridayClose']))
    values.append(field_formatting_helper(location['saturdayOpen']))
    values.append(field_formatting_helper(location['saturdayClose']))
    values.append(field_formatting_helper(location['sundayOpen']))
    values.append(field_formatting_helper(location['sundayClose']))
    values.append(field_formatting_helper(location['mondayDriveThruOpen']))
    values.append(field_formatting_helper(location['mondayDriveThruClose']))
    values.append(field_formatting_helper(location['tuesdayDriveThruOpen']))
    values.append(field_formatting_helper(location['tuesdayDriveThruClose']))
    values.append(field_formatting_helper(location['wednesdayDriveThruOpen']))
    values.append(field_formatting_helper(location['wednesdayDriveThruClose']))
    values.append(field_formatting_helper(location['thursdayDriveThruOpen']))
    values.append(field_formatting_helper(location['thursdayDriveThruClose']))
    values.append(field_formatting_helper(location['fridayDriveThruOpen']))
    values.append(field_formatting_helper(location['fridayDriveThruClose']))
    values.append(field_formatting_helper(location['saturdayDriveThruOpen']))
    values.append(field_formatting_helper(location['saturdayDriveThruClose']))
    values.append(field_formatting_helper(location['sundayDriveThruOpen']))
    values.append(field_formatting_helper(location['sundayDriveThruClose']))
    values.append(field_formatting_helper(location['cashless']))
    values.append(field_formatting_helper(location['driveThruOnly']))
    values.append(field_formatting_helper(location['limitedTransactions']))
    values.append(field_formatting_helper(location['militaryIdRequired']))
    values.append(field_formatting_helper(location['selfServiceDevice']))
    values.append(field_formatting_helper(location['selfServiceOnly']))
    return _insert_sql_statement(values, 'Locations')

'''
Generates SQL-insert-statement-friendly string
param: value (a string value corresponding to one field of one location from the Co-Op API response)
return: value (as a string) formatted in such a way that will work with Azure's SQL Server
'''
def field_formatting_helper(value):
    if not value:
        return value
    else:
        return "'" + value + "'"

'''
Generates the insert SQL statements that will be executed in the db 
'''
def _insert_sql_statement(value_list, table):
    statement = 'INSERT INTO {} VALUES ('.format(table)
    count=1
    for value in value_list:
        #If it is the first value in the record, then check if record exists
        if count == 1:
            statement = "if not exists (select ReferenceID from [maphawks_flat].[dbo].[Locations] where ReferenceID = "+value+") Begin " + statement
        #If the webaddress, location [11], has a wrong value, change the value to null
        if count == 11:
            if value == "http" or value == "https" or value == "http://" or value == "https://":
                value = 'NULL'
        #if any value is empty, replace it with null
        if value == '': value = 'NULL'
        elif type(value) == str: value = "'{}'".format(value)
        statement += str(value) + ', '
        count += 1
    statement = statement[:-2] # Gets rid of last comma and trailing whitespace
    statement += ") END;"
    return statement

'''
Parses location object to generate the "sql update statements" into Locations table
param: location (a single location object)
return: sql update statement (string)
'''
def _update_sql_statement(location, table):
    type_name = location['locatorType']
    if type_name == 'A':
        typed_name = "ATM"
    elif type_name == 'S':
        typed_name ="Shared Branch"
    else: 
        typed_name = ""
    statement = 'update '+table+' set '
    statement += str("Name='"+location['institutionName']+"', Address='"+location['address']+"', City='"+location['city']+"', County='"+location['county']+"', State='"+location['state']+"', PostalCode='"+location['zip']+"', Country='"+location['country']+"', Phone='"+location['phone']+"', Fax='"+location['fax']+"', WebAddress='"+location['webAddress']+"', Latitude='"+location['latitude']+"', Longitude='"+location['longitude']+"', Hours='"+location['hours_open_value']+"', RetailOutlet='"+location['retailOutlet']+"', RestrictedAccess='"+location['restrictedAccess']+"', AcceptDeposit='"+location['acceptDeposit']+"', AcceptCash='"+location['acceptCash']+"', EnvelopeRequired='"+location['envelopeRequired']+"', OnMilitaryBase='"+location['onMilitaryBase']+"', OnPremise='"+location['onPremise']+"', Surcharge='"+location['surcharge']+"', Access='"+location['access']+"', AccessNotes='"+location['accessNote']+"', InstallationType='"+location['installationType']+"', HandicapAccess='"+location['handicapAccess']+"', LocationType='"+typed_name+"', HoursMonOpen='"+location['mondayOpen']+"', HoursMonClose='"+location['mondayClose']+"', HoursTueOpen='"+location['tuesdayOpen']+"', HoursTueClose='"+location['tuesdayClose']+"', HoursWedOpen='"+location['wednesdayOpen']+"', HoursWedClose='"+location['wednesdayClose']+"', HoursThuOpen='"+location['thursdayOpen']+"', HoursThuClose='"+location['thursdayClose']+"', HoursFriOpen='"+location['fridayOpen']+"', HoursFriClose='"+location['fridayClose']+"', HoursSatOpen='"+location['saturdayOpen']+"', HoursSatClose='"+location['saturdayClose']+"', HoursSunOpen='"+location['sundayOpen']+"', HoursSunClose='"+location['sundayClose']+"', HoursDTMonOpen='"+location['mondayDriveThruOpen']+"', HoursDTMonClose='"+location['mondayDriveThruClose']+"', HoursDTTueOpen='"+location['tuesdayDriveThruOpen']+"', HoursDTTueClose='"+location['tuesdayDriveThruClose']+"', HoursDTWedOpen='"+location['wednesdayDriveThruOpen']+"', HoursDTWedClose='"+location['wednesdayDriveThruClose']+"', HoursDTThuOpen='"+location['thursdayDriveThruOpen']+"', HoursDTThuClose='"+location['thursdayDriveThruClose']+"', HoursDTFriOpen='"+location['fridayDriveThruOpen']+"', HoursDTFriClose='"+location['fridayDriveThruClose']+"', HoursDTSatOpen='"+location['saturdayDriveThruOpen']+"', HoursDTSatClose='"+location['saturdayDriveThruClose']+"', HoursDTSunOpen='"+location['sundayDriveThruOpen']+"', HoursDTSunClose='"+location['sundayDriveThruClose']+"', Cashless='"+location['cashless']+"', DriveThruOnly='"+location['driveThruOnly']+"', LimitedTransactions='"+location['limitedTransactions']+"', MilitaryIdRequired='"+location['militaryIdRequired']+"', SelfServiceDevice='"+location['selfServiceDevice']+"', SelfServiceOnly='"+location['selfServiceOnly']+"' where ReferenceID='"+location['institutionRtn']+location['terminalId']+"';")
    statement=statement.replace("''","NULL")
    return statement

'''
Helper function to assist in measuring runtime of functions
'''
def time_convert(sec):
    mins = sec // 60
    sec = sec % 60
    hours = mins // 60
    mins = mins % 60
    print("Time Lapsed for flat DB = {0}:{1}:{2}".format(int(hours),int(mins),sec))



class api_call_thread(threading.Thread):

    def __init__(self, zipcode, key):
        threading.Thread.__init__(self) # must be called in class init
        self.zipcode = zipcode
        self.key = key

    def run(self):
        normalized_json_thread_semaphore.acquire()
        get_key()
        temp=self.zipcode.strip()
        with open('sql_startup_'+temp+'.txt', 'w+') as file:
            sql_statements = _insert_driver(temp)
            for statement in sql_statements:
                file.write(statement)
            file.close()
                     
            File1 = open('sql_startup_'+temp+'.txt', 'r')
            sqlCommand=""
            for line in File1:
                sqlCommand= sqlCommand + line.strip()
                #if str(line).find("325183220ACQ11796") != -1:
                    #print(sqlCommand, " SQL COMMAND ")
            
            if sqlCommand != "":
                try:
                    conn3 = pypyodbc.connect('Driver={ODBC Driver 17 for SQL Server};Server=maphawks.database.windows.net;Database=maphawks_flat;uid=jLadera;pwd=6VxbvqwMazBseP')
                    cursor3 = conn3.cursor()
                    cursor3.execute(sqlCommand)
                    cursor3.commit()
                # NB : you won't get an IntegrityError when reading
                except (pypyodbc.ProgrammingError, pypyodbc.Error)  as e:
                    print("The following sql command Retruned an error for zip code:",temp," when trying to execute in the db:" +'\n',sqlCommand + '\n', "error=", e)
                    pass
            
                cursor3.close()
                conn3.close()
            File1.close()
            os.remove('sql_startup_'+temp+'.txt')
        normalized_json_thread_semaphore.release()


if __name__ == "__main__":

    with open('cache_COOP_LocationIDs.txt','r') as cacheFile:
        checkTable=cacheFile.readlines()
 
    with open('key.txt', 'r') as file:
        key = file.read().strip()

    with open("zipCode_Sampe.txt",'r') as myfile:
        zipcodeList = myfile.readlines(200)

    #zipcodeList = open('sample_zipcodes.txt', 'r')
    start = time.time()
    threads = [api_call_thread(zipcode, key) for zipcode in zipcodeList]
    [thread.start() for thread in threads] # begins running the thread. this calls api_call_thread.run()
    [thread.join() for thread in threads] # joins threads for an accurate measure of concurrency
    end = time.time()
    time_convert(end-start)