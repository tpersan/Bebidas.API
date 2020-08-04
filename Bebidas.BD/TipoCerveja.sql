CREATE TABLE [dbo].[TipoCerveja]
(
	[Id] INT  IDENTITY (1, 1) NOT NULL, 
    [Sigla] VARCHAR (50) NOT NULL,
    [Descricao] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TipoCerveja_Key] PRIMARY KEY NONCLUSTERED ([Id] ASC)
)
