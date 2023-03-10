GO
IF EXISTS (SELECT name FROM sysdatabases WHERE name='QL_HDPHONGLAB')
	DROP DATABASE QL_HDPHONGLAB
GO
CREATE DATABASE QL_HDPHONGLAB
GO
USE QL_HDPHONGLAB
GO
-------------------------------------------TẠO TABLE PHÂN QUYỀN---------------------------------------------
CREATE TABLE PHANQUYEN
(
	MAQUYEN INT NOT NULL,
	TENQUYEN NVARCHAR(50) NULL,
	CONSTRAINT PK_MAQUYEN PRIMARY KEY(MAQUYEN),
)
	
-------------------------------------------TẠO TABLE NGƯỜI DÙNG-------------------------------------------
CREATE TABLE NGUOIDUNG
(
	MAND INT NOT NULL,
	USERNAME VARCHAR(15) NOT NULL,
	PASSWORD NVARCHAR(50) NOT NULL,
	CONFIRMPASSWORD NVARCHAR(50) NOT NULL,
	HOTEN NVARCHAR(50) NULL,
	EMAIL NVARCHAR(50) NULL,
	DIENTHOAI NVARCHAR(12) NULL,
	DIACHI NVARCHAR(MAX) NULL,
	MAQUYEN INT NULL,
	CONSTRAINT PK_NGUOIDUNG PRIMARY KEY(MAND),
	CONSTRAINT FK_NGUOIDUNG_PHANQUYEN FOREIGN KEY(MAQUYEN) REFERENCES PHANQUYEN(MAQUYEN),
)

-------------------------------------------TẠO TABLE ĐỔI MẬT KHẨU-------------------------------------------------
CREATE TABLE DOIMATKHAU
(
	MAND INT NOT NULL,
	EMAIL NVARCHAR(50) NOT NULL,
	PASSWORD NVARCHAR(50) NOT NULL,
	NEWPASSWORD NVARCHAR(50) NOT NULL,
	CONFIRMPASSWORD NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_DOIMATKHAU PRIMARY KEY(MAND),
	CONSTRAINT FK_DOIMATKHAU_NGUOIDUNG FOREIGN KEY(MAND) REFERENCES NGUOIDUNG(MAND),
)
-------------------------------------------TẠO TABLE PHÒNG LAB----------------------------------------
CREATE TABLE PHONGLAB
(
	MAPHLAB VARCHAR(20) NOT NULL,
	TENPHLAB NVARCHAR(MAX) NULL,
	DIADIEM NVARCHAR(MAX) NULL,
	GHICHU NVARCHAR(MAX) NULL,
	CONSTRAINT PK_PHLAB PRIMARY KEY(MAPHLAB),
)

--------------------------------------------TẠO TABLE PHÒNG BAN--------------------------------------------
CREATE TABLE PHONGBAN
(
	MAPHBAN VARCHAR(20) NOT NULL,
	TENPHBAN NVARCHAR(MAX) NULL,
	DIADIEM NVARCHAR(MAX) NULL,
	GHICHU NVARCHAR(MAX) NULL,
	CONSTRAINT PK_PHBAN PRIMARY KEY(MAPHBAN),
)

--------------------------------------------TẠO TABLE GIẢNG VIÊN---------------------------------------------
CREATE TABLE GIANGVIEN
(
	MAGV VARCHAR(20) NOT NULL,
	HOTEN NVARCHAR(MAX) NULL,
	NGAYSINH DATETIME NULL,
	GIOITINH NVARCHAR(6) NULL,
	SDT NVARCHAR(12) NULL,
	DIACHI NVARCHAR(MAX) NULL,
	MAPHBAN VARCHAR(20) NOT NULL,
	MAPHLAB VARCHAR(20) NOT NULL,
	VAITRO NVARCHAR(50) NULL,
	CONSTRAINT PK_GIANGVIEN PRIMARY KEY(MAGV),
	CONSTRAINT FK_GIANGVIEN_PHBAN FOREIGN KEY(MAPHBAN) REFERENCES PHONGBAN(MAPHBAN),
	CONSTRAINT FK_GIANGVIEN_PHLAB FOREIGN KEY(MAPHLAB) REFERENCES PHONGLAB(MAPHLAB),
)

---------------------------------------------TẠO TABLE PHIẾU XUẤT PHÒNG THÍ NGHIỆM-----------------------------------------------
CREATE TABLE PHIEUXUATPHONGLAB
(
	MAPXPHLAB INT NOT NULL,
	THOIGIANXUAT DATETIME NULL,
	NOIDUNG NVARCHAR(MAX) NULL,
	NGUOIXUAT NVARCHAR(MAX) NULL,
	MAPHLAB VARCHAR(20) NOT NULL,
	CHAPNHAN BIT NULL,
	NGUOICHAPNHAN NVARCHAR(MAX) NULL,
	GHICHU NVARCHAR(MAX) NULL,
	CONSTRAINT PK_PHIEUXUATPHONGLAB PRIMARY KEY(MAPXPHLAB),
	CONSTRAINT FK_PHIEUXUATPHONGLAB_PHLAB FOREIGN KEY(MAPHLAB) REFERENCES PHONGLAB(MAPHLAB),
)

---------------------------------------------TẠO TABLE SINH VIÊN-----------------------------------------------
CREATE TABLE SINHVIEN
(
	MASV VARCHAR(20) NOT NULL,
	TENSV NVARCHAR(MAX) NULL,
	NGAYSINH DATETIME NULL,
	GIOITINH NVARCHAR(6) NULL,
	MAPXPHLAB INT NOT NULL,
	CONSTRAINT PK_SINHVIEN PRIMARY KEY(MASV),
	CONSTRAINT FK_SINHVIEN_PHIEUXUATPHONGLAB FOREIGN KEY(MAPXPHLAB) REFERENCES PHIEUXUATPHONGLAB(MAPXPHLAB),
)

---------------------------------------------TẠO TABLE NHÀ SẢN XUẤT--------------------------------------------
CREATE TABLE NSX
(
	MANSX VARCHAR(20) NOT NULL,
	TENNSX NVARCHAR(MAX) NULL,
	SDT NVARCHAR(12) NULL,
	DIACHI NVARCHAR(MAX) NULL,
	CONSTRAINT PK_NSX PRIMARY KEY(MANSX)
)

---------------------------------------------TẠO TABLE NHÀ CUNG CẤP---------------------------------------------
CREATE TABLE NCC
(
	MANCC VARCHAR(20) NOT NULL,
	TENNCC NVARCHAR(MAX) NULL,
	SDT NVARCHAR(12) NULL,
	DIACHI NVARCHAR(MAX) NULL,
	CONSTRAINT PK_NCC PRIMARY KEY(MANCC),
)

----------------------------------------------TẠO TABLE LOẠI HOÁ CHẤT--------------------------------------------
CREATE TABLE LOAIHOACHAT
(
	MALHC INT NOT NULL,
	TENLHC NVARCHAR(100) NULL,
	TONGSOLUONG INT NULL,
	CONSTRAINT PK_LOAIHC PRIMARY KEY(MALHC),
)

---------------------------------------------TẠO TABLE HÓA CHẤT-----------------------------------------------
CREATE TABLE HOACHAT
(
	MAHC VARCHAR(20) NOT NULL,
	TENHC NVARCHAR(MAX) NULL,
	MALHC INT NULL,
	THONGSO INT NULL,
	CASNO NVARCHAR(MAX) NULL,
	DONVI NVARCHAR(MAX) NULL,
	LUONGNHAP INT NULL,
	LUONGXUAT int NULL,
	LUONGTON int NULL,
	LUONGTHANHLY INT NULL,
	GIANHAP FLOAT NULL,
	CONSTRAINT PK_HOACHAT PRIMARY KEY(MAHC),
	CONSTRAINT FK_HOACHAT_LOAIHC FOREIGN KEY(MALHC) REFERENCES LOAIHOACHAT(MALHC),
)

