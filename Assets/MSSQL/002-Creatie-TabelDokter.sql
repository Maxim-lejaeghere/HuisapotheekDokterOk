USE huisapotheek

GO 

CREATE TABLE dokter
(

	dokterid int NOT NULL PRIMARY KEY IDENTITY(1,1),
	voornaam nvarchar(30) NOT NULL,
	achternaam nvarchar(50) NOT NULL,
	straat nvarchar(80) NOT NULL,
	huisnummer nvarchar(4),
	bus nvarchar(4),
	postcode nvarchar(15),
	stad nvarchar(30),
	land nvarchar(30),
	telefoon nvarchar(20),
	mobiel nvarchar (20),
	email nvarchar(100),
	reservatieurl nvarchar(256)
)
GO


