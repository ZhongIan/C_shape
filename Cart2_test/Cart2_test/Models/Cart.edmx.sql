
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/16/2019 17:32:14
-- Generated from EDMX file: D:\Program Files\infomation\MVC\TEST\Cart_Test\Cart2_test\Cart2_test\Models\Cart.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Cart];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderOrderDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetail] DROP CONSTRAINT [FK_OrderOrderDetail];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[OrderDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderDetail];
GO
IF OBJECT_ID(N'[dbo].[Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [ImageUrl] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrderDetail'
CREATE TABLE [dbo].[OrderDetail] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [Quantity] int  NOT NULL
);
GO

-- Creating table 'Order'
CREATE TABLE [dbo].[Order] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [RecieverName] nvarchar(max)  NOT NULL,
    [RecieverPhone] nvarchar(max)  NOT NULL,
    [RecieverAddress] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProductComment'
CREATE TABLE [dbo].[ProductComment] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [CreateDate] nvarchar(max)  NOT NULL,
    [ProductId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderDetail'
ALTER TABLE [dbo].[OrderDetail]
ADD CONSTRAINT [PK_OrderDetail]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Order'
ALTER TABLE [dbo].[Order]
ADD CONSTRAINT [PK_Order]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductComment'
ALTER TABLE [dbo].[ProductComment]
ADD CONSTRAINT [PK_ProductComment]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [OrderId] in table 'OrderDetail'
ALTER TABLE [dbo].[OrderDetail]
ADD CONSTRAINT [FK_OrderOrderDetail]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[Order]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderDetail'
CREATE INDEX [IX_FK_OrderOrderDetail]
ON [dbo].[OrderDetail]
    ([OrderId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------