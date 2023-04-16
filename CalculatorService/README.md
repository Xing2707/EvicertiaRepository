# Calculator Service

## Calculator Client

### Introduction

Calculator Client is program Created for core, he function is print menu in console, get input operation selected send resquest a service and get service response.
![imagen Client Console](https://i.imgur.com/NsMNjOb.png)

### Functions

- TestInput

![imagen Test Input Function](https://i.imgur.com/7JWfXYK.png)

This function in paramete ask input type string while input parse intege is false print error message ask new input to input parse intege is true

- Save

![imagen Save Function](https://i.imgur.com/Az0oyx2.png)

This function ask user introduce input, fromat input to lowercase while input is different "s" or "n" print error message ask new input to input is "s" or "n"
if input is "s" return true else return false

-CreateId

![imagen CreateId Function](https://i.imgur.com/rewchCV.png)

This function create id in random string with length is 5

-SendRequest

![imagen SedRequest](https://i.imgur.com/cmBz7pN.png)

This function in paramete ask integer number, string host, string path, object data
If number selected is smaller than 6 call function Save obtain Save return variable
Create new RestClient with host
Create new RestRquest whit path and Method Post
Add request header
If Save return is true and num is smaller than 6
Call function CreateId obtian CreateId return variable
Add request header with id created
Else if num is smaller than 6
Add request header with id xxx becauser function sevar returno false
Add body format json with data
in switch with number selected call number selected function

-GetResponse

![imagen getResponse Function](https://i.imgur.com/ZoEF2mH.png)

This is function generic in in paramete required resquest,response,restClient,restRequest,string name
Create response using restClient excecute response in paramete request
If response return "OK" status code get response.data and get response.data properties using foreach print result.
end foreach using name for generate log.
If response return status code is different "OK" print Response ErrorMessage and generate errorlog using name.


### Enum

-Menu

![imagen menu](https://i.imgur.com/X5eT01C.png)

The menu is using in client menu for each operation convert string selected in number

-Urls

![imagen urls](https://i.imgur.com/K05FIEZ.png)

The urls is using for request path

### Nuggets

-RestSharp(109.0.1)
	install and using in Service and Client

-Nlogs(5.1.3)
	install and using in Service and Client

---

## Calculator Librery

- Addition
	* AdditionRequest class
	* AdditionResponse class
	* Using in Client and Service
- Subtraction
	* SubtractionRequest class
	* SubtractionResponse class
	* Using in Client and Service
- Multiplication
	* MultiplicationRequest class
	* MUltiplicationResponse class
	* Using in Client and Service
- Divide
	* DivideRequest class
	* DivideResponse class
	* Using in Client and Service
- Square
	* SquareRequest class
	* SquareResponse class
	* Using in Client and Service
- Journal
	* JournalRequest class
	* JournalResponse class
	* Using in Client and Service

## Caluculator Server

### Introduction

Calculato Server is program create with framework WEB API the Server is responsible receive resquest by client, validate request calculate request data and return result in response.

![Imagen Web Api Swagger](https://i.imgur.com/DO0Q7Ck.png)

### Cotrollers

- Addition Controlle
	* the cotroller have function calculate addtion.
	* the cotroller have function save for save Journal if user ask save.
	* Using solo Service.
- Subtraction Controlle
	* the cotroller have function calculate subtraction.
	* the cotroller have function save for save Journal if user ask save.
	* Using solo Service.
- Multiplication Controlle
	* the cotroller have function calculate multiplication.
	* the cotroller have function save for save Journal if user ask save.
	* Using solo Service.
- Divide Controlle
	* the cotroller have function calculate divide.
	* the cotroller have function save for save Journal if user ask save.
	* Using solo Service.
- Square Controlle
	* the cotroller have function calculate square.
	* the cotroller have function save for save Journal if user ask save.
	* Using solo Service.
- Journal Controlle
	* the cotroller have function savejournal.
	* the cotroller have function getjournal.
	* Using solo Service.

### Models

-BadRequstModel

-InternalErrorModel

### filters

- CustomBadRequstFilterAttribute
	* overwriter swager bad request error default using badRequstModel

- CustomInternaFilterAttribute

* overwrites swagger internal error deafult using InternalErrorModel

### Nuggets
	
- RestSharp

-NLog