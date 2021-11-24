using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(1)]
    public class CreateEmployeeTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists employees(
                    id BIGSERIAL PRIMARY KEY,
                    first_name TEXT NOT NULL,
                    last_name TEXT,
                    second_name TEXT);");
        }
        
        public override void Down()
        {
            Execute.Sql(@"DROP TABLE if exists employees;");
        }
    }
}