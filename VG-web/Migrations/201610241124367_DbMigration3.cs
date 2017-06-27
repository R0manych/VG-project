namespace VG_web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbMigration3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Product_data", newName: "ProductDatas");
            DropForeignKey("dbo.Product_data", new[] { "SubcategoryID", "PropertyID" }, "dbo.Subcategory_property");
            DropForeignKey("dbo.Subcategory_property", "PropertyID", "dbo.Properties");
            DropForeignKey("dbo.Subcategory_property", "SubcategoryID", "dbo.Subcategories");
            DropIndex("dbo.Subcategory_property", new[] { "SubcategoryID" });
            DropIndex("dbo.Subcategory_property", new[] { "PropertyID" });
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        UI = c.Guid(nullable: false),
                        Picture = c.Binary(),
                    })
                .PrimaryKey(t => t.ImageID);
            
            CreateTable(
                "dbo.SubcategoryProperties",
                c => new
                    {
                        SubcategoryID = c.Int(nullable: false),
                        PropertyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SubcategoryID, t.PropertyID })
                .ForeignKey("dbo.Properties", t => t.PropertyID, cascadeDelete: true)
                .ForeignKey("dbo.Subcategories", t => t.SubcategoryID, cascadeDelete: true)
                .Index(t => t.SubcategoryID)
                .Index(t => t.PropertyID);
            
            AddColumn("dbo.Categories", "ImageID", c => c.Int());
            AddColumn("dbo.Subcategories", "ImageID", c => c.Int());
            AddColumn("dbo.Products", "ImageID", c => c.Int());
            AddColumn("dbo.Makers", "ImageID", c => c.Int());
            CreateIndex("dbo.Categories", "ImageID");
            CreateIndex("dbo.Makers", "ImageID");
            CreateIndex("dbo.Products", "ImageID");
            CreateIndex("dbo.Subcategories", "ImageID");
            AddForeignKey("dbo.Categories", "ImageID", "dbo.Images", "ImageID");
            AddForeignKey("dbo.Makers", "ImageID", "dbo.Images", "ImageID");
            AddForeignKey("dbo.Products", "ImageID", "dbo.Images", "ImageID");
            AddForeignKey("dbo.ProductDatas", new[] { "SubcategoryID", "PropertyID" }, "dbo.SubcategoryProperties", new[] { "SubcategoryID", "PropertyID" }, cascadeDelete: true);
            AddForeignKey("dbo.Subcategories", "ImageID", "dbo.Images", "ImageID");
            DropColumn("dbo.Categories", "UI");
            DropColumn("dbo.Categories", "Image");
            DropColumn("dbo.Subcategories", "UI");
            DropColumn("dbo.Subcategories", "Image");
            DropColumn("dbo.Products", "UI");
            DropColumn("dbo.Products", "Image");
            DropColumn("dbo.Makers", "UI");
            DropColumn("dbo.Makers", "Image");
            DropTable("dbo.Subcategory_property");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Subcategory_property",
                c => new
                    {
                        SubcategoryID = c.Int(nullable: false),
                        PropertyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SubcategoryID, t.PropertyID });
            
            AddColumn("dbo.Makers", "Image", c => c.Binary());
            AddColumn("dbo.Makers", "UI", c => c.Guid(nullable: false));
            AddColumn("dbo.Products", "Image", c => c.Binary());
            AddColumn("dbo.Products", "UI", c => c.Guid(nullable: false));
            AddColumn("dbo.Subcategories", "Image", c => c.Binary());
            AddColumn("dbo.Subcategories", "UI", c => c.Guid(nullable: false));
            AddColumn("dbo.Categories", "Image", c => c.Binary());
            AddColumn("dbo.Categories", "UI", c => c.Guid(nullable: false));
            DropForeignKey("dbo.SubcategoryProperties", "SubcategoryID", "dbo.Subcategories");
            DropForeignKey("dbo.Subcategories", "ImageID", "dbo.Images");
            DropForeignKey("dbo.SubcategoryProperties", "PropertyID", "dbo.Properties");
            DropForeignKey("dbo.ProductDatas", new[] { "SubcategoryID", "PropertyID" }, "dbo.SubcategoryProperties");
            DropForeignKey("dbo.Products", "ImageID", "dbo.Images");
            DropForeignKey("dbo.Makers", "ImageID", "dbo.Images");
            DropForeignKey("dbo.Categories", "ImageID", "dbo.Images");
            DropIndex("dbo.Subcategories", new[] { "ImageID" });
            DropIndex("dbo.SubcategoryProperties", new[] { "PropertyID" });
            DropIndex("dbo.SubcategoryProperties", new[] { "SubcategoryID" });
            DropIndex("dbo.Products", new[] { "ImageID" });
            DropIndex("dbo.Makers", new[] { "ImageID" });
            DropIndex("dbo.Categories", new[] { "ImageID" });
            DropColumn("dbo.Makers", "ImageID");
            DropColumn("dbo.Products", "ImageID");
            DropColumn("dbo.Subcategories", "ImageID");
            DropColumn("dbo.Categories", "ImageID");
            DropTable("dbo.SubcategoryProperties");
            DropTable("dbo.Images");
            CreateIndex("dbo.Subcategory_property", "PropertyID");
            CreateIndex("dbo.Subcategory_property", "SubcategoryID");
            AddForeignKey("dbo.Subcategory_property", "SubcategoryID", "dbo.Subcategories", "SubcategoryID", cascadeDelete: true);
            AddForeignKey("dbo.Subcategory_property", "PropertyID", "dbo.Properties", "PropertyID", cascadeDelete: true);
            AddForeignKey("dbo.Product_data", new[] { "SubcategoryID", "PropertyID" }, "dbo.Subcategory_property", new[] { "SubcategoryID", "PropertyID" }, cascadeDelete: true);
            RenameTable(name: "dbo.ProductDatas", newName: "Product_data");
        }
    }
}
