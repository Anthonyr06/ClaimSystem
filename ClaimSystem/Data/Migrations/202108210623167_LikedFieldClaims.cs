namespace ClaimSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LikedFieldClaims : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Claims", "Liked", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Claims", "Liked");
        }
    }
}
