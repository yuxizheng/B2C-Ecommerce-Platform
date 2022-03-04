--会员部分--
--1 会员注册
declare @UserID char(8),@UserName varchar(16),@gender char(1),@birthday varchar(8),@tel char(11),@UserPwd varchar(18),@province varchar(15)
insert into Users (UserID,UserName,gender,birthday,tel,Userpwd,province) values(@UserID,@UserName,@gender,@birthday,@tel,@Userpwd,@province)

--2 查询符合信息的商品
declare @keyword varchar(8), @shopname varchar(8)
select p.productid, p.name,p.unitprice,p.introduction,p.inventory,p.image,b.shopname from products p,Businesses b where p.BusinessID = b.BusinessID and name like '%'+@keyword+ '%' and b.ShopName like '%' + @shopname + '%'

--3 对符合条件的商品排名，选取最便宜的前五个
select t.* from
(select p.productid, p.name,p.unitprice,p.introduction,p.inventory,p.image,b.shopname,rank() over(order by p.unitprice) as rank_num from products p,Businesses b where p.BusinessID = b.BusinessID and name like '%" + keyword + "%' and b.ShopName like '%" + shopname + "%')
t where rank_num < 6

--4 选取所有商品
select p.productid, p.name,p.unitprice,p.introduction,p.inventory,p.image,b.shopname from products p,Businesses b where p.BusinessID = b.BusinessID

--订单部分--

--1 显示用户的所有订单细节
select o.OrderDate,o.OrderID,od.DetailID,p.name,o.Address FROM Orders o,Orderdetail od, Products p where p.ProductID = od.ProductID and od.OrderID = o.OrderID and o.UserID=@userid

--2 按商品名称，显示特定用户购买的次数的排名
select name,BusinessID,count(name) as frequency,rank() over(order by count(name) desc) as rank from (SELECT p.name, o.BusinessID FROM Orders o, Orderdetail od, Products p where p.ProductID = od.ProductID and od.OrderID = o.OrderID and o.UserID = @userid) as a group by name,BusinessID

--3 购物车买单
exec check_out_final '00000004','street'
select UserID,ProductID,ProductQuantity from Cart where UserID='00000004'

--评价部分--

--1 为commentid，userid，productid创建索引，建立comment视图，显示评价信息--
if exists(select * from sys.views where name='AllID_Comment')
--删除单个视图
drop view AllID_Comment;
go
create view AllID_Comment WITH SCHEMABINDING 
as 
select c.CommentID, c.DetailID, o.OrderID, o.UserID, od.ProductID 
from dbo.Comments c,dbo.Orderdetail od, dbo.Orders o 
where od.OrderID=o.OrderID and c.DetailID=od.DetailID;
go
create unique clustered index ic_commentid on AllID_Comment(CommentID);
go
create index ic_userid on AllID_Comment(UserId);
go
create index ic_productid on AllID_Comment(ProductId);
go

--2 给定用户，显示所有评价
declare @userid char(8)
set @userid = '00000004'
select c.date,ac.UserID,c.comment from AllID_Comment ac,dbo.Comments c 
where ac.CommentID=c.CommentID and ac.UserID=@userid;

--3 建立productid的索引
create index ip_businessid on Products(Businessid);
go

--4 给定商品，显示所有评价
declare @productid char(12)
set @productid = '000000000001'
select c.CommentID, ac.DetailID, c.comment, c.date from AllID_Comment ac,dbo.Comments c
where ac.CommentID=c.CommentID and ac.ProductID=@productid;

--商家部分--
--1 显示给定商家的最热卖的三个商品
if exists(select * from sys.procedures where name='Business_top3')
drop procedure dbo.Business_top3;
go
create procedure Business_top3
@businessid char(8)
as
select * from
	(select BusinessID, ProductID, sum(quantity) sum_quantity,
	rank() over (partition by BusinessID order by sum(quantity) desc) as ranks
	from Orders, Orderdetail
	where Orders.OrderID = Orderdetail.OrderID
	group by BusinessID, ProductID
	) as t1 
where ranks <= 3 and BusinessID=@businessid;
go

--2 显示商家总销售额
if exists(select * from sys.procedures where name='Business_amout_year')
drop procedure dbo.Business_amout_year;
go
create procedure Business_amout_year
@year int,@businessid char(8)
as
select sum(amount) Sum
from Orders
where OrderDate between CONCAT(@year, '-01-01') and CONCAT(@year, '-12-31') and BusinessID=@businessid;
go

--3 查询语句：订单，评价
declare @businessid char(8)
select * from orders where businessid = @businessid;
go
declare @businessid char(8)
select * from products where businessid = @businessid;
go

--管理部分--
--1 显示每个用户购买最多的三个商品
if exists(select * from sys.procedures where name='User_purchasemost')
drop procedure dbo.User_purchasemost;
go
create procedure User_purchasemost
as
select * from
	(select UserID, ProductID, sum(quantity) Maxq, 
	rank() over (partition by UserID order by sum(quantity) desc) as ranks
	from Orders, Orderdetail
	where Orders.OrderID = Orderdetail.OrderID
	group by UserID, ProductID
	) as t1
where ranks <= 1;
go

--2 显示每个省的会员购买额信息
if exists(select * from sys.procedures where name='Province_detail')
drop procedure dbo.Province_detail;
go
create procedure Province_detail
as
select province, avg(UserSum) Avg_sales_amount, max(UserSum) Max_sales_amount, min(UserSum) Min_sales_amount
from (
select province, Users.UserID, sum(amount) UserSum
from dbo.Users, dbo.Orders
where Users.UserID = Orders.UserID
group by province, Users.UserID
) as user_sum_province
group by province
order by Avg_sales_amount desc;
go
