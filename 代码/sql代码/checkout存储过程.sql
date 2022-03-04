--create random string----
CREATE VIEW v_rand
AS
SELECT RAND() AS val;
go
CREATE OR ALTER FUNCTION rand_num(@n INT)
RETURNS INT
AS BEGIN
    SELECT @n=@n * val FROM v_rand
    RETURN @n
END;
go
CREATE OR ALTER FUNCTION random_string(
  @num INT,
  @chars VARCHAR(1024) = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz'
) RETURNS VARCHAR(1024)
AS
BEGIN
  DECLARE @res_str VARCHAR(1024) = ''
  DECLARE @i INT=0

  WHILE(@i<@num)BEGIN
        SET @res_str = @res_str + SUBSTRING(@chars, FLOOR(dbo.rand_num(len(@chars))) + 1, 1)
        SET @i = @i + 1
  END
  
  RETURN @res_str
END;
go
--create type table----
if exists(select 1 from sys.types where name='Cart_MultiID')
	drop type dbo.Cart_MultiID
go
create type dbo.Cart_MultiID as table
(
CartID char(12) null,
UserID char(8) not null,
ProductID char(12) not null,
ProductQuantity int not null,
new_inventory int null default -1,  --new inventory
amount real default null,
BusinessID char(8) default null,
OrderID char(12) default null,
unitprice real default null
);
go

----Procedure Check inventory----
if exists(select * from sys.procedures where name='Check_inventory')
drop procedure dbo.Check_inventory;
go
create procedure Check_inventory
(@Carts_temp as Cart_MultiID readonly)
as
begin
	select ProductID 
	from @Carts_temp 
	where new_inventory < 0;
end
go

select * from Cart

----Procedure Buy products----
if exists(select * from sys.procedures where name='Buy')
drop procedure dbo.Buy;
go
create procedure Buy
(@Carts_temp as Cart_MultiID readonly, @userid as char(8), @addr as char(100))
as
begin
declare @orderdate_temp Date=Convert(varchar(20), getdate(),101)
--generate Orders
begin
	insert into Orders(OrderID, BusinessID, amount, UserID, OrderDate, Address) 
	select distinct OrderID, BusinessID, amount, UserID,  @orderdate_temp, @addr
	from @Carts_temp
end

--generate Orderdetails
begin
	insert into Orderdetail
	select dbo.random_string(12, default), OrderID, ProductID, unitprice, ProductQuantity 
	from @Carts_temp
end

--update Products.inventory
begin
	update Products
	set inventory = ctemp.new_inventory
	from Products, @Carts_temp ctemp
	where Products.ProductID = ctemp.ProductID;
end
end

go

--final procedure to check out
if exists(select * from sys.procedures where name='check_out_final')
drop procedure dbo.check_out_final;
go
create procedure check_out_final 
(@UserID char(8), @Address char(100)) 
as 
begin
declare @Carts_temp as dbo.Cart_MultiID
insert into @Carts_temp (UserID,ProductID,ProductQuantity) (select UserID,ProductID,ProductQuantity from Cart where UserID=@UserID) 
select * from @Carts_temp

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
begin tran;
update @Carts_temp
set new_inventory = Products.inventory-ProductQuantity, unitprice=Products.unitprice, BusinessID=Products.BusinessID
from @Carts_temp ctemp, Products with(updlock, holdlock)
where ctemp.ProductID = Products.ProductID;

with temp 
as (select BusinessID, sum(ctemp.ProductQuantity * ctemp.unitprice) as amount
from @Carts_temp ctemp 
group by ctemp.BusinessID)

update @Carts_temp
set amount = temp.amount
from temp, @Carts_temp ctemp
where ctemp.BusinessID = temp.BusinessID;

declare @idtemp table(BusinessID char(8), OrderID char(12))
insert into @idtemp(BusinessID) select distinct BusinessID from @Carts_temp
update @idtemp
set OrderID = dbo.random_string(12, default)

update @Carts_temp
set OrderID = idtemp.OrderID
from @idtemp idtemp, @Carts_temp ctemp
where ctemp.BusinessID = idtemp.BusinessID;

begin
if not exists(select ProductID from @Carts_temp where new_inventory < 0)
	begin
		exec Buy @Carts_temp,@UserID,@Address;
	end
select ProductID from @Carts_temp where new_inventory < 0;
waitfor delay '00:00:30';
commit tran;
end

end

-- example
exec check_out_final '00000004','street'
select UserID,ProductID,ProductQuantity from Cart where UserID='00000004';

go
