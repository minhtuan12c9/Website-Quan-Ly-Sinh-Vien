namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createlop : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        nganh = c.String(),
                        khoa = c.String(),
                        soluong = c.String(),
                        GVCN = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Classes");
        }
    }
}
