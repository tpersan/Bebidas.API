CREATE TABLE [dbo].[Cervejas]
(
	[Id] INT  IDENTITY (1, 1) NOT NULL, 
    [cerveja] VARCHAR (50) NOT NULL,
    [dados] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Cerveja_Key] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [Record should be formatted as JSON dados ] CHECK (isjson([dados])=(1))
)
