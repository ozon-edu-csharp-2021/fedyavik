using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(4)]
    public class CreateRequestStatusesTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists request_statuses(
                    id BIGSERIAL PRIMARY KEY,
                    name TEXT NOT NULL);");
        }
        
        public override void Down()
        {
            Execute.Sql(@"DROP TABLE if exists request_statuses;");
        }
    }
}