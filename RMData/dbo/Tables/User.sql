﻿CREATE TABLE [dbo].[User]
(
    [Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [EmailAdress] NVARCHAR(50) NOT NULL, 
    [CreatedDate] DATE NOT NULL DEFAULT getutcdate()
)
