CREATE TABLE [dbo].[UserCampaignPermission]
(
    [Id] INT NOT NULL PRIMARY KEY NONCLUSTERED IDENTITY,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [CampaignId] UNIQUEIDENTIFIER NOT NULL
)

GO
CREATE CLUSTERED INDEX [ClusteredUserIndex] ON [dbo].[UserCampaignPermission] ([UserId],[Id])
GO
CREATE INDEX [CampaignIndex] ON [dbo].[UserCampaignPermission] ([CampaignId])