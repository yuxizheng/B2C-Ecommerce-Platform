CREATE DATABASE B2C --�������ݿ�
USE B2C
CREATE TABLE Users
(UserID	CHAR(8) PRIMARY KEY,
UserName VARCHAR(16),
gender CHAR(1),
birthday DATE,
tel CHAR(11),
UserPwd VARCHAR(18),
province VARCHAR(15))
--�����û�������

CREATE TABLE Businesses
(BusinessID	CHAR(8) PRIMARY KEY,
ShopName VARCHAR(50),
OprName VARCHAR(16),
BusinessPwd VARCHAR(18))
--�����̼һ�����

CREATE TABLE Producttype
(TypeID CHAR(12) PRIMARY KEY,
type VARCHAR(18))
--������Ʒ���������

CREATE TABLE Products
(ProductID CHAR (12) PRIMARY KEY,
BusinessID CHAR(8),
TypeID CHAR(12),
name VARCHAR(18),
unitprice REAL,
introduction TEXT,
image VARCHAR(200),
inventory SMALLINT,
FOREIGN KEY(BusinessID)REFERENCES Businesses(BusinessID),
FOREIGN KEY(TypeID)REFERENCES Producttype(TypeID))
--������Ʒ������

CREATE TABLE Orders
(OrderID CHAR(12) PRIMARY KEY,
UserID CHAR(8),
BusinessID CHAR(8),
Address CHAR(100),
amount REAL,
OrderDate DATETIME,
FOREIGN KEY(UserID)REFERENCES Users(UserID),
FOREIGN KEY(BusinessID)REFERENCES Businesses(BusinessID))
--��������������

CREATE TABLE Orderdetail
(DetailID CHAR(12) PRIMARY KEY,
OrderID CHAR(12),
ProductID CHAR (12),
unitprice REAL,
quantity INTEGER,
FOREIGN KEY(OrderID)REFERENCES Orders(OrderID),
FOREIGN KEY(ProductID)REFERENCES Products(ProductID))
--����������ϸ������

CREATE TABLE Comments
(CommentID INT IDENTITY(1,1) PRIMARY KEY,
DetailID CHAR(12),
comment TEXT,
date DATETIME,
FOREIGN KEY(DetailID)REFERENCES Orderdetail(DetailID))
--�������ۻ�����

CREATE TABLE Cart
(CartID CHAR(12) PRIMARY KEY,
UserID CHAR(8),
ProductID CHAR(12),
ProductQuantity INTEGER,
FOREIGN KEY(UserID)REFERENCES Users(UserID),
FOREIGN KEY(ProductID)REFERENCES Products(ProductID))
--�������ﳵ������

GO
CREATE TRIGGER tgr_orders --����������ϵ���Ĵ�����
ON Orderdetail
FOR INSERT
AS
DECLARE @DetailID CHAR(12),@OrderID CHAR(12),@ProductID CHAR(12),@unitprice REAL,@quantity INTEGER;
SELECT @DetailID=DetailID,@OrderID=OrderID,@ProductID=ProductID,@unitprice=unitprice,@quantity=quantity
FROM INSERTED;
  IF((SELECT inventory FROM Products WHERE @ProductID=ProductID)>=@quantity)
  BEGIN
    UPDATE Products
    SET inventory=inventory-@quantity
    where ProductID=@ProductID;
  END
  ELSE 
  BEGIN
    PRINT('��治�㣡')
    ROLLBACK
  END
GO

GO
CREATE TRIGGER update_inventory --�����̼��޸Ŀ��Ĵ�����
ON Products 
FOR update
AS
BEGIN
	declare @newinv int
	select @newinv = inventory from inserted;
	if (@newinv <0)
		print('�޸�ʧ�ܣ�')
		rollback
END
GO

/*��������*/
use B2C
go
--clean all datas in B2C
truncate table dbo.Cart
truncate table dbo.Comments
truncate table dbo.Address
truncate table dbo.Orderdetail
truncate table dbo.Products
delete from dbo.Producttype
delete from dbo.Orders
delete from dbo.Businesses
delete from dbo.Users
go
--insert dbo.Users
bulk insert dbo.Users
from 'D:\SQLdata\Users.txt'
with
(
FirstRow = 2,
CODEPAGE = '936',
DATAFILETYPE = 'widechar',
fieldterminator='\t',
rowterminator='\n')
go
--insert dbo.Businesses
bulk insert dbo.Businesses
from 'D:\SQLdata\Businesses.txt'
with
(
FirstRow = 2,
CODEPAGE = '936',
DATAFILETYPE = 'widechar',
fieldterminator='\t',
rowterminator='\n')
go
--insert dbo.Producttype
bulk insert dbo.Producttype
from 'D:\SQLdata\Producttype.txt'
with
(
FirstRow = 2,
CODEPAGE = '936',
DATAFILETYPE = 'widechar',
fieldterminator='\t',
rowterminator='\n')
go
--insert dbo.Products
bulk insert dbo.Products
from 'D:\SQLdata\Products.txt'
with
(
FirstRow = 2,
CODEPAGE = '936',
DATAFILETYPE = 'widechar',
fieldterminator='\t',
rowterminator='\n')
go
--insert dbo.Orders
bulk insert dbo.Orders
from 'D:\SQLdata\Orders.txt'
with
(
FirstRow = 2,
CODEPAGE = '936',
DATAFILETYPE = 'widechar',
fieldterminator='\t',
rowterminator='\n')
go
--insert dbo.Orderdetail
bulk insert dbo.Orderdetail
from 'D:\SQLdata\Orderdetail.txt'
with
(
FirstRow = 2,
CODEPAGE = '936',
DATAFILETYPE = 'widechar',
fieldterminator='\t',
rowterminator='\n')
go
--insert dbo.Address
bulk insert dbo.Address
from 'D:\SQLdata\Address.txt'
with
(
FirstRow = 2,
CODEPAGE = '936',
DATAFILETYPE = 'widechar',
fieldterminator='\t',
rowterminator='\n')
go
--insert dbo.Comments
bulk insert dbo.Comments
from 'D:\SQLdata\Comments.txt'
with
(
FirstRow = 2,
CODEPAGE = '936',
DATAFILETYPE = 'widechar',
fieldterminator='\t',
rowterminator='\n')
go
--insert dbo.Cart
bulk insert dbo.Cart
from 'D:\SQLdata\Cart.txt'
with
(
FirstRow = 2,
CODEPAGE = '936',
DATAFILETYPE = 'widechar',
fieldterminator='\t',
rowterminator='\n')
go