----------------------------------------------TẠO TABLE CHI TIẾT HÓA CHẤT------------------------------------------------
CREATE TABLE CT_HOACHAT
(
	MAHC VARCHAR(20) NOT NULL,
	SOLO INT NULL,
	XUATXU NVARCHAR(MAX) NULL,
	DONVI NVARCHAR(MAX) NULL,
	NGAYNHAP DATETIME NULL,
	SOPHIEUNHAP INT NULL,
	DVCUNGCAP NVARCHAR(MAX) NULL,
	NGAYHETHAN DATETIME NULL,
	CONSTRAINT PK_CT_HOACHAT PRIMARY KEY(MAHC),
	CONSTRAINT FK_CT_HOACHAT_HOACHAT FOREIGN KEY(MAHC) REFERENCES HOACHAT(MAHC),
)

-----------------------------------------TẠO TABLE DỰ TRÙ HÓA CHẤT-----------------------------------------------
CREATE TABLE DUTRUHOACHAT
(
	MAHC VARCHAR(20) NOT NULL,
	TENHC NVARCHAR(MAX) NULL,
	DACTA NVARCHAR(MAX) NULL,
	SOLUONG INT NULL,
	CONSTRAINT PK_DUTRUHOACHAT PRIMARY KEY(MAHC),
	CONSTRAINT FK_DUTRUHOACHAT_HOACHAT FOREIGN KEY(MAHC) REFERENCES HOACHAT(MAHC),
)

------------------------------------------------TẠO TABLE THIẾT BỊ---------------------------------------------------
CREATE TABLE THIETBI
(
	MATB VARCHAR(20) NOT NULL,
	TENTB NVARCHAR(MAX) NULL,
	QUICACH NVARCHAR(MAX) NULL,
	SLBANDAU INT NULL,
	SLXUAT INT NULL,
	SLTON INT NULL,
	SLTHANHLY INT NULL,
	TAPHUAN BIT NULL,
	CONSTRAINT PK_THIETBI PRIMARY KEY(MATB),
)

--------------------------------------------------TẠO TABLE CHI TIẾT THIẾT BI--------------------------------------------------
CREATE TABLE CT_THIETBI
(
	MATB VARCHAR(20) NOT NULL,
	SOLO INT NULL,
	SERIAL NVARCHAR(MAX) NULL,
	XUATXU NVARCHAR(MAX) NULL,
	DONVI NVARCHAR(MAX) NULL,
	LUONGNHAP INT NULL,
	LUONGXUAT INT NULL,
	LUONGTON INT NULL,
	NGAYNHAP DATETIME NULL,
	SOPHIEUNHAP INT NULL,
	DVCUNGCAP NVARCHAR(MAX) NULL,
	NGAYHETHAN DATETIME NULL,
	CONSTRAINT PK_CT_THIETBI PRIMARY KEY(MATB),
	CONSTRAINT FK_CT_THIETBI_THIETBI FOREIGN KEY(MATB) REFERENCES THIETBI(MATB),
)

-----------------------------------------TẠO TABLE DỰ TRÙ THIẾT BỊ-----------------------------------------------
CREATE TABLE DUTRUTHIETBI
(
	MATB VARCHAR(20) NOT NULL,
	TENTB NVARCHAR(MAX) NULL,
	DACTA NVARCHAR(MAX) NULL,
	SOLUONG INT NULL,
	CONSTRAINT PK_DUTRUTHIETBI PRIMARY KEY(MATB),
	CONSTRAINT FK_DUTRUTHIETBI_THIETBI FOREIGN KEY(MATB) REFERENCES THIETBI(MATB),
)

--------------------------------------------TẠO TABLE DỤNG CỤ----------------------------------------------------
CREATE TABLE DUNGCU
(
	MADC VARCHAR(20) NOT NULL,
	TENDC NVARCHAR(MAX) NULL,
	LUONGNHAP INT NULL,
	LUONGXUAT INT NULL,
	LUONGTON INT NULL,
	LUONGTHANHLY INT NULL,
	DVT NVARCHAR(MAX) NULL,
	NGAYNHAP DATETIME NULL,
	GIOSD INT NULL,
	CONSTRAINT PK_DUNGCU PRIMARY KEY (MADC)
)

----------------------------------------------TẠO TABLE CT_DỤNG CỤ-------------------------------------------------
CREATE TABLE CT_DUNGCU
(
	MADC VARCHAR(20) NOT NULL,
	SOLO INT NULL,
	XUATXU NVARCHAR(MAX) NULL,
	DONVI NVARCHAR(MAX) NULL,
	SOPHIEUNHAP INT NULL,
	CONSTRAINT PK_CT_DUNGCU PRIMARY KEY(MADC),
	CONSTRAINT FK_CT_DUNGCU_DUNGCU FOREIGN KEY(MADC) REFERENCES DUNGCU(MADC)
)

-----------------------------------------TẠO TABLE DỰ TRÙ DỤNG CỤ-----------------------------------------------
CREATE TABLE DUTRUDUNGCU
(
	MADC VARCHAR(20) NOT NULL,
	TENDC NVARCHAR(MAX) NULL,
	DACTA NVARCHAR(MAX) NULL,
	SOLUONG INT NULL,
	CONSTRAINT PK_DUTRUDUNGCU PRIMARY KEY(MADC),
	CONSTRAINT FK_DUTRUDUNGCU_DUNGCU FOREIGN KEY(MADC) REFERENCES DUNGCU(MADC),
)

-------------------------------------------------TẠO TABLE SỰ KIỆN-------------------------------------------------------
CREATE TABLE SUKIEN
(
	MASK INT NOT NULL,
	MAPHLAB VARCHAR(20) NOT NULL,
	MAGV VARCHAR(20) NOT NULL,
	BD DATETIME NULL,
	KT DATETIME NULL,
	ThemeColor NVARCHAR(10) NULL,
	MOTA NVARCHAR(max) NULL,
	CONSTRAINT PK_SK PRIMARY KEY (MASK),
	CONSTRAINT FK_SK_GV FOREIGN KEY(MAGV) REFERENCES GIANGVIEN(MAGV),
	CONSTRAINT FK_SK_PHLAB FOREIGN KEY(MAPHLAB) REFERENCES PHONGLAB(MAPHLAB)
)

-------------------------------------------------TẠO TABLE PHIẾU XUẤT----------------------------------------------------
CREATE TABLE PHIEUXUAT
(
	MAPX INT NOT NULL,
	THOIGIANXUAT INT NULL,
	NOIDUNG NVARCHAR(MAX) NULL,
	NGAYXUAT DATETIME NULL,
	MAPHBAN VARCHAR(20) NOT NULL,
	CHAPNHAN BIT NULL,
	NGUOICHAPNHAN NVARCHAR(MAX) NULL,
	TU NVARCHAR(50) NULL,
	DEN NVARCHAR(50) NULL,
	GHICHU NVARCHAR(MAX) NULL,
	CONSTRAINT PK_PHIEUXUAT PRIMARY KEY(MAPX),
	CONSTRAINT FK_PHIEUXUAT_PHONGBAN FOREIGN KEY(MAPHBAN) REFERENCES PHONGBAN(MAPHBAN),
	
)

