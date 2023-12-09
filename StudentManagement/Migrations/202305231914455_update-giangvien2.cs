namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updategiangvien2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "chucvu", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "chucvu", c => c.String(nullable: false));
        }
    }
}
