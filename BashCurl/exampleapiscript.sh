#!/bin/bash

# bash/curl examples for interacting with the NameBright Domain API using the REST endpoints.

#get a token
token=`curl -d grant_type=client_credentials -d client_id=<account name>:<application name> --data-urlencode "client_secret=<enter api secret here>" https://api.namebright.com/auth/token | sed  's/{"access_token":"\([^"]*\).*/\1/'`

#list the host records
curl -H "Authorization: Bearer $token" https://api.namebright.com/rest/account/domains/example.com/hostrecords

#create a new host record with POST data (application/x-www-form-urlencoded)
curl -i -H "Authorization: Bearer $token" -d Subdomain=foo -d IPV4Address=123.123.123.123 https://api.namebright.com/rest/account/domains/example.com/hostrecords/a

#delete the record by id
curl -i -X DELETE -H "Authorization: Bearer $token" https://api.namebright.com/rest/account/domains/example.com/hostrecords/txt/9100

#add a nameserver to a domain. Note: curl doesn't send Content-Length when no message body is present so we
#need to do this ourselves:
curl -i -X PUT -H "Authorization: Bearer $token" -H "Content-Length: 0" https://api.namebright.com/rest/account/domains/example.com/nameservers/ns1.namebrightdns.com

#change contact info
curl -i -X PUT -H "Authorization: Bearer $token" -d Phone=303.555.1234 -d PostalCode=80203 -d FirstName=John -d LastName=Doe -d Email=johndoe@example.com -d "Address1=200 East Colfax Avenue" -d City=Denver -d Region=CO -d Country=US https://api.namebright.com/rest/account/domains/example.com/contacts/technical

#change locking, auto-renew, or whois privacy
curl -i -X PUT -H "Authorization: Bearer $token" -d DomainName=example.com -d Status=active -d ExpirationDate=2017-05-06T00:00:00 -d Locked=true -d AutoRenew=false -d WhoIsPrivacy=true -d "Category=Default Category" -d UpgradedDomain=false https://api.namebright.com/rest/account/domains/example.com
