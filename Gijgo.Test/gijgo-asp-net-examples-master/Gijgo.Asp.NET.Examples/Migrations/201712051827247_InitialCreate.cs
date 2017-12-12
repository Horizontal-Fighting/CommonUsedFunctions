namespace Gijgo.Asp.NET.Examples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ParentID = c.Int(),
                        Name = c.String(),
                        Checked = c.Boolean(nullable: false),
                        OrderNumber = c.Int(nullable: false),
                        Population = c.Long(),
                        FlagUrl = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Locations", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PlaceOfBirth = c.String(),
                        CountryID = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false, storeType: "date"),
                        IsActive = c.Boolean(nullable: false),
                        OrderNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Locations", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.PlayerTeams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PlayerID = c.Int(nullable: false),
                        FromYear = c.Int(nullable: false),
                        ToYear = c.Int(nullable: false),
                        Team = c.String(),
                        Apps = c.Int(nullable: false),
                        Goals = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.PlayerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerTeams", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.Players", "CountryID", "dbo.Locations");
            DropForeignKey("dbo.Locations", "ParentID", "dbo.Locations");
            DropIndex("dbo.PlayerTeams", new[] { "PlayerID" });
            DropIndex("dbo.Players", new[] { "CountryID" });
            DropIndex("dbo.Locations", new[] { "ParentID" });
            DropTable("dbo.PlayerTeams");
            DropTable("dbo.Players");
            DropTable("dbo.Locations");
        }
    }
}
