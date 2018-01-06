namespace EF.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_CashAccount",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AuditModel_CreatedAt = c.DateTime(),
                        AuditModel_CreatedBy = c.String(),
                        AuditModel_UpdatedAt = c.DateTime(),
                        AuditModel_UpdatedBy = c.String(),
                        Account_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Account", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.T_SingleCurrencyCashAccount",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CurrencyType = c.Int(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AuditModel_CreatedAt = c.DateTime(),
                        AuditModel_CreatedBy = c.String(),
                        AuditModel_UpdatedAt = c.DateTime(),
                        AuditModel_UpdatedBy = c.String(),
                        CashAccount_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_CashAccount", t => t.CashAccount_Id)
                .Index(t => t.CashAccount_Id);
            
            CreateTable(
                "dbo.T_Commisions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AuditModel_CreatedAt = c.DateTime(),
                        AuditModel_CreatedBy = c.String(),
                        AuditModel_UpdatedAt = c.DateTime(),
                        AuditModel_UpdatedBy = c.String(),
                        CurrentYearCommision_Id = c.Long(),
                        Account_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Commision", t => t.CurrentYearCommision_Id)
                .ForeignKey("dbo.T_Account", t => t.Account_Id)
                .Index(t => t.CurrentYearCommision_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.T_Commision",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrencyType = c.Int(nullable: false),
                        AuditModel_CreatedAt = c.DateTime(),
                        AuditModel_CreatedBy = c.String(),
                        AuditModel_UpdatedAt = c.DateTime(),
                        AuditModel_UpdatedBy = c.String(),
                        Commisions_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Commisions", t => t.Commisions_Id)
                .Index(t => t.Commisions_Id);
            
            CreateTable(
                "dbo.T_Account",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AccountType = c.Int(nullable: false),
                        AuditModel_CreatedAt = c.DateTime(),
                        AuditModel_CreatedBy = c.String(),
                        AuditModel_UpdatedAt = c.DateTime(),
                        AuditModel_UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_MarginAccount",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TotalAssets = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CashBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PositionValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnrealizedPnL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AuditModel_CreatedAt = c.DateTime(),
                        AuditModel_CreatedBy = c.String(),
                        AuditModel_UpdatedAt = c.DateTime(),
                        AuditModel_UpdatedBy = c.String(),
                        CashAccount_Id = c.Long(),
                        MainAccount_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_CashAccount", t => t.CashAccount_Id)
                .ForeignKey("dbo.T_MainAccount", t => t.MainAccount_Id)
                .Index(t => t.CashAccount_Id)
                .Index(t => t.MainAccount_Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Author = c.String(nullable: false),
                        ISBN = c.String(nullable: false),
                        Published = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrencyType = c.Int(nullable: false),
                        AuditModel_CreatedAt = c.DateTime(),
                        AuditModel_CreatedBy = c.String(),
                        AuditModel_UpdatedAt = c.DateTime(),
                        AuditModel_UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_UserIdentity",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        AuditModel_CreatedAt = c.DateTime(),
                        AuditModel_CreatedBy = c.String(),
                        AuditModel_UpdatedAt = c.DateTime(),
                        AuditModel_UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_MainAccount",
                c => new
                    {
                        Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Account", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.T_PersonalAccount",
                c => new
                    {
                        Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Account", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.T_PersonalAccount", new[] { "Id" });
            DropIndex("dbo.T_MainAccount", new[] { "Id" });
            DropIndex("dbo.T_MarginAccount", new[] { "MainAccount_Id" });
            DropIndex("dbo.T_MarginAccount", new[] { "CashAccount_Id" });
            DropIndex("dbo.T_Commision", new[] { "Commisions_Id" });
            DropIndex("dbo.T_Commisions", new[] { "Account_Id" });
            DropIndex("dbo.T_Commisions", new[] { "CurrentYearCommision_Id" });
            DropIndex("dbo.T_SingleCurrencyCashAccount", new[] { "CashAccount_Id" });
            DropIndex("dbo.T_CashAccount", new[] { "Account_Id" });
            DropForeignKey("dbo.T_PersonalAccount", "Id", "dbo.T_Account");
            DropForeignKey("dbo.T_MainAccount", "Id", "dbo.T_Account");
            DropForeignKey("dbo.T_MarginAccount", "MainAccount_Id", "dbo.T_MainAccount");
            DropForeignKey("dbo.T_MarginAccount", "CashAccount_Id", "dbo.T_CashAccount");
            DropForeignKey("dbo.T_Commision", "Commisions_Id", "dbo.T_Commisions");
            DropForeignKey("dbo.T_Commisions", "Account_Id", "dbo.T_Account");
            DropForeignKey("dbo.T_Commisions", "CurrentYearCommision_Id", "dbo.T_Commision");
            DropForeignKey("dbo.T_SingleCurrencyCashAccount", "CashAccount_Id", "dbo.T_CashAccount");
            DropForeignKey("dbo.T_CashAccount", "Account_Id", "dbo.T_Account");
            DropTable("dbo.T_PersonalAccount");
            DropTable("dbo.T_MainAccount");
            DropTable("dbo.T_UserIdentity");
            DropTable("dbo.Books");
            DropTable("dbo.T_MarginAccount");
            DropTable("dbo.T_Account");
            DropTable("dbo.T_Commision");
            DropTable("dbo.T_Commisions");
            DropTable("dbo.T_SingleCurrencyCashAccount");
            DropTable("dbo.T_CashAccount");
        }
    }
}
