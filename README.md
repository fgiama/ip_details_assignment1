# ip_stack_assesment

The Framework used for this solution is .NET Framework 4.7.2

This solution creates:

1. A library 'IPDetailsLibrary' that creates a request to IPStack API to get information about an IP. 
	- When the IPStack responses with an error, then an IpServiceNotAccessibleException is thrown. 
	- When the no Ip inforamtion are received, then an IpNotFoundException is thrown.


2. An ASP.NET Wep Api that calls the library above to get IP information. Moreover:
	- It uses EntityFramework 6 as an ORM to connect to db.
	- .NET Memory cache is used for caching.
	- For background jobs HangFire is used.
	- For dependency injection Autofac is used.

To setup the database

	- Use the scripts in 'ip_stack_assesment\IPDetailsWebApi\DBScripts' to create the db and tables 

To use the Web API use the following uri:

	- Get ip details : api/ipdetails/{requested_ip} (get)
	- Post list to update : api/ipdetails/ (post)
	- Get job's status : api/jobdetails/{requested_job} (get)
 