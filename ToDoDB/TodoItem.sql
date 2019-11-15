CREATE TABLE [dbo].[TodoItem]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Description] NCHAR(1000) NOT NULL, 
    [AddedAt] DATETIME NOT NULL, 
    [AddedBy] NVARCHAR(128) NOT NULL, 
    [WasDone] TINYINT NOT NULL, 
    [WasDoneAt] DATETIME NULL, 
    [DueDate] DATETIME NULL
)
