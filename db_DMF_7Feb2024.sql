USE [db_DMF]
GO
/****** Object:  Table [dbo].[AgenciesInfo]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AgenciesInfo](
	[AgencyID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[StateID] [int] NULL,
	[DistID] [int] NULL,
	[OwnerName] [nvarchar](200) NULL,
	[EmailID] [varchar](100) NULL,
	[MobileNo] [varchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[IsActive] [bit] NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[AgencyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BankMaster]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankMaster](
	[BankID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__BankMast__AA08CB33FCD89CE5] PRIMARY KEY CLUSTERED 
(
	[BankID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DepartmentMaster]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepartmentMaster](
	[DeptID] [int] IDENTITY(1,1) NOT NULL,
	[DeptCode] [varchar](10) NULL,
	[DeptName] [nvarchar](200) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[DeptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DesignationMaster]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DesignationMaster](
	[DesingantionID] [int] IDENTITY(1,1) NOT NULL,
	[DesignationCode] [varchar](10) NULL,
	[Designation] [nvarchar](200) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[DesingantionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DistrictMaster]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DistrictMaster](
	[DistrictId] [int] IDENTITY(1,1) NOT NULL,
	[DistrictName] [nvarchar](100) NULL,
	[DistrictCode] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[StateID] [int] NULL,
 CONSTRAINT [PK_DistrictMaster] PRIMARY KEY CLUSTERED 
(
	[DistrictId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeRegistration]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeRegistration](
	[RegistrationID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[FatherName] [nvarchar](200) NULL,
	[DOB] [date] NULL,
	[EmailID] [varchar](100) NULL,
	[MobileNo] [varchar](15) NULL,
	[PAN] [varchar](15) NULL,
	[DistrictID] [int] NULL,
	[BlockID] [int] NULL,
	[Address] [varchar](500) NULL,
	[Nationality] [varchar](50) NULL,
	[StatusID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[AllowEdit] [bit] NULL,
	[AdminRemark] [nvarchar](1000) NULL,
	[ApplicationNo] [varchar](50) NULL,
	[AadharNo] [varchar](50) NULL,
	[ApproveDate] [datetime] NULL,
	[ValidTill] [date] NULL,
	[ProfilePic] [nvarchar](500) NULL,
	[LoginID] [bigint] NULL,
	[DesingantionID] [int] NULL,
	[DeptID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[RegistrationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingAgenda]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingAgenda](
	[AgendaID] [bigint] IDENTITY(1,1) NOT NULL,
	[MeetingID] [bigint] NULL,
	[ProjectID] [int] NULL,
	[TaskDescription] [nvarchar](1000) NULL,
	[FinishingDate] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[AgendaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingAttendies]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingAttendies](
	[AttendiesID] [bigint] IDENTITY(1,1) NOT NULL,
	[MeetingID] [bigint] NULL,
	[EmpID] [bigint] NULL,
	[TsakAssigned] [nvarchar](500) NULL,
	[PresenceStatus] [bit] NULL,
	[TaskDeliveryDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[ProjectID] [int] NULL,
	[IsTaskFinished] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[AttendiesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingInfo]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingInfo](
	[MeetingID] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Task] [nvarchar](1000) NULL,
	[OwnerEmpID] [bigint] NULL,
	[EmpID] [bigint] NULL,
	[MeetingDate] [date] NULL,
	[MeetingTime] [time](7) NULL,
	[AnchorEmpID] [bigint] NULL,
	[LeadChairmanEmpID] [bigint] NULL,
	[IsActive] [bit] NULL,
	[Location] [varchar](1000) NULL,
	[Duration] [int] NULL,
	[MeetingTypeID] [int] NULL,
	[VCLink] [nvarchar](max) NULL,
	[MeetingNo] [varchar](50) NULL,
	[EmpIDs] [varchar](500) NULL,
	[NextMeetingDate] [date] NULL,
	[RefMeetingID] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[SignedDocument] [nvarchar](1000) NULL,
	[MeetingRemark] [nvarchar](1000) NULL,
	[ProjectTypeID] [int] NULL,
	[ProjTypeIDs] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[MeetingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingTypeMaster]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingTypeMaster](
	[MeetingTypeID] [int] IDENTITY(1,1) NOT NULL,
	[MeetingType] [varchar](200) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MeetingTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuMaster]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuMaster](
	[MenuID] [bigint] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](500) NULL,
	[ControllerName] [varchar](200) NULL,
	[ActionName] [varchar](200) NULL,
	[IaActive] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectMaster]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectMaster](
	[ProjectID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](200) NULL,
	[ProjDescription] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[ProjectTypeID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectType]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectType](
	[ProjectTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectType] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleMenuMappings]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMenuMappings](
	[MappingID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserRoleID] [int] NULL,
	[MenuID] [bigint] NULL,
	[CanView] [bit] NULL,
	[CanInsert] [bit] NULL,
	[CanUpdate] [bit] NULL,
	[CanDelete] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MappingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SectorTypeMaster]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SectorTypeMaster](
	[SectorTypeID] [int] IDENTITY(1,1) NOT NULL,
	[SectorType] [nvarchar](300) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[SectorTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StateMaster]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateMaster](
	[StateID] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [nvarchar](500) NULL,
	[CountryID] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[StateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatusMaster]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusMaster](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskUpdates]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskUpdates](
	[UpdateID] [bigint] IDENTITY(1,1) NOT NULL,
	[ref_AttendiesID] [bigint] NULL,
	[UpdateRemark] [varchar](1000) NULL,
	[UpdateAttachment] [varchar](200) NULL,
	[UpdateDate] [date] NULL,
	[EntryDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[ref_MeetingID] [bigint] NULL,
	[IsTaskFinished] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UpdateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[LoginID] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NULL,
	[UserName] [varchar](100) NULL,
	[Password] [varchar](100) NULL,
	[EmployeeID] [bigint] NULL,
	[IsActive] [bit] NULL,
	[DistID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[LoginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 2/7/2024 7:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AgenciesInfo] ON 
GO
INSERT [dbo].[AgenciesInfo] ([AgencyID], [Name], [StateID], [DistID], [OwnerName], [EmailID], [MobileNo], [Address], [CreatedOn], [CreatedBy], [IsActive], [ModifyDate], [ModifyBy]) VALUES (1, N'TEST AGENCY', 26, 50, N'TEST OWNER', N'testowner@gmail.com', N'7052268140', N'TEST AGENCY', CAST(N'2024-02-07T18:48:19.653' AS DateTime), 1, 1, CAST(N'2024-02-07T19:09:27.823' AS DateTime), 1)
GO
INSERT [dbo].[AgenciesInfo] ([AgencyID], [Name], [StateID], [DistID], [OwnerName], [EmailID], [MobileNo], [Address], [CreatedOn], [CreatedBy], [IsActive], [ModifyDate], [ModifyBy]) VALUES (2, N'TEST AGENCY 2', 26, 2, N'TEST OWNER 1', N'testowner111@gmail.com', N'7052268140', N'test address lkoooo 1111', CAST(N'2024-02-07T18:53:18.887' AS DateTime), 1, 1, CAST(N'2024-02-07T18:56:02.573' AS DateTime), 1)
GO
INSERT [dbo].[AgenciesInfo] ([AgencyID], [Name], [StateID], [DistID], [OwnerName], [EmailID], [MobileNo], [Address], [CreatedOn], [CreatedBy], [IsActive], [ModifyDate], [ModifyBy]) VALUES (4, N'TEST AGENCYY', 26, 9, N'TEST OWNER ', N'ramkush', N'7052268140', N'adresssss', CAST(N'2024-02-07T19:05:07.817' AS DateTime), 1, 1, CAST(N'2024-02-07T19:08:57.063' AS DateTime), 1)
GO
INSERT [dbo].[AgenciesInfo] ([AgencyID], [Name], [StateID], [DistID], [OwnerName], [EmailID], [MobileNo], [Address], [CreatedOn], [CreatedBy], [IsActive], [ModifyDate], [ModifyBy]) VALUES (5, N'test', 26, 5, N'testtt data', N'TEST@GMAIL', N'1234567890', N'11', CAST(N'2024-02-07T19:15:09.643' AS DateTime), 1, 1, CAST(N'2024-02-07T19:36:38.797' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[AgenciesInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[BankMaster] ON 
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (1, N'ALLAHABAD DISTRICT COOPERATIVE BANK LTD.', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (2, N'ARYAVART BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (3, N'ARYAVART GRAMIN BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (4, N'ARYAVART KSHETRIYA GRAMIN BANK ', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (5, N'AU SMALL FINANCE BANK LTD.', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (6, N'AWADH GRAMIN BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (7, N'AXIS BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (8, N'BANDHAN BANK LTD', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (9, N'BANK NAME ', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (10, N'BANK OF BARODA', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (11, N'BANK OF INDIA', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (12, N'BANK OF MAHARASHTRA', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (13, N'BARODA U.P. GRAMIN BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (14, N'BHUMI VIKAS BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (15, N'BOI', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (16, N'BOY', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (17, N'CANARA BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (18, N'CENTRAL BANK OF INDIA', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (19, N'CO-OPERATIVE BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (20, N'DCB BANK LIMITED', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (21, N'DISTRICT COOPERATIVE BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (22, N'EQUITAS SMALL FINANCE BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (23, N'ETAWAH DISTRICT CO-OPERATIVE BANK LTD', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (24, N'FEDERAL BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (25, N'FINO PAYMENTS BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (26, N'GRAMIN BANK OF ARYAVRAT', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (27, N'HDFC BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (28, N'ICICI BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (29, N'IDBI BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (30, N'IDFC BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (31, N'INDIA POST PAYMENTS BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (32, N'INDIAN BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (33, N'INDIAN OVERSEAS BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (34, N'INDUSIND BANK LIMITED', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (35, N'INDUSIND BANK LTD', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (36, N'JAMMU & KASHMIR BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (37, N'JANA SFB LTD.', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (38, N'ZILA SAHKARI BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (39, N'ZILA SAHKARI BANK LTD. MUZAFFARNAGAR', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (40, N'ZILA SAHKARI BANK MIRZAPUR', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (41, N'KARNATAKA BANK LTD', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (42, N'KASHI GOMTI SAMYUT GRAMIN BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (43, N'KOTAK MAHINDRA BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (44, N'KSHETRIYA KISAN GAMIN BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (45, N'NAGAR SAHKARI BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (46, N'OTHERS', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (47, N'PRATHAMA BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (48, N'PRATHAMA UP GRAMIN BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (49, N'PUNJAB AND SIND BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (50, N'PUNJAB NATIONAL BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (51, N'PURVANCHAL  GRAMIN BAND', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (52, N'PURWANCHAL BANK    ', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (53, N'PURWANCHAL GRAMEEN BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (54, N'SARVA UP GRAMIN BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (55, N'SHAHJAHANPUR DISTRICT CENTRAL CO OPERATIVE BANK LTD.', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (56, N'SHAHJAHANPUR KSHETRIYA GRAMIN BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (57, N'SOUTH INDIAN BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (58, N'STATE BANK OF INDIA', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (59, N'SYNDICATE BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (60, N'THE NAINITAL BANK LIMITED', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (61, N'TRIVENI KG BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (62, N'UCO BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (63, N'UJJIVAN SFB', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (64, N'UNION BANK OF INDIA', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (65, N'UPSGV', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (66, N'URBAN CO-OPERATIVE BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (67, N'UTKARSH SMALL FINANCE BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (68, N'UTKARSH SMALL FINANCE BANK LIMITED', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (69, N'YES BANK', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (70, N'ZILA SAHKARI BANK LTD.', 1)
GO
INSERT [dbo].[BankMaster] ([BankID], [Name], [IsActive]) VALUES (71, N'ZILA SAHKARI BANKLTD. GHAZIYABAD', 1)
GO
SET IDENTITY_INSERT [dbo].[BankMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[DepartmentMaster] ON 
GO
INSERT [dbo].[DepartmentMaster] ([DeptID], [DeptCode], [DeptName], [IsActive]) VALUES (1, N'D0001', N'MANAGEMENT', 1)
GO
INSERT [dbo].[DepartmentMaster] ([DeptID], [DeptCode], [DeptName], [IsActive]) VALUES (2, N'D001', N'Development', 1)
GO
INSERT [dbo].[DepartmentMaster] ([DeptID], [DeptCode], [DeptName], [IsActive]) VALUES (3, N'D002', N'HR', 1)
GO
INSERT [dbo].[DepartmentMaster] ([DeptID], [DeptCode], [DeptName], [IsActive]) VALUES (4, N'D003', N'Admin', 1)
GO
INSERT [dbo].[DepartmentMaster] ([DeptID], [DeptCode], [DeptName], [IsActive]) VALUES (5, N'D004', N'Account', 1)
GO
SET IDENTITY_INSERT [dbo].[DepartmentMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[DesignationMaster] ON 
GO
INSERT [dbo].[DesignationMaster] ([DesingantionID], [DesignationCode], [Designation], [IsActive]) VALUES (1, N'DS01', N'S/W Developer', 1)
GO
INSERT [dbo].[DesignationMaster] ([DesingantionID], [DesignationCode], [Designation], [IsActive]) VALUES (2, N'DS02', N'Sr S/W Developer', 1)
GO
INSERT [dbo].[DesignationMaster] ([DesingantionID], [DesignationCode], [Designation], [IsActive]) VALUES (3, N'DS03', N'Project Head', 1)
GO
INSERT [dbo].[DesignationMaster] ([DesingantionID], [DesignationCode], [Designation], [IsActive]) VALUES (4, N'DS04', N'Team Leader', 1)
GO
INSERT [dbo].[DesignationMaster] ([DesingantionID], [DesignationCode], [Designation], [IsActive]) VALUES (5, N'DS05', N'HR', 1)
GO
INSERT [dbo].[DesignationMaster] ([DesingantionID], [DesignationCode], [Designation], [IsActive]) VALUES (6, N'DS06', N'Accountant', 1)
GO
INSERT [dbo].[DesignationMaster] ([DesingantionID], [DesignationCode], [Designation], [IsActive]) VALUES (7, N'DS07', N'Admin', 1)
GO
INSERT [dbo].[DesignationMaster] ([DesingantionID], [DesignationCode], [Designation], [IsActive]) VALUES (9, N'DS08', N'General Manager(GM)', 1)
GO
INSERT [dbo].[DesignationMaster] ([DesingantionID], [DesignationCode], [Designation], [IsActive]) VALUES (10, N'DS09', N'Relationship Manager(RM)', 1)
GO
INSERT [dbo].[DesignationMaster] ([DesingantionID], [DesignationCode], [Designation], [IsActive]) VALUES (11, N'DS10', N'Project Coordinator', 1)
GO
INSERT [dbo].[DesignationMaster] ([DesingantionID], [DesignationCode], [Designation], [IsActive]) VALUES (12, N'DS11', N'UX/UI Analyst', 1)
GO
INSERT [dbo].[DesignationMaster] ([DesingantionID], [DesignationCode], [Designation], [IsActive]) VALUES (13, N'DS12', N'CTO', 1)
GO
INSERT [dbo].[DesignationMaster] ([DesingantionID], [DesignationCode], [Designation], [IsActive]) VALUES (14, N'DS13', N'CEO', 1)
GO
INSERT [dbo].[DesignationMaster] ([DesingantionID], [DesignationCode], [Designation], [IsActive]) VALUES (15, N'DS14', N'Dev', 1)
GO
SET IDENTITY_INSERT [dbo].[DesignationMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[DistrictMaster] ON 
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (1, N'Agra', N'3120', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (2, N'Aligarh', N'3118', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (3, N'Prayagraj', N'310', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (4, N'Ambedkar Nagar', N'3178', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (5, N'Amethi', N'3182', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (6, N'Amroha', N'3167', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (7, N'Auraiya', N'3169', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (8, N'Azamgarh', N'3157', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (9, N'Baghpat', N'3165', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (10, N'Bahraich', N'3146', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (11, N'Ballia', N'3159', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (12, N'Balrampur', N'3175', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (13, N'Banda', N'3142', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (14, N'Barabanki', N'3148', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (15, N'Bareilly', N'3125', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (16, N'Basti', N'3153', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (17, N'Bhadohi', N'3173', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (18, N'Bijnor', N'319', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (19, N'Budaun', N'3124', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (20, N'Bulandshahr', N'3117', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (21, N'Chandauli', N'3171', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (22, N'Chitrakoot', N'3177', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (23, N'Deoria', N'3155', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (24, N'Etah', N'3122', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (25, N'Etawah', N'3135', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (26, N'Ayodhya', N'310', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (27, N'Farrukhabad', N'3134', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (28, N'Fatehpur', N'3143', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (29, N'Firozabad', N'3121', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (30, N'Gautam Buddha Nagar', N'3164', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (31, N'Ghaziabad', N'3116', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (32, N'Ghazipur', N'3160', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (33, N'Gonda', N'3147', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (34, N'Gorakhpur', N'3154', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (35, N'Hamirpur', N'3141', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (36, N'Hapur', N'3184', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (37, N'Hardoi', N'3130', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (38, N'Hathras', N'3166', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (39, N'Jalaun', N'3138', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (40, N'Jaunpur', N'3158', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (41, N'Jhansi', N'3139', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (42, N'Kannauj', N'3168', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (43, N'Kanpur Dehat', N'3136', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (44, N'Kanpur Nagar', N'3137', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (45, N'Kasganj', N'3180', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (46, N'Kaushambi', N'3170', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (47, N'Kheri', N'3128', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (48, N'Kushinagar', N'310', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (49, N'Lalitpur', N'3140', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (50, N'Lucknow', N'3132', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (51, N'Maharajganj', N'3152', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (52, N'Mahoba', N'3179', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (53, N'Mainpuri', N'3123', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (54, N'Mathura', N'3119', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (55, N'Mau', N'3156', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (56, N'Meerut', N'3115', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (57, N'Mirzapur', N'3162', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (58, N'Moradabad', N'3110', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (59, N'Muzaffarnagar', N'3114', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (60, N'Pilibhit', N'3126', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (61, N'Pratapgarh', N'3144', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (62, N'Raebareli', N'310', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (63, N'Rampur', N'3111', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (64, N'Saharanpur', N'3112', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (65, N'Sambhal', N'3181', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (66, N'Sant Kabeer Nagar', N'3174', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (67, N'Shahjahanpur', N'3127', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (68, N'Shamli', N'3183', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (69, N'Shravasti', N'3176', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (70, N'Siddharth Nagar', N'3151', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (71, N'Sitapur', N'3129', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (72, N'Sonbhadra', N'3163', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (73, N'Sultanpur', N'3150', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (74, N'Unnao', N'3131', 1, 26)
GO
INSERT [dbo].[DistrictMaster] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [StateID]) VALUES (75, N'Varanasi', N'3161', 1, 26)
GO
SET IDENTITY_INSERT [dbo].[DistrictMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeRegistration] ON 
GO
INSERT [dbo].[EmployeeRegistration] ([RegistrationID], [Name], [FatherName], [DOB], [EmailID], [MobileNo], [PAN], [DistrictID], [BlockID], [Address], [Nationality], [StatusID], [CreatedOn], [AllowEdit], [AdminRemark], [ApplicationNo], [AadharNo], [ApproveDate], [ValidTill], [ProfilePic], [LoginID], [DesingantionID], [DeptID]) VALUES (1, N'Admin', N'Admin', CAST(N'1996-09-13' AS Date), N'admin@gmail.com', N'70******40', N'E********L', 1, 1, N'Vill & Post : Amsin', N'INDIAN', 1, CAST(N'2023-05-21T14:23:17.920' AS DateTime), 0, N'Done', N'RG0001', N'92******8014', CAST(N'2023-05-21T14:23:17.920' AS DateTime), CAST(N'2123-05-21' AS Date), N'', 1, 7, 1)
GO
INSERT [dbo].[EmployeeRegistration] ([RegistrationID], [Name], [FatherName], [DOB], [EmailID], [MobileNo], [PAN], [DistrictID], [BlockID], [Address], [Nationality], [StatusID], [CreatedOn], [AllowEdit], [AdminRemark], [ApplicationNo], [AadharNo], [ApproveDate], [ValidTill], [ProfilePic], [LoginID], [DesingantionID], [DeptID]) VALUES (2, N'VAIBHAV MATHUR', N'--', CAST(N'1996-09-13' AS Date), N'vaibhav.mathur@margsoft.com', N'70******40', N'E********L', 50, 1, N'LUCKNOW', N'INDIAN', 1, CAST(N'2023-05-21T14:25:00.307' AS DateTime), 0, N'Done', N'RG0002', N'92******8014', CAST(N'2023-05-21T14:25:00.307' AS DateTime), CAST(N'2123-05-21' AS Date), N'', NULL, 3, 2)
GO
INSERT [dbo].[EmployeeRegistration] ([RegistrationID], [Name], [FatherName], [DOB], [EmailID], [MobileNo], [PAN], [DistrictID], [BlockID], [Address], [Nationality], [StatusID], [CreatedOn], [AllowEdit], [AdminRemark], [ApplicationNo], [AadharNo], [ApproveDate], [ValidTill], [ProfilePic], [LoginID], [DesingantionID], [DeptID]) VALUES (3, N'AJAY KUSHWAHA', N'---', CAST(N'1996-09-13' AS Date), N'ajay.kushwaha@margsoft.com', N'70******40', N'E********L', 50, 1, N'LUCKNOW', N'INDIAN', 1, CAST(N'2023-05-21T14:25:08.667' AS DateTime), 0, N'Done', N'RG0003', N'92******8014', CAST(N'2023-05-21T14:25:08.667' AS DateTime), CAST(N'2123-05-21' AS Date), N'', NULL, 3, 2)
GO
INSERT [dbo].[EmployeeRegistration] ([RegistrationID], [Name], [FatherName], [DOB], [EmailID], [MobileNo], [PAN], [DistrictID], [BlockID], [Address], [Nationality], [StatusID], [CreatedOn], [AllowEdit], [AdminRemark], [ApplicationNo], [AadharNo], [ApproveDate], [ValidTill], [ProfilePic], [LoginID], [DesingantionID], [DeptID]) VALUES (4, N'AVNISH BHATTA', N'---', CAST(N'1996-09-14' AS Date), N'avnish.bhatt@margsoft.com', N'7052268140', N'E****50**L', 50, 1, N'LUCKNOW', N'INDIAN', 1, CAST(N'2023-05-21T14:26:19.170' AS DateTime), 0, N'Done', N'RG0004', N'92**76**8014', CAST(N'2023-05-21T14:26:19.170' AS DateTime), CAST(N'2123-05-21' AS Date), N'', NULL, 9, 4)
GO
INSERT [dbo].[EmployeeRegistration] ([RegistrationID], [Name], [FatherName], [DOB], [EmailID], [MobileNo], [PAN], [DistrictID], [BlockID], [Address], [Nationality], [StatusID], [CreatedOn], [AllowEdit], [AdminRemark], [ApplicationNo], [AadharNo], [ApproveDate], [ValidTill], [ProfilePic], [LoginID], [DesingantionID], [DeptID]) VALUES (5, N'Ramkush Maurya', N'TEST FATHER', CAST(N'1996-09-13' AS Date), N'test@gmail.com', N'7052268140', N'EGMPM5092L', 1, NULL, N'TEST', NULL, NULL, CAST(N'2023-06-05T23:25:44.023' AS DateTime), NULL, NULL, N'RG0005', N'4545644546', NULL, NULL, NULL, 2, NULL, NULL)
GO
INSERT [dbo].[EmployeeRegistration] ([RegistrationID], [Name], [FatherName], [DOB], [EmailID], [MobileNo], [PAN], [DistrictID], [BlockID], [Address], [Nationality], [StatusID], [CreatedOn], [AllowEdit], [AdminRemark], [ApplicationNo], [AadharNo], [ApproveDate], [ValidTill], [ProfilePic], [LoginID], [DesingantionID], [DeptID]) VALUES (6, N'Ramkush Maurya', N'----', CAST(N'1996-09-25' AS Date), N'test@gmail.com', N'7052268140', N'EGMPM5092L', 4, NULL, N'LUCKNOW', NULL, 1, CAST(N'2023-06-05T23:31:50.690' AS DateTime), 0, NULL, N'RG0006', N'4545644546', NULL, NULL, NULL, 3, 2, 2)
GO
INSERT [dbo].[EmployeeRegistration] ([RegistrationID], [Name], [FatherName], [DOB], [EmailID], [MobileNo], [PAN], [DistrictID], [BlockID], [Address], [Nationality], [StatusID], [CreatedOn], [AllowEdit], [AdminRemark], [ApplicationNo], [AadharNo], [ApproveDate], [ValidTill], [ProfilePic], [LoginID], [DesingantionID], [DeptID]) VALUES (7, N'VIVEKANAND SHUKLA', N'K.P. SHUKLA', CAST(N'1982-08-07' AS Date), N'vivekanand.shukla@margsoft.com', N'9838354188', N'buusd3212s', 50, NULL, N'lko', NULL, 1, CAST(N'2023-06-19T12:47:08.737' AS DateTime), 0, NULL, N'RG0007', N'123412341234', NULL, NULL, NULL, 4, 3, 2)
GO
INSERT [dbo].[EmployeeRegistration] ([RegistrationID], [Name], [FatherName], [DOB], [EmailID], [MobileNo], [PAN], [DistrictID], [BlockID], [Address], [Nationality], [StatusID], [CreatedOn], [AllowEdit], [AdminRemark], [ApplicationNo], [AadharNo], [ApproveDate], [ValidTill], [ProfilePic], [LoginID], [DesingantionID], [DeptID]) VALUES (8, N'Mohd. Asif', N'----', CAST(N'1996-09-13' AS Date), N'mohd.asif@margsoftware.com', N'7865652323', N'buusd3212s', 50, NULL, N'LUCKNOW', NULL, NULL, CAST(N'2023-06-30T14:36:53.107' AS DateTime), NULL, NULL, N'RG0008', N'123412341234', NULL, NULL, NULL, 5, 10, 4)
GO
INSERT [dbo].[EmployeeRegistration] ([RegistrationID], [Name], [FatherName], [DOB], [EmailID], [MobileNo], [PAN], [DistrictID], [BlockID], [Address], [Nationality], [StatusID], [CreatedOn], [AllowEdit], [AdminRemark], [ApplicationNo], [AadharNo], [ApproveDate], [ValidTill], [ProfilePic], [LoginID], [DesingantionID], [DeptID]) VALUES (9, N'Saumya Chandani', N'----', CAST(N'1996-09-13' AS Date), N'saumya@margsoft.com', N'7865652323', N'buusd3212s', 50, NULL, N'LUCKNOW', NULL, NULL, CAST(N'2023-06-30T14:38:22.903' AS DateTime), NULL, NULL, N'RG0009', N'123412341234', NULL, NULL, NULL, 6, 11, 4)
GO
INSERT [dbo].[EmployeeRegistration] ([RegistrationID], [Name], [FatherName], [DOB], [EmailID], [MobileNo], [PAN], [DistrictID], [BlockID], [Address], [Nationality], [StatusID], [CreatedOn], [AllowEdit], [AdminRemark], [ApplicationNo], [AadharNo], [ApproveDate], [ValidTill], [ProfilePic], [LoginID], [DesingantionID], [DeptID]) VALUES (10, N'PAWAN UTTAM', N'----', CAST(N'1996-09-13' AS Date), N'vivekgupta@margsoft.com', N'7865652323', N'buusd3212s', 50, NULL, N'LUCKNOW', NULL, 1, CAST(N'2023-06-30T15:38:20.930' AS DateTime), 0, NULL, N'RG0010', N'123412341234', NULL, NULL, NULL, 7, 13, 2)
GO
INSERT [dbo].[EmployeeRegistration] ([RegistrationID], [Name], [FatherName], [DOB], [EmailID], [MobileNo], [PAN], [DistrictID], [BlockID], [Address], [Nationality], [StatusID], [CreatedOn], [AllowEdit], [AdminRemark], [ApplicationNo], [AadharNo], [ApproveDate], [ValidTill], [ProfilePic], [LoginID], [DesingantionID], [DeptID]) VALUES (11, N'VIVEK GUPTA', N'---', CAST(N'1996-09-13' AS Date), N'vivekgupta@margsoft.com', N'7865652323', N'buusd3212s', 50, NULL, N'LUCKNOW', NULL, NULL, CAST(N'2023-06-30T15:39:21.983' AS DateTime), NULL, NULL, N'RG0011', N'123412341234', NULL, NULL, NULL, 8, 14, 1)
GO
INSERT [dbo].[EmployeeRegistration] ([RegistrationID], [Name], [FatherName], [DOB], [EmailID], [MobileNo], [PAN], [DistrictID], [BlockID], [Address], [Nationality], [StatusID], [CreatedOn], [AllowEdit], [AdminRemark], [ApplicationNo], [AadharNo], [ApproveDate], [ValidTill], [ProfilePic], [LoginID], [DesingantionID], [DeptID]) VALUES (12, N'KAMAL PANT', N'test', CAST(N'1996-09-13' AS Date), N'kamal.pant@margsoft.technology', N'7865652323', N'buusd3212s', 50, NULL, N'LUCKNOW test', NULL, 1, CAST(N'2023-06-30T15:41:55.857' AS DateTime), 0, NULL, N'RG0012', N'123412341234', NULL, NULL, NULL, 9, 11, 4)
GO
INSERT [dbo].[EmployeeRegistration] ([RegistrationID], [Name], [FatherName], [DOB], [EmailID], [MobileNo], [PAN], [DistrictID], [BlockID], [Address], [Nationality], [StatusID], [CreatedOn], [AllowEdit], [AdminRemark], [ApplicationNo], [AadharNo], [ApproveDate], [ValidTill], [ProfilePic], [LoginID], [DesingantionID], [DeptID]) VALUES (13, N'AMARDEEP SHARMA', N'---', CAST(N'1996-09-13' AS Date), N'amar.sharma@margsoft.com ', N'7865652323', N'buusd3212s', 50, NULL, N'LUCKNOW', NULL, NULL, CAST(N'2023-06-30T16:19:01.450' AS DateTime), NULL, NULL, N'RG0013', N'123412341234', NULL, NULL, NULL, 10, 12, 2)
GO
INSERT [dbo].[EmployeeRegistration] ([RegistrationID], [Name], [FatherName], [DOB], [EmailID], [MobileNo], [PAN], [DistrictID], [BlockID], [Address], [Nationality], [StatusID], [CreatedOn], [AllowEdit], [AdminRemark], [ApplicationNo], [AadharNo], [ApproveDate], [ValidTill], [ProfilePic], [LoginID], [DesingantionID], [DeptID]) VALUES (14, N'Ramkush Maurya', NULL, NULL, N'myakansh786@gmail.com', N'7052268140', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-02-03T17:48:00.893' AS DateTime), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[EmployeeRegistration] OFF
GO
SET IDENTITY_INSERT [dbo].[MeetingAgenda] ON 
GO
INSERT [dbo].[MeetingAgenda] ([AgendaID], [MeetingID], [ProjectID], [TaskDescription], [FinishingDate], [CreatedOn], [IsActive]) VALUES (21, 18, 8, N'UPDATE the STATUS', CAST(N'2023-06-28T00:00:00.000' AS DateTime), CAST(N'2023-06-27T15:59:37.800' AS DateTime), 1)
GO
INSERT [dbo].[MeetingAgenda] ([AgendaID], [MeetingID], [ProjectID], [TaskDescription], [FinishingDate], [CreatedOn], [IsActive]) VALUES (22, 19, 14, N'NEW PROJECT METLIZATION', CAST(N'2023-07-10T00:00:00.000' AS DateTime), CAST(N'2023-06-30T16:33:38.517' AS DateTime), 1)
GO
INSERT [dbo].[MeetingAgenda] ([AgendaID], [MeetingID], [ProjectID], [TaskDescription], [FinishingDate], [CreatedOn], [IsActive]) VALUES (23, 20, 11, N'CLOSE ON 15 DAYS', CAST(N'2023-07-20T00:00:00.000' AS DateTime), CAST(N'2023-06-30T16:50:18.820' AS DateTime), 1)
GO
INSERT [dbo].[MeetingAgenda] ([AgendaID], [MeetingID], [ProjectID], [TaskDescription], [FinishingDate], [CreatedOn], [IsActive]) VALUES (24, 21, 7, N'agenda 1', CAST(N'2023-07-30T00:00:00.000' AS DateTime), CAST(N'2023-06-30T23:20:52.130' AS DateTime), 1)
GO
INSERT [dbo].[MeetingAgenda] ([AgendaID], [MeetingID], [ProjectID], [TaskDescription], [FinishingDate], [CreatedOn], [IsActive]) VALUES (25, 21, 8, N'agenda 2', CAST(N'2023-07-30T00:00:00.000' AS DateTime), CAST(N'2023-06-30T23:20:52.193' AS DateTime), 1)
GO
INSERT [dbo].[MeetingAgenda] ([AgendaID], [MeetingID], [ProjectID], [TaskDescription], [FinishingDate], [CreatedOn], [IsActive]) VALUES (26, 24, 4, N'test agenda 1', CAST(N'2024-02-10T00:00:00.000' AS DateTime), CAST(N'2024-02-03T17:59:53.103' AS DateTime), 1)
GO
INSERT [dbo].[MeetingAgenda] ([AgendaID], [MeetingID], [ProjectID], [TaskDescription], [FinishingDate], [CreatedOn], [IsActive]) VALUES (27, 24, 13, N'test agenda 2', CAST(N'2024-02-10T00:00:00.000' AS DateTime), CAST(N'2024-02-03T17:59:53.110' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[MeetingAgenda] OFF
GO
SET IDENTITY_INSERT [dbo].[MeetingAttendies] ON 
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (29, 18, 8, N'Updated PCF ERP Status Sheet', 1, CAST(N'2023-06-27T00:00:00.000' AS DateTime), 1, CAST(N'2023-06-27T15:59:37.817' AS DateTime), 8, NULL)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (30, 18, 12, N'Status against the 57 services on a regular basis for CeG Project ', 1, CAST(N'2023-06-28T00:00:00.000' AS DateTime), 1, CAST(N'2023-06-27T15:59:37.830' AS DateTime), 12, NULL)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (31, 18, 11, N'Call Mr. Alok Dixit for a meeting tie-up', 1, CAST(N'2023-06-28T00:00:00.000' AS DateTime), 1, CAST(N'2023-06-27T15:59:37.830' AS DateTime), 8, NULL)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (32, 19, 8, N'Draft the enquiry letter for UPSRTC', 1, CAST(N'2023-07-01T00:00:00.000' AS DateTime), 1, CAST(N'2023-06-30T16:33:38.530' AS DateTime), 16, NULL)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (33, 19, 8, N'WORK ORDER', 1, CAST(N'2023-07-05T00:00:00.000' AS DateTime), 1, CAST(N'2023-06-30T16:33:38.547' AS DateTime), 16, NULL)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (34, 19, 4, N'Draft the enquiry letter for ULB', 1, CAST(N'2023-07-02T00:00:00.000' AS DateTime), 1, CAST(N'2023-06-30T16:33:38.563' AS DateTime), 14, NULL)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (35, 20, 7, N'TEST', 1, CAST(N'2023-07-05T00:00:00.000' AS DateTime), 1, CAST(N'2023-06-30T16:50:18.837' AS DateTime), 10, NULL)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (36, 20, 7, N'TEST123', 1, CAST(N'2023-07-07T00:00:00.000' AS DateTime), 1, CAST(N'2023-06-30T16:50:18.837' AS DateTime), 10, NULL)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (37, 20, 13, N'DESIGN', 1, CAST(N'2023-07-06T00:00:00.000' AS DateTime), 1, CAST(N'2023-06-30T16:50:18.850' AS DateTime), 11, NULL)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (38, 20, 2, N'TEST222', 1, CAST(N'2023-07-10T00:00:00.000' AS DateTime), 1, CAST(N'2023-06-30T16:50:18.850' AS DateTime), 11, NULL)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (39, 21, 6, N'task 1', 1, CAST(N'2023-07-01T00:00:00.000' AS DateTime), 1, CAST(N'2023-06-30T23:20:52.240' AS DateTime), 7, 1)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (40, 21, 6, N'task 2', 1, CAST(N'2023-07-05T00:00:00.000' AS DateTime), 1, CAST(N'2023-06-30T23:20:52.303' AS DateTime), 7, NULL)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (41, 21, 2, N'task 3', 1, CAST(N'2023-06-30T00:00:00.000' AS DateTime), 1, CAST(N'2023-06-30T23:20:52.350' AS DateTime), 7, NULL)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (42, 21, 3, N'task 4', 1, CAST(N'2023-07-25T00:00:00.000' AS DateTime), 1, CAST(N'2023-06-30T23:20:52.397' AS DateTime), 7, NULL)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (43, 21, 4, N'', 0, CAST(N'1900-01-01T00:00:00.000' AS DateTime), 1, CAST(N'2023-06-30T23:20:52.443' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (44, 24, 1, N'test', 1, CAST(N'2024-02-03T00:00:00.000' AS DateTime), 1, CAST(N'2024-02-03T17:59:53.110' AS DateTime), 4, NULL)
GO
INSERT [dbo].[MeetingAttendies] ([AttendiesID], [MeetingID], [EmpID], [TsakAssigned], [PresenceStatus], [TaskDeliveryDate], [IsActive], [CreatedOn], [ProjectID], [IsTaskFinished]) VALUES (45, 24, 1, N'ert45', 1, CAST(N'2024-02-10T00:00:00.000' AS DateTime), 1, CAST(N'2024-02-03T17:59:53.117' AS DateTime), 13, NULL)
GO
SET IDENTITY_INSERT [dbo].[MeetingAttendies] OFF
GO
SET IDENTITY_INSERT [dbo].[MeetingInfo] ON 
GO
INSERT [dbo].[MeetingInfo] ([MeetingID], [Title], [Task], [OwnerEmpID], [EmpID], [MeetingDate], [MeetingTime], [AnchorEmpID], [LeadChairmanEmpID], [IsActive], [Location], [Duration], [MeetingTypeID], [VCLink], [MeetingNo], [EmpIDs], [NextMeetingDate], [RefMeetingID], [CreatedOn], [SignedDocument], [MeetingRemark], [ProjectTypeID], [ProjTypeIDs]) VALUES (18, N'Monitoring MIS for Asif Sir’s Project', NULL, 9, NULL, CAST(N'2023-06-27' AS Date), CAST(N'15:00:00' AS Time), NULL, 11, 1, N'MARGSOFT', 2, 3, N'http://margsoft.video.com', N'MT-0001', N'11,7,8,12', CAST(N'2023-06-29' AS Date), NULL, CAST(N'2023-06-27T15:40:38.843' AS DateTime), NULL, N'NEXT MEETING WILL BE HELD ON 30th JUNE', 5, NULL)
GO
INSERT [dbo].[MeetingInfo] ([MeetingID], [Title], [Task], [OwnerEmpID], [EmpID], [MeetingDate], [MeetingTime], [AnchorEmpID], [LeadChairmanEmpID], [IsActive], [Location], [Duration], [MeetingTypeID], [VCLink], [MeetingNo], [EmpIDs], [NextMeetingDate], [RefMeetingID], [CreatedOn], [SignedDocument], [MeetingRemark], [ProjectTypeID], [ProjTypeIDs]) VALUES (19, N'NEW PROJECT BUSINESS MEETING', NULL, 9, NULL, CAST(N'2023-06-30' AS Date), CAST(N'16:30:00' AS Time), NULL, 11, 1, N'MARGSOFT', 60, 3, N'HTTP://video.com', N'MT-0002', N'4,8', CAST(N'2023-07-11' AS Date), NULL, CAST(N'2023-06-30T16:24:08.283' AS DateTime), NULL, N'NEXT MEETING WILL BE HELD ON 11 JULY', NULL, NULL)
GO
INSERT [dbo].[MeetingInfo] ([MeetingID], [Title], [Task], [OwnerEmpID], [EmpID], [MeetingDate], [MeetingTime], [AnchorEmpID], [LeadChairmanEmpID], [IsActive], [Location], [Duration], [MeetingTypeID], [VCLink], [MeetingNo], [EmpIDs], [NextMeetingDate], [RefMeetingID], [CreatedOn], [SignedDocument], [MeetingRemark], [ProjectTypeID], [ProjTypeIDs]) VALUES (20, N'CURRENT PROJECT MEETING', NULL, 9, NULL, CAST(N'2023-07-03' AS Date), CAST(N'16:45:00' AS Time), NULL, 11, 1, N'MARGSOFT', 60, 3, NULL, N'MT-0003', N'2,7,13', CAST(N'2023-07-21' AS Date), NULL, CAST(N'2023-06-30T16:47:47.227' AS DateTime), NULL, N'NEXT MEETING WILL BE HELD ON 21 JULY', 5, NULL)
GO
INSERT [dbo].[MeetingInfo] ([MeetingID], [Title], [Task], [OwnerEmpID], [EmpID], [MeetingDate], [MeetingTime], [AnchorEmpID], [LeadChairmanEmpID], [IsActive], [Location], [Duration], [MeetingTypeID], [VCLink], [MeetingNo], [EmpIDs], [NextMeetingDate], [RefMeetingID], [CreatedOn], [SignedDocument], [MeetingRemark], [ProjectTypeID], [ProjTypeIDs]) VALUES (21, N'Test Meeting RKM', NULL, 1, NULL, CAST(N'2023-06-30' AS Date), CAST(N'23:20:00' AS Time), NULL, 1, 1, N'test location', 20, 2, N'Video Confrencing Link', N'MT-0004', N'2,3,4,6', NULL, NULL, CAST(N'2023-06-30T23:16:32.907' AS DateTime), NULL, N'Remark', NULL, N'5,6')
GO
INSERT [dbo].[MeetingInfo] ([MeetingID], [Title], [Task], [OwnerEmpID], [EmpID], [MeetingDate], [MeetingTime], [AnchorEmpID], [LeadChairmanEmpID], [IsActive], [Location], [Duration], [MeetingTypeID], [VCLink], [MeetingNo], [EmpIDs], [NextMeetingDate], [RefMeetingID], [CreatedOn], [SignedDocument], [MeetingRemark], [ProjectTypeID], [ProjTypeIDs]) VALUES (22, N'MSD PAYMENT TARGET', NULL, 9, NULL, CAST(N'2023-07-05' AS Date), CAST(N'15:47:00' AS Time), NULL, 11, 1, N'MSTPL', 30, 3, NULL, N'MT-0005', N'2,3', NULL, NULL, CAST(N'2023-07-05T15:48:18.490' AS DateTime), NULL, NULL, NULL, N'5')
GO
INSERT [dbo].[MeetingInfo] ([MeetingID], [Title], [Task], [OwnerEmpID], [EmpID], [MeetingDate], [MeetingTime], [AnchorEmpID], [LeadChairmanEmpID], [IsActive], [Location], [Duration], [MeetingTypeID], [VCLink], [MeetingNo], [EmpIDs], [NextMeetingDate], [RefMeetingID], [CreatedOn], [SignedDocument], [MeetingRemark], [ProjectTypeID], [ProjTypeIDs]) VALUES (23, N'ASIF TASK ', NULL, 11, NULL, CAST(N'2023-07-05' AS Date), CAST(N'15:53:00' AS Time), NULL, 11, 1, N'MSTPL', 5, 1, NULL, N'MT-0006', N'8', NULL, NULL, CAST(N'2023-07-05T15:54:02.517' AS DateTime), NULL, NULL, NULL, N'5')
GO
INSERT [dbo].[MeetingInfo] ([MeetingID], [Title], [Task], [OwnerEmpID], [EmpID], [MeetingDate], [MeetingTime], [AnchorEmpID], [LeadChairmanEmpID], [IsActive], [Location], [Duration], [MeetingTypeID], [VCLink], [MeetingNo], [EmpIDs], [NextMeetingDate], [RefMeetingID], [CreatedOn], [SignedDocument], [MeetingRemark], [ProjectTypeID], [ProjTypeIDs]) VALUES (24, N'test title for meeting', NULL, 2, NULL, CAST(N'2024-02-03' AS Date), CAST(N'18:00:00' AS Time), NULL, 1, 1, N'test location data', 20, 1, N'test.vd.com?id=yuiyewuiywre7527635426735', N'MT-0007', N'1', CAST(N'2024-02-24' AS Date), NULL, CAST(N'2024-02-03T17:56:38.250' AS DateTime), N'/Documents/UsersDocs/Bank_Doc_MT-0007-2024-03-02--17-59-53.png', N'test', NULL, N'4,5,6,7')
GO
SET IDENTITY_INSERT [dbo].[MeetingInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[MeetingTypeMaster] ON 
GO
INSERT [dbo].[MeetingTypeMaster] ([MeetingTypeID], [MeetingType], [IsActive]) VALUES (1, N'Individual', 1)
GO
INSERT [dbo].[MeetingTypeMaster] ([MeetingTypeID], [MeetingType], [IsActive]) VALUES (2, N'Project Wise', 1)
GO
INSERT [dbo].[MeetingTypeMaster] ([MeetingTypeID], [MeetingType], [IsActive]) VALUES (3, N'Common', 1)
GO
SET IDENTITY_INSERT [dbo].[MeetingTypeMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[MenuMaster] ON 
GO
INSERT [dbo].[MenuMaster] ([MenuID], [MenuName], [ControllerName], [ActionName], [IaActive], [CreatedOn], [CreatedBy], [ModifyDate], [ModifyBy]) VALUES (1, N'State Master', N'Master', N'StateMaster', 1, CAST(N'2024-02-06T11:15:03.003' AS DateTime), 1, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[MenuMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[ProjectMaster] ON 
GO
INSERT [dbo].[ProjectMaster] ([ProjectID], [ProjectName], [ProjDescription], [IsActive], [ProjectTypeID]) VALUES (1, N'Mines on MAP', N'Mines on MAP', 1, NULL)
GO
INSERT [dbo].[ProjectMaster] ([ProjectID], [ProjectName], [ProjDescription], [IsActive], [ProjectTypeID]) VALUES (2, N'Up Mine Mitra', N'Up Mine Mitra', 1, NULL)
GO
INSERT [dbo].[ProjectMaster] ([ProjectID], [ProjectName], [ProjDescription], [IsActive], [ProjectTypeID]) VALUES (3, N'Decision Support System', N'Decision Support System', 1, NULL)
GO
INSERT [dbo].[ProjectMaster] ([ProjectID], [ProjectName], [ProjDescription], [IsActive], [ProjectTypeID]) VALUES (4, N'TEST PROJECT', N'TEST DESCRIPTION', NULL, 4)
GO
INSERT [dbo].[ProjectMaster] ([ProjectID], [ProjectName], [ProjDescription], [IsActive], [ProjectTypeID]) VALUES (5, N'TEST DEPT', N'TEST DESCRIPTION', NULL, 4)
GO
INSERT [dbo].[ProjectMaster] ([ProjectID], [ProjectName], [ProjDescription], [IsActive], [ProjectTypeID]) VALUES (6, N'TEST DEPT 2', N'TEST DESCRIPTION', NULL, 5)
GO
INSERT [dbo].[ProjectMaster] ([ProjectID], [ProjectName], [ProjDescription], [IsActive], [ProjectTypeID]) VALUES (7, N'PRD MIS', N'PRD', 1, 5)
GO
INSERT [dbo].[ProjectMaster] ([ProjectID], [ProjectName], [ProjDescription], [IsActive], [ProjectTypeID]) VALUES (8, N'PCF-ERP', N'PCF', 1, 5)
GO
INSERT [dbo].[ProjectMaster] ([ProjectID], [ProjectName], [ProjDescription], [IsActive], [ProjectTypeID]) VALUES (10, N'Mines-ON-MAP', N'DASHBOARD REGARDING MINING', 1, 5)
GO
INSERT [dbo].[ProjectMaster] ([ProjectID], [ProjectName], [ProjDescription], [IsActive], [ProjectTypeID]) VALUES (11, N'TIMS-SIMA', N'Training Management', 1, 5)
GO
INSERT [dbo].[ProjectMaster] ([ProjectID], [ProjectName], [ProjDescription], [IsActive], [ProjectTypeID]) VALUES (12, N'CEG', N'eDistrict Integration', 1, 5)
GO
INSERT [dbo].[ProjectMaster] ([ProjectID], [ProjectName], [ProjDescription], [IsActive], [ProjectTypeID]) VALUES (13, N'YUVA SATHI', N'PRD & YUVA KALYAN', 1, 6)
GO
INSERT [dbo].[ProjectMaster] ([ProjectID], [ProjectName], [ProjDescription], [IsActive], [ProjectTypeID]) VALUES (14, N'SUDM 5 Schemes', N'ULB', 1, 6)
GO
INSERT [dbo].[ProjectMaster] ([ProjectID], [ProjectName], [ProjDescription], [IsActive], [ProjectTypeID]) VALUES (15, N'PCF E-Admin (6-Modules)', N'PCF', 1, 6)
GO
INSERT [dbo].[ProjectMaster] ([ProjectID], [ProjectName], [ProjDescription], [IsActive], [ProjectTypeID]) VALUES (16, N'UPSRTC Portal', N'UPSRTC', 1, 6)
GO
SET IDENTITY_INSERT [dbo].[ProjectMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[ProjectType] ON 
GO
INSERT [dbo].[ProjectType] ([ProjectTypeID], [ProjectType], [IsActive]) VALUES (4, N'AMC', 1)
GO
INSERT [dbo].[ProjectType] ([ProjectTypeID], [ProjectType], [IsActive]) VALUES (5, N'Current Project', 1)
GO
INSERT [dbo].[ProjectType] ([ProjectTypeID], [ProjectType], [IsActive]) VALUES (6, N'New Business', 1)
GO
INSERT [dbo].[ProjectType] ([ProjectTypeID], [ProjectType], [IsActive]) VALUES (7, N'ACCOUNT', 1)
GO
INSERT [dbo].[ProjectType] ([ProjectTypeID], [ProjectType], [IsActive]) VALUES (8, N'HR', 1)
GO
INSERT [dbo].[ProjectType] ([ProjectTypeID], [ProjectType], [IsActive]) VALUES (9, N'ADMIN', 1)
GO
SET IDENTITY_INSERT [dbo].[ProjectType] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleMenuMappings] ON 
GO
INSERT [dbo].[RoleMenuMappings] ([MappingID], [UserRoleID], [MenuID], [CanView], [CanInsert], [CanUpdate], [CanDelete], [CreatedBy], [CreatedOn], [IsActive]) VALUES (1, 1, 1, 1, 1, 1, 1, 1, CAST(N'2024-02-06T12:19:04.093' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[RoleMenuMappings] OFF
GO
SET IDENTITY_INSERT [dbo].[SectorTypeMaster] ON 
GO
INSERT [dbo].[SectorTypeMaster] ([SectorTypeID], [SectorType], [IsActive]) VALUES (1, N'SECTOR TYPE - 1', 1)
GO
INSERT [dbo].[SectorTypeMaster] ([SectorTypeID], [SectorType], [IsActive]) VALUES (4, N'SECTOR TYPE - 2', 1)
GO
SET IDENTITY_INSERT [dbo].[SectorTypeMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[StateMaster] ON 
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (1, N'Andhra Pradesh', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (2, N'Arunachal Pradesh ', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (3, N'Assam ', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (4, N'Bihar', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (5, N'Chhattisgarh ', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (6, N'Goa ', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (7, N'Gujarat', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (8, N'Haryana', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (9, N'Himachal Pradesh ', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (10, N'Jharkhand ', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (11, N'Karnataka', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (12, N'Kerala ', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (13, N'Madhya Pradesh', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (14, N'Maharashtra', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (15, N'Manipur', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (16, N'Meghalaya', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (17, N'Mizoram', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (18, N'Nagaland', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (19, N'Odisha', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (20, N'Punjab', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (21, N'Rajasthan ', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (22, N'Sikkim ', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (23, N'Tamil Nadu', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (24, N'Telangana ', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (25, N'Tripura ', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (26, N'Uttar Pradesh', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (27, N'Uttarakhand', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [IsActive], [CreatedOn]) VALUES (28, N'West Bengal', NULL, 1, CAST(N'2024-02-07T15:12:55.870' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[StateMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[StatusMaster] ON 
GO
INSERT [dbo].[StatusMaster] ([StatusID], [Name], [IsActive]) VALUES (1, N'Submitted', 1)
GO
INSERT [dbo].[StatusMaster] ([StatusID], [Name], [IsActive]) VALUES (2, N'Approved', 1)
GO
INSERT [dbo].[StatusMaster] ([StatusID], [Name], [IsActive]) VALUES (3, N'Return', 1)
GO
INSERT [dbo].[StatusMaster] ([StatusID], [Name], [IsActive]) VALUES (4, N'Rejected', 1)
GO
SET IDENTITY_INSERT [dbo].[StatusMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[TaskUpdates] ON 
GO
INSERT [dbo].[TaskUpdates] ([UpdateID], [ref_AttendiesID], [UpdateRemark], [UpdateAttachment], [UpdateDate], [EntryDate], [IsActive], [ref_MeetingID], [IsTaskFinished]) VALUES (17, 29, N'I have updated Excel Sheet Successfully and send to Vivek Sir.', NULL, NULL, CAST(N'2023-06-27T16:15:13.737' AS DateTime), 1, 18, NULL)
GO
INSERT [dbo].[TaskUpdates] ([UpdateID], [ref_AttendiesID], [UpdateRemark], [UpdateAttachment], [UpdateDate], [EntryDate], [IsActive], [ref_MeetingID], [IsTaskFinished]) VALUES (18, 32, N'WORKING ON IT', NULL, NULL, CAST(N'2023-06-30T16:34:55.090' AS DateTime), 1, 19, NULL)
GO
INSERT [dbo].[TaskUpdates] ([UpdateID], [ref_AttendiesID], [UpdateRemark], [UpdateAttachment], [UpdateDate], [EntryDate], [IsActive], [ref_MeetingID], [IsTaskFinished]) VALUES (19, 33, N'INPROCESS', NULL, NULL, CAST(N'2023-06-30T16:35:35.327' AS DateTime), 1, 19, NULL)
GO
INSERT [dbo].[TaskUpdates] ([UpdateID], [ref_AttendiesID], [UpdateRemark], [UpdateAttachment], [UpdateDate], [EntryDate], [IsActive], [ref_MeetingID], [IsTaskFinished]) VALUES (20, 35, N'INPROCESS', NULL, NULL, CAST(N'2023-06-30T16:51:13.653' AS DateTime), 1, 20, NULL)
GO
INSERT [dbo].[TaskUpdates] ([UpdateID], [ref_AttendiesID], [UpdateRemark], [UpdateAttachment], [UpdateDate], [EntryDate], [IsActive], [ref_MeetingID], [IsTaskFinished]) VALUES (21, 35, N'TEST324343', NULL, NULL, CAST(N'2023-06-30T16:51:32.823' AS DateTime), 1, 20, NULL)
GO
INSERT [dbo].[TaskUpdates] ([UpdateID], [ref_AttendiesID], [UpdateRemark], [UpdateAttachment], [UpdateDate], [EntryDate], [IsActive], [ref_MeetingID], [IsTaskFinished]) VALUES (22, 36, N'WORKING', NULL, NULL, CAST(N'2023-06-30T16:51:45.153' AS DateTime), 1, 20, NULL)
GO
INSERT [dbo].[TaskUpdates] ([UpdateID], [ref_AttendiesID], [UpdateRemark], [UpdateAttachment], [UpdateDate], [EntryDate], [IsActive], [ref_MeetingID], [IsTaskFinished]) VALUES (23, 39, N'test update 1', NULL, NULL, CAST(N'2023-06-30T23:54:13.203' AS DateTime), 1, 21, 0)
GO
INSERT [dbo].[TaskUpdates] ([UpdateID], [ref_AttendiesID], [UpdateRemark], [UpdateAttachment], [UpdateDate], [EntryDate], [IsActive], [ref_MeetingID], [IsTaskFinished]) VALUES (24, 39, N'test update 2', NULL, NULL, CAST(N'2023-06-30T23:54:28.200' AS DateTime), 1, 21, 0)
GO
INSERT [dbo].[TaskUpdates] ([UpdateID], [ref_AttendiesID], [UpdateRemark], [UpdateAttachment], [UpdateDate], [EntryDate], [IsActive], [ref_MeetingID], [IsTaskFinished]) VALUES (25, 39, N'task update 3', NULL, NULL, CAST(N'2023-06-30T23:55:29.700' AS DateTime), 1, 21, 0)
GO
INSERT [dbo].[TaskUpdates] ([UpdateID], [ref_AttendiesID], [UpdateRemark], [UpdateAttachment], [UpdateDate], [EntryDate], [IsActive], [ref_MeetingID], [IsTaskFinished]) VALUES (26, 39, N'task update 4', NULL, NULL, CAST(N'2023-06-30T23:59:47.113' AS DateTime), 1, 21, 1)
GO
INSERT [dbo].[TaskUpdates] ([UpdateID], [ref_AttendiesID], [UpdateRemark], [UpdateAttachment], [UpdateDate], [EntryDate], [IsActive], [ref_MeetingID], [IsTaskFinished]) VALUES (27, 40, N'test update 1 for task 2', NULL, NULL, CAST(N'2023-07-01T00:12:48.943' AS DateTime), 1, 21, 0)
GO
INSERT [dbo].[TaskUpdates] ([UpdateID], [ref_AttendiesID], [UpdateRemark], [UpdateAttachment], [UpdateDate], [EntryDate], [IsActive], [ref_MeetingID], [IsTaskFinished]) VALUES (28, 44, N'test updates on 4 feb 2024', N'/Documents/UsersDocs/Update_Attachment_44-2024-03-02--18-56-11.png', NULL, CAST(N'2024-02-03T18:56:11.397' AS DateTime), 1, 24, 0)
GO
SET IDENTITY_INSERT [dbo].[TaskUpdates] OFF
GO
SET IDENTITY_INSERT [dbo].[UserLogins] ON 
GO
INSERT [dbo].[UserLogins] ([LoginID], [RoleID], [UserName], [Password], [EmployeeID], [IsActive], [DistID]) VALUES (1, 1, N'admin@gmail.com', N'Bj6TAvspjgfdLwqPmnppwA==', 1, 1, NULL)
GO
INSERT [dbo].[UserLogins] ([LoginID], [RoleID], [UserName], [Password], [EmployeeID], [IsActive], [DistID]) VALUES (2, 2, N'RG0005', N'v9Mqvqcv0f8=', 5, 1, NULL)
GO
INSERT [dbo].[UserLogins] ([LoginID], [RoleID], [UserName], [Password], [EmployeeID], [IsActive], [DistID]) VALUES (3, 2, N'RG0006', N'sHeKS7hJ0pQrHT5RrTzr8g==', 6, 1, NULL)
GO
INSERT [dbo].[UserLogins] ([LoginID], [RoleID], [UserName], [Password], [EmployeeID], [IsActive], [DistID]) VALUES (4, 2, N'RG0007', N'v9Mqvqcv0f8=', 7, 1, NULL)
GO
INSERT [dbo].[UserLogins] ([LoginID], [RoleID], [UserName], [Password], [EmployeeID], [IsActive], [DistID]) VALUES (5, 2, N'RG0008', N'v9Mqvqcv0f8=', 8, 1, NULL)
GO
INSERT [dbo].[UserLogins] ([LoginID], [RoleID], [UserName], [Password], [EmployeeID], [IsActive], [DistID]) VALUES (6, 2, N'RG0009', N'v9Mqvqcv0f8=', 9, 1, NULL)
GO
INSERT [dbo].[UserLogins] ([LoginID], [RoleID], [UserName], [Password], [EmployeeID], [IsActive], [DistID]) VALUES (7, 2, N'RG0010', N'v9Mqvqcv0f8=', 10, 1, NULL)
GO
INSERT [dbo].[UserLogins] ([LoginID], [RoleID], [UserName], [Password], [EmployeeID], [IsActive], [DistID]) VALUES (8, 2, N'RG0011', N'v9Mqvqcv0f8=', 11, 1, NULL)
GO
INSERT [dbo].[UserLogins] ([LoginID], [RoleID], [UserName], [Password], [EmployeeID], [IsActive], [DistID]) VALUES (9, 2, N'RG0012', N'v9Mqvqcv0f8=', 12, 1, NULL)
GO
INSERT [dbo].[UserLogins] ([LoginID], [RoleID], [UserName], [Password], [EmployeeID], [IsActive], [DistID]) VALUES (10, 2, N'RG0013', N'v9Mqvqcv0f8=', 13, 1, NULL)
GO
INSERT [dbo].[UserLogins] ([LoginID], [RoleID], [UserName], [Password], [EmployeeID], [IsActive], [DistID]) VALUES (11, 2, N'myakansh786@gmail.com', N'v9Mqvqcv0f8=', 14, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[UserLogins] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 
GO
INSERT [dbo].[UserRoles] ([RoleID], [RoleName]) VALUES (1, N'Admin')
GO
INSERT [dbo].[UserRoles] ([RoleID], [RoleName]) VALUES (2, N'Employee')
GO
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
ALTER TABLE [dbo].[BankMaster] ADD  CONSTRAINT [DF__BankMaste__IsAct__35BCFE0A]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[EmployeeRegistration] ADD  DEFAULT ((1)) FOR [AllowEdit]
GO
ALTER TABLE [dbo].[MeetingAttendies] ADD  DEFAULT ((0)) FOR [IsTaskFinished]
GO
ALTER TABLE [dbo].[MeetingInfo] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[RoleMenuMappings] ADD  DEFAULT ((0)) FOR [CanView]
GO
ALTER TABLE [dbo].[RoleMenuMappings] ADD  DEFAULT ((0)) FOR [CanInsert]
GO
ALTER TABLE [dbo].[RoleMenuMappings] ADD  DEFAULT ((0)) FOR [CanUpdate]
GO
ALTER TABLE [dbo].[RoleMenuMappings] ADD  DEFAULT ((0)) FOR [CanDelete]
GO
ALTER TABLE [dbo].[StatusMaster] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[TaskUpdates] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[TaskUpdates] ADD  DEFAULT ((0)) FOR [IsTaskFinished]
GO
