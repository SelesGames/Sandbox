--USER FKs
ALTER TABLE [dbo].[User] ADD CONSTRAINT [FK_dbo.User_dbo.Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([Id]) ON DELETE CASCADE

--Group FKs
--ALTER TABLE [dbo].[Group] ADD CONSTRAINT [FK_dbo.Group_dbo.Project_LatestProject_Id] FOREIGN KEY ([LatestProject_Id]) REFERENCES [dbo].[Project] ([Id])

--CAMPAIGN FKs
ALTER TABLE [dbo].[Campaign] ADD CONSTRAINT [FK_dbo.Campaign_dbo.Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([Id]) ON DELETE CASCADE

--PROJECT FKs
ALTER TABLE [dbo].[Project] ADD CONSTRAINT [FK_dbo.Project_dbo.Campaign_CampaignId] FOREIGN KEY ([CampaignId]) REFERENCES [dbo].[Campaign] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Project] ADD CONSTRAINT [FK_dbo.Project_dbo.Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([Id])

--ROUND FKs
ALTER TABLE [dbo].[Round] ADD CONSTRAINT [FK_dbo.Round_dbo.Project_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]) ON DELETE CASCADE

--CONTENT FKs
ALTER TABLE [dbo].[Content] ADD CONSTRAINT [FK_dbo.Content_dbo.Round_RoundId] FOREIGN KEY ([RoundId]) REFERENCES [dbo].[Round] ([Id]) ON DELETE CASCADE

--CAMPAIGN FKs
ALTER TABLE [dbo].[UserCampaignPermission] ADD CONSTRAINT [FK_dbo.UserCampaignPermission_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[UserCampaignPermission] ADD CONSTRAINT [FK_dbo.UserCampaignPermission_dbo.Campaign_CampaignId] FOREIGN KEY ([CampaignId]) REFERENCES [dbo].[Campaign] ([Id]) ON DELETE CASCADE


