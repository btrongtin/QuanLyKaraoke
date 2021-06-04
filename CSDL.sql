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
	idCategory INT,
	price FLOAT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id) on delete set null
)
GO

CREATE TABLE BILL
(
	id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATETIME NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATETIME,
	idRoom INT,
	status INT NOT NULL DEFAULT 0, -- 1: đã thanh toán && 0: chưa thanh toán
	discount int,
	totalprice float
	
	FOREIGN KEY (idRoom) REFERENCES ROOM(id) on delete set null
)
GO

alter table bill add totalRoomPrice float
GO

alter table bill add totalFoodPrice float
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

CREATE PROC USP_GetRoomList
AS SELECT *	FROM ROOM
GO

CREATE PROC USP_Login
@userName nvarchar(100), @passWord nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName AND PassWord = @passWord
END
GO

CREATE PROC USP_InsertBill
@idRoom INT
AS
BEGIN
	INSERT dbo.Bill 
	        ( DateCheckIn ,
	          DateCheckOut ,
	          idRoom ,
	          status
	        )
	VALUES  ( GETDATE() , -- DateCheckIn - date
	          NULL , -- DateCheckOut - date
	          @idRoom , -- idTable - int
	          0  -- status - int
	        )
END
GO

CREATE PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @count INT
AS
BEGIN

	DECLARE @isExitsBillInfo INT
	DECLARE @foodCount INT = 1
	
	SELECT @isExitsBillInfo = id, @foodCount = b.count 
	FROM dbo.BillInfo AS b 
	WHERE idBill = @idBill AND idFood = @idFood

	IF (@isExitsBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @foodCount + @count
		IF (@newCount > 0)
			UPDATE dbo.BillInfo	SET count = @foodCount + @count WHERE idFood = @idFood
		ELSE
			DELETE dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
	END
	ELSE
	BEGIN
		INSERT	dbo.BillInfo
        ( idBill, idFood, count )
		VALUES  ( @idBill, -- idBill - int
          @idFood, -- idFood - int
          @count  -- count - int
          )
	END
END
GO

CREATE TRIGGER UTG_InsertBill
ON BILL FOR INSERT
AS
BEGIN
	DECLARE @idbill INT
	SELECT @idbill = ID FROM inserted
	DECLARE @idroom INT
	SELECT @idroom = idRoom FROM BILL WHERE id = @idbill AND status = 0
	UPDATE ROOM SET status = N'Có người' WHERE id = @idroom
END
GO

CREATE TRIGGER UTG_UpdateBillInfo
ON BILLINFO FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idbill INT
	SELECT @idbill = idBill FROM inserted
	DECLARE @idroom INT
	SELECT @idroom = idRoom FROM BILL WHERE id = @idbill AND status = 0
	UPDATE ROOM SET status = N'Có người' WHERE id = @idroom
END
GO

CREATE TRIGGER UTG_UpdateBill
ON BILL FOR UPDATE
AS
BEGIN
	DECLARE @idbill INT
	SELECT @idbill = id FROM inserted
	DECLARE @idroom INT
	SELECT @idroom = idRoom FROM BILL WHERE id = @idbill
	DECLARE @count INT = 0
	SELECT @count = COUNT(*) FROM BILL WHERE idRoom = @idroom AND status = 0
	IF (@count = 0)
		UPDATE ROOM SET status = N'Trống' WHERE id = @idroom
END
GO

CREATE TRIGGER UTG_DeleteBillInfo
ON BILLINFO FOR DELETE
AS
BEGIN
	DECLARE @idbillinfo INT
	DECLARE @idbill INT
	SELECT @idbillinfo = id ,@idbill = idBill FROM deleted

	DECLARE @idroom INT
	SELECT @idroom = idRoom FROM BILL WHERE id = @idbill

	DECLARE @count INT = 0

	SELECT @count = COUNT(*) FROM BILLINFO AS BI, BILL AS B WHERE B.id = BI.idBill AND B.id = @idbill AND B.status = 0

	IF(@count = 0)
		UPDATE ROOM SET status = N'Trống' WHERE id = @idroom
END
GO

CREATE PROC USP_GetListBillByDate
@datecheckin SMALLDATETIME, @datecheckout SMALLDATETIME
AS
BEGIN
	SELECT name as N'Tên phòng', DateCheckIn as N'Giờ vào', DateCheckOut as N'Giờ ra', totalFoodPrice as N'Tiền dịch vụ', totalRoomPrice as N'Tiền phòng',discount as N'Giảm giá (%)', totalprice as N'Tổng tiền'
	FROM BILL, ROOM
	WHERE DateCheckIn >= @datecheckin AND DateCheckOut <= @datecheckout
		AND BILL.status = 1
		AND BILL.idRoom = ROOM.id
END
GO

CREATE PROC USP_UpdateAccount
@userName NVARCHAR(100), @displayName NVARCHAR(100), @password NVARCHAR(100), @newPassword NVARCHAR(100)
AS
BEGIN
	DECLARE @isRightPass INT = 0
	
	SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE USERName = @userName AND PassWord = @password
	
	IF (@isRightPass = 1)
	BEGIN
		IF (@newpassword = NULL OR @newpassword = '')
			UPDATE ACCOUNT SET DisplayName = @displayName WHERE UserName = @userName
		ELSE
			UPDATE ACCOUNT SET DisplayName = @displayName, PassWord = @newPassword WHERE UserName = @userName
	end
END
GO

--FUNCTION doi chuoi ve khong dau de so sanh (phuc vu cho tim kiem)
CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END

--select  ROOM.name as N'Tên phòng', totalRoomPrice as N'Tiền phòng',discount as N'Giảm giá (%)', totalprice as N'Tổng tiền', FOOD.name as N'Tên món',count as N'Số lượng', FOOD.price as N'Đơn giá'
--from bill, BILLINFO, FOOD, ROOM
--where BILL.id = 1150
--and BILL.id = BILLINFO.idBill
--and FOOD.id = BILLINFO.idFood
--and ROOM.id = BILL.idRoom

--update bill set totalhour = cast((DateCheckOut - DateCheckIn) as time)

--SELECT cast(DateCheckOut as time) [time] --[time] la ten cot trong output
--FROM BILL
--where id = 117
