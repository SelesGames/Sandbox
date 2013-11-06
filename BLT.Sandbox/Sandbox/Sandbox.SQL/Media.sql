CREATE TABLE [dbo].[PostMediaItem]
(
    [Id] INT NOT NULL PRIMARY KEY NONCLUSTERED IDENTITY,
    [MediaId] UNIQUEIDENTIFIER NOT NULL,
    [PostId] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(MAX) NOT NULL, 
    [MediaItemUrl] NVARCHAR(MAX) NOT NULL,
    [ContentType] NVARCHAR(MAX) NOT NULL,
    [ContentLength] INT NOT NULL
)

GO
CREATE UNIQUE INDEX [KeyIndex] ON [dbo].[PostMediaItem] ([PostMediaItemId])
GO
CREATE CLUSTERED INDEX [ClusteredPostIndex] ON [dbo].[PostMediaItem] ([PostId],[Id])