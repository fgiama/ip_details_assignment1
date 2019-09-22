﻿USE [master]
GO

/****** Object:  Database [IPDetailsDB]    Script Date: 9/21/2019 8:35:20 PM ******/
DROP DATABASE [IPDetailsDB]
GO

/****** Object:  Database [IPDetailsDB]    Script Date: 9/21/2019 8:35:20 PM ******/
CREATE DATABASE [IPDetailsDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IPDetailsDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\IPDetailsDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IPDetailsDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\IPDetailsDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IPDetailsDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [IPDetailsDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [IPDetailsDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [IPDetailsDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [IPDetailsDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [IPDetailsDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [IPDetailsDB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [IPDetailsDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [IPDetailsDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [IPDetailsDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [IPDetailsDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [IPDetailsDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [IPDetailsDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [IPDetailsDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [IPDetailsDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [IPDetailsDB] SET  DISABLE_BROKER 
GO

ALTER DATABASE [IPDetailsDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [IPDetailsDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [IPDetailsDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [IPDetailsDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [IPDetailsDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [IPDetailsDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [IPDetailsDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [IPDetailsDB] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [IPDetailsDB] SET  MULTI_USER 
GO

ALTER DATABASE [IPDetailsDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [IPDetailsDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [IPDetailsDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [IPDetailsDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [IPDetailsDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [IPDetailsDB] SET QUERY_STORE = OFF
GO

ALTER DATABASE [IPDetailsDB] SET  READ_WRITE 
GO

