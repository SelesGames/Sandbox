CREATE TABLE [dbo].[Client]
(
    [Id] INT NOT NULL PRIMARY KEY CLUSTERED IDENTITY,
    [ClientId] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(MAX) NOT NULL, 
    [ProjectCount] INT NOT NULL, 
    [LatestPostId] UNIQUEIDENTIFIER NULL, 
    [LatestPostTime] DATETIME NULL, 
    [LatestPostName] NVARCHAR(MAX) NULL
)

GO
CREATE UNIQUE INDEX [KeyIndex] ON [dbo].[Client] ([ClientId])
