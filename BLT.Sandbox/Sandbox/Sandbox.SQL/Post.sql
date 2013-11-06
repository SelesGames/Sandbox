CREATE TABLE [dbo].[Post]
(
    [Id] INT NOT NULL PRIMARY KEY NONCLUSTERED IDENTITY,
    [PostId] UNIQUEIDENTIFIER NOT NULL,
    [ClientId] UNIQUEIDENTIFIER NOT NULL,
    [ProjectId] UNIQUEIDENTIFIER NOT NULL,
    [CategoryId] UNIQUEIDENTIFIER NOT NULL,
    --[ClientName] NVARCHAR(MAX) NOT NULL, only if we want to denormalize a bit
    --[ProjectName] NVARCHAR(MAX) NOT NULL, only if we want to denormalize a bit
    [Name] NVARCHAR(MAX) NOT NULL, 
    [FileCount] INT NOT NULL, 
    [PostedOn] DATETIME NULL, 
)

GO
CREATE UNIQUE INDEX [KeyIndex] ON [dbo].[Post] ([PostId])
GO
CREATE CLUSTERED INDEX [ClusteredClientIndex] ON [dbo].[Post] ([ClientId],[Id])
GO
CREATE INDEX [ProjectIndex] ON [dbo].[Post] ([ProjectId])
GO
CREATE INDEX [CategoryIndex] ON [dbo].[Post] ([CategoryId])