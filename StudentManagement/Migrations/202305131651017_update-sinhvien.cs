namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesinhvien : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sinhviens",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        mssv = c.String(),
                        lop = c.String(),
                        ngaysinh = c.String(),
                        phone = c.String(),
                        khoa = c.String(),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.userId)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sinhviens", "userId", "dbo.Users");
            DropIndex("dbo.Sinhviens", new[] { "userId" });
            DropTable("dbo.Sinhviens");
        }
    }
}