-------------------------------------------------TẠO TABLE CHI TIẾT PHIẾU XUẤT--------------------------------------------------
CREATE TABLE CHITIETPHIEUXUAT
(
	MACTPX INT NOT NULL,
	MAHC VARCHAR(20) NULL,
	MATB VARCHAR(20) NULL,
	MADC VARCHAR(20) NULL,
	NGAYXUAT DATETIME NULL,
	CONSTRAINT PK_CTPX PRIMARY KEY(MACTPX),
	CONSTRAINT FK_CTPX_HC FOREIGN KEY(MAHC) REFERENCES HOACHAT(MAHC),
	CONSTRAINT FK_CTPX_TB FOREIGN KEY(MATB) REFERENCES THIETBI(MATB),
	CONSTRAINT FK_CTPX_DC FOREIGN KEY(MADC) REFERENCES DUNGCU(MADC),
)
GO
ALTER TABLE CHITIETPHIEUXUAT ADD  DEFAULT (getdate()) FOR NGAYXUAT
-------------------------------------------------TẠO TABLE XUẤT HOÁ CHẤT------------------------------------------------------
CREATE TABLE XUATHOACHAT
(
	MACTPX INT NOT NULL,
	MAHC_id VARCHAR(20) NOT NULL,
	SOLUONGXUAT INT NULL,
	CONSTRAINT PK_XUATHOACHAT_MAPX_MAHC PRIMARY KEY(MACTPX, MAHC_id),
	CONSTRAINT FK_XUATHOACHAT_CTPX FOREIGN KEY(MACTPX) REFERENCES CHITIETPHIEUXUAT(MACTPX),
)
-------------------------------------------------TẠO TABLE PHIẾU NHẬP-----------------------------------------------------
CREATE TABLE PHIEUNHAP
(
	MAPN INT NOT NULL,
	NGAYNHAP DATETIME NULL,
	NOIDUNG NVARCHAR(MAX) NULL,
	MANCC VARCHAR(20) NULL,
	NGUOINHAN NVARCHAR(MAX) NULL,
	MAPHLAB VARCHAR(20) NULL,
	TU NVARCHAR(50) NULL,
	DEN NVARCHAR(50) NULL,
	TONGTIEN FLOAT NULL,
	GHICHU NVARCHAR(MAX) NULL,
	CONSTRAINT PK_PHIEUNHAP PRIMARY KEY(MAPN),
	CONSTRAINT FK_PHIEUNHAP_PHONGLAB FOREIGN KEY(MAPHLAB) REFERENCES PHONGLAB(MAPHLAB),
	CONSTRAINT FK_PHIEUNHAP_NCC FOREIGN KEY(MANCC) REFERENCES NCC(MANCC),
)

-----------------------------------------------TẠO TABLE CHI TIẾT PHIẾU NHẬP----------------------------------------------------
CREATE TABLE CHITIETPHIEUNHAP
(
	MAPN INT NOT NULL,
	MAHC VARCHAR(20) NOT NULL,
	MATB VARCHAR(20) NOT NULL,
	MADC VARCHAR(20) NOT NULL,
	SOLUONGNHAP INT NULL,
	GIANHAP FLOAT NULL,
	THANHTIEN FLOAT NULL,
	CONSTRAINT PK_CTPN_PN PRIMARY KEY(MAPN),
	CONSTRAINT FK_CTPN_PN FOREIGN KEY(MAPN) REFERENCES PHIEUNHAP(MAPN),
	CONSTRAINT FK_CTPN_HC FOREIGN KEY(MAHC) REFERENCES HOACHAT(MAHC),
	CONSTRAINT FK_CTPN_TB FOREIGN KEY(MATB) REFERENCES THIETBI(MATB),
	CONSTRAINT FK_CTPN_DC FOREIGN KEY(MADC) REFERENCES DUNGCU(MADC),
)
-----------------------------------------------TẠO TABLE PHIẾU TRẢ-------------------------------------------
CREATE TABLE PHIEUTRA
(
	MAPT INT NOT NULL,
	NGAYTRA DATETIME NULL,
	NOIDUNG NVARCHAR(MAX) NULL,
	MAHC VARCHAR(20) NULL,
	NGUOITRA NVARCHAR(MAX) NULL,
	MAPHLAB VARCHAR(20) NULL,
	SLTRA int null,
	TU NVARCHAR(50) NULL,
	DEN NVARCHAR(50) NULL,
	GHICHU NVARCHAR(MAX) NULL,
	CONSTRAINT PK_PHIEUTRA PRIMARY KEY(MAPT),
	CONSTRAINT FK_PHIEUTRA_HOACHAT  FOREIGN KEY(MAHC) REFERENCES HOACHAT(MAHC),
	CONSTRAINT FK_PHIEUTRA_PHONGLAB FOREIGN KEY(MAPHLAB) REFERENCES PHONGLAB(MAPHLAB),
)

CREATE TABLE PHIEUMUON
(
	MAPM INT NOT NULL,
	NGAYMUON DATETIME NULL,
	NGAYTRA DATETIME NULL,
	NOIDUNG NVARCHAR(MAX) NULL,
	MAHC VARCHAR(20) NULL,
	NGUOITRA NVARCHAR(MAX) NULL,
	MAPHLAB VARCHAR(20) NULL,
	SLMUON int null,
	TU NVARCHAR(50) NULL,
	DEN NVARCHAR(50) NULL,
	TRANGTHAI int, -- 0 đã trả, 1 chưa trả
	GHICHU NVARCHAR(MAX) NULL,
	CONSTRAINT PK_PHIEUMUON PRIMARY KEY(MAPM),
	CONSTRAINT PK_PHIEUMUON_HOACHAT  FOREIGN KEY(MAHC) REFERENCES HOACHAT(MAHC),
	CONSTRAINT PK_PHIEUMUON_PHONGLAB FOREIGN KEY(MAPHLAB) REFERENCES PHONGLAB(MAPHLAB),
)

----------------------------------------------THÊM DỮ LIỆU-----------------------------------------------------
---------------------------------------------------------------------------------------------------------------
GO
--THÊM BẢNG PHÂN QUYỀN
INSERT INTO PHANQUYEN VALUES(1, N'TRƯỞNG KHOA')
INSERT INTO PHANQUYEN VALUES(2, N'GIẢNG VIÊN')
INSERT INTO PHANQUYEN VALUES(3, N'SINH VIÊN')
INSERT INTO PHANQUYEN VALUES(4, N'NHÂN VIÊN KHO')

GO
--THÊM BẢNG NGƯỜI DÙNG
INSERT INTO NGUOIDUNG VALUES(1, N'giaovien01', N'gv01', N'gv01', N'HOÀNG VĂN THỤ', N'hoangvanthu01@gmail.com', N'096854234', N'TÂN PHÚ', 2)
INSERT INTO NGUOIDUNG VALUES(2, N'giaovien02', N'gv02', N'gv02', N'HUỲNH HOÀNG HOA', N'huynhhoanghoa02@gmail.com', N'08965548', N'CỦ CHI', 2)
INSERT INTO NGUOIDUNG VALUES(3, N'truongkhoa03', N'tk03', N'tk03', N'NGUYỄN HOÀNG DƯƠNG', N'nguyenhoangduong03@gmail.com', N'096854234', N'PHÚ NHUẬN', 3)
INSERT INTO NGUOIDUNG VALUES(4, N'giaovien04', N'gv04', N'gv04', N'TRẦN VĂN HÀO', N'tranvanhao04@gmail.com', N'068545785', N'BÌNH THẠNH', 2)
INSERT INTO NGUOIDUNG VALUES(5, N'sinhvien05', N'sv05', N'sv05', N'PHAN THỊ BÍCH', N'phanthibich05@gmail.com', N'035485852', N'QUẬN 10', 3)
INSERT INTO NGUOIDUNG VALUES(6, N'nhanvienkho06', N'nvk06', N'nvk06', N'HOÀNG VĂN HOÀNG', N'hoangvanhoang@gmail.com', N'05558885', N'QUẬN 10', 4)

GO
--THÊM BẢNG ĐỔI MẬT KHẨU
INSERT INTO DOIMATKHAU VALUES(1, N'hoangvanthu01@gmail.com', N'gv01', N'123456', N'123456')
INSERT INTO DOIMATKHAU VALUES(2, N'huynhhoanghoa02@gmail.com', N'gv02', N'123456', N'123456')
INSERT INTO DOIMATKHAU VALUES(3, N'nguyenhoangduong03@gmail.com', N'tk03', N'123456', N'123456')
INSERT INTO DOIMATKHAU VALUES(4, N'tranvanhao04@gmail.com', N'gv04', N'123456', N'123456')
INSERT INTO DOIMATKHAU VALUES(5, N'phanthibich05@gmail.com', N'sv05', N'123456', N'123456')

