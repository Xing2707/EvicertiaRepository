# Calculator Service
- [Calculator Client](#calculator-client)
	* [Introduction](#Introduction)
	* Functions
		+ TestInput
		+ Save
		+ CreateId
		+ SendRequest and GetResponse
	* Enum
		+ Menu
		+ Urls
	* Nuggets
		+ RestSharp
		+ NLog
- Calculator Librery
	* Addition
	* Subtraction
	* Multiplication
	* Divide
	* Square
	* Journal
- Caluculator Server
	* Introduction
	* Controlles
		+ Addition Controlle
		+ Subtraction Controlle
		+ Multiplication Controlle
		+ Divide Controlle
		+ Square Controlle
		+ Journal Controlle
	* Models
		+ Bad Request Model
		+ Interna Error Model
	* Filters
		+ Bad Request Filters
		+ Interna Error Filters
	* Nuggets
		+ RestSharp
		+ NLog

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------

## Calculator Client

### Introduction

Calculator Client is program Created for core, he function is print menu in console, get input operation selected send resquest a service and get service response.
[imagen Client Console](https://i.imgur.com/NsMNjOb.png)

### Functions

- TestInput
[imagen Test Input](https://i.imgur.com/7JWfXYK.png);
	*In paramete ask input type string while input parse intege is false