using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(5)]
    public class CreateMerchRequestsTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists merch_requests(
                    id BIGSERIAL PRIMARY KEY,
                    status INT NOT NULL DEFAULT 1,
                    merch_item INT NOT NULL,
                    employee INT NOT NULL
                    );");
        }
        
        public override void Down()
        {
            Execute.Sql(@"DROP TABLE if exists merch_requests;");
        }
    }
}