GO
--THÊM BẢNG PHÒNG LAB
INSERT INTO PHONGLAB VALUES(N'LAB01', N'TTTNTH CÔNG NGHỆ THÔNG TIN', N'140 LÊ TRỌNG TẤN TPHCM QUẬN TÂN PHÚ', N'PHÒNG ĐANG NÂNG CẤP VÀ SỬA SANG')
INSERT INTO PHONGLAB VALUES(N'LAB02', N'TTTNTH CÔNG NGHIỆP THỰC PHẨM', N'140 LÊ TRỌNG TẤN TPHCM QUẬN TÂN PHÚ', N'PHÒNG ĐANG NÂNG CẤP VÀ SỬA SANG')
INSERT INTO PHONGLAB VALUES(N'LAB03', N'TTTNTH CÔNG NGHỆ HÓA HỌC', N'140 LÊ TRỌNG TẤN TPHCM QUẬN TÂN PHÚ', N'PHÒNG ĐANG NÂNG CẤP VÀ SỬA SANG')
INSERT INTO PHONGLAB VALUES(N'LAB04', N'TTTNTH ĐIỆN ĐIỆN TỬ', N'140 LÊ TRỌNG TẤN TPHCM QUẬN TÂN PHÚ', N'PHÒNG ĐANG NÂNG CẤP VÀ SỬA SANG')
INSERT INTO PHONGLAB VALUES(N'LAB05', N'TTTNTH NGOẠI NGỮ ', N'140 LÊ TRỌNG TẤN TPHCM QUẬN TÂN PHÚ', N'PHÒNG ĐANG NÂNG CẤP VÀ SỬA SANG')

GO
--THÊM BẢNG PHÒNG BAN
INSERT INTO PHONGBAN VALUES(N'KHOA01', N'KHOA CÔNG NGHỆ THÔNG TIN', N'LẦU B', N'CUNG CẤP THIẾT BỊ VÀ DỤNG CỤ CHO PHÒNG LAB')
INSERT INTO PHONGBAN VALUES(N'KHOA02', N'KHOA CÔNG NGHIỆP THỰC PHẨM', N'LẦU B', N'CUNG CẤP THIẾT BỊ VÀ DỤNG CỤ CHO PHÒNG LAB')
INSERT INTO PHONGBAN VALUES(N'KHOA03', N'KHOA CÔNG NGHỆ HÓA HỌC', N'LẦU F', N'CUNG CẤP THIẾT BỊ VÀ DỤNG CỤ CHO PHÒNG LAB')
INSERT INTO PHONGBAN VALUES(N'KHOA04', N'KHOA CÔNG NGHỆ ĐIỆN ĐIỆN TỬ', N'LẦU F', N'CUNG CẤP THIẾT BỊ VÀ DỤNG CỤ CHO PHÒNG LAB')
INSERT INTO PHONGBAN VALUES(N'KHOA05', N'KHOA CÔNG NGHỆ NGOẠI NGỮ', N'LẦU C', N'CUNG CẤP THIẾT BỊ VÀ DỤNG CỤ CHO PHÒNG LAB')

GO
--THÊM BẢNG GIẢNG VIÊN
INSERT INTO GIANGVIEN VALUES(N'GV01', N'HUỲNH HOÀNG HOA', N'1994-01-02', N'NỮ', N'09877837', N'TPHCM', N'KHOA01', N'LAB01', N'GIẢNG VIÊN')
INSERT INTO GIANGVIEN VALUES(N'GV02', N'TRẦN HUỲNH SA', N'1990-04-03', N'NỮ', N'08277837', N'CẦN GIỜ', N'KHOA02', N'LAB02', N'GIẢNG VIÊN')
INSERT INTO GIANGVIEN VALUES(N'GV03', N'HUỲNH HOÀNG CHƯƠNG', N'1989-11-12', N'NAM', N'09277837', N'CỦ CHI', N'KHOA03', N'LAB03', N'GIẢNG VIÊN')
INSERT INTO GIANGVIEN VALUES(N'GV04', N'PHAN CHÂU TRINH', N'1994-12-12', N'NỮ', N'06777837', N'TPHCM', N'KHOA05', N'LAB04', N'GIẢNG VIÊN')
INSERT INTO GIANGVIEN VALUES(N'TK05', N'NGUYỄN THANH NGUYÊN', N'1994-09-05', N'NAM', N'07217837', N'HÀ NỘI', N'KHOA05', N'LAB05', N'TRƯỞNG KHOA')

--THÊM BẢNG PHIẾU XUẤT PHÒNG LAB
INSERT INTO PHIEUXUATPHONGLAB VALUES(1, N'2022-09-09', N'ĐĂNG KÝ CHO SINH VIÊN MƯỢN PHÒNG', N'HUỲNH MỸ KHOA', N'LAB01', 1, N'NGUYỄN VÂN ANH', N'MƯỢN PHÒNG')
INSERT INTO PHIEUXUATPHONGLAB VALUES(2, N'2022-10-10', N'ĐĂNG KÝ CHO SINH VIÊN MƯỢN PHÒNG', N'PHAN MỸ KIỀU', N'LAB02', 1, N'NGUYỄN VÂN ANH', N'MƯỢN PHÒNG')
INSERT INTO PHIEUXUATPHONGLAB VALUES(3, N'2022-11-11', N'ĐĂNG KÝ CHO SINH VIÊN MƯỢN PHÒNG', N'HUỲNH MỸ KHOA', N'LAB03', 1, N'NGUYỄN VÂN ANH', N'MƯỢN PHÒNG')

GO
--THÊM BẢNG SINH VIÊN
INSERT INTO SINHVIEN VALUES(N'SV01', N'PHAN KIỀU', N'2001-09-09', N'NỮ', 1)
INSERT INTO SINHVIEN VALUES(N'SV02', N'PHAN KIỀU TRINH', N'2001-10-10', N'NỮ', 2)
INSERT INTO SINHVIEN VALUES(N'SV03', N'PHAN THANH TÚ', N'2001-02-06', N'NAM', 3)
INSERT INTO SINHVIEN VALUES(N'SV04', N'NGUYỄN THANH HÀ', N'2001-09-07', N'NAM', 1)
INSERT INTO SINHVIEN VALUES(N'SV05', N'PHAN CHU TRINH', N'2001-11-02', N'NỮ', 2)
INSERT INTO SINHVIEN VALUES(N'SV06', N'TRẦN TỐ NỮ', N'2001-09-02', N'NỮ', 3)
INSERT INTO SINHVIEN VALUES(N'SV07', N'TRẦN HOÀNG KHA', N'2001-12-12', N'NAM', 1)
INSERT INTO SINHVIEN VALUES(N'SV08', N'TỐNG ĐĂNG KHOA', N'2001-03-29', N'NAM', 2)
INSERT INTO SINHVIEN VALUES(N'SV09', N'KIỀU MINH TÚ', N'2001-02-02', N'NỮ', 3)
INSERT INTO SINHVIEN VALUES(N'SV10', N'TỐNG NGỌC NHƯ Ý', N'2001-03-07', N'NỮ', 1)
INSERT INTO SINHVIEN VALUES(N'SV11', N'PHAN MẠNH QUỲNH', N'2001-04-03', N'NAM', 2)
INSERT INTO SINHVIEN VALUES(N'SV12', N'SƠN THẠCH', N'2001-05-02', N'NAM', 3)
INSERT INTO SINHVIEN VALUES(N'SV13', N'TỐ HOA', N'2001-06-05', N'NỮ', 1)
INSERT INTO SINHVIEN VALUES(N'SV14', N'KIỀM MINH ANH', N'2001-07-12', N'NỮ', 2)
INSERT INTO SINHVIEN VALUES(N'SV15', N'TỐNG NGỌC NHƯ Ý', N'2001-08-10', N'NỮ', 3)
INSERT INTO SINHVIEN VALUES(N'SV16', N'TRẦN NGUYÊN GIÁP', N'2001-08-01', N'NAM', 1)
INSERT INTO SINHVIEN VALUES(N'SV17', N'PHẠM VĂN THÔNG', N'2001-01-10', N'NAM', 2)
INSERT INTO SINHVIEN VALUES(N'SV18', N'NGUYỄN HẢI QUANG', N'2001-03-10', N'NAM', 3)
INSERT INTO SINHVIEN VALUES(N'SV19', N'HỒ VĂN Ý', N'2001-08-09', N'NAM', 3)
INSERT INTO SINHVIEN VALUES(N'SV20', N'NGUYỄN MINH TRÍ', N'2001-02-10', N'NAM', 2)

