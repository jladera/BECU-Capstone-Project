import sys
import os
sys.path.append("Lib\site-packages")
sys.path.append(os.getcwd())

import pypyodbc
import time

CREATE_LOCATIONS_TBL = "IF OBJECT_ID('[maphawks_flat].[dbo].[Locations]', 'U') IS NULL BEGIN CREATE TABLE Locations(ReferenceID  VARCHAR(64) NOT NULL PRIMARY KEY,Name  VARCHAR(64),Address  VARCHAR(64) NOT NULL,City  VARCHAR(64) NOT NULL,County  VARCHAR(64),State  VARCHAR(64) NOT NULL,PostalCode  VARCHAR(64),Country  VARCHAR(64),Phone  VARCHAR(64),Fax  VARCHAR(64),WebAddress  VARCHAR(64),Latitude  DECIMAL(9,6),Longitude  DECIMAL(9,6),Hours  VARCHAR(64),RetailOutlet  VARCHAR(64),RestrictedAccess  VARCHAR(64),AcceptDeposit  VARCHAR(64),AcceptCash  VARCHAR(64),EnvelopeRequired  VARCHAR(64),OnMilitaryBase  VARCHAR(64),OnPremise  VARCHAR(64),Surcharge  VARCHAR(64),Access  VARCHAR(64),AccessNotes  VARCHAR(64),InstallationType  VARCHAR(64),HandicapAccess  VARCHAR(64),LocationType  VARCHAR(64),HoursMonOpen VARCHAR(200),HoursMonClose     VARCHAR(200),HoursTueOpen      VARCHAR(200),HoursTueClose     VARCHAR(200),HoursWedOpen      VARCHAR(200),HoursWedClose     VARCHAR(200),HoursThuOpen      VARCHAR(200),HoursThuClose     VARCHAR(200),HoursFriOpen      VARCHAR(200),HoursFriClose     VARCHAR(200),HoursSatOpen      VARCHAR(200),HoursSatClose     VARCHAR(200),HoursSunOpen      VARCHAR(200),HoursSunClose     VARCHAR(200),HoursDTMonOpen    VARCHAR(200),HoursDTMonClose   VARCHAR(200),HoursDTTueOpen    VARCHAR(200),HoursDTTueClose   VARCHAR(200),HoursDTWedOpen    VARCHAR(200),HoursDTWedClose   VARCHAR(200),HoursDTThuOpen    VARCHAR(200),HoursDTThuClose   VARCHAR(200),HoursDTFriOpen    VARCHAR(200),HoursDTFriClose   VARCHAR(200),HoursDTSatOpen    VARCHAR(200),HoursDTSatClose   VARCHAR(200),HoursDTSunOpen    VARCHAR(200),HoursDTSunClose   VARCHAR(200),Cashless  VARCHAR(64),DriveThruOnly VARCHAR(64),LimitedTransactions VARCHAR(64),MilitaryIdRequired VARCHAR(64),SelfServiceDevice VARCHAR(64),SelfServiceOnly VARCHAR(64)) END;\n"
sqlCommand="SELECT ReferenceID FROM [maphawks_flat].[dbo].[Locations];"
checkTable = ""

if __name__ == "__main__":
    conn = pypyodbc.connect('Driver={ODBC Driver 17 for SQL Server};Server=maphawks.database.windows.net;Database=maphawks_flat;uid=jLadera;pwd=6VxbvqwMazBseP')
    cursor = conn.cursor()


    try:
        cursor.execute(CREATE_LOCATIONS_TBL)
        cursor.commit()
        # NB : you won't get an IntegrityError when reading
    except (pypyodbc.ProgrammingError, pypyodbc.Error)  as e:
        print("Error while trying to create table. Error is= ",e)

    time.sleep(3)

    try:
        cursor.execute(sqlCommand)
        checkTable=cursor.fetchall()
        # NB : you won't get an IntegrityError when reading
    except (pypyodbc.ProgrammingError, pypyodbc.Error)  as e:
        print("Error while trying to coop location IDs= ",e)
        pass

    with open("cache_COOP_LocationIDs.txt",'w+') as myfile:
        myfile.write(str(checkTable))

    conn.close()
    cursor.close()