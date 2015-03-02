namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Entity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Entity");
        }
    }
}
