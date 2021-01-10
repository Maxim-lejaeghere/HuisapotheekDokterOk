USE huisapotheek
GO

CREATE TABLE medicijn
(
medicijnid int NOT NULL PRIMARY KEY IDENTITY(1,1),
volledigenaam nvarchar(150) NOT NULL,
groep nvarchar(50) NOT NULL,
vervaldatum date NOT NULL,
opVoorschrift bit NOT NULL,
postcode nvarchar(15),
bijsluiter nvarchar(256),
urlBijsluiter nvarchar(256),
dokterid int FOREIGN KEY REFERENCES dokter(dokterid)
)

GO