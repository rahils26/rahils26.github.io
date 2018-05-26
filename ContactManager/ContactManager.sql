CREATE DATABASE ContactManager;
 
CREATE TABLE Contacts (
	ID int IDENTITY(1,1) PRIMARY KEY,    
    FirstName varchar(255) NOT NULL,
	LastName varchar(255) NOT NULL,
    Email varchar(255) NOT NULL,
	PhoneNumber varchar(20) NOT NULL,
	ContactStatus varchar(30) NOT NULL
);


INSERT INTO ContactManager.dbo.Contacts VALUES ('Rahil', 'Shaikh','srahil01@gmail.com','9503419330','Active');

