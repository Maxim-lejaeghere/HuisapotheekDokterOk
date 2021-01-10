USE huisapotheek
GO
CREATE TABLE patient
(
patientid int NOT NULL PRIMARY KEY IDENTITY(1,1),
voornaam nvarchar(30) NOT NULL,
achternaam nvarchar(50) NOT NULL,
geboortedatumdatum date NOT NULL,
email nvarchar(100)
)
GO