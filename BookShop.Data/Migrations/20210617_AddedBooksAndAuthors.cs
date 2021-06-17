using Data.Entities;
using FluentMigrator;

namespace Data.Migrations
{
    //Формат версии год месяц день часы минуты
    [Migration(202106170018)]
    public class AddedBooksAndAuthors : Migration
    {
        public override void Up()
        {
            Create.Table("Author")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsFixedLengthString(30).NotNullable()
                .WithColumn("Surname").AsFixedLengthString(30).NotNullable()
                .WithColumn("Age").AsInt16().Nullable();

            Create.Table("Book")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Title").AsFixedLengthString(150).NotNullable()
                .WithColumn("Description").AsString();
        }

        public override void Down()
        {
            Delete.Table("Author");
            Delete.Table("Book"); 
        }
    }
}