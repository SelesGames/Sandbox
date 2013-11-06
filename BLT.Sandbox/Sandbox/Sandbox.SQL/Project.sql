CREATE TABLE [dbo].[Project]
(
    [Id] INT NOT NULL PRIMARY KEY NONCLUSTERED IDENTITY,
    [ProjectId] UNIQUEIDENTIFIER NOT NULL,
    [ClientId] UNIQUEIDENTIFIER NOT NULL,
    --[ClientName] NVARCHAR(MAX) NOT NULL, only if we want to denormalize a bit
    [Name] NVARCHAR(MAX) NOT NULL, 
    [PostCount] INT NOT NULL, 
    [LatestPostId] UNIQUEIDENTIFIER NULL, 
    [LatestPostTime] DATETIME NULL, 
    [LatestPostName] NVARCHAR(MAX) NULL
)

GO
CREATE UNIQUE INDEX [KeyIndex] ON [dbo].[Project] ([ProjectId])
GO
CREATE CLUSTERED INDEX [ClusteredClientIndex] ON [dbo].[Project] ([ClientId],[Id])