USE [IPDetailsDB]
GO

/****** Object: Table [dbo].[IPDetails] Script Date: 9/11/2019 9:51:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IPDetails] (
    [Ip]        VARCHAR (50)  NOT NULL,
    [City]      NVARCHAR (50) NULL,
    [Country]   NVARCHAR (50) NULL,
    [Latitude]  VARCHAR (50)  NULL,
    [Longitude] VARCHAR (50)  NULL,
    [Continent] VARCHAR (50)  NULL
);


