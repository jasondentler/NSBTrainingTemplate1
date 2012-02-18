USE [master]
DROP DATABASE HospitalDomain
DROP DATABASE HospitalReadModelWeb
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
/****** Object:  Table [dbo].[Patients]    Script Date: 02/16/2012 07:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[PatientId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[BedAssignment] [int] NULL,
	[IsAdmitted] [bit] NOT NULL,
	[IsDischarged] [bit] NOT NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Patients] ([PatientId], [FirstName], [LastName], [BedAssignment], [IsAdmitted], [IsDischarged]) VALUES (N'e9784a33-a8dc-42e7-abc0-bf7b99f0939e', N'Wesley', N'Crusher', 2, 1, 0)
INSERT [dbo].[Patients] ([PatientId], [FirstName], [LastName], [BedAssignment], [IsAdmitted], [IsDischarged]) VALUES (N'7444fd8c-acd4-45e6-a3f4-ebcb399a4b48', N'Geordi', N'La Forge', NULL, 1, 0)
INSERT [dbo].[Patients] ([PatientId], [FirstName], [LastName], [BedAssignment], [IsAdmitted], [IsDischarged]) VALUES (N'c62efc87-0934-432c-be9a-c7eb91f0cd32', N'Red', N'Shirt', NULL, 1, 1)
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
GO
USE [master]
GO
/****** Object:  Database [HospitalDomain]    Script Date: 02/16/2012 19:43:35 ******/
CREATE DATABASE [HospitalDomain] 
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'HospitalDomain', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HospitalDomain].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HospitalDomain] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [HospitalDomain] SET ANSI_NULLS OFF
GO
ALTER DATABASE [HospitalDomain] SET ANSI_PADDING OFF
GO
ALTER DATABASE [HospitalDomain] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [HospitalDomain] SET ARITHABORT OFF
GO
ALTER DATABASE [HospitalDomain] SET AUTO_CLOSE ON
GO
ALTER DATABASE [HospitalDomain] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [HospitalDomain] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [HospitalDomain] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [HospitalDomain] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [HospitalDomain] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [HospitalDomain] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [HospitalDomain] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [HospitalDomain] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [HospitalDomain] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [HospitalDomain] SET  ENABLE_BROKER
GO
ALTER DATABASE [HospitalDomain] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [HospitalDomain] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [HospitalDomain] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [HospitalDomain] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [HospitalDomain] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [HospitalDomain] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [HospitalDomain] SET  READ_WRITE
GO
ALTER DATABASE [HospitalDomain] SET RECOVERY SIMPLE
GO
ALTER DATABASE [HospitalDomain] SET  MULTI_USER
GO
ALTER DATABASE [HospitalDomain] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [HospitalDomain] SET DB_CHAINING OFF
GO
USE [HospitalDomain]
GO
/****** Object:  Table [dbo].[Snapshots]    Script Date: 02/16/2012 19:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Snapshots](
	[StreamId] [uniqueidentifier] NOT NULL,
	[StreamRevision] [int] NOT NULL,
	[Payload] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Snapshots] PRIMARY KEY CLUSTERED 
(
	[StreamId] ASC,
	[StreamRevision] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Commits]    Script Date: 02/16/2012 19:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Commits](
	[StreamId] [uniqueidentifier] NOT NULL,
	[StreamRevision] [int] NOT NULL,
	[Items] [tinyint] NOT NULL,
	[CommitId] [uniqueidentifier] NOT NULL,
	[CommitSequence] [int] NOT NULL,
	[CommitStamp] [datetime] NOT NULL,
	[Dispatched] [bit] NOT NULL,
	[Headers] [varbinary](max) NULL,
	[Payload] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Commits] PRIMARY KEY CLUSTERED 
(
	[StreamId] ASC,
	[CommitSequence] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF__Commits__Dispatc__7F60ED59]    Script Date: 02/16/2012 19:43:36 ******/
ALTER TABLE [dbo].[Commits] ADD  DEFAULT ((0)) FOR [Dispatched]
GO
/****** Object:  Check [CK__Snapshots__Paylo__060DEAE8]    Script Date: 02/16/2012 19:43:36 ******/
ALTER TABLE [dbo].[Snapshots]  WITH CHECK ADD CHECK  ((datalength([Payload])>(0)))
GO
/****** Object:  Check [CK__Snapshots__Strea__07020F21]    Script Date: 02/16/2012 19:43:36 ******/
ALTER TABLE [dbo].[Snapshots]  WITH CHECK ADD CHECK  (([StreamRevision]>(0)))
GO
/****** Object:  Check [CK__Commits__CommitI__00551192]    Script Date: 02/16/2012 19:43:36 ******/
ALTER TABLE [dbo].[Commits]  WITH CHECK ADD CHECK  (([CommitId]<>0x00))
GO
/****** Object:  Check [CK__Commits__CommitS__014935CB]    Script Date: 02/16/2012 19:43:36 ******/
ALTER TABLE [dbo].[Commits]  WITH CHECK ADD CHECK  (([CommitSequence]>(0)))
GO
/****** Object:  Check [CK__Commits__Headers__023D5A04]    Script Date: 02/16/2012 19:43:36 ******/
ALTER TABLE [dbo].[Commits]  WITH CHECK ADD CHECK  (([Headers] IS NULL OR datalength([Headers])>(0)))
GO
/****** Object:  Check [CK__Commits__Items__03317E3D]    Script Date: 02/16/2012 19:43:36 ******/
ALTER TABLE [dbo].[Commits]  WITH CHECK ADD CHECK  (([Items]>(0)))
GO
/****** Object:  Check [CK__Commits__Payload__0425A276]    Script Date: 02/16/2012 19:43:36 ******/
ALTER TABLE [dbo].[Commits]  WITH CHECK ADD CHECK  ((datalength([Payload])>(0)))
GO
/****** Object:  Check [CK__Commits__StreamR__0519C6AF]    Script Date: 02/16/2012 19:43:36 ******/
ALTER TABLE [dbo].[Commits]  WITH CHECK ADD CHECK  (([StreamRevision]>(0)))
GO
