from aenum import Enum, auto

class Sql_Commands(Enum):
    CREATE = auto()
    INSERT = auto()
    SELECT = auto()
    UPDATE = auto()

class Sql_Tables(Enum):
    Locations = auto()
    Contacts = auto()
    SpecialQualities = auto()
    DailyHours= auto()