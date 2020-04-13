namespace INT1448.EntityFramework.Migrations
{
    using INT1448.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<INT1448.EntityFramework.INT1448DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(INT1448.EntityFramework.INT1448DbContext context)
        {
            if (context.Publishers.Count() == 0)
            {
                IList<Publisher> defaultPublisher = new List<Publisher>()
                {
                    new Publisher(){Name="Nhà xuất bản Kim Đồng", Address="55 Quang Trung, Hai Bà Trưng, Hà Nội", Phone="02439434730", Books=null},
                    new Publisher(){Name="Nhà xuất bản Trẻ", Address="161B Lý Chính Thắng, Phường 7, Quận 3, Thành phố Hồ Chí Minh", Phone="(028) 39316289", Books=null},
                    new Publisher(){Name="Nhà xuất bản Tổng hợp thành phố Hồ Chí Minh", Address="62 Nguyễn Thị Minh Khai, Phường Đa Kao, Quận 1, TP.HCM", Phone="(028) 38 225 340", Books=null},
                    new Publisher(){Name="Nhà xuất bản chính trị quốc gia sự thật", Address="Số 6/86 Duy Tân, Cầu Giấy, Hà Nội", Phone="024.3822-1633", Books=null},
                    new Publisher(){Name="Nhà xuất bản giáo dục", Address="81 Trần Hưng Đạo, Hà Nội", Phone="024.38220801", Books=null},
                    new Publisher(){Name="Nhà xuất bản Hội Nhà văn", Address="Số 65 Nguyễn Du, Hà Nội", Phone="024.38222135", Books=null},
                    new Publisher(){Name="Nhà xuất bản Tư pháp", Address="Số 35 Trần Quốc Toản, Hoàn Kiếm, Hà Nội", Phone="024.62632078", Books=null},
                    new Publisher(){Name="Nhà xuất bản thông tin và truyền thông", Address="Tầng 6, Tòa nhà Cục Tần số Vô tuyến điện, số 115 Trần Duy Hưng, Hà Nội ", Phone="024 35772145", Books=null},
                    new Publisher(){Name="Nhà xuất bản lao động", Address="175 Giảng Võ, Đống Đa, Hà Nội", Phone="0243.8515380 ", Books=null},
                    new Publisher(){Name="Nhà xuất bản giao thông vận tải", Address="80B Trần Hưng Đạo, Hoàn Kiếm, Hà Nội", Phone="024 3.9423346", Books=null},
                    new Publisher(){Name="Nhà xuất bản Đại học Quốc Gia Hà Nội", Address="16 Hàng Chuối, Phạm Đình Hổ, Hai Bà Trưng, Hà Nội", Phone="024 3971 4896", Books=null},
                    new Publisher(){Name="Nhà xuất bản Thanh niên", Address="27B Nguyễn Đình Chiểu, Đa Kao, Quận 1, Hồ Chí Minh", Phone=" 028 3930 5243", Books=null},
                   // new Publisher(){Name="", Address="", Phone=""},
                };
                context.Publishers.AddRange(defaultPublisher);
                context.SaveChanges();
            }

            if (context.BookCategories.Count() == 0)
            {
                IList<BookCategory> defaultBookCategory = new List<BookCategory>()
                {
                    new BookCategory(){Name="Chính trị – pháp luật", Alias="chinh-tri-phap-luat", Description="none", Books = null},
                    new BookCategory(){Name="Khoa học công nghệ – Kinh tế", Alias="khoa-hoc-cong-nghe-kinh-te", Description="none", Books = null},
                    new BookCategory(){Name="Văn hóa xã hội – Lịch sử", Alias="van-hoa-xa-hoi-lich-su", Description="none", Books = null},
                    new BookCategory(){Name="Văn học nghệ thuật", Alias="van-hoc-nghe-thuat", Description="none", Books = null},
                    new BookCategory(){Name="Giáo trình", Alias="giao-trinh", Description="none", Books = null},
                    new BookCategory(){Name="Truyện - tiểu thuyết", Alias="truyen-tieu-thuyet", Description="none", Books = null},
                    new BookCategory(){Name="Tâm lý - tâm linh - tôn giáo", Alias="tam-ly-tam-linh-ton-giao", Description="none", Books = null},
                    new BookCategory(){Name="Thiếu nhi", Alias="thieu-nhi", Description="none", Books = null},
                    //new BookCategory(){Name="", Alias="", Description="", Books = null},
                };
                context.BookCategories.AddRange(defaultBookCategory);
                context.SaveChanges();
            }

            if(context.Authors.Count() == 0)
            {
                IList<Author> defaultAuthors = new List<Author>()
                {
                    new Author(){FullName="Hồ Chí Minh", Address="làng Sen, xã Kim Liên, huyện Nam Đàn, tỉnh Nghệ An"},
                    new Author(){FullName="Nguyễn Hữu Phước", Address="Xã Khánh Hội, huyện U Minh, Cà Mau"},
                    new Author(){FullName="Ngô Minh Vân", Address="778/12 Nguyen Kiem, Ward 4, Phu Nhuan Dist, Ho Chi Minh City"},
                    new Author(){FullName="Ngọc Linh", Address="17 St. 8, Ward 7, Go Vap Dist, Ho Chi Minh City"},
                    new Author(){FullName="Lê Văn Thông", Address="8 Hoang Van Thu Street, Phu Nhuan District, Ho Chi Minh City"},
                    new Author(){FullName="PGS.TS.Võ Văn Nhị", Address="352 Lang Thuong Street, Dong Da District, Hanoi"},
                    new Author(){FullName="Tô Hoài", Address="136/36 Nguyen Thi Tan Street, Ward 2, District 8, Ho Chi Minh City"},
                    new Author(){FullName="Hồ Biểu Chánh", Address="220/42/16 Nguyen Xi St., Ward 26, Binh Thanh Dist, Ho Chi Minh City"},
                    new Author(){FullName="Khánh Linh", Address="304/65 Ho Van Hue, Ward 9, Phu Nhuan Dist, Ho Chi Minh"},
                    new Author(){FullName="Gia Bảo", Address="Str. 10, Song Than 1 IZ, Di An Townlet, Binh Duong"},
                    new Author(){FullName="Việt Chương", Address="129 Khanh Hoi St., Ward 3, Dist. 4, Ho Chi Minh"},
                    new Author(){FullName="Thiên Kim", Address="11 Pasteur St., Nguyen Thai Binh Ward, Dist. 1, Ho Chi Minh"},
                    //new Author(){FullName="", Address=""},
                };
                context.Authors.AddRange(defaultAuthors);
                context.SaveChanges();
            }

            if(context.Books.Count() == 0)
            {
                IList<Book> defaultBooks = new List<Book>()
                {
                    new Book(){Name="Bộ Sách Hồ Chí Minh - Tác Phẩm (Hộp 5 Cuốn)", PubDate=new DateTime(2020, 04, 13), Cost=148000, Retail=118000, CategoryID=1, PublisherID=2 },
                    new Book(){Name="Di Chúc Của Chủ Tịch Hồ Chí Minh", PubDate=new DateTime(2020, 04, 13), Cost=30000, Retail=24000, CategoryID=3, PublisherID=2 },
                    new Book(){Name="Nhật Ký Trong Tù", PubDate=new DateTime(2020, 04, 13), Cost=35000, Retail=32000, CategoryID=3, PublisherID=5 },
                    new Book(){Name="Dế Mèn Phiêu Lưu Ký (Tái Bản 2018)", PubDate=new DateTime(2020, 04, 13), Cost=120000, Retail=110000, CategoryID=6, PublisherID=1 },
                    new Book(){Name="Tô Hoài - Tuyển Tập Truyện Ngắn Hay", PubDate=new DateTime(2020, 04, 13), Cost=98000, Retail=78000, CategoryID=6, PublisherID=6 },
                    new Book(){Name="Hai Khối Tình", PubDate=new DateTime(2020, 04, 13), Cost=42000, Retail=34000, CategoryID=6, PublisherID=6 },
                    new Book(){Name="Chút Phận Linh Đinh", PubDate=new DateTime(2020, 04, 13), Cost=106000, Retail=96000, CategoryID=6, PublisherID=6 },
                    new Book(){Name="10 Vạn Câu Hỏi Vì Sao - Khám Phá Thế Giới Vi Sinh Vật", PubDate=new DateTime(2020, 04, 13), Cost=55000, Retail=44000, CategoryID=8, PublisherID=12 },
                    new Book(){Name="10 Vạn Câu Hỏi Vì Sao - Khám Phá Thế Giới Thực ", PubDate=new DateTime(2020, 04, 13), Cost=50000, Retail=40000, CategoryID=8, PublisherID=12 },
                    new Book(){Name="10 Vạn Câu Hỏi Vì Sao - Khám Phá Thế Giới Động Vật", PubDate=new DateTime(2020, 04, 13), Cost=65000, Retail=60000, CategoryID=8, PublisherID=12 },
                    new Book(){Name="Hướng Dẫn Thực Hành Kế Toán Chi Phí Sản Xuất Và Tính Giá Thành Sản Phẩm Trong Doanh Nghiệp", PubDate=new DateTime(2020, 04, 13), Cost=120000, Retail=110000, CategoryID=2, PublisherID=9 },
                    new Book(){Name="Kế Toán Tài Chính", PubDate=new DateTime(2020, 04, 13), Cost=95000, Retail=76000, CategoryID=2, PublisherID=5 },
                    new Book(){Name="Người Tị Nạn", PubDate=new DateTime(2020, 04, 13), Cost=195000, Retail=156000, CategoryID=7, PublisherID=6 },
                    new Book(){Name="Từ Điển Thành Ngữ Tục Ngữ Ca Dao Việt Nam ", PubDate=new DateTime(2020, 04, 13), Cost=189000, Retail=151000, CategoryID=5, PublisherID=3 },
                    //new Book(){Name="", PubDate=new DateTime(2020, 04, 13), Cost=0, Retail=0, CategoryID=1, PublisherID=1 },

                };
                context.Books.AddRange(defaultBooks);
                context.SaveChanges();
            }

            if(context.BookAuthors.Count() == 0)
            {
                IList<BookAuthor> defaultBookAuthors = new List<BookAuthor>()
                {
                    new BookAuthor(){BookID=1, AuthorID=1},
                    new BookAuthor(){BookID=2, AuthorID=1},
                    new BookAuthor(){BookID=3, AuthorID=1},
                    new BookAuthor(){BookID=4, AuthorID=7},
                    new BookAuthor(){BookID=5, AuthorID=7},
                    new BookAuthor(){BookID=6, AuthorID=8},
                    new BookAuthor(){BookID=7, AuthorID=8},
                    new BookAuthor(){BookID=8, AuthorID=4},
                    new BookAuthor(){BookID=9, AuthorID=4},
                    new BookAuthor(){BookID=10, AuthorID=4},
                    new BookAuthor(){BookID=11, AuthorID=6},
                    new BookAuthor(){BookID=12, AuthorID=6},
                    new BookAuthor(){BookID=13, AuthorID=11},
                    new BookAuthor(){BookID=14, AuthorID=11},
                    //new BookAuthor(){BookID=1, AuthorID=1},
                };
                context.BookAuthors.AddRange(defaultBookAuthors);
                context.SaveChanges();
            }
        }
    }
}
