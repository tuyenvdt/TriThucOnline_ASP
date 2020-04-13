USE master

create DATABASE SQL_TriThucOnline_BanSach
-- ON PRIMARY
-- (NAME = N'SQL_TriThucOnline_BanSach', FILENAME = 'G:\TTFC\Learn_ASP_NET\TRITHUCONLINE_FINAL\Data_TriThucOnline\SQL_TriThucOnline_BanSach.mdf', SIZE = 100, MAXSIZE = 2GB, FILEGROWTH = 10)

-- LOG ON
-- (NAME = N'SQL_TriThucOnline_BanSach_Log', FILENAME = 'G:\TTFC\Learn_ASP_NET\TRITHUCONLINE_FINAL\Data_TriThucOnline\SQL_TriThucOnline_BanSach_Log.ldf', SIZE = 200, MAXSIZE = UNLIMITED, FILEGROWTH = 20)


GO

USE SQL_TriThucOnline_BanSach

GO

create table NXB
(
    MaNXB int IDENTITY(1,1) PRIMARY KEY,
    TenNXB nvarchar(50),
)
create table TACGIA
(
    MaTG int IDENTITY(1,1) PRIMARY KEY,
    TenTG nvarchar(50),
    Thongtingioithieu nvarchar(max),
    PicTG varchar(max),
)
create TABLE THELOAI
(
    MaTL int IDENTITY(1,1) PRIMARY KEY,
    TenTL nvarchar(50),
)

create table KHACHHANG
(
    IDUser int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Username nvarchar(50),
    Diachi nvarchar(100),
    SDT int,
    Email varchar(100),
    Password varchar (100),
    PicUser varchar(max),
    UserType INT
)

create table DAUSACH
(
    Masach int IDENTITY(1,1) PRIMARY KEY ,
    Tensach nvarchar(50),
    MaNXB int
        CONSTRAINT FKNAME
FOREIGN KEY(MaNXB) REFERENCES NXB(MaNXB) ON DELETE CASCADE ,
    MaTL int FOREIGN KEY REFERENCES THELOAI(MaTL) ON DELETE CASCADE ,
    MaTG int FOREIGN KEY REFERENCES TACGIA(MaTG) ON DELETE CASCADE ,
    Namxuatban int,
    Price float,
    PicBook varchar(max),
    Sotrang int,
    Bocucsach nvarchar(max),
    Trichdan nvarchar(max),

)
create table HOADON
(
    Mahd int IDENTITY(1,1) PRIMARY KEY ,
    Ngaymuahang datetime,
    IDUser int FOREIGN KEY REFERENCES KHACHHANG(IDUser) ON DELETE CASCADE
    ,
)

CREATE TABLE CT_HOADON
(
    Mahd INT FOREIGN KEY REFERENCES HOADON(Mahd),
    Masach int FOREIGN KEY REFERENCES DAUSACH(Masach) ON DELETE CASCADE,
    SoLuong INT,
    DonGia FLOAT(53),
    GiamGia FLOAT(53),
    ThanhTien FLOAT(53)
    PRIMARY KEY(Mahd, Masach)
)

