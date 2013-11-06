CREATE TABLE [dbo].[Client]
(
    [SG_KEY] INT NOT NULL PRIMARY KEY CLUSTERED IDENTITY,
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(MAX) NOT NULL, 
    [ProjectCount] INT NOT NULL, 
    [LatestProjectId] UNIQUEIDENTIFIER, 
    [LatestProjectTime] DATETIME, 
    [LatestProjectName] NVARCHAR(MAX)
)

GO
CREATE UNIQUE INDEX [KeyIndex] ON [dbo].[Client] ([Id])
