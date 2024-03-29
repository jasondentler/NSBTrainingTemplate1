USE [master]
GO
/****** Object:  Database [HospitalReadModelWeb]    Script Date: 02/16/2012 07:11:11 ******/
CREATE DATABASE [HospitalReadModelWeb] 
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'HospitalReadModelWeb', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HospitalReadModelWeb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HospitalReadModelWeb] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET ANSI_NULLS OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET ANSI_PADDING OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET ARITHABORT OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [HospitalReadModelWeb] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [HospitalReadModelWeb] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [HospitalReadModelWeb] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET  DISABLE_BROKER
GO
ALTER DATABASE [HospitalReadModelWeb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [HospitalReadModelWeb] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [HospitalReadModelWeb] SET  READ_WRITE
GO
ALTER DATABASE [HospitalReadModelWeb] SET RECOVERY SIMPLE
GO
ALTER DATABASE [HospitalReadModelWeb] SET  MULTI_USER
GO
ALTER DATABASE [HospitalReadModelWeb] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [HospitalReadModelWeb] SET DB_CHAINING OFF
GO
USE [HospitalReadModelWeb]
GO
/****** Object:  Table [dbo].[PatientDetails]    Script Date: 02/16/2012 07:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientDetails](
	[PatientId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[BedAssignment] [int] NULL,
	[Admitted] [datetime] NULL,
	[Discharged] [datetime] NULL,
	[CanAdmit] [bit] NOT NULL,
	[CanAssignBed] [bit] NOT NULL,
	[CanDischarge] [bit] NOT NULL
) ON [PRIMARY]
GO
INSERT [dbo].[PatientDetails] ([PatientId], [FirstName], [LastName], [BedAssignment], [Admitted], [Discharged], [CanAdmit], [CanAssignBed], [CanDischarge]) VALUES (N'e9784a33-a8dc-42e7-abc0-bf7b99f0939e', N'Wesley', N'Crusher', 2, CAST(0x00009FF700C1A250 AS DateTime), NULL, 0, 1, 1)
INSERT [dbo].[PatientDetails] ([PatientId], [FirstName], [LastName], [BedAssignment], [Admitted], [Discharged], [CanAdmit], [CanAssignBed], [CanDischarge]) VALUES (N'7444fd8c-acd4-45e6-a3f4-ebcb399a4b48', N'Geordi', N'La Forge', NULL, CAST(0x00009FF700C1A250 AS DateTime), NULL, 0, 1, 1)
INSERT [dbo].[PatientDetails] ([PatientId], [FirstName], [LastName], [BedAssignment], [Admitted], [Discharged], [CanAdmit], [CanAssignBed], [CanDischarge]) VALUES (N'c62efc87-0934-432c-be9a-c7eb91f0cd32', N'Red', N'Shirt', NULL, CAST(0x00009FF700C1A250 AS DateTime), CAST(0x00009FF700C72090 AS DateTime), 0, 0, 0)
/****** Object:  Table [dbo].[AvailableBeds]    Script Date: 02/16/2012 07:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AvailableBeds](
	[Bed] [int] NOT NULL,
 CONSTRAINT [PK_AvailableBeds] PRIMARY KEY CLUSTERED 
(
	[Bed] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[AvailableBeds] ([Bed]) VALUES (1)
INSERT [dbo].[AvailableBeds] ([Bed]) VALUES (3)
/****** Object:  Table [dbo].[Stats]    Script Date: 02/16/2012 07:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stats](
	[AdmittedPatients] [int] NOT NULL,
	[AvailableBeds] [int] NOT NULL,
	[WaitingForBeds] [int] NOT NULL
) ON [PRIMARY]
GO
INSERT [dbo].[Stats] ([AdmittedPatients], [AvailableBeds], [WaitingForBeds]) VALUES (2, 2, 1)
/****** Object:  Table [dbo].[AdmittedPatients]    Script Date: 02/16/2012 07:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdmittedPatients](
	[PatientId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[BedAssignment] [int] NULL,
 CONSTRAINT [PK_AdmittedPatients] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[AdmittedPatients] ([PatientId], [FirstName], [LastName], [BedAssignment]) VALUES (N'e9784a33-a8dc-42e7-abc0-bf7b99f0939e', N'Wesley', N'Crusher', 2)
INSERT [dbo].[AdmittedPatients] ([PatientId], [FirstName], [LastName], [BedAssignment]) VALUES (N'7444fd8c-acd4-45e6-a3f4-ebcb399a4b48', N'Geordi', N'La Forge', NULL)
/****** Object:  Table [dbo].[WaitingForBeds]    Script Date: 02/16/2012 07:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaitingForBeds](
	[PatientId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
 CONSTRAINT [PK_WaitingForBeds] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[WaitingForBeds] ([PatientId], [FirstName], [LastName]) VALUES (N'7444fd8c-acd4-45e6-a3f4-ebcb399a4b48', N'Geordi', N'La Forge')
/****** Object:  Default [DF_AdmittedPatients_PatientId]    Script Date: 02/16/2012 07:11:12 ******/
ALTER TABLE [dbo].[AdmittedPatients] ADD  CONSTRAINT [DF_AdmittedPatients_PatientId]  DEFAULT (newid()) FOR [PatientId]
GO
