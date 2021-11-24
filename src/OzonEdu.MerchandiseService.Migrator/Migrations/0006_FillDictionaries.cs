using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(6)]
    public class FillDictionaries: ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                INSERT INTO merch_types (id, name)
                VALUES 
                    (1, 'pen'),
                    (2, 'pad')
                ON CONFLICT DO NOTHING
            ");

            Execute.Sql(@"
                INSERT INTO request_statuses (id, name)
                VALUES 
                    (1,  'InWork'),
                    (2,  'Done')
                ON CONFLICT DO NOTHING
            ");
        }
    }
}