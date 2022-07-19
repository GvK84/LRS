IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'LRS_DB')
    BEGIN
        CREATE DATABASE [LRS_DB]
    END
GO

USE [LRS_DB]
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserType')
    BEGIN
        CREATE TABLE [dbo].[UserType] ([Id] INT IDENTITY (1, 1) NOT NULL,
                                       [Description] NVARCHAR (20) NOT NULL,
                                       [Code] NCHAR (2) NOT NULL,
                                       CONSTRAINT [PK_UserTypeId] PRIMARY KEY CLUSTERED ([Id] ASC));
    END
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserTitle')
    BEGIN
        CREATE TABLE [dbo].[UserTitle] ([Id] INT IDENTITY (1, 1) NOT NULL,
                                        [Description] NVARCHAR (20) NOT NULL,
                                        CONSTRAINT [PK_UserTitleId] PRIMARY KEY CLUSTERED ([Id] ASC));
    END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='User')
    BEGIN
        CREATE TABLE [dbo].[User] ([Id] INT IDENTITY (1, 1) NOT NULL,
                                   [Name] NVARCHAR (20) NULL,
                                   [Surname] NVARCHAR (20) NULL, 
                                   [BirthDate] DATE NULL,
                                   [UserTypeId] INT NOT NULL,
                                   [UserTitleId] INT NOT NULL,
                                   [EmailAddress] NVARCHAR (50) NULL,
                                   [IsActive] BIT NULL,
                                   CONSTRAINT [PK_UserId] PRIMARY KEY CLUSTERED ([Id] ASC),
                                   CONSTRAINT [FK_User_ToUserTitle] FOREIGN KEY ([UserTitleId]) 
                                              REFERENCES [dbo].[UserTitle] ([Id]),
                                   CONSTRAINT [FK_User_ToUserType] FOREIGN KEY ([UserTypeId]) 
                                              REFERENCES [dbo].[UserType] ([Id]));
    END