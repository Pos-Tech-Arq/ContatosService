IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ContatosDb')
BEGIN
    CREATE DATABASE ContatosDb;
END
GO