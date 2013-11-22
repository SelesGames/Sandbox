CREATE TABLE [dbo].[Campaign]
(
    [SG_KEY] INT NOT NULL PRIMARY KEY NONCLUSTERED IDENTITY,
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [GroupId] UNIQUEIDENTIFIER NOT NULL,
    --[GroupName] NVARCHAR(MAX) NOT NULL, only if we want to denormalize a bit
    [Name] NVARCHAR(MAX) NOT NULL, 
    [ProjectCount] INT NOT NULL, 
    [LatestProjectId] UNIQUEIDENTIFIER, 
    [LatestProjectTime] DATETIME, 
    [LatestProjectName] NVARCHAR(MAX)
)

GO
CREATE UNIQUE INDEX [KeyIndex] ON [dbo].[Campaign] ([Id])
GO
CREATE CLUSTERED INDEX [ClusteredGroupIndex] ON [dbo].[Campaign] ([GroupId],[SG_KEY])