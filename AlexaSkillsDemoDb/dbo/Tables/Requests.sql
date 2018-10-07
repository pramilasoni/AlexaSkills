CREATE TABLE [dbo].[Requests] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [MemberId]    INT           NOT NULL,
    [SessionId]   VARCHAR (300) NOT NULL,
    [AppId]       VARCHAR (300) NOT NULL,
    [RequestId]   VARCHAR (300) NOT NULL,
    [UserId]      VARCHAR (300) NOT NULL,
    [TimeStamp]   DATETIME      NOT NULL,
    [Intent]      VARCHAR (50)  NULL,
    [Slots]       VARCHAR (50)  NULL,
    [IsNew]       BIT           NOT NULL,
    [Version]     VARCHAR (5)   NOT NULL,
    [Type]        VARCHAR (50)  NOT NULL,
    [Reason]      VARCHAR (50)  NULL,
    [DateCreated] DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Members_Requests] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Members] ([id])
);

