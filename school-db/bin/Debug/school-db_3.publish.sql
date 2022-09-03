﻿/*
Deployment script for brickbooks

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "brickbooks"
:setvar DefaultFilePrefix "brickbooks"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The type for column Id in table [dbo].[users] is currently  BIGINT NOT NULL but is being changed to  INT NOT NULL. Data loss could occur and deployment may fail if the column contains data that is incompatible with type  INT NOT NULL.
*/

IF EXISTS (select top 1 1 from [dbo].[users])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Starting rebuilding table [dbo].[users]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_users] (
    [Id]        INT           NOT NULL,
    [username]  VARCHAR (250) NOT NULL,
    [password]  VARCHAR (250) NOT NULL,
    [lastlogin] DATETIME      NULL,
    [firstname] VARCHAR (50)  NULL,
    [lastname]  VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[users])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_users] ([Id], [username], [password], [lastlogin], [firstname], [lastname])
        SELECT   [Id],
                 [username],
                 [password],
                 [lastlogin],
                 [firstname],
                 [lastname]
        FROM     [dbo].[users]
        ORDER BY [Id] ASC;
    END

DROP TABLE [dbo].[users];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_users]', N'users';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating Index [dbo].[users].[IX_Username]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Username]
    ON [dbo].[users]([username] ASC);


GO
PRINT N'Update complete.';


GO