GO
--THÊM BẢNG NHÀ SẢN XUẤT
INSERT INTO NSX VALUES(N'NSX01', N'NHÀ SẢN XUẤT HÓA CHẤT VIỆT KIỀU', N'03258892', N'ENGLAND')
INSERT INTO NSX VALUES(N'NSX02', N'NHÀ SẢN XUẤT THIẾT BỊ Á ÂU', N'05889525', N'AMERICA')
INSERT INTO NSX VALUES(N'NSX03', N'NHÀ SẢN XUẤT DỤNG CU JETPOST', N'0255692', N'GERMANY')
INSERT INTO NSX VALUES(N'NSX04', N'NHÀ SẢN XUẤT HÓA CHẤT MYSTERIOUS', N'087552152', N'INDIA')
INSERT INTO NSX VALUES(N'NSX05', N'NHÀ SẢN XUẤT CÔNG CỤ PHONEPIS', N'056632589', N'BRAZIL')

GO
--THÊM BẢNG NHÀ CUNG CẤP
INSERT INTO NCC VALUES(N'NCC01', N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'05632258', N'TPHCM')
INSERT INTO NCC VALUES(N'NCC02', N'NHÀ CUNG CẤP VÀ PHÂN PHỐI PHI PHỤNG', N'02555332', N'HÀ NỘI')
INSERT INTO NCC VALUES(N'NCC03', N'NHÀ CUNG CẤP VÀ PHÂN PHỐI MỸ HOÀNG GIA', N'05558872', N'CỦ CHI')
INSERT INTO NCC VALUES(N'NCC04', N'NHÀ CUNG CẤP VÀ PHÂN PHỐI PHAN HUY ÍCH', N'07896233', N'BÌNH THUẬN')
INSERT INTO NCC VALUES(N'NCC05', N'NHÀ CUNG CẤP VÀ PHÂN PHỐI PHAN CHÂU TRINH', N'01452235', N'BÌNH CHÁNH')

GO
--THÊM BẢNG LOẠI HOÁ CHẤT
INSERT INTO LOAIHOACHAT VALUES(1, N'NƯỚC', 20)
INSERT INTO LOAIHOACHAT VALUES(2, N'AXIT', 30)
INSERT INTO LOAIHOACHAT VALUES(3, N'AMONIAC', 10)
INSERT INTO LOAIHOACHAT VALUES(4, N'CACBON', 50)

GO
--THÊM BẢNG HÓA CHẤT
INSERT INTO HOACHAT VALUES(N'HC01', N'KALI HIDROXIT KOH', 1, 1300, N'SZHA0199', N'CHAI', 60, 10, 50, 0, 150000)
INSERT INTO HOACHAT VALUES(N'HC02', N'NATRI HIDROXIT NAOH', 2, 1200, N'JHXHJS112', N'CHAI', 60, 10, 50, 0, 120000)
INSERT INTO HOACHAT VALUES(N'HC03', N'BARI HIDROXIT BaOH', 3, 1100, N'NSHJ10293', N'CHAI',60, 10, 50, 0, 110000)
INSERT INTO HOACHAT VALUES(N'HC04', N'AMON CLORUA NH4CL', 4, 1000, N'JDHHSK123', N'CHAI', 60,10, 50, 0, 130000)
INSERT INTO HOACHAT VALUES(N'HC05', N'AMON SUNFAT (NH4)2SO4', 1, 900, N'DHSG1872', N'CHAI', 60,10, 50, 0, 140000)
INSERT INTO HOACHAT VALUES(N'HC06', N'AMON NITRAT NH4NO3', 2, 1400, N'HHDGG192', N'CHAI', 60,10, 50, 0, 170000)
INSERT INTO HOACHAT VALUES(N'HC07', N'NATRI NITRAT NANO3', 3, 1500, N'HDGYS182', N'CHAI',60, 10, 50, 0, 160000)
INSERT INTO HOACHAT VALUES(N'HC08', N'CANXI NITRAT CA(NO3)2', 4, 2200, N'HFWY1234', N'CHAI', 60,10, 50, 0, 180000)
INSERT INTO HOACHAT VALUES(N'HC09', N'KALI CLORUA KCL', 1, 1400, N'JDGSH1283', N'CHAI', 60,10, 50, 0, 190000)
INSERT INTO HOACHAT VALUES(N'HC10', N'KALI SUNFAT K2SO4', 2, 1500, N'HDJS1857', N'CHAI',60, 10, 50, 0, 200000)

