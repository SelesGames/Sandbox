CREATE TABLE [dbo].[Project]
(
    [SG_KEY] INT NOT NULL PRIMARY KEY NONCLUSTERED IDENTITY,
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [GroupId] UNIQUEIDENTIFIER NOT NULL,
    [CampaignId] UNIQUEIDENTIFIER NOT NULL,
    --[GroupName] NVARCHAR(MAX) NOT NULL, only if we want to denormalize a bit
    --[CampaignName] NVARCHAR(MAX) NOT NULL, only if we want to denormalize a bit
    [Name] NVARCHAR(MAX) NOT NULL,
	[CoverImageUrl] NVARCHAR(MAX),
    [LatestRoundModified] DATETIME
)

GO
CREATE UNIQUE INDEX [KeyIndex] ON [dbo].[Project] ([Id])
GO
CREATE CLUSTERED INDEX [ClusteredGroupIndex] ON [dbo].[Project] ([GroupId],[SG_KEY])
GO
CREATE INDEX [CampaignIndex] ON [dbo].[Project] ([CampaignId])