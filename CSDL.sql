CREATE DATABASE KARAOKE
GO
USE KARAOKE
GO

CREATE TABLE ACCOUNT
(
	username NVARCHAR(100) PRIMARY KEY,	
	displayname NVARCHAR(100) NOT NULL,
	password NVARCHAR(1000) NOT NULL DEFAULT 0,
	type INT NOT NULL  DEFAULT 0 -- 1: admin && 0: staff
)
GO

CREATE TABLE ROOM
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Phòng chưa có tên',
	price FLOAT NOT NULL DEFAULT 0,
	status NVARCHAR(100) NOT NULL DEFAULT N'Trống'	-- Trống || Có người
)
GO

CREATE TABLE FOODCATEGORY
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO

CREATE TABLE FOOD
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id)
)
GO

CREATE TABLE BILL
(
	id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	idRoom INT NOT NULL,
	status INT NOT NULL DEFAULT 0, -- 1: đã thanh toán && 0: chưa thanh toán
	discount int,
	totalprice float
	
	FOREIGN KEY (idRoom) REFERENCES ROOM(id)
)
GO

CREATE TABLE BILLINFO
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	count INT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
	FOREIGN KEY (idFood) REFERENCES dbo.Food(id)
)
GO

SELECT * FROM ROOM
INSERT INTO ROOM(name,price) VALUES (N'Phòng 1',100000)
INSERT INTO ROOM(name,price) VALUES (N'Phòng 2',100000)
INSERT INTO ROOM(name,price) VALUES (N'Phòng 3',100000)
INSERT INTO ROOM(name,price) VALUES (N'Phòng 4',100000)
INSERT INTO ROOM(name,price) VALUES (N'Phòng 5',100000)
INSERT INTO ROOM(name,price) VALUES (N'Phòng 6',100000)
INSERT INTO ROOM(name,price) VALUES (N'Phòng 7',100000)
INSERT INTO ROOM(name,price) VALUES (N'Phòng 8',100000)
INSERT INTO ROOM(name,price) VALUES (N'Phòng 9',100000)
INSERT INTO ROOM(name,price) VALUES (N'Phòng 10',100000)
INSERT INTO ROOM(name,price) VALUES (N'Phòng 11',100000)
INSERT INTO ROOM(name,price) VALUES (N'Phòng 12',100000)
update room	set status = N'Có người' where id = 8 

INSERT INTO FOODCATEGORY(name) VALUES (N'Trái cây')
INSERT INTO FOODCATEGORY(name) VALUES (N'Nước giải khát')
INSERT INTO FOODCATEGORY(name) VALUES (N'Bánh kẹo')
INSERT INTO FOODCATEGORY(name) VALUES (N'Thức ăn chế biến')


INSERT INTO FOOD(name, idCategory, price) VALUES (N'Combo trái cây dĩa',1, 120000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Xoài non chấm muối ớt',1, 40000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Ổi nữ hoàng',1, 35000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Dưa hấu ướp lạnh',1, 45000)

INSERT INTO FOOD(name, idCategory, price) VALUES (N'Cà phê đen đá',2, 20000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Cà phê sữa đá',2, 22000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Sting',2, 20000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Cà phê 247',2, 20000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Bia 333',2, 25000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Bia Heneiken',2, 27000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'7 up',2, 20000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Cocacola',2, 20000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Chanh đá',2, 15000)

INSERT INTO FOOD(name, idCategory, price) VALUES (N'Khô mực',3, 80000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Khô bò',3, 95000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Snack ',3, 10000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Chuối sấy',3, 22000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Mít sấy',3, 25000)

INSERT INTO FOOD(name, idCategory, price) VALUES (N'Mì xào thịt bò',4, 40000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Gỏi xoài khô cá sặc',4, 80000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Mực nướng muối ớt ',4, 85000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Tôm nướng muối ớt',4, 85000)
INSERT INTO FOOD(name, idCategory, price) VALUES (N'Chả ram chiên',4, 60000)

INSERT INTO ACCOUNT VALUES ('admin', N'Quản trị viên', '1', 1)
INSERT INTO ACCOUNT VALUES ('staff1', N'Bùi Trọng Tín', '1', 0)
INSERT INTO ACCOUNT VALUES ('staff2', N'Nguyễn Văn A', '1', 0)

INSERT INTO BILL(DateCheckIn, DateCheckOut, idRoom, status, discount, totalprice) 
			VALUES (GETDATE(), NULL, 2, 1, 10, 99000)
INSERT INTO BILL(DateCheckIn, DateCheckOut, idRoom, status, discount, totalprice) 
			VALUES (GETDATE(), NULL, 3, 1, 20, 50000)
INSERT INTO BILL(DateCheckIn, DateCheckOut, idRoom, status, discount, totalprice) 
			VALUES (GETDATE(), NULL, 2, 0, 0, 75000)

INSERT INTO BILLINFO(idBill, idFood, count) VALUES(1, 2, 1)
INSERT INTO BILLINFO(idBill, idFood, count) VALUES(1, 3, 2)
INSERT INTO BILLINFO(idBill, idFood, count) VALUES(2, 4, 3)
INSERT INTO BILLINFO(idBill, idFood, count) VALUES(2, 2, 1)
INSERT INTO BILLINFO(idBill, idFood, count) VALUES(3, 2, 1)
INSERT INTO BILLINFO(idBill, idFood, count) VALUES(3, 2, 2)

select * from FOOD
select * from FOODCATEGORY
select * from ACCOUNT
select * from BILL
select * from BILLINFO


CREATE PROC USP_GetRoomList
AS SELECT *	FROM food
GO
EXEC USP_GetRoomList