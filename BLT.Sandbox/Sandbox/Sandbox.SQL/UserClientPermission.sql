CREATE TABLE [dbo].[UserClientPermission]
(
    [Id] INT NOT NULL PRIMARY KEY NONCLUSTERED IDENTITY,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [ClientId] UNIQUEIDENTIFIER NOT NULL
)

GO
CREATE CLUSTERED INDEX [ClusteredUserIndex] ON [dbo].[UserClientPermission] ([UserId],[Id])
GO
CREATE INDEX [ClientIndex] ON [dbo].[UserClientPermission] ([ClientId])