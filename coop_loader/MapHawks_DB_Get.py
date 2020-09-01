import requests
import json
import pypyodbc
import sys
import threading

class MapHawks_DB_Get:
 
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
        locationsNoUNoD, locationsNoUD, locationsUD, locationsUNoD = self._maphawks_db_get(zipCode)
        return locationsNoUNoD, locationsNoUD, locationsUD, locationsUNoD
        
    '''
    SUMMARY:
    Helper function. Calls Co-Op API.
    param: offset; offset as viewed by Co-Op. If offset = 0, result = [1,100]. If offset = 100, result = [101, 200]
    return: List of locations, number of records if offset = 0, else just locations list.
    '''
    def _maphawks_db_get(self,zipCode):
        conn = pypyodbc.connect('Driver={ODBC Driver 17 for SQL Server};Server=maphawks.database.windows.net;Database=maphawks;uid=jLadera;pwd=6VxbvqwMazBseP')
        cursor = conn.cursor()
        sqlCommandNoUNoD="select Locations.CoopLocationId, Locations.Name,Locations.Address,Locations.City,Locations.County,Locations.State,Locations.PostalCode,Locations.Country,Contacts.Phone,Contacts.Fax,Contacts.WebAddress,Locations.Latitude,Locations.Longitude,Locations.Hours,Locations.RetailOutlet,SpecialQualities.RestrictedAccess,SpecialQualities.AcceptDeposit,SpecialQualities.AcceptCash,SpecialQualities.EnvelopeRequired,SpecialQualities.OnMilitaryBase,SpecialQualities.OnPremise,SpecialQualities.Surcharge,SpecialQualities.Access,SpecialQualities.AccessNotes,SpecialQualities.InstallationType,SpecialQualities.HandicapAccess,Locations.LocationType,DailyHours.HoursMonOpen,DailyHours.HoursMonClose,DailyHours.HoursTueOpen,DailyHours.HoursTueClose,DailyHours.HoursWedOpen,DailyHours.HoursWedClose,DailyHours.HoursThuOpen,DailyHours.HoursThuClose,DailyHours.HoursFriOpen,DailyHours.HoursFriClose,DailyHours.HoursSatOpen,DailyHours.HoursSatClose,DailyHours.HoursSunOpen,DailyHours.HoursSunClose,DailyHours.HoursDTMonOpen,DailyHours.HoursDTMonClose,DailyHours.HoursDTTueOpen,DailyHours.HoursDTTueClose,DailyHours.HoursDTWedOpen,DailyHours.HoursDTWedClose,DailyHours.HoursDTThuOpen,DailyHours.HoursDTThuClose,DailyHours.HoursDTFriOpen,DailyHours.HoursDTFriClose,DailyHours.HoursDTSatOpen,DailyHours.HoursDTSatClose,DailyHours.HoursDTSunOpen,DailyHours.HoursDTSunClose,SpecialQualities.Cashless,SpecialQualities.DriveThruOnly,SpecialQualities.LimitedTransactions,SpecialQualities.MilitaryIdRequired,SpecialQualities.SelfServiceDevice,SpecialQualities.SelfServiceOnly from [Maphawks].[dbo].locations inner join [Maphawks].[dbo].DailyHours  on (locations.LocationID=DailyHours.LocationID) inner join [Maphawks].[dbo].contacts on (contacts.LocationID=locations.LocationID) inner join [Maphawks].[dbo].SpecialQualities on (Locations.LocationID=SpecialQualities.LocationID) where locations.TakeCoopData=0 and locations.SoftDelete=0 and Locations.PostalCode='"+zipCode+"';"
        sqlCommandNoUD="select Locations.CoopLocationId, Locations.Name,Locations.Address,Locations.City,Locations.County,Locations.State,Locations.PostalCode,Locations.Country,Contacts.Phone,Contacts.Fax,Contacts.WebAddress,Locations.Latitude,Locations.Longitude,Locations.Hours,Locations.RetailOutlet,SpecialQualities.RestrictedAccess,SpecialQualities.AcceptDeposit,SpecialQualities.AcceptCash,SpecialQualities.EnvelopeRequired,SpecialQualities.OnMilitaryBase,SpecialQualities.OnPremise,SpecialQualities.Surcharge,SpecialQualities.Access,SpecialQualities.AccessNotes,SpecialQualities.InstallationType,SpecialQualities.HandicapAccess,Locations.LocationType,DailyHours.HoursMonOpen,DailyHours.HoursMonClose,DailyHours.HoursTueOpen,DailyHours.HoursTueClose,DailyHours.HoursWedOpen,DailyHours.HoursWedClose,DailyHours.HoursThuOpen,DailyHours.HoursThuClose,DailyHours.HoursFriOpen,DailyHours.HoursFriClose,DailyHours.HoursSatOpen,DailyHours.HoursSatClose,DailyHours.HoursSunOpen,DailyHours.HoursSunClose,DailyHours.HoursDTMonOpen,DailyHours.HoursDTMonClose,DailyHours.HoursDTTueOpen,DailyHours.HoursDTTueClose,DailyHours.HoursDTWedOpen,DailyHours.HoursDTWedClose,DailyHours.HoursDTThuOpen,DailyHours.HoursDTThuClose,DailyHours.HoursDTFriOpen,DailyHours.HoursDTFriClose,DailyHours.HoursDTSatOpen,DailyHours.HoursDTSatClose,DailyHours.HoursDTSunOpen,DailyHours.HoursDTSunClose,SpecialQualities.Cashless,SpecialQualities.DriveThruOnly,SpecialQualities.LimitedTransactions,SpecialQualities.MilitaryIdRequired,SpecialQualities.SelfServiceDevice,SpecialQualities.SelfServiceOnly from [Maphawks].[dbo].locations inner join [Maphawks].[dbo].DailyHours  on (locations.LocationID=DailyHours.LocationID) inner join [Maphawks].[dbo].contacts on (contacts.LocationID=locations.LocationID) inner join [Maphawks].[dbo].SpecialQualities on (Locations.LocationID=SpecialQualities.LocationID) where locations.TakeCoopData=0 and locations.SoftDelete=1 and Locations.PostalCode='"+zipCode+"';"
        sqlCommandUNoD="select Locations.CoopLocationId, Locations.Name,Locations.Address,Locations.City,Locations.County,Locations.State,Locations.PostalCode,Locations.Country,Contacts.Phone,Contacts.Fax,Contacts.WebAddress,Locations.Latitude,Locations.Longitude,Locations.Hours,Locations.RetailOutlet,SpecialQualities.RestrictedAccess,SpecialQualities.AcceptDeposit,SpecialQualities.AcceptCash,SpecialQualities.EnvelopeRequired,SpecialQualities.OnMilitaryBase,SpecialQualities.OnPremise,SpecialQualities.Surcharge,SpecialQualities.Access,SpecialQualities.AccessNotes,SpecialQualities.InstallationType,SpecialQualities.HandicapAccess,Locations.LocationType,DailyHours.HoursMonOpen,DailyHours.HoursMonClose,DailyHours.HoursTueOpen,DailyHours.HoursTueClose,DailyHours.HoursWedOpen,DailyHours.HoursWedClose,DailyHours.HoursThuOpen,DailyHours.HoursThuClose,DailyHours.HoursFriOpen,DailyHours.HoursFriClose,DailyHours.HoursSatOpen,DailyHours.HoursSatClose,DailyHours.HoursSunOpen,DailyHours.HoursSunClose,DailyHours.HoursDTMonOpen,DailyHours.HoursDTMonClose,DailyHours.HoursDTTueOpen,DailyHours.HoursDTTueClose,DailyHours.HoursDTWedOpen,DailyHours.HoursDTWedClose,DailyHours.HoursDTThuOpen,DailyHours.HoursDTThuClose,DailyHours.HoursDTFriOpen,DailyHours.HoursDTFriClose,DailyHours.HoursDTSatOpen,DailyHours.HoursDTSatClose,DailyHours.HoursDTSunOpen,DailyHours.HoursDTSunClose,SpecialQualities.Cashless,SpecialQualities.DriveThruOnly,SpecialQualities.LimitedTransactions,SpecialQualities.MilitaryIdRequired,SpecialQualities.SelfServiceDevice,SpecialQualities.SelfServiceOnly from [Maphawks].[dbo].locations inner join [Maphawks].[dbo].DailyHours  on (locations.LocationID=DailyHours.LocationID) inner join [Maphawks].[dbo].contacts on (contacts.LocationID=locations.LocationID) inner join [Maphawks].[dbo].SpecialQualities on (Locations.LocationID=SpecialQualities.LocationID) where locations.TakeCoopData=1 and locations.SoftDelete=0 and Locations.PostalCode='"+zipCode+"';"
        sqlCommandUD="select Locations.CoopLocationId, Locations.Name,Locations.Address,Locations.City,Locations.County,Locations.State,Locations.PostalCode,Locations.Country,Contacts.Phone,Contacts.Fax,Contacts.WebAddress,Locations.Latitude,Locations.Longitude,Locations.Hours,Locations.RetailOutlet,SpecialQualities.RestrictedAccess,SpecialQualities.AcceptDeposit,SpecialQualities.AcceptCash,SpecialQualities.EnvelopeRequired,SpecialQualities.OnMilitaryBase,SpecialQualities.OnPremise,SpecialQualities.Surcharge,SpecialQualities.Access,SpecialQualities.AccessNotes,SpecialQualities.InstallationType,SpecialQualities.HandicapAccess,Locations.LocationType,DailyHours.HoursMonOpen,DailyHours.HoursMonClose,DailyHours.HoursTueOpen,DailyHours.HoursTueClose,DailyHours.HoursWedOpen,DailyHours.HoursWedClose,DailyHours.HoursThuOpen,DailyHours.HoursThuClose,DailyHours.HoursFriOpen,DailyHours.HoursFriClose,DailyHours.HoursSatOpen,DailyHours.HoursSatClose,DailyHours.HoursSunOpen,DailyHours.HoursSunClose,DailyHours.HoursDTMonOpen,DailyHours.HoursDTMonClose,DailyHours.HoursDTTueOpen,DailyHours.HoursDTTueClose,DailyHours.HoursDTWedOpen,DailyHours.HoursDTWedClose,DailyHours.HoursDTThuOpen,DailyHours.HoursDTThuClose,DailyHours.HoursDTFriOpen,DailyHours.HoursDTFriClose,DailyHours.HoursDTSatOpen,DailyHours.HoursDTSatClose,DailyHours.HoursDTSunOpen,DailyHours.HoursDTSunClose,SpecialQualities.Cashless,SpecialQualities.DriveThruOnly,SpecialQualities.LimitedTransactions,SpecialQualities.MilitaryIdRequired,SpecialQualities.SelfServiceDevice,SpecialQualities.SelfServiceOnly from [Maphawks].[dbo].locations inner join [Maphawks].[dbo].DailyHours  on (locations.LocationID=DailyHours.LocationID) inner join [Maphawks].[dbo].contacts on (contacts.LocationID=locations.LocationID) inner join [Maphawks].[dbo].SpecialQualities on (Locations.LocationID=SpecialQualities.LocationID) where locations.TakeCoopData=1 and locations.SoftDelete=1 and Locations.PostalCode='"+zipCode+"';"


        checkTableNoUNoD=""
        checkTableNoUD=""
        checkTableUNoD=""
        checkTableUD=""

        try:
            cursor.execute(sqlCommandNoUNoD)
            checkTableNoUNoD=cursor.fetchall()
            cursor.execute(sqlCommandNoUD)
            checkTableNoUD=cursor.fetchall()
            cursor.execute(sqlCommandUNoD)
            checkTableUNoD=cursor.fetchall()
            cursor.execute(sqlCommandUD)
            checkTableUD=cursor.fetchall()
            # NB : you won't get an IntegrityError when reading
        except (pypyodbc.ProgrammingError, pypyodbc.Error)  as e:
            s=str(e)
            if (s.find("Invalid object name 'maphawks_flat.dbo.Locations'") != -1):
                print("Table is not there")
            else:
                pass
        
        locationsNoUNoD=self.get_params_(checkTableNoUNoD)
        locationsNoUD=self.get_params_(checkTableNoUD)
        locationsUNoD=self.get_params_(checkTableUNoD)
        locationsUD=self.get_params_(checkTableUD)

        conn.close()
        cursor.close()

        return locationsNoUNoD, locationsNoUD, locationsUD, locationsUNoD
        
    def get_params_(self,checkTable):
        locations=""
        numberOfRecords=1
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

    def get_all_zipCodes(self):
        conn2 = pypyodbc.connect('Driver={ODBC Driver 17 for SQL Server};Server=maphawks.database.windows.net;Database=maphawks_flat;uid=jLadera;pwd=6VxbvqwMazBseP')
        cursor2 = conn2.cursor()
        sqlCommandGetZipCode="select distinct Locations.PostalCode from Locations;"

        getAllZipCodes=""

        try:
            cursor2.execute(sqlCommandGetZipCode)
            getAllZipCodes=cursor2.fetchall()
            # NB : you won't get an IntegrityError when reading
        except (pypyodbc.ProgrammingError, pypyodbc.Error)  as e:
            s=str(e)
            if (s.find("Invalid object name 'maphawks_flat.dbo.Locations'") != -1):
                print("Table is not there")
            else:
                pass

        with open('ZipCode_List_FlatDB.txt', 'w+') as file:
            for line in getAllZipCodes:
                file.write(str(line).strip("('").strip("',)")+"\n")
            file.close()

        conn2.close()
        cursor2.close()        


    def set_delete_(self,zipCode):

        conn3 = pypyodbc.connect('Driver={ODBC Driver 17 for SQL Server};Server=maphawks.database.windows.net;Database=maphawks;uid=jLadera;pwd=6VxbvqwMazBseP')
        cursor3 = conn3.cursor()
        sqlCommand="UPDATE maphawks.dbo.Locations set SoftDelete=0 where PostalCode='"+zipCode+"' and maphawks.dbo.Locations.CoopLocationId not in ( select maphawks_flat.DBO.Locations.ReferenceID FROM maphawks_flat.DBO.Locations where PostalCode='"+zipCode+"');"
        getDeleteResponse=""

        try:
            cursor3.execute(sqlCommand)
            getDeleteResponse=cursor3.fetchall()

        except (pypyodbc.ProgrammingError, pypyodbc.Error)  as e:
            s=str(e)
            if (s.find("Invalid object name 'maphawks_flat.dbo.Locations'") != -1):
                print("Table is not there")
            else:
                pass

        conn3.close()
        cursor3.close()

    def execute_command_(self,sqlCommands):
        conn4 = pypyodbc.connect('Driver={ODBC Driver 17 for SQL Server};Server=maphawks.database.windows.net;Database=maphawks;uid=jLadera;pwd=6VxbvqwMazBseP')
        cursor4 = conn4.cursor()
        if sqlCommands != "":
            try:
                cursor4.execute(sqlCommands)
                cursor4.commit()
            
            # NB : you won't get an IntegrityError when reading
            except (pypyodbc.ProgrammingError, pypyodbc.Error)  as e:
                print("The following sql command Retruned an error when trying to execute in the db:" +'\n',sqlCommands + '\n', "error=", e)
                pass
                
        cursor4.close()
        conn4.close()