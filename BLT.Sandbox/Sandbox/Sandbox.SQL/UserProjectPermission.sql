CREATE TABLE [dbo].[UserProjectPermission]
(
    [Id] INT NOT NULL PRIMARY KEY NONCLUSTERED IDENTITY,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [ProjectId] UNIQUEIDENTIFIER NOT NULL
)

GO
CREATE CLUSTERED INDEX [ClusteredUserIndex] ON [dbo].[UserProjectPermission] ([UserId],[Id])
GO
CREATE INDEX [ProjectIndex] ON [dbo].[UserProjectPermission] ([ProjectId])