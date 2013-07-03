DomainApiClientExamples 
======================= 
This repository contains example code for interacting with NameBright's 
Domain API. NameBright.com is a registrar for buying and managing domain 
names. NameBright has exposed this API for advanced users who wish to 
automate tasks related to domain management. 

To use the code examples, you must first have an account at 
NameBright.com and request API access. 

1. Create a NameBright account here: 
https://www.namebright.com/NewAccount 
2. Once you've created an account you can request API access here: 
https://www.namebright.com/Settings#Api 

Supported Functions 
======================= 

The following is a list of functions currently supported by the Domain 
API: 

* Check domain availability 
* Register a domain 
* Renew a domain 
* Retrieve and update name server information for a domain 
* Retrieve and update DNS host records for a domain 
* Retrieve and update contact information for a domain 
* Change domain level settings such as: locked status, auto-renew status 
and privacy protection 

General Concepts 
================

Authorization 
------------- 

Each call to the Domain API must include a valid OAuth2 Bearer Token to 
authorize the request. To retrieve a bearer token, you need to POST an 
OAuth2 client_credentials authorization request to NameBright's 
authentication service. Each code example shows you how to do this but 
you need to set valid values for the cilent_id (NameBright's API 
Application name) and client_secret fields. See: 
https://api.namebright.com/auth/help and 
http://aaronparecki.com/articles/2012/07/29/1/oauth2-simplified#application-access for details. 

Each OAuth2 bearer token is valid for 30 minutes. If you are writing a 
process which needs to run for longer than 30 minutes, it is recommended 
that you expect to receive HTTP 401 Authorization Required responses 
within your program flow. 

API Applications 
---------------- 
You can set up as many API Applications as you wish at 
https://www.namebright.com/Settings#Api. Each needs to have a name 
unique to your account and specify an IP whitelist. After creating an 
API Application, you will be assigned a secret used to obtain OAuth2 
Bearer Tokens. Full application names are in the format of "account 
name:application name". e.g. "MyAccount:MyApp". 

Restrictions 
------------ 

* Purchases: The API currently supports registering and renewing domains 
within your account by drawing on your account's pre-funded balance. You 
may only make purchases through the API by pre-funding your account. 

* Rate limits: The API enforces a limit of 30 requests per 30 seconds. 
* IP Whitelist: The API checks your IPv4 address against the whitelist 
specified in your account for that API application. 
* The NameBright API does not allow you to "play the drop". Domains 
which are dropping that day will be listed as "unavailable" by the API. 
This is by design. 

Endpoints 
--------- 

* OAuth2 authorization: https://api.namebright.com/auth/token Help page: 
https://api.namebright.com/auth/Help 
* Domain API SOAP RPC 
  * Purchasing: https://api.namebright.com/soap/DomainPurchasing.svc?singleWsdl
  * Management: https://api.namebright.com/soap/DomainManagement.svc?singleWsdl

