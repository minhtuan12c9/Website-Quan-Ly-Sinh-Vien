namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creategiangvien : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Giangviens",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        msgv = c.String(),
                        lop = c.String(),
                        ngaysinh = c.String(),
                        phone = c.String(),
                        khoa = c.String(),
                        gioitinh = c.String(),
                        tongiao = c.String(),
                        nganh = c.String(),
                        avatar = c.String(),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.userId)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Giangviens", "userId", "dbo.Users");
            DropIndex("dbo.Giangviens", new[] { "userId" });
            DropTable("dbo.Giangviens");
        }
    }
}
