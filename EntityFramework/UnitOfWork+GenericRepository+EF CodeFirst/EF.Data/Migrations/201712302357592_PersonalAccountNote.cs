namespace EF.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonalAccountNote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Remark", c => c.String());
            AddColumn("dbo.T_PersonalAccount", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_PersonalAccount", "Note");
            DropColumn("dbo.Books", "Remark");
        }
    }
}
