Now i don't know what useful things i can give in Admin and User docs and bcuz of it they're empty:)  
Read the text below)  

## How to interact via Swagger:

In general, data is fed to Swagger (as from a pseudo-frontend): the guid or the name of the user on whose behalf the action is being performed, the guid of the person to whom the action is directed,
the id (if we are talking about the picture), as well as parameters (description of the picture, etc.). If the data is entered correctly, then the action executed successfully.  
In case of input errors, comments and/or corresponding error codes are returned.  
### The error may occur if  
enter a simple password of type 123. In ASP.NET Identity has a password validator by default, which will not miss such tricks. The password must contain special characters, letters, numbers,
different case.
### Uploaded images  
can be found in the directory specified in appsettings.json (the path is configurable).  

## About comments:

During the absence of the frontend, the attributes [Authorize] and [ValidateAntiForgeryToken] are commented out in the code.  
This decision was made due to the absence, according to the author of the project, of the need to reinvent the wheel:
In a full-fledged site (and this is only a backend), the attributes will work.  

There are also comments in the Pictures controller. This is due to the considered (and so far postponed for an unknown period) possibility
to check the uniqueness of the pictures - and, in the case of finding the same (in content, not in name) - simply give access to the same picture  
multiple users. However, the opportunity is not considered necessary yet.  

The rest of the comments are also purposefully left in the code (at least for the most part :)).

Thanks for reading!)