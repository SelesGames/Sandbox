CREATE TABLE [dbo].[Category]
(
    [Id] INT NOT NULL PRIMARY KEY CLUSTERED IDENTITY,
    [CategoryId] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(MAX) NOT NULL, 
    [PostCount] INT NOT NULL
)

GO
CREATE UNIQUE INDEX [KeyIndex] ON [dbo].[Category] ([CategoryId])