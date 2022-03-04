--��Ա����--
--1 ��Աע��
declare @UserID char(8),@UserName varchar(16),@gender char(1),@birthday varchar(8),@tel char(11),@UserPwd varchar(18),@province varchar(15)
insert into Users (UserID,UserName,gender,birthday,tel,Userpwd,province) values(@UserID,@UserName,@gender,@birthday,@tel,@Userpwd,@province)

--2 ��ѯ������Ϣ����Ʒ
declare @keyword varchar(8), @shopname varchar(8)
select p.productid, p.name,p.unitprice,p.introduction,p.inventory,p.image,b.shopname from products p,Businesses b where p.BusinessID = b.BusinessID and name like '%'+@keyword+ '%' and b.ShopName like '%' + @shopname + '%'

--3 �Է�����������Ʒ������ѡȡ����˵�ǰ���
select t.* from
(select p.productid, p.name,p.unitprice,p.introduction,p.inventory,p.image,b.shopname,rank() over(order by p.unitprice) as rank_num from products p,Businesses b where p.BusinessID = b.BusinessID and name like '%" + keyword + "%' and b.ShopName like '%" + shopname + "%')
t where rank_num < 6

--4 ѡȡ������Ʒ
select p.productid, p.name,p.unitprice,p.introduction,p.inventory,p.image,b.shopname from products p,Businesses b where p.BusinessID = b.BusinessID

--��������--

--1 ��ʾ�û������ж���ϸ��
select o.OrderDate,o.OrderID,od.DetailID,p.name,o.Address FROM Orders o,Orderdetail od, Products p where p.ProductID = od.ProductID and od.OrderID = o.OrderID and o.UserID=@userid

--2 ����Ʒ���ƣ���ʾ�ض��û�����Ĵ���������
select name,BusinessID,count(name) as frequency,rank() over(order by count(name) desc) as rank from (SELECT p.name, o.BusinessID FROM Orders o, Orderdetail od, Products p where p.ProductID = od.ProductID and od.OrderID = o.OrderID and o.UserID = @userid) as a group by name,BusinessID

--3 ���ﳵ��
exec check_out_final '00000004','street'
select UserID,ProductID,ProductQuantity from Cart where UserID='00000004'

--���۲���--

--1 Ϊcommentid��userid��productid��������������comment��ͼ����ʾ������Ϣ--
if exists(select * from sys.views where name='AllID_Comment')
--ɾ��������ͼ
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

--2 �����û�����ʾ��������
declare @userid char(8)
set @userid = '00000004'
select c.date,ac.UserID,c.comment from AllID_Comment ac,dbo.Comments c 
where ac.CommentID=c.CommentID and ac.UserID=@userid;

--3 ����productid������
create index ip_businessid on Products(Businessid);
go

--4 ������Ʒ����ʾ��������
declare @productid char(12)
set @productid = '000000000001'
select c.CommentID, ac.DetailID, c.comment, c.date from AllID_Comment ac,dbo.Comments c
where ac.CommentID=c.CommentID and ac.ProductID=@productid;

--�̼Ҳ���--
--1 ��ʾ�����̼ҵ���������������Ʒ
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

--2 ��ʾ�̼������۶�
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

--3 ��ѯ��䣺����������
declare @businessid char(8)
select * from orders where businessid = @businessid;
go
declare @businessid char(8)
select * from products where businessid = @businessid;
go

--������--
--1 ��ʾÿ���û���������������Ʒ
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

--2 ��ʾÿ��ʡ�Ļ�Ա�������Ϣ
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
