# OctobridgeDatabase

## Overview

The ```OctobridgeDatabase``` project contains the database schema and scripts to build the database.
It is designed to support the backend data requirements for all Octobridge services and clients, including REST APIs and web applications.

---

## Project Structure

- **dbo/**  
  Contains the tables, triggers, types and stored procedures.
- **Scripts/**  
  Contains scripts to run post deployment.

---

## Prerequisites

- SQL Server 2022 or later (local or remote instance)

---

## Deployment Instructions  

Select the ```Publish``` command from the context menu of the project.  

Check the following settings in the Publish dialog:
* Target database connection: Select required SQL Server instance
* Database name: ```Octobridge```
* Publish script: ```OctobridgeDatabase.sql```

Press the ```Publish``` button to deploy the database.  
