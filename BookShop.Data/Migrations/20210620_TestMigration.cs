using FluentMigrator;

namespace Data.Migrations
{
    [Profile("Develop")]
    public class TestMigration: Migration {
        public override void Up()
        {
            Insert.IntoTable("Authors")
                .Row(new {Name = "Loren", Surname = "Wash", Age = 17 })
                .Row(new {Name = "Alysha", Surname = "Holland", Age = 38 })
                .Row(new {Name = "Michelle", Surname = "Young", Age = 55 })
                .Row(new {Name = "Harriett", Surname = "Fosse", Age = 32 })
                .Row(new {Name = "Jade", Surname = "Christopherson", Age = 27 });
        }

        public override void Down()
        {
        }
    }
}