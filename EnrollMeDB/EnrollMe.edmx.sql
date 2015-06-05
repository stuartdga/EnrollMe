
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/05/2015 10:04:26
-- Generated from EDMX file: C:\Projects\EnrollMe\EnrollMeDB\EnrollMe.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EnrollMe];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ClassesInstructors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Classes] DROP CONSTRAINT [FK_ClassesInstructors];
GO
IF OBJECT_ID(N'[dbo].[FK_ClassesStudents]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_ClassesStudents];
GO
IF OBJECT_ID(N'[dbo].[FK_OrganizationsClasses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Classes] DROP CONSTRAINT [FK_OrganizationsClasses];
GO
IF OBJECT_ID(N'[dbo].[FK_OrganizationsInstructors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Instructors] DROP CONSTRAINT [FK_OrganizationsInstructors];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Classes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Classes];
GO
IF OBJECT_ID(N'[dbo].[Instructors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Instructors];
GO
IF OBJECT_ID(N'[dbo].[Organizations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Organizations];
GO
IF OBJECT_ID(N'[dbo].[Students]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Students];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Classes'
CREATE TABLE [dbo].[Classes] (
    [ClassId] int IDENTITY(1,1) NOT NULL,
    [ClassName] nvarchar(max)  NOT NULL,
    [DayOfClass] nvarchar(max)  NOT NULL,
    [TimeOfClass] nvarchar(max)  NOT NULL,
    [Location] nvarchar(max)  NOT NULL,
    [InstructorId] int  NOT NULL,
    [Organization] varchar(900)  NOT NULL
);
GO

-- Creating table 'Instructors'
CREATE TABLE [dbo].[Instructors] (
    [InstructorId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [MiddleName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Organization] varchar(900)  NOT NULL
);
GO

-- Creating table 'Organizations'
CREATE TABLE [dbo].[Organizations] (
    [Organization] varchar(900)  NOT NULL
);
GO

-- Creating table 'Students'
CREATE TABLE [dbo].[Students] (
    [StudentId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [MiddleName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [ClassesId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ClassId] in table 'Classes'
ALTER TABLE [dbo].[Classes]
ADD CONSTRAINT [PK_Classes]
    PRIMARY KEY CLUSTERED ([ClassId] ASC);
GO

-- Creating primary key on [InstructorId] in table 'Instructors'
ALTER TABLE [dbo].[Instructors]
ADD CONSTRAINT [PK_Instructors]
    PRIMARY KEY CLUSTERED ([InstructorId] ASC);
GO

-- Creating primary key on [Organization] in table 'Organizations'
ALTER TABLE [dbo].[Organizations]
ADD CONSTRAINT [PK_Organizations]
    PRIMARY KEY CLUSTERED ([Organization] ASC);
GO

-- Creating primary key on [StudentId] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [PK_Students]
    PRIMARY KEY CLUSTERED ([StudentId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [InstructorId] in table 'Classes'
ALTER TABLE [dbo].[Classes]
ADD CONSTRAINT [FK_ClassesInstructors]
    FOREIGN KEY ([InstructorId])
    REFERENCES [dbo].[Instructors]
        ([InstructorId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClassesInstructors'
CREATE INDEX [IX_FK_ClassesInstructors]
ON [dbo].[Classes]
    ([InstructorId]);
GO

-- Creating foreign key on [ClassesId] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [FK_ClassesStudents]
    FOREIGN KEY ([ClassesId])
    REFERENCES [dbo].[Classes]
        ([ClassId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClassesStudents'
CREATE INDEX [IX_FK_ClassesStudents]
ON [dbo].[Students]
    ([ClassesId]);
GO

-- Creating foreign key on [Organization] in table 'Classes'
ALTER TABLE [dbo].[Classes]
ADD CONSTRAINT [FK_OrganizationsClasses]
    FOREIGN KEY ([Organization])
    REFERENCES [dbo].[Organizations]
        ([Organization])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrganizationsClasses'
CREATE INDEX [IX_FK_OrganizationsClasses]
ON [dbo].[Classes]
    ([Organization]);
GO

-- Creating foreign key on [Organization] in table 'Instructors'
ALTER TABLE [dbo].[Instructors]
ADD CONSTRAINT [FK_OrganizationsInstructors]
    FOREIGN KEY ([Organization])
    REFERENCES [dbo].[Organizations]
        ([Organization])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrganizationsInstructors'
CREATE INDEX [IX_FK_OrganizationsInstructors]
ON [dbo].[Instructors]
    ([Organization]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------