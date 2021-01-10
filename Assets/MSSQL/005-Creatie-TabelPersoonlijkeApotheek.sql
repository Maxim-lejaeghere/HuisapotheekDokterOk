USE huisapotheek
GO
CREATE TABLE persoonlijkeapotheek
(

	apotheekid int NOT NULL PRIMARY KEY IDENTITY(1,1),
	dosering nvarchar(50) NOT NULL,
	groep nvarchar(50) NOT NULL,
	actiefIngenomen bit NOT NULL,
	inApotheek bit NOT NULL,
	opmerkingen nvarchar(500),
	patientid int NOT NULL FOREIGN KEY REFERENCES patient(patientid),
	medicijnid int NOT NULL FOREIGN KEY REFERENCES medicijn(medicijnid)
)
GO