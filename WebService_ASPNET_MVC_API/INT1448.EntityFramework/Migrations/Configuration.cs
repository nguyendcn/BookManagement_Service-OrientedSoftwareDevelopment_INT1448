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
        }
    }
}
