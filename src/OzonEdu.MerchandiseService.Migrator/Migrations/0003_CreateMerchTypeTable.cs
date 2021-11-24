using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(3)]
    public class CreateMerchTypeTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists merch_types(
                    id BIGSERIAL PRIMARY KEY,
                    name TEXT NOT NULL);");
        }
        
        public override void Down()
        {
            Execute.Sql(@"DROP TABLE if exists merch_types;");
        }
    }
}