CREATE TABLE [dbo].[Project]
(
    [SG_KEY] INT NOT NULL PRIMARY KEY NONCLUSTERED IDENTITY,
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [ClientId] UNIQUEIDENTIFIER NOT NULL,
    [CampaignId] UNIQUEIDENTIFIER NOT NULL,
    [CategoryId] UNIQUEIDENTIFIER NOT NULL,
    --[ClientName] NVARCHAR(MAX) NOT NULL, only if we want to denormalize a bit
    --[CampaignName] NVARCHAR(MAX) NOT NULL, only if we want to denormalize a bit
    [Name] NVARCHAR(MAX) NOT NULL, 
    [LatestRoundModified] DATETIME
)

GO
CREATE UNIQUE INDEX [KeyIndex] ON [dbo].[Project] ([Id])
GO
CREATE CLUSTERED INDEX [ClusteredClientIndex] ON [dbo].[Project] ([ClientId],[SG_KEY])
GO
CREATE INDEX [ProjectIndex] ON [dbo].[Project] ([CampaignId])
GO
CREATE INDEX [CategoryIndex] ON [dbo].[Project] ([CategoryId])