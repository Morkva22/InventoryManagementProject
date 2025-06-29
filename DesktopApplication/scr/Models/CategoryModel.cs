using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace ManagementSystem
{
    [Table("categories")]
    public class CategoryModel : BaseModel
    {
        [PrimaryKey("id", false)] // false означает, что ключ НЕ должен включаться в INSERT
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = "Новая категория";

        // Метод валидации
        public bool IsValid(out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(Name))
            {
                errorMessage = "Название категории не может быть пустым";
                return false;
            }

            if (Name.Length > 100)
            {
                errorMessage = "Название категории не может быть длиннее 100 символов";
                return false;
            }

            return true;
        }

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
            return new CategoryModel { Name = "Новая категория" };
        }
    }
}