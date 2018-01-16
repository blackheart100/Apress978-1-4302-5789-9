
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/16/2018 18:13:36
-- Generated from EDMX file: D:\ThmoasHe\SPA\Repos\Apress978-1-4302-5789-9\Apress978-1-4302-5789-9\QueryEntityDataModel\Associate.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EF6Recipes];
GO
IF SCHEMA_ID(N'Chapter3') IS NULL EXECUTE(N'CREATE SCHEMA [Chapter3]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[Chapter3].[FK_AssociateSalaries_Associates]', 'F') IS NOT NULL
    ALTER TABLE [Chapter3].[AssociateSalaries] DROP CONSTRAINT [FK_AssociateSalaries_Associates];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[Chapter3].[Associates]', 'U') IS NOT NULL
    DROP TABLE [Chapter3].[Associates];
GO
IF OBJECT_ID(N'[Chapter3].[AssociateSalaries]', 'U') IS NOT NULL
    DROP TABLE [Chapter3].[AssociateSalaries];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AssociateSalaries'
CREATE TABLE [Chapter3].[AssociateSalaries] (
    [SalaryID] int IDENTITY(1,1) NOT NULL,
    [AssociateID] int  NOT NULL,
    [Salary] decimal(18,2)  NOT NULL,
    [SalaryDate] datetime  NOT NULL
);
GO

-- Creating table 'Associates'
CREATE TABLE [Chapter3].[Associates] (
    [AssociateID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [SalaryID] in table 'AssociateSalaries'
ALTER TABLE [Chapter3].[AssociateSalaries]
ADD CONSTRAINT [PK_AssociateSalaries]
    PRIMARY KEY CLUSTERED ([SalaryID] ASC);
GO

-- Creating primary key on [AssociateID] in table 'Associates'
ALTER TABLE [Chapter3].[Associates]
ADD CONSTRAINT [PK_Associates]
    PRIMARY KEY CLUSTERED ([AssociateID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AssociateID] in table 'AssociateSalaries'
ALTER TABLE [Chapter3].[AssociateSalaries]
ADD CONSTRAINT [FK_AssociateSalaries_Associates]
    FOREIGN KEY ([AssociateID])
    REFERENCES [Chapter3].[Associates]
        ([AssociateID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AssociateSalaries_Associates'
CREATE INDEX [IX_FK_AssociateSalaries_Associates]
ON [Chapter3].[AssociateSalaries]
    ([AssociateID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------