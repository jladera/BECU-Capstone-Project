# -*- coding: utf-8 -*-
class Contacts_Table:
	CREATE_CONTACTS_TABLE = """IF OBJECT_ID('[maphawks].[dbo].[Contacts]', 'U') IS NULL BEGIN CREATE TABLE Contacts(LocationID varchar(64) NOT NULL PRIMARY KEY,Phone varchar(64) NULL, Fax varchar(64) NULL, WebAddress varchar(64) NULL, FOREIGN KEY (LocationID) REFERENCES [Locations] (LocationID) ON UPDATE NO ACTION ON DELETE CASCADE) END; \n"""

	api_fields_corresponding_to_column_order = ['phone','fax','webAddress']

	def get_create_table(self):
		return Contacts_Table.CREATE_CONTACTS_TABLE

	'''
	Creates insert statement for contacts table.
	Arg:locationId is the BECU locationId  GUID
	'''
	def get_insert_row(self, location, locationId):
		all_null = True
		array=location.split(",")
		statement = "if exists (select LocationID from Locations where LocationID='{0}') BEGIN INSERT INTO Contacts VALUES ('{0}'".format(locationId)
		for values in array:
			value=values.split(":")
			for api_field in Contacts_Table.api_fields_corresponding_to_column_order:
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
			statement = "UPDATE Contacts SET "
			for values in array:
				value=values.split(":")
				for api_field in Contacts_Table.api_fields_corresponding_to_column_order:
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
