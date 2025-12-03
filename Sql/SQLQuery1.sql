CREATE DATABASE [TomadaStore.CustomerDB]
GO
USE [TomadaStore.CustomerDB]
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL UNIQUE,
	[PhoneNumber] [nvarchar](15) NULL
	)

	SELECT * FROM Customers