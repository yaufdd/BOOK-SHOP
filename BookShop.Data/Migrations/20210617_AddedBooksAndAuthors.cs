using System.Data;
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
            Create.Table("Authors")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsFixedLengthString(30).NotNullable()
                .WithColumn("Surname").AsFixedLengthString(30).NotNullable()
                .WithColumn("Age").AsInt16().Nullable();

            Create.Table("Books")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AuthorId").AsInt64().NotNullable()
                .WithColumn("Title").AsFixedLengthString(150).NotNullable()
                .WithColumn("Description").AsString().Nullable();

            Create.Index("IX_Books_AuthorId")
                .OnTable("Books")
                .OnColumn("AuthorId")
                .Ascending();
            
            Create.Index("IX_Books_Title")
                            .OnTable("Books")
                            .OnColumn("Title")
                            .Ascending();
            
            Create.ForeignKey("FK_Books_Authors_AuthorId")
                .FromTable("Books").ForeignColumn("AuthorId")
                .ToTable("Authors").PrimaryColumn("Id")
                .OnDeleteOrUpdate(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("Authors");
            Delete.Table("Books"); 
        }
    }
}