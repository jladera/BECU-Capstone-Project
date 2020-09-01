# -*- coding: utf-8 -*-
class SpecialQualities_Table:
	CREATE_LOCATIONS_TABLE = """if not exists (select * from sysobjects where name='SpecialQualities' and xtype='U') CREATE TABLE SpecialQualities(
		LocationID varchar(64) NOT NULL PRIMARY KEY,
		RestrictedAccess varchar(64) NULL,
		AcceptDeposit varchar(64) NULL,
		AcceptCash varchar(64) NULL,
		EnvelopeRequired varchar(64) NULL,
		OnMilitaryBase varchar(64) NULL,
		OnPremise varchar(64) NULL,
		Surcharge varchar(64) NULL,
		Access varchar(64) NULL,
		AccessNotes varchar(64) NULL,
		InstallationType varchar(64) NULL,
		HandicapAccess varchar(64) NULL,
		Cashless varchar(64) NULL,
		DriveThruOnly varchar(64) NULL,
		LimitedTransactions varchar(64) NULL,
		MilitaryIdRequired varchar(64) NULL,
		SelfServiceDevice varchar(64) NULL,
		SelfServiceOnly varchar(64) NULL,
		CoinStar varchar(64) NULL,
		TellerServices varchar(64) NULL,
		[24HourExpressBox] varchar(64) NULL,
		PartnerCreditUnion varchar(64) NULL,
		MemberConsultant varchar(64) NULL,
		InstantDebitCardReplacement varchar(64) NULL,
		FOREIGN KEY (LocationID) REFERENCES [Locations] (LocationID) ON UPDATE NO ACTION ON DELETE CASCADE);"""	
	api_fields_corresponding_to_column_order = ['restrictedAccess',
		'acceptDeposit',
		'acceptCash',
		'envelopeRequired',
		'onMilitaryBase',
		'onPremise',
		'surcharge',
		'access',
		'accessNotes',
		'installationType',
		'handicapAccess',
		'cashless',
		'driveThruOnly',
		'limitedTransactions',
		'militaryIdRequired',
		'selfServiceDevice',
		'selfServiceOnly']

	def __init(self):
		self.lowkey = 'lowkey'

	def get_create_table(self):
		return SpecialQualities_Table.CREATE_LOCATIONS_TABLE
	
	def get_insert_row(self, location, locationId):
		all_null = True
		statement = "if exists (select LocationID from Locations where LocationID='{0}') BEGIN INSERT INTO SpecialQualities VALUES ('{0}'".format(locationId)
		array=location.split(",")
		for values in array:
			value=values.split(":")
			for api_field in SpecialQualities_Table.api_fields_corresponding_to_column_order:
				if api_field == value[0]: # if data present for column
					if value[1] == 'None':
						statement += ", NULL"
					else:
						statement += ", '"+value[1]+"'"
						if all_null: all_null = False
		statement += ",NULL,NULL,NULL,NULL,NULL,NULL) END;\n"
		return statement

	def get_update_row(self, location, refID):
		array=location.split(",")
		count=1
		try:
			statement = "UPDATE SpecialQualities SET "
			for values in array:
				value=values.split(":")
				for api_field in SpecialQualities_Table.api_fields_corresponding_to_column_order:
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