CREATE TABLE [dbo].[Lancamentos]
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
    Valor   DECIMAL(18,2) NOT NULL,
    Tipo    NVARCHAR(10) CHECK (Tipo IN ('Credito', 'Debito')) NOT NULL,
    DataLancamento DATETIME DEFAULT GETDATE()
);
