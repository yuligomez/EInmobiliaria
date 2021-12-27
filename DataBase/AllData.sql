SELECT * FROM Apartments;
set IDENTITY_INSERT Apartments ON
INSERT INTO Apartments (Id, Name, Description, Latitude, Longitude, State)
VALUES (1, 'Acevedo Diaz', 'Apartamento con vista al mar' , '-34.9116573', '-56.169921599999995', 'REGISTERED');
INSERT INTO Apartments (Id, Name, Description, Latitude, Longitude, State)
VALUES (2, 'Gonzalo Ramirez', 'Casa con fondo' , '-34.902296', '-56.188375', 'REGISTERED');
INSERT INTO Apartments (Id, Name, Description, Latitude, Longitude, State)
VALUES (3, 'Pablo de Maria', 'Apartamento amplio' , '-34.865682', '-56.155243', 'REGISTERED');
INSERT INTO Apartments (Id, Name, Description, Latitude, Longitude, State)
VALUES (4, 'Edil Hugo Prato', 'Loft centrico' , '-34.875058', '-56.095345', 'REGISTERED');
INSERT INTO Apartments (Id, Name, Description, Latitude, Longitude, State)
VALUES (5, 'Ramon Marquez', 'Apartamento con terraza' , '-34.860269', '-56.014836', 'REGISTERED');
set IDENTITY_INSERT Apartments OFF


SELECT * FROM Users;
set IDENTITY_INSERT Users ON
INSERT INTO Users (Id, Name, Email, Password, Role)
VALUES (1, 'Valentina','vale@gmail.com','vale1234', 'ADMIN');
INSERT INTO Users (Id, Name, Email, Password, Role)
VALUES (2, 'Jose','jose@gmail.com','jose1234', 'ADMIN');
INSERT INTO Users (Id, Name, Email, Password, Role)
VALUES (3, 'Sami','sami@gmail.com','sami1234', 'ADMIN');
INSERT INTO Users (Id, Name, Email, Password, Role)
VALUES (4, 'Penny','penny@gmail.com','penny1234', 'CHECKER');
INSERT INTO Users (Id, Name, Email, Password, Role)
VALUES (5, 'Sheldon','sheldon@gmail.com','sheldon1234', 'CHECKER');
INSERT INTO Users (Id, Name, Email, Password, Role)
VALUES (6, 'Yuliana','yuli@gmail.com','yuli1234', 'CHECKER');
set IDENTITY_INSERT Users OFF

SELECT * FROM Checks;


SELECT * FROM Photos;

SELECT * FROM Elements;
set IDENTITY_INSERT Elements ON
INSERT INTO Elements (Id, Name, Amount, IsBroken, PhotoId, ApartmentId, UserId)
VALUES (1,'Tenedor', 6, 0, NULL, 1, 1);
INSERT INTO Elements (Id, Name, Amount, IsBroken, PhotoId, ApartmentId, UserId)
VALUES (2,'Cuchillo', 6, 0, NULL, 1, 6);
INSERT INTO Elements (Id, Name, Amount, IsBroken, PhotoId, ApartmentId, UserId)
VALUES (3,'Mesa', 1, 0, NULL, 2, 2);
INSERT INTO Elements (Id, Name, Amount, IsBroken, PhotoId, ApartmentId, UserId)
VALUES (4,'Sillas', 6, 0, NULL, 2, 2);
INSERT INTO Elements (Id, Name, Amount, IsBroken, PhotoId, ApartmentId, UserId)
VALUES (5,'Sillon', 1, 0, NULL, 1, 1);
INSERT INTO Elements (Id, Name, Amount, IsBroken, PhotoId, ApartmentId, UserId)
VALUES (6,'Vasos', 2, 0, NULL, 1, 1);
set IDENTITY_INSERT Elements OFF


SELECT * FROM Rentals;
set IDENTITY_INSERT Rentals ON
INSERT INTO Rentals (Id, ApartmentId, StartDate, EndingDate, HasCheck)
VALUES (1, 1, '2020/1/1', '2020/2/2', 0);
INSERT INTO Rentals (Id, ApartmentId, StartDate, EndingDate, HasCheck)
VALUES (2, 2, '2020/3/3', '2020/4/4', 0);
INSERT INTO Rentals (Id, ApartmentId, StartDate, EndingDate, HasCheck)
VALUES (3, 3, '2020/5/5', '2020/6/6', 0);
INSERT INTO Rentals (Id, ApartmentId, StartDate, EndingDate, HasCheck)
VALUES (4, 4, '2020/1/1', '2020/2/2', 0);
INSERT INTO Rentals (Id, ApartmentId, StartDate, EndingDate, HasCheck)
VALUES (5, 5, '2022/7/7', '2022/8/8', 0);
set IDENTITY_INSERT Rentals OFF

SELECT * FROM Sessions;
