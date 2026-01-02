USE [KlantenSimulator]
GO
/****** Object:  Table [dbo].[Achternaam]    Script Date: 1/2/2026 4:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Achternaam](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[versieid] [int] NOT NULL,
	[naam] [nvarchar](50) NOT NULL,
	[frequency] [float] NULL,
 CONSTRAINT [PK_Achternaam] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gemeente]    Script Date: 1/2/2026 4:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gemeente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[versieid] [int] NOT NULL,
	[naam] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Gemeente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GemeenteInstellingen]    Script Date: 1/2/2026 4:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GemeenteInstellingen](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[naam] [nchar](10) NOT NULL,
	[percentage] [float] NOT NULL,
	[siminstellingid] [int] NOT NULL,
 CONSTRAINT [PK_GemeenteInstellingen] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Land]    Script Date: 1/2/2026 4:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Land](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[naam] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Land] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SimulatieInfo]    Script Date: 1/2/2026 4:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SimulatieInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[aanmaakdatum] [datetime] NOT NULL,
	[aantalaangemaakt] [int] NOT NULL,
	[jongsteleeftijd] [int] NOT NULL,
	[oudsteleeftijd] [int] NOT NULL,
	[gemiddeldeleeftijd] [float] NOT NULL,
	[versieid] [int] NOT NULL,
	[opdrachtgever] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SimulatieInfo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SimulatieInstellingen]    Script Date: 1/2/2026 4:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SimulatieInstellingen](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[land] [nvarchar](50) NOT NULL,
	[aantalklanten] [int] NOT NULL,
	[minleeftijd] [int] NOT NULL,
	[maxleeftijd] [int] NOT NULL,
	[opdrachtgever] [nvarchar](50) NOT NULL,
	[maxhuisnummer] [int] NOT NULL,
	[percentagemetletter] [float] NOT NULL,
	[siminfoid] [int] NOT NULL,
 CONSTRAINT [PK_SimulatieInstellingen] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SimulatieKlant]    Script Date: 1/2/2026 4:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SimulatieKlant](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[voornaam] [nvarchar](100) NOT NULL,
	[achternaam] [nvarchar](100) NOT NULL,
	[gender] [nvarchar](20) NOT NULL,
	[voornaamkans] [nvarchar](50) NOT NULL,
	[achternaamkans] [nvarchar](50) NOT NULL,
	[gemeente] [nvarchar](100) NOT NULL,
	[straat] [nvarchar](200) NOT NULL,
	[huisnummer] [nvarchar](50) NOT NULL,
	[geboortedatum] [datetime] NOT NULL,
	[siminfoid] [int] NOT NULL,
 CONSTRAINT [PK_SimulatieKlant] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Straat]    Script Date: 1/2/2026 4:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Straat](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[versieid] [int] NOT NULL,
	[gemeenteid] [int] NULL,
	[naam] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Straat] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Versie]    Script Date: 1/2/2026 4:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Versie](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[landenid] [int] NOT NULL,
	[versie] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Versie] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voornaam]    Script Date: 1/2/2026 4:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voornaam](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[versieid] [int] NOT NULL,
	[naam] [nvarchar](50) NOT NULL,
	[gender] [nvarchar](50) NULL,
	[frequency] [float] NULL,
 CONSTRAINT [PK_Voornaam] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Achternaam]  WITH NOCHECK ADD  CONSTRAINT [FK_Achternaam_Versie] FOREIGN KEY([versieid])
REFERENCES [dbo].[Versie] ([id])
GO
ALTER TABLE [dbo].[Achternaam] CHECK CONSTRAINT [FK_Achternaam_Versie]
GO
ALTER TABLE [dbo].[Gemeente]  WITH CHECK ADD  CONSTRAINT [FK_Gemeente_Versie] FOREIGN KEY([versieid])
REFERENCES [dbo].[Versie] ([id])
GO
ALTER TABLE [dbo].[Gemeente] CHECK CONSTRAINT [FK_Gemeente_Versie]
GO
ALTER TABLE [dbo].[GemeenteInstellingen]  WITH CHECK ADD  CONSTRAINT [FK_GemeenteInstellingen_SimulatieInstellingen] FOREIGN KEY([siminstellingid])
REFERENCES [dbo].[SimulatieInstellingen] ([id])
GO
ALTER TABLE [dbo].[GemeenteInstellingen] CHECK CONSTRAINT [FK_GemeenteInstellingen_SimulatieInstellingen]
GO
ALTER TABLE [dbo].[SimulatieInfo]  WITH CHECK ADD  CONSTRAINT [FK_SimulatieInfo_Versie] FOREIGN KEY([versieid])
REFERENCES [dbo].[Versie] ([id])
GO
ALTER TABLE [dbo].[SimulatieInfo] CHECK CONSTRAINT [FK_SimulatieInfo_Versie]
GO
ALTER TABLE [dbo].[SimulatieInstellingen]  WITH CHECK ADD  CONSTRAINT [FK_SimulatieInstellingen_SimulatieInfo] FOREIGN KEY([siminfoid])
REFERENCES [dbo].[SimulatieInfo] ([id])
GO
ALTER TABLE [dbo].[SimulatieInstellingen] CHECK CONSTRAINT [FK_SimulatieInstellingen_SimulatieInfo]
GO
ALTER TABLE [dbo].[SimulatieKlant]  WITH CHECK ADD  CONSTRAINT [FK_SimulatieKlant_SimulatieInfo] FOREIGN KEY([siminfoid])
REFERENCES [dbo].[SimulatieInfo] ([id])
GO
ALTER TABLE [dbo].[SimulatieKlant] CHECK CONSTRAINT [FK_SimulatieKlant_SimulatieInfo]
GO
ALTER TABLE [dbo].[Straat]  WITH NOCHECK ADD  CONSTRAINT [FK_Straat_Gemeente] FOREIGN KEY([gemeenteid])
REFERENCES [dbo].[Gemeente] ([id])
GO
ALTER TABLE [dbo].[Straat] CHECK CONSTRAINT [FK_Straat_Gemeente]
GO
ALTER TABLE [dbo].[Straat]  WITH NOCHECK ADD  CONSTRAINT [FK_Straat_Versie] FOREIGN KEY([versieid])
REFERENCES [dbo].[Versie] ([id])
GO
ALTER TABLE [dbo].[Straat] CHECK CONSTRAINT [FK_Straat_Versie]
GO
ALTER TABLE [dbo].[Versie]  WITH CHECK ADD  CONSTRAINT [FK_Versie_Land] FOREIGN KEY([landenid])
REFERENCES [dbo].[Land] ([id])
GO
ALTER TABLE [dbo].[Versie] CHECK CONSTRAINT [FK_Versie_Land]
GO
ALTER TABLE [dbo].[Voornaam]  WITH NOCHECK ADD  CONSTRAINT [FK_Voornaam_Versie] FOREIGN KEY([versieid])
REFERENCES [dbo].[Versie] ([id])
GO
ALTER TABLE [dbo].[Voornaam] CHECK CONSTRAINT [FK_Voornaam_Versie]
GO
