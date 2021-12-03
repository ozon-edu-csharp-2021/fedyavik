using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(2)]
    public class CreateMerchItemTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists merch_items(
                    id BIGSERIAL PRIMARY KEY,
                    sku INT NOT NULL,
                    name TEXT NOT NULL,
                    merch_type INT);");
        }
        
        public override void Down()
        {
            Execute.Sql(@"DROP TABLE if exists merch_items;");
        }
    }
}