# -*- coding: utf-8 -*-
class DailyHours_Table:
	CREATE_HOURS_TABLE = """IF OBJECT_ID('[maphawks].[dbo].[DailyHours]', 'U') IS NULL BEGIN	CREATE TABLE DailyHours(
		LocationID varchar(64) NOT NULL PRIMARY KEY,
		HoursMonOpen varchar(10) NULL,
		HoursMonClose varchar(10) NULL,
		HoursTueOpen varchar(10) NULL,
		HoursTueClose varchar(10) NULL,
		HoursWedOpen varchar(10) NULL,
		HoursWedClose varchar(10) NULL,
		HoursThuOpen varchar(10) NULL,
		HoursThuClose varchar(10) NULL,
		HoursFriOpen varchar(10) NULL,
		HoursFriClose varchar(10) NULL,
		HoursSatOpen varchar(10) NULL,
		HoursSatClose varchar(10) NULL,
		HoursSunOpen varchar(10) NULL,
		HoursSunClose varchar(10) NULL,
		HoursDTMonOpen varchar(10) NULL,
		HoursDTMonClose varchar(10) NULL,
		HoursDTTueOpen varchar(10) NULL,
		HoursDTTueClose varchar(10) NULL,
		HoursDTWedOpen varchar(10) NULL,
		HoursDTWedClose varchar(10) NULL,
		HoursDTThuOpen varchar(10) NULL,
		HoursDTThuClose varchar(10) NULL,
		HoursDTFriOpen varchar(10) NULL,
		HoursDTFriClose varchar(10) NULL,
		HoursDTSatOpen varchar(10) NULL,
		HoursDTSatClose varchar(10) NULL,
		HoursDTSunOpen varchar(10) NULL,
		HoursDTSunClose varchar(10) NULL,
		FOREIGN KEY (LocationID) REFERENCES [Locations] (LocationID) ON UPDATE NO ACTION ON DELETE CASCADE) END; \n"""
	
	api_fields_corresponding_to_column_order = ['HoursMonOpen',
		'HoursMonClose',
		'HoursTueOpen',
		'HoursTueClose',
		'HoursWedOpen',
		'HoursWedClose',
		'HoursThuOpen',
		'HoursThuClose',
		'HoursFriOpen',
		'HoursFriClose',
		'HoursSatOpen',
		'HoursSatClose',
		'HoursSunOpen',
		'HoursSunClose',
		'HoursDTMonOpen',
		'HoursDTMonClose',
		'HoursDTTueOpen',
		'HoursDTTueClose',
		'HoursDTWedOpen',
		'HoursDTWedClose',
		'HoursDTThuOpen',
		'HoursDTThuClose',
		'HoursDTFriOpen',
		'HoursDTFriClose',
		'HoursDTSatOpen',
		'HoursDTSatClose',
		'HoursDTSunOpen',
		'HoursDTSunClose']

	def get_create_table(self):
		return DailyHours_Table.CREATE_HOURS_TABLE

	def get_insert_row(self, location, locationId):
		all_null = True # if true after looping, return an empty string
		statement = "if exists (select LocationID from Locations where LocationID='{0}') BEGIN INSERT INTO DailyHours VALUES ('{0}'".format(locationId)
		array=location.split(",")
		for values in array:
			value=values.split(":")
			for api_field in DailyHours_Table.api_fields_corresponding_to_column_order:
				if api_field == value[0]: # if data present for column
					if value[1] == 'None':
						statement += ", NULL"
					else:
						statement += ", '"+value[1]+"'"
						if all_null: all_null = False
		statement += ") END;\n"
		return statement

	def get_update_row(self, location, refID):
		array=location.split(",")
		count=1
		try:
			statement = "UPDATE DailyHours SET "
			for values in array:
				value=values.split(":")
				for api_field in DailyHours_Table.api_fields_corresponding_to_column_order:
					if api_field == value[0]: # if data present for column
						if count == 1:
							statement += api_field+" = "
							count += 1
						else:
							statement += ", "+api_field+" = "

						if value[1] == 'None':
							statement += "NULL"
						else:
							statement += "'"+value[1]+"'"
			statement += " where LocationID = (select LocationID from Locations where CoopLocationId = '"+refID+"');\n"
			return statement
		except Exception as e:
			print("error: {}\nApi Field: {}\nStatement thus far: {}".format(e, api_field, statement))