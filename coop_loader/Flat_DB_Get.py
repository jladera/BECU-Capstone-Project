import sys
import os
sys.path.append("Lib\site-packages")
sys.path.append(os.getcwd())

import requests
import json
import pypyodbc

class Flat_DB_Get:
    KEY = ""
    MAX_NUMBER_RECORDS_RETURNED = 100

    '''
    Constructor
    SUMMARY:
    Reads CO-OP API key from key.txt (contained in root directory). key.txt contains 1 line: the API key
    '''
    def __init__(self,zipCode):
        self.zipCode=zipCode
        with open('key.txt', 'r') as file:
            Flat_DB_Get.KEY = file.read().strip()

    '''
    SUMMARY:
    Run driver. This calls _call_api() helper function. 
        Step 1: Initial call to helper function returns the API response and list of locations as a list 
                of JSON objects. 
        Step 2: Check how many records are available (in this case checking for locations in zipcode 98122)
        Step 3a: If more locations in zipcode than were returned, continue calling _call_api() helper 
                function with offset parameter set. API returns **MAX** 100 records at a time, so to get
                records 101-200, the method call is _call_api(100)
        Step 3b: Add new records to locations list and increment offset. Loop again as necessary.
        Step 4: Return locations list.
    RETURN:
    List of locations. Each location in list is a Python Dictionary (like C# key,value pair). Keys are as
    documented in the CO-OP API documentation.
    '''
    def run(self,zipCode):
        locations = self._call_api(zipCode)
        return locations

    '''
    SUMMARY:
    Helper function. Calls Co-Op API.
    param: offset; offset as viewed by Co-Op. If offset = 0, result = [1,100]. If offset = 100, result = [101, 200]
    return: List of locations, number of records if offset = 0, else just locations list.
    '''
    def _call_api(self,zipCode):
        conn = pypyodbc.connect('Driver={ODBC Driver 17 for SQL Server};Server=maphawks.database.windows.net;Database=maphawks_flat;uid=jLadera;pwd=6VxbvqwMazBseP')
        cursor = conn.cursor()
        sqlCommand="SELECT * FROM [maphawks_flat].[dbo].[Locations] where PostalCode='"+zipCode+"';"
        checkTable=""
        locations=""
        numberOfRecords=1

        try:
            cursor.execute(sqlCommand)
            checkTable=cursor.fetchall()
            # NB : you won't get an IntegrityError when reading
        except (pypyodbc.ProgrammingError, pypyodbc.Error)  as e:
            s=str(e)
            if (s.find("Invalid object name 'maphawks_flat.dbo.Locations'") != -1):
                print("Table is not there")
            else:
                pass
        
        conn.close()
        cursor.close()
        
        for location in checkTable:
            count=1
            param=""
            for paramValue in location:
                param=self.get_db_param(count)
                if count == len(location):
                    locations=locations+""+str(param)+":"+str(paramValue)+""
                else:
                    locations=locations+""+str(param)+":"+str(paramValue)+","
                count=count+1
            if numberOfRecords == len(checkTable):
                locations=locations+";"
            else:
                locations=locations+";"
            numberOfRecords = numberOfRecords + 1  

        return locations

    def get_db_param(self,count):
        switcher = {
            1: "referenceID",
            2: "institutionName",
            3: "address",
            4: "city",
            5: "county",
            6: "state",
            7: "zip",
            8: "country",
            9: "phone",
            10: "fax",
            11: "webAddress",
            12: "latitude",
            13: "longitude",
            14: "hours_open_value",
            15: "retailOutlet",
            16: "restrictedAccess",
            17: "acceptDeposit",
            18: "acceptCash",
            19: "envelopeRequired",
            20: "onMilitaryBase",
            21: "onPremise",
            22: "surcharge",
            23: "access",
            24: "accessNotes",
            25: "installationType",
            26: "handicapAccess",
            27: "LocationType",
            28: "HoursMonOpen",
            29: "HoursMonClose",
            30: "HoursTueOpen",
            31: "HoursTueClose",
            32: "HoursWedOpen",
            33: "HoursWedClose",
            34: "HoursThuOpen",
            35: "HoursThuClose",
            36: "HoursFriOpen",
            37: "HoursFriClose",
            38: "HoursSatOpen",
            39: "HoursSatClose",
            40: "HoursSunOpen",
            41: "HoursSunClose",
            42: "HoursDTMonOpen",
            43: "HoursDTMonClose",
            44: "HoursDTTueOpen",
            45: "HoursDTTueClose",
            46: "HoursDTWedOpen",
            47: "HoursDTWedClose",
            48: "HoursDTThuOpen",
            49: "HoursDTThuClose",
            50: "HoursDTFriOpen",
            51: "HoursDTFriClose",
            52: "HoursDTSatOpen",
            53: "HoursDTSatClose",
            54: "HoursDTSunOpen",
            55: "HoursDTSunClose",
            56: "cashless",
            57: "driveThruOnly",
            58: "limitedTransactions",
            59: "militaryIdRequired",
            60: "selfServiceDevice",
            61: "selfServiceOnly"
        }
        return switcher.get(count)      