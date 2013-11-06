CREATE TABLE [dbo].[Content]
(
    [SG_KEY] INT NOT NULL PRIMARY KEY NONCLUSTERED IDENTITY,
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [RoundId] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(MAX) NOT NULL, 
    [ContentUrl] NVARCHAR(MAX) NOT NULL,
    [ContentType] NVARCHAR(MAX) NOT NULL,
    [ContentLength] INT NOT NULL
)

GO
CREATE UNIQUE INDEX [KeyIndex] ON [dbo].[Content] ([Id])
GO
CREATE CLUSTERED INDEX [ClusteredPostIndex] ON [dbo].[Content] ([RoundId],[SG_KEY])