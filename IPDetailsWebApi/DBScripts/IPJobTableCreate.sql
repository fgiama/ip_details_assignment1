USE [IPDetailsDB]
GO

/****** Object: Table [dbo].[IPJob] Script Date: 9/11/2019 9:53:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IPJob] (
    [Id]         VARCHAR (50) NOT NULL,
    [StartedOn]  DATETIME     NOT NULL,
    [FinishedOn] DATETIME     NULL,
    [Status]     INT          NOT NULL,
    [Progress]   INT          NOT NULL,
    [Total]      INT          NOT NULL
);


