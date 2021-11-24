using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(7)]
    public class FillData: ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                INSERT INTO employees (id, first_name, last_name, second_name)
                VALUES 
                    (1, 'name1', 'F', 'S'),
                    (2, 'name2', 'Ff', 'Ss'),
                    (3, 'name3', 'Fff', 'Sss'),
                    (4, 'name4', 'Ffff', 'Ssss'),
                    (5, 'name5', 'Fffff', 'Sssss')
                ON CONFLICT DO NOTHING
            ");

            Execute.Sql(@"
                INSERT INTO merch_items (id, sku, name, merch_type)
                VALUES 
                    (1, '1', 'blue pen', 1),
                    (2, '2', 'red pen', 1),
                    (3, '3', 'green pen', 1),
                    (4, '4', 'pad', 2),
                    (5, '5', 'big pad', 2)
                ON CONFLICT DO NOTHING
            ");
        }
    }
}