##### AMSDB2DB
* author:caojin
* start date:2019-6-17
* descrption:this project used to export one table to another table of another db
* only db2 and oracle are supported preliminarily
* * *
######How to use:
Please enter App Directory,then create a task json file(like task1.json),files name is your task name:
{
  "From": {				//From DB Setting
    "DBType": "Oracle",			//Oracle or DB2,Ignore letter case
    "HostName": "10.132.0.38",		//DB Server Address(IPAddress&HostName all supported),If dbtype is db2,you can let this empity
    "Port": 1521,			//DB Server TCP/IP Port,If dbtype is db2,you can let this empity
    "UserID": "ace_loader",		//DB User
    "Password": "KT4LOADER",		//DB Password
    "ServiceName": "acexp",		//Oracle:ServiceName,DB2:AliasName
    "Table": "ACME.ACEUSAGE",		//Table
    "Columns": [ "*" ],			//Columns,* represent all columns.you can set given columns split by comma
    "Sequence": "Time"			//Sequence,the column name that can tell the program which rows has been copyed
  },
  "To": {				//To is the same as From except Sequence.To no need to set sequence
    "DBType": "DB2",
    "UserID": "sitst1",
    "Password": "sitstcim1",
    "ServiceName": "ATRPT1",
    "Table": "ISTRPT.ACEUSAGE"
  },
  "Interval": 300			//How long(seconds) excute the task once
}

Then,write your task name in service.json:
{

  "Tasks": [
    "task1"
  ]
}

After you run the program,a {YourTaskName}.seq.json will be created which keep the information of which rows has been copyed if you set sequence value in {TaskName}.json file.


