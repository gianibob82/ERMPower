# ERMPower

## Project structure
A EnergyReading API is consumed by a Console app.

EnergyReading service is broken down following DDD architecture and is fully independent from the console app
EnergyReading API doesn't expose documentation (like with Swagger) because not really necessary for now.

## Csv files error handling
I assume CSV files used as datasource are correct and follow the same structure as provided.

## How to run it
You can run the project without having to deploy API to webserver. Launch VS having API project as main project.
Build the console app and run the ps1 command
