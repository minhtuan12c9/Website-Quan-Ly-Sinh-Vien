namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updategiangvien : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "name", c => c.String());
            AlterColumn("dbo.Users", "email", c => c.String());
            AlterColumn("dbo.Users", "password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "name", c => c.String(nullable: false));
        }
    }
}
