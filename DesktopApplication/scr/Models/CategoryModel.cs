using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace ManagementSystem
{
    [Table("categories")]
    public class CategoryModel : BaseModel
    {
        [PrimaryKey("Id", true)]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        public CategoryModel Clone()
        {
            return new CategoryModel
            {
                Id = this.Id,
                Name = this.Name
            };
        }

        public CategoryModel WithName(string name)
        {
            Name = name;
            return this;
        }

        public static CategoryModel CreateDefault()
        {
            return new CategoryModel { Name = "New Category" };
        }
    }
}