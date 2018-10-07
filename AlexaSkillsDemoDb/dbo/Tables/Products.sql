CREATE TABLE [dbo].[Products] (
    [id]          INT          IDENTITY (1, 1) NOT NULL,
    [ProductName] VARCHAR (50) NOT NULL,
    [Price]       DECIMAL (18) NOT NULL,
    [Description] TEXT         NOT NULL,
    [Votes]       INT          NOT NULL,
    [CreatedDate] DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

