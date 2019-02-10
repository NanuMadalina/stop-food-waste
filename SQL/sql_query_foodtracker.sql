
create table USERS(
id_user int not null IDENTITY(1,1) primary key,
fullname varchar not null,
email varchar not null,
age int not null,
gender varchar not null)

drop table product_category
create table PRODUCT_CATEGORIES(
id_category int not null IDENTITY(1,1) primary key,
category_name varchar not null
)


drop table products
create table PRODUCTS(
id_product int not null IDENTITY(1,1) primary key,
product_name varchar not null,
product_description varchar not null,
quantity float not null,
unit_of_measurement varchar not null,
id_category int not null,
CONSTRAINT product_category1 FOREIGN KEY (id_category)     
    REFERENCES PRODUCT_CATEGORIES (id_category)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE    )

