namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesinhvien2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sinhviens", "gioitinh", c => c.String());
            AddColumn("dbo.Sinhviens", "tongiao", c => c.String());
            AddColumn("dbo.Sinhviens", "nganh", c => c.String());
            AddColumn("dbo.Sinhviens", "avatar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sinhviens", "avatar");
            DropColumn("dbo.Sinhviens", "nganh");
            DropColumn("dbo.Sinhviens", "tongiao");
            DropColumn("dbo.Sinhviens", "gioitinh");
        }
    }
}
