DROP DATABASE HrisExample;
GO

CREATE DATABASE HrisExample;
GO

USE HrisExample;
GO

CREATE TABLE Employee (
    EmployeeKey int IDENTITY(1,1) NOT NULL PRIMARY KEY
    ,EmployeeId UNIQUEIDENTIFIER NOT NULL
    ,EmployeeName VARCHAR(max) not NULL
    ,Salary money not NULL
);
GO