GO
--THÊM BẢNG CT HÓA CHẤT
INSERT INTO CT_HOACHAT VALUES(N'HC01', 1, N'HÀN QUỐC', N'CHAI', N'2022-01-01', 1, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2023-01-01')
INSERT INTO CT_HOACHAT VALUES(N'HC02', 2, N'TRUNG QUỐC', N'CHAI', N'2022-02-02', 2, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI PHI PHỤNG', N'2023-02-02')
INSERT INTO CT_HOACHAT VALUES(N'HC03', 3, N'NHẬT BẢN', N'CHAI', N'2022-03-03', 3, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI MỸ HOÀNG GIA', N'2023-03-03')
INSERT INTO CT_HOACHAT VALUES(N'HC04', 4, N'THÁI LAN', N'CHAI', N'2022-04-04', 4, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI PHAN HUY ÍCH', N'2023-04-04')
INSERT INTO CT_HOACHAT VALUES(N'HC05', 5, N'SINGAPORE', N'CHAI', N'2022-05-05', 5, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI PHAN CHÂU TRINH', N'2023-05-05')
INSERT INTO CT_HOACHAT VALUES(N'HC06', 6, N'VIỆT NAM', N'CHAI', N'2022-06-06', 6, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI PHAN CHÂU TRINH', N'2023-06-06')
INSERT INTO CT_HOACHAT VALUES(N'HC07', 7, N'QUATA', N'CHAI', N'2022-07-07', 7, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI PHAN HUY ÍCH', N'2023-07-07')
INSERT INTO CT_HOACHAT VALUES(N'HC08', 8, N'DONGTIMOR', N'CHAI', N'2022-08-08', 8, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2023-08-08')
INSERT INTO CT_HOACHAT VALUES(N'HC09', 9, N'HÀ LAN', N'CHAI', N'2022-09-09', 9, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2023-09-09')
INSERT INTO CT_HOACHAT VALUES(N'HC10', 10, N'ANH QUỐC', N'CHAI', N'2022-10-10', 10, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI MỸ HOÀNG GIA', N'2023-10-10')

GO
--THÊM BẢNG DỰ TRÙ HÓA CHẤT
INSERT INTO DUTRUHOACHAT VALUES(N'HC01', N'KALI HIDROXIT KOH', N'TỔNG NHẬP XUẤT HÓA CHẤT TRONG NĂM NAY', 50)
INSERT INTO DUTRUHOACHAT VALUES(N'HC02', N'NATRI HIDROXIT NAOH', N'TỔNG NHẬP XUẤT HÓA CHẤT TRONG NĂM NAY', 60)
INSERT INTO DUTRUHOACHAT VALUES(N'HC03', N'BARI HIDROXIT BaOH', N'TỔNG NHẬP XUẤT HÓA CHẤT TRONG NĂM NAY', 70)
INSERT INTO DUTRUHOACHAT VALUES(N'HC04', N'AMON CLORUA NH4CL', N'TỔNG NHẬP XUẤT HÓA CHẤT TRONG NĂM NAY', 80)
INSERT INTO DUTRUHOACHAT VALUES(N'HC05', N'AMON SUNFAT (NH4)2SO4', N'TỔNG NHẬP XUẤT HÓA CHẤT TRONG NĂM NAY', 90)
INSERT INTO DUTRUHOACHAT VALUES(N'HC06', N'AMON NITRAT NH4NO3', N'TỔNG NHẬP XUẤT HÓA CHẤT TRONG NĂM NAY', 100)
INSERT INTO DUTRUHOACHAT VALUES(N'HC07', N'NATRI NITRAT NANO3', N'TỔNG NHẬP XUẤT HÓA CHẤT TRONG NĂM NAY', 40)
INSERT INTO DUTRUHOACHAT VALUES(N'HC08', N'CANXI NITRAT CA(NO3)2', N'TỔNG NHẬP XUẤT HÓA CHẤT TRONG NĂM NAY', 30)
INSERT INTO DUTRUHOACHAT VALUES(N'HC09', N'KALI CLORUA KCL', N'TỔNG NHẬP XUẤT HÓA CHẤT TRONG NĂM NAY', 20)
INSERT INTO DUTRUHOACHAT VALUES(N'HC10', N'KALI SUNFAT K2SO4', N'TỔNG NHẬP XUẤT HÓA CHẤT TRONG NĂM NAY', 10)

GO
--THÊM BẢNG THIẾT BỊ
INSERT INTO THIETBI VALUES(N'TB01', N'THÙNG MÁY CPU', N'SẢN PHẨM PHẢI ĐẠT LOẠI TỐT', 150, 30, 120, 0, 1)
INSERT INTO THIETBI VALUES(N'TB02', N'QUẠT TẢN NHIỆT', N'SẢN PHẨM PHẢI ĐẠT LOẠI TỐT', 150, 30, 120, 0, 1)
INSERT INTO THIETBI VALUES(N'TB03', N'CPU', N'SẢN PHẨM PHẢI ĐẠT LOẠI TỐT', 150, 30, 120, 0, 1)
INSERT INTO THIETBI VALUES(N'TB04', N'RAM', N'SẢN PHẨM PHẢI ĐẠT LOẠI TỐT', 150, 30, 120, 0,1)
INSERT INTO THIETBI VALUES(N'TB05', N'MAINBOARD', N'SẢN PHẨM PHẢI ĐẠT LOẠI TỐT', 150, 30, 120, 0, 1)
INSERT INTO THIETBI VALUES(N'TB06', N'Ổ CỨNG', N'SẢN PHẨM PHẢI ĐẠT LOẠI TỐT', 150, 30, 120, 0, 1)
INSERT INTO THIETBI VALUES(N'TB07', N'CARD ĐỒ HỌA', N'SẢN PHẨM PHẢI ĐẠT LOẠI TỐT', 150, 30, 120, 0, 1)
INSERT INTO THIETBI VALUES(N'TB08', N'DÂY CÁP', N'SẢN PHẨM PHẢI ĐẠT LOẠI TỐT', 150, 30, 120, 0, 1)
INSERT INTO THIETBI VALUES(N'TB09', N'BỘ NGUỒN MÁY TÍNH', N'SẢN PHẨM PHẢI ĐẠT LOẠI TỐT', 150, 30, 120, 0, 1)
INSERT INTO THIETBI VALUES(N'TB10', N'CỔNG KẾT NỐI', N'SẢN PHẨM PHẢI ĐẠT LOẠI TỐT', 150, 30, 120, 0, 1)
INSERT INTO THIETBI VALUES(N'TB11', N'CHUỘT', N'SẢN PHẨM PHẢI ĐẠT LOẠI TỐT', 150, 30, 120, 0, 0)
INSERT INTO THIETBI VALUES(N'TB12', N'MÀN HÌNH', N'SẢN PHẨM PHẢI ĐẠT LOẠI TỐT', 150, 30, 120, 0, 1)
INSERT INTO THIETBI VALUES(N'TB13', N'BÀN PHÍM', N'SẢN PHẨM PHẢI ĐẠT LOẠI TỐT', 150, 30, 120, 0, 1)
INSERT INTO THIETBI VALUES(N'TB14', N'ĐỒ LÓT CHUỘT', N'SẢN PHẨM PHẢI ĐẠT LOẠI TỐT', 150, 30, 120, 0, 0)
INSERT INTO THIETBI VALUES(N'TB15', N'CỔNG DÂY KẾT NỐI HDMI', N'SẢN PHẨM PHẢI ĐẠT LOẠI TỐT', 150, 30, 120, 0, 0)

GO
--THÊM BẢNG CT THIẾT BỊ
INSERT INTO CT_THIETBI VALUES(N'TB01', 123, N'OHHJW29093', N'TRUNG QUỐC', N'CÁI', 100, 20, 80, N'2022-09-08', 1, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2024-09-08')
INSERT INTO CT_THIETBI VALUES(N'TB02', 124, N'OBDGHSH281', N'TRUNG QUỐC', N'CÁI', 100, 20, 80, N'2022-09-08', 2, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2024-09-08')
INSERT INTO CT_THIETBI VALUES(N'TB03', 125, N'HDKSKAN892', N'TRUNG QUỐC', N'CÁI', 100, 20, 80, N'2022-09-08', 3, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2024-09-08')
INSERT INTO CT_THIETBI VALUES(N'TB04', 126, N'HJJDUS2345', N'TRUNG QUỐC', N'CÁI', 100, 20, 80, N'2022-09-08', 4, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2024-09-08')
INSERT INTO CT_THIETBI VALUES(N'TB05', 127, N'JAHDIIW231', N'TRUNG QUỐC', N'CÁI', 100, 20, 80, N'2022-09-08', 5, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2024-09-08')
INSERT INTO CT_THIETBI VALUES(N'TB06', 128, N'JDHAHA1111', N'TRUNG QUỐC', N'CÁI', 100, 20, 80, N'2022-09-08', 6, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2024-09-08')
INSERT INTO CT_THIETBI VALUES(N'TB07', 129, N'JWUQIAO123', N'TRUNG QUỐC', N'CÁI', 100, 20, 80, N'2022-09-08', 7, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2024-09-08')
INSERT INTO CT_THIETBI VALUES(N'TB08', 130, N'OHBDJW9123', N'TRUNG QUỐC', N'CÁI', 100, 20, 80, N'2022-09-08', 8, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2024-09-08')
INSERT INTO CT_THIETBI VALUES(N'TB09', 131, N'KJWIDK9213', N'TRUNG QUỐC', N'CÁI', 100, 20, 80, N'2022-09-08', 9, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2024-09-08')
INSERT INTO CT_THIETBI VALUES(N'TB10', 132, N'KDHHDWI985', N'TRUNG QUỐC', N'CÁI', 100, 20, 80, N'2022-09-08', 10, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2024-09-08')
INSERT INTO CT_THIETBI VALUES(N'TB11', 133, N'KDJAOLAL10', N'TRUNG QUỐC', N'CÁI', 100, 20, 80, N'2022-09-08', 11, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2024-09-08')
INSERT INTO CT_THIETBI VALUES(N'TB12', 134, N'LDAJK19203', N'TRUNG QUỐC', N'CÁI', 100, 20, 80, N'2022-09-08', 12, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2024-09-08')
INSERT INTO CT_THIETBI VALUES(N'TB13', 135, N'LLDJAOO125', N'TRUNG QUỐC', N'CÁI', 100, 20, 80, N'2022-09-08', 13, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2024-09-08')
INSERT INTO CT_THIETBI VALUES(N'TB14', 136, N'JAOSIDM178', N'TRUNG QUỐC', N'CÁI', 100, 20, 80, N'2022-09-08', 14, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2024-09-08')
INSERT INTO CT_THIETBI VALUES(N'TB15', 137, N'OHHJW29093', N'TRUNG QUỐC', N'CÁI', 100, 20, 80, N'2022-09-08', 15, N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN', N'2024-09-08')

GO
--THÊM BẢNG DỰ TRÙ THIẾT BỊ
INSERT INTO DUTRUTHIETBI VALUES(N'TB01', N'THÙNG MÁY CPU', N'TÍNH TOÁN SẢN LƯỢNG TRONG NĂM NAY', 100)
INSERT INTO DUTRUTHIETBI VALUES(N'TB02', N'QUẠT TẢN NHIỆT', N'TÍNH TOÁN SẢN LƯỢNG TRONG NĂM NAY', 50)
INSERT INTO DUTRUTHIETBI VALUES(N'TB03', N'CPU', N'TÍNH TOÁN SẢN LƯỢNG TRONG NĂM NAY', 70)
INSERT INTO DUTRUTHIETBI VALUES(N'TB04', N'RAM', N'TÍNH TOÁN SẢN LƯỢNG TRONG NĂM NAY', 80)
INSERT INTO DUTRUTHIETBI VALUES(N'TB05', N'MAINBOARD', N'TÍNH TOÁN SẢN LƯỢNG TRONG NĂM NAY', 40)

GO
--THÊM BẢNG DỤNG CỤ
INSERT INTO DUNGCU VALUES(N'DC01', N'CHẢO',140, 20, 120 , 0, N'CÁI', N'2022-06-04', 4)
INSERT INTO DUNGCU VALUES(N'DC02', N'DAO', 140, 20, 120, 0, N'CÁI', N'2022-06-03', 2)
INSERT INTO DUNGCU VALUES(N'DC03', N'ĐĨA', 140, 20, 120, 0, N'CÁI', N'2022-06-02', 4)
INSERT INTO DUNGCU VALUES(N'DC04', N'MUỖNG',140, 20, 120, 0, N'CÁI', N'2022-06-01', 3)
INSERT INTO DUNGCU VALUES(N'DC05', N'NỒI',140, 20, 120, 0 , N'CÁI', N'2022-06-06', 4)
INSERT INTO DUNGCU VALUES(N'DC06', N'TẠP DỀ',140, 20, 120, 0, N'CÁI', N'2022-06-07', 2)
INSERT INTO DUNGCU VALUES(N'DC07', N'NÊU', 140, 20, 120, 0, N'CÁI', N'2022-06-08', 1)
INSERT INTO DUNGCU VALUES(N'DC08', N'XON', 140, 20, 120, 0, N'CÁI', N'2022-06-09', 3)
INSERT INTO DUNGCU VALUES(N'DC09', N'VÁ', 140, 20, 120, 0, N'CÁI', N'2022-06-10', 2)
INSERT INTO DUNGCU VALUES(N'DC10', N'THỚT',140, 20, 120, 0, N'CÁI', N'2022-06-11', 1)
INSERT INTO DUNGCU VALUES(N'DC11', N'ĐŨA',140, 20, 120, 0 , N'CÁI', N'2022-06-12', 1)
INSERT INTO DUNGCU VALUES(N'DC12', N'CHÉN',140, 20, 120 , 0, N'CÁI', N'2022-06-01', 5)
INSERT INTO DUNGCU VALUES(N'DC13', N'DĨA',140, 20, 120 , 0, N'CÁI', N'2022-06-02', 3)
INSERT INTO DUNGCU VALUES(N'DC14', N'BẾP GA',140, 20, 120 , 0, N'CÁI', N'2022-06-03', 1)
INSERT INTO DUNGCU VALUES(N'DC15', N'GIA VỊ',140, 20, 120 , 0, N'CÁI', N'2022-06-04', 2)

GO
--THÊM BẢNG CT DỤNG CỤ
INSERT INTO CT_DUNGCU VALUES(N'DC01', 123, N'TRUNG QUỐC', N'CÁI', 1)
INSERT INTO CT_DUNGCU VALUES(N'DC02', 123, N'TRUNG QUỐC', N'CÁI', 2)
INSERT INTO CT_DUNGCU VALUES(N'DC03', 123, N'TRUNG QUỐC', N'CÁI', 3)
INSERT INTO CT_DUNGCU VALUES(N'DC04', 123, N'TRUNG QUỐC', N'CÁI', 4)
INSERT INTO CT_DUNGCU VALUES(N'DC05', 123, N'TRUNG QUỐC', N'CÁI', 5)
INSERT INTO CT_DUNGCU VALUES(N'DC06', 123, N'TRUNG QUỐC', N'CÁI', 6)
INSERT INTO CT_DUNGCU VALUES(N'DC07', 123, N'TRUNG QUỐC', N'CÁI', 7)
INSERT INTO CT_DUNGCU VALUES(N'DC08', 123, N'TRUNG QUỐC', N'CÁI', 8)
INSERT INTO CT_DUNGCU VALUES(N'DC09', 123, N'TRUNG QUỐC', N'CÁI', 9)
INSERT INTO CT_DUNGCU VALUES(N'DC10', 123, N'TRUNG QUỐC', N'CÁI', 10)
INSERT INTO CT_DUNGCU VALUES(N'DC11', 123, N'TRUNG QUỐC', N'CÁI', 11)
INSERT INTO CT_DUNGCU VALUES(N'DC12', 123, N'TRUNG QUỐC', N'CÁI', 12)
INSERT INTO CT_DUNGCU VALUES(N'DC13', 123, N'TRUNG QUỐC', N'CÁI', 13)
INSERT INTO CT_DUNGCU VALUES(N'DC14', 123, N'TRUNG QUỐC', N'CÁI', 14)
INSERT INTO CT_DUNGCU VALUES(N'DC15', 123, N'TRUNG QUỐC', N'CÁI', 15)

GO
--THÊM BẢNG DỰ TRÙ DỤNG CỤ
INSERT INTO DUTRUDUNGCU VALUES(N'DC01', N'CHẢO', N'QUẢN LÝ DỤNG CỤ DOANH THU TRONG NĂM', 20)
INSERT INTO DUTRUDUNGCU VALUES(N'DC02', N'DAO', N'QUẢN LÝ DỤNG CỤ DOANH THU TRONG NĂM', 60)
INSERT INTO DUTRUDUNGCU VALUES(N'DC03', N'ĐĨA', N'QUẢN LÝ DỤNG CỤ DOANH THU TRONG NĂM', 30)
INSERT INTO DUTRUDUNGCU VALUES(N'DC04', N'MUỖNG', N'QUẢN LÝ DỤNG CỤ DOANH THU TRONG NĂM', 40)
INSERT INTO DUTRUDUNGCU VALUES(N'DC05', N'NỒI', N'QUẢN LÝ DỤNG CỤ DOANH THU TRONG NĂM', 50)

GO
--THÊM BẢNG SỰ KIỆN
INSERT INTO SUKIEN VALUES(1, N'LAB01', N'GV01', '20221212 09:35:09 AM','20221225 10:34:09 AM','white', N'140 LÊ TRỌNG TẤN')
INSERT INTO SUKIEN VALUES(2, N'LAB02', N'GV02', '20221213 08:31:09 AM','20221226 10:34:09 AM','blue', N'140 LÊ TRỌNG TẤN')
INSERT INTO SUKIEN VALUES(3, N'LAB03', N'GV03', '20221214 07:32:09 AM','20221231 10:34:09 AM','yellow', N'140 LÊ TRỌNG TẤN')
INSERT INTO SUKIEN VALUES(4, N'LAB04', N'GV04', '20221215 08:33:09 AM','20221228 10:34:09 AM','red', N'TÂN KỲ TÂN QUÝ')
INSERT INTO SUKIEN VALUES(5, N'LAB05', N'TK05', '20221216 10:30:09 AM','20221229 10:34:09 AM','green', N'TÂN KỲ TÂN QUÝ')

GO
--THÊM BẢNG PHIẾU XUẤT
INSERT INTO PHIEUXUAT VALUES (1,8,N'Xuất trang thiết bị về Khoa','2022-12-16',N'KHOA02',0,N'NGUYỄN HOÀNG DƯƠNG',N'Trung tâm thí nghiệm thực hành',N'Khoa CNTP',N'')
INSERT INTO PHIEUXUAT VALUES (2,8,N'Xuất trang thiết bị về Khoa','2022-01-22',N'KHOA02',1,N'NGUYỄN HOÀNG DƯƠNG',N'Trung tâm thí nghiệm thực hành',N'Khoa CNTP',N'')
INSERT INTO PHIEUXUAT VALUES (3,12,N'Xuất trang thiết bị về Khoa','2022-02-8',N'KHOA02',1,N'NGUYỄN HOÀNG DƯƠNG',N'Trung tâm thí nghiệm thực hành',N'Khoa CNTP',N'')
INSERT INTO PHIEUXUAT VALUES (4,8,N'Xuất trang thiết bị về Khoa','2022-12-16',N'KHOA02',0,N'NGUYỄN HOÀNG DƯƠNG',N'Trung tâm thí nghiệm thực hành',N'Khoa CNTP',N'')
INSERT INTO PHIEUXUAT VALUES (5,15,N'Xuất trang thiết bị về Khoa','2022-12-14',N'KHOA02',1,N'NGUYỄN HOÀNG DƯƠNG',N'Trung tâm thí nghiệm thực hành',N'Khoa CNTP',N'')
INSERT INTO PHIEUXUAT VALUES (6,12,N'Xuất trang thiết bị về Khoa','2022-12-28',N'KHOA02',1,N'NGUYỄN HOÀNG DƯƠNG',N'Trung tâm thí nghiệm thực hành',N'Khoa CNTP',N'')
INSERT INTO PHIEUXUAT VALUES (7,1,N'Xuất trang thiết bị về Khoa','2022-12-31',N'KHOA02',0,N'NGUYỄN HOÀNG DƯƠNG',N'Trung tâm thí nghiệm thực hành',N'Khoa CNTP',N'')
INSERT INTO PHIEUXUAT VALUES (8,15,N'Xuất trang thiết bị về Khoa','2023-01-22',N'KHOA02',0,N'NGUYỄN HOÀNG DƯƠNG',N'Trung tâm thí nghiệm thực hành',N'Khoa CNTP',N'')
INSERT INTO PHIEUXUAT VALUES (9,15,N'Xuất trang thiết bị về Khoa','2023-03-02',N'KHOA02',1,N'NGUYỄN HOÀNG DƯƠNG',N'Trung tâm thí nghiệm thực hành',N'Khoa CNTP',N'')
INSERT INTO PHIEUXUAT VALUES (10,12,N'Xuất trang thiết bị về Khoa','2023-05-22',N'KHOA02',1,N'NGUYỄN HOÀNG DƯƠNG',N'Trung tâm thí nghiệm thực hành',N'Khoa CNTP',N'')

GO
--THÊM BẢNG CHI TIẾT PHIẾU XUẤT
INSERT INTO CHITIETPHIEUXUAT VALUES(1, N'HC01', N'TB01', N'DC01', CAST(N'2022-12-21' AS DateTime))
INSERT INTO CHITIETPHIEUXUAT VALUES(2, N'HC02', N'TB02', N'DC02', CAST(N'2022-08-22' AS DateTime))
INSERT INTO CHITIETPHIEUXUAT VALUES(3, N'HC03', N'TB03', N'DC03', CAST(N'2022-12-25' AS DateTime))
INSERT INTO CHITIETPHIEUXUAT VALUES(4, N'HC04', N'TB04', N'DC04', NULL)
INSERT INTO CHITIETPHIEUXUAT VALUES(5, N'HC05', N'TB05', N'DC05', NULL)
INSERT INTO CHITIETPHIEUXUAT VALUES(6, N'HC06', N'TB06', N'DC06', NULL)
INSERT INTO CHITIETPHIEUXUAT VALUES(7, N'HC07', N'TB07', N'DC07', CAST(N'2022-11-22' AS DateTime))
INSERT INTO CHITIETPHIEUXUAT VALUES(8, N'HC08', N'TB08', N'DC08', NULL)
INSERT INTO CHITIETPHIEUXUAT VALUES(9, N'HC09', N'TB09', N'DC09', CAST(N'2022-10-12' AS DateTime))
INSERT INTO CHITIETPHIEUXUAT VALUES(10, N'HC10', N'TB10', N'DC10', CAST(N'2022-11-11' AS DateTime))

GO
--THÊM BẢNG HOÁ CHẤT XUẤT
INSERT INTO XUATHOACHAT VALUES(1, N'HC01', 10)
INSERT INTO XUATHOACHAT VALUES(2, N'HC02', 20)
INSERT INTO XUATHOACHAT VALUES(3, N'HC03', 15)
INSERT INTO XUATHOACHAT VALUES(4, N'HC04', 12)
INSERT INTO XUATHOACHAT VALUES(5, N'HC05', 13)

GO
--THÊM BẢNG PHIẾU NHẬP
INSERT INTO PHIEUNHAP VALUES (1,'2022-08-22',N'Nhập trang thiết bị từ NCC',N'NCC01',N'NGUYỄN TRỌNG NGHĨA',N'LAB01',N'NHÀ CUNG CẤP VÀ PHÂN PHỐI BÌNH ĐIỀN',N'Trung tâm thí nghiệm thực hành',100000,N'')
INSERT INTO PHIEUNHAP VALUES (2,'2022-09-10',N'Nhập trang thiết bị từ NCC',N'NCC02',N'NGUYỄN MINH KHUÊ',N'LAB02',N'NHÀ CUNG CẤP VÀ PHÂN PHỐI PHI PHỤNG',N'Trung tâm thí nghiệm thực hành',100000,N'')
INSERT INTO PHIEUNHAP VALUES (3,'2022-07-12',N'Nhập trang thiết bị từ NCC',N'NCC03',N'ĐỖ MINH TRÍ',N'LAB03',N'NHÀ CUNG CẤP VÀ PHÂN PHỐI MỸ HOÀNG GIA',N'Trung tâm thí nghiệm thực hành',100000,N'')
INSERT INTO PHIEUNHAP VALUES (4,'2022-08-06',N'Nhập trang thiết bị từ NCC',N'NCC04',N'TỐNG ĐĂNG KHOA',N'LAB04',N'NHÀ CUNG CẤP VÀ PHÂN PHỐI PHAN HUY ÍCH',N'Trung tâm thí nghiệm thực hành',100000,N'')
INSERT INTO PHIEUNHAP VALUES (5,'2022-10-06',N'Nhập trang thiết bị từ NCC',N'NCC04',N'HỒ VĂN HOÀNG',N'LAB04',N'NHÀ CUNG CẤP VÀ PHÂN PHỐI PHAN HUY ÍCH',N'Trung tâm thí nghiệm thực hành',100000,N'')

GO
--THÊM BẢNG CHI TIẾT PHIẾU NHẬP
INSERT INTO CHITIETPHIEUNHAP VALUES(1, N'HC01', N'TB01', N'DC01', 30, 100000, 30000000)
INSERT INTO CHITIETPHIEUNHAP VALUES(2, N'HC02', N'TB02', N'DC02', 25, 100000, 28000000)
INSERT INTO CHITIETPHIEUNHAP VALUES(3, N'HC03', N'TB03', N'DC03', 27, 100000, 27000000)
INSERT INTO CHITIETPHIEUNHAP VALUES(4, N'HC04', N'TB04', N'DC04', 45, 100000, 28000000)
GO
--THÊM BẢNG PHIẾU MƯỢN
INSERT INTO PHIEUMUON VALUES(1, '01/02/2023', '01/03/2023', N'Mượn hóa chất', 'HC01', N'Lê Vũ', 'LAB01', 1, 'Khu A', N'Khu Thí Nghiệm', 0, N'Vi Phạm');
INSERT INTO PHIEUMUON VALUES(2, '02/02/2023', '02/03/2023', N'Mượn hóa chất', 'HC02', N'Lê Vũ', 'LAB02', 1, 'Khu B', N'Khu Thí Nghiệm', 1, N'Vi Phạm');
INSERT INTO PHIEUMUON VALUES(3, '03/02/2023', '03/03/2023', N'Mượn hóa chất', 'HC03', N'Lê Vũ', 'LAB03', 1, 'Khu C', N'Khu Thí Nghiệm', 1, N'Vi Phạm');
INSERT INTO PHIEUMUON VALUES(4, '04/02/2023', '04/03/2023', N'Mượn hóa chất', 'HC04', N'Lê Vũ', 'LAB04', 1, 'Khu D', N'Khu Thí Nghiệm', 0, N'Vi Phạm');
INSERT INTO PHIEUMUON VALUES(5, '05/02/2023', '05/03/2023', N'Mượn hóa chất', 'HC05', N'Lê Vũ', 'LAB05', 1, 'Khu E', N'Khu Thí Nghiệm', 1, N'Vi Phạm');
