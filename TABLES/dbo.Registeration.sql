﻿CREATE TABLE [dbo].[REGISTERATION]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [NAME] VARCHAR(30) NOT NULL, 
    [PASSWORD] VARCHAR(10) NOT NULL, 
    [SECURITY QUESTION] INT NOT NULL, 
    [SECURITY ANSWER] VARCHAR(10) NOT NULL, 
    [CATEGORY] VARCHAR(10) NOT NULL, 
    [PHONE NUMBER] VARCHAR(13) NOT NULL, 
    [DATE OF BIRTH] DATE NOT NULL, 
    [PIN CODE] CHAR(6) NOT NULL
)