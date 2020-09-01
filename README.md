# Purpose
Our goal for this project was to build an Azure-hosted Location Mapping Service for a financial instution and its affiliate institutions
utilizing location data from a third-party API. 

Our solution contains 3 pillars:
1. Location Management Console (adminconsole)

   Allows for an internal user to perform CRUD operations on ATM/Branch locations.
   
   
2. Loction Mapping Service (Locator)

   Displays sortable and filterable locations on a map to an external user. The user may choose a location and receive
   directions to the aforementioned location.


3. Location Data Loader (coop_loader)

   Receives location records as responses from a third-party API, cleans the data it receives, and Inserts/Updates records
   into local databases.


# Tech Stack
### Server
- .Net Core 3.1
- EntityFrameworkCore 3.1.2
- NetTopologySquite 2.0.0
- Razor views
- GoogleMaps (via GoogleMaps API)

### Client
- HTML5
- Bootstrap (latest)
- Bootstrap Table 1.15.4
- JQuery 3.4.1
- AJAX 3.4.1
- GoogleMaps (via GoogleMaps API)

### Script
- Python 3.7.2
- Requests 2.23.4
- Aenum 2.2.3
- Pypyodbc 1.3.4

### Persistent Data Store
- Azure SQL Server 12.0.2000.8
- Azure SQL Database

### Testing
- MSTest 2.1.0
- MOQ 4.13.1


# Launch
To deploy, you must:
1. Posess or create a Microsoft Azure Account
2. Create a new Azure SQL Database resouce

   - Create 2 databases 'maphawks' and 'maphawks_flat'
   - Set firewall to allow Azure services (this is how we developed, however we acknowlege the security risks 
      associated with this approach and thereby recommend setting your own firewall rules upon deployment)
   - Get connection strings
3. Update conenction string in Locator, adminconsole, and coop_loader projects.
4. Execute run.py:
   
   ```python3 run.py```
   This will create and populate the necessary tables in the 2 databases in your Azure SQL Server instance

5. Publish adminconsole and Locator projects to Azure WebApp instances
