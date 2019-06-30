# Carsales Monitoring Solution - Prototype
## Overview
This is prototype to demonstrate the potential solution for Carsales monitoring solution. 

The system contains below components
1. AdminClient
2. MonitoringWebApi
3. PollingService

**1. AdminClient**

Client configured to run on http://localhost:53659/ that calls fetches data from the webapi.
The http endpoints are hardcoded to get historical data between 2018-01-01 to 2020-01-01 ( from database ) and the latest messagecount from rabbitmq

**2. MonitoringWebApi**

Simple webapi configured to run on http://localhost:60634/ that returns

a. Live message count directly from rabbitmq

b. Historical information ( top 10 order by createddate desc ) from database

**3. PollingService**

Simple windows service configured to run every x minutes. The service polls number of messages at given point of time and updates
database.

## How to run?
a. Setup a new database 'Carsales' on MSSQL and run below script to create QueueHistory table
USE [Carsales]
GO

CREATE TABLE [dbo].[QueueHistory](
	[MessageCount] [int] NULL,
	[QueueName] [varchar](255) NULL,
	[CreatedAt] [datetime] NULL
) ON [PRIMARY]

GO

b. Ensure an exchange 'TestExchange' and 'Queue' is created and publish messages

c. Download the code

d. Open MonitoringSolution.sln in VS 2017

e. Ensure that values in the config are correct ( Connectionstring to database, rabbitmq details, exchange and queuenames, etc )
for the MonitoringWebi and PollingService

f. Run the solution

## Notes

1. No logging, auditing and few hardcodings at the client values are done since this is a prototype

2. To bypass CORS issue, the webapi has below value hard coded in order to accept request from the client that runs on localhost:53659

[EnableCors(origins: "http://localhost:53659", headers: "*", methods: "*")] 

3. Notification Service is not implemented.


