CREATE TABLE [dbo].[users]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [username] VARCHAR(250) NOT NULL, 
    [password] VARCHAR(250) NOT NULL, 
    [lastlogin] DATETIME NULL, 
    [firstname] VARCHAR(50) NULL, 
    [lastname] VARCHAR(50) NULL
)

GO


CREATE UNIQUE INDEX [IX_Username] ON [dbo].[users] ([username])
