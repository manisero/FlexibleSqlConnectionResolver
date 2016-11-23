CREATE TABLE [dbo].[Task] (
    [TaskId]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (100) NOT NULL CONSTRAINT [UQ_Task_Name] UNIQUE,
	[IsComplete] BIT            NOT NULL CONSTRAINT [DF_Task_IsComplete] DEFAULT(0),
    CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([TaskId] ASC)
);
