# -*- coding: utf-8 -*-
import uuid

class Locations_Table:
	CREATE_LOCATIONS_TABLE = """if not exists (select * from sysobjects where name='Locations' and xtype='U')CREATE TABLE Locations(
		LocationID varchar(64) NOT NULL PRIMARY KEY, 
		CoopLocationId varchar(64) NULL, 
		TakeCoopData bit NOT NULL, 
		SoftDelete bit NOT NULL, 
		Name varchar(64) NOT NULL, 
		Address varchar(64) NOT NULL, 
		City varchar(64) NOT NULL, 
		County varchar(64) NULL, 
		State varchar(64) NOT NULL, 
		PostalCode varchar(64) NOT NULL, 
		Country varchar(64) NULL, 
		Latitude decimal(9, 6) NOT NULL, 
		Longitude decimal(9, 6) NOT NULL, 
		Hours varchar(64) NULL, 
		RetailOutlet varchar(64) NULL, 
		LocationType varchar(64) NOT NULL,
		Point as geography::Point(Latitude, Longitude, 4326)
		);"""
		
		
	# combination of terminalId and institutionRtn creates the CoopLocationId field
	# example: 'terminalId-institutionRtn'
	api_fields_corresponding_to_column_order = ['referenceID', 
		'NULL', # Corresponds to bit value TakeCoopData and SoftDelete
		'institutionName', 
		'address', 
		'city', 
		'county', 
		'state', 
		'zip', 
		'country', 
		'latitude', 
		'longitude', 
		'hours_open_value', 
		'retailOutlet', 
		'LocationType']


	def get_create_table(self):
		return Locations_Table.CREATE_LOCATIONS_TABLE

	def get_insert_row(self, location):
		array=location.split(",")
		count=1
		try:
			locationId = str(uuid.uuid4()) # BECU LocationId
			statement = "INSERT INTO Locations VALUES ('{}' ".format(locationId)
			for values in array:
				value=values.split(":")
				if value[0] == "referenceID":
					statement = "if not exists (select CoopLocationId from [Maphawks].[dbo].[Locations] where CoopLocationId = '" + value[1]+"') " + statement
				if count==2:
					statement += ', 1, 0' # Sets TakeCoopData to TRUE and SoftDelete to FALSE
				for api_field in Locations_Table.api_fields_corresponding_to_column_order:
					if api_field == value[0]: # if data present for column
						if value[1] == 'None':
							statement += ", NULL"
						else:
							statement += ", '"+value[1]+"'"
				count += 1
			statement += ");\n"
			return statement, locationId
		except Exception as e:
			print("error: {}\nApi Field: {}\nStatement thus far: {}".format(e, api_field, statement))
		

	def get_update_row(self, location, refID):
		print ("Updating rows")
		array=location.split(",")
		count=1
		try:
			statement = "UPDATE Locations SET "
			for values in array:
				value=values.split(":")
				for api_field in Locations_Table.api_fields_corresponding_to_column_order:
					if api_field != 'referenceID':
						if api_field == value[0]: # if data present for column
							if api_field == 'institutionName':
								api_field = 'Name'
							if api_field == 'hours_open_value':
								api_field = 'Hours'
							if api_field =='zip':
								api_field= 'PostalCode'
							if count == 1:
								statement += api_field+" = "
								count += 1
							else:
								statement += ", "+api_field+" = "

							if value[1] == 'None':
								statement += "NULL"
							else:
								statement += "'"+value[1]+"'"
			statement += " where CoopLocationId = '"+refID+"';\n"
			return statement
		except Exception as e:
			print("error: {}\nApi Field: {}\nStatement thus far: {}".format(e, api_field, statement))
		

