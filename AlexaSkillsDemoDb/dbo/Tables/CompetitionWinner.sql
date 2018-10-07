CREATE TABLE [dbo].[CompetitionWinner]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Tweet] NVARCHAR(MAX) NOT NULL, 
	[ProfileImageUrl] NVARCHAR(MAX) NULL,
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL DEFAULT GETDATE()
)
