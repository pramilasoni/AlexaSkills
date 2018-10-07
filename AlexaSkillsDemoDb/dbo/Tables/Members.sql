CREATE TABLE [dbo].[Members] (
    [id]              INT           IDENTITY (1, 1) NOT NULL,
    [AlexaUserId]     VARCHAR (300) NOT NULL,
    [RequestCount]    INT           NOT NULL,
    [LastRequestDate] DATETIME      NOT NULL,
    [CreatedDate]     DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

