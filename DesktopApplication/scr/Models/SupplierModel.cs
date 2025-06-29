using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using System.Text.RegularExpressions;

namespace ManagementSystem
{
    [Table("suppliers")]
    public class SupplierModel : BaseModel
    {
        [PrimaryKey("id", false)] 
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = "New supplier";
        [Column("contactperson")]
        public string ContactPerson { get; set; } = "";

        [Column("phone")]
        public string Phone { get; set; } = "";

        [Column("email")]
        public string Email { get; set; } = "";
        
        public bool IsValid(out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(Name))
            {
                errorMessage = "The supplier name cannot be empty";                return false;
            }

            if (Name.Length > 100)
            {
                errorMessage = "The supplier name cannot exceed 100 characters";                return false;
            }

            if (!string.IsNullOrEmpty(ContactPerson) && ContactPerson.Length > 100)
            {
                errorMessage = "The contact person's name cannot exceed 100 characters";                return false;
            }

            if (!string.IsNullOrEmpty(Phone) && (Phone.Length > 20 || !Regex.IsMatch(Phone, @"^[0-9]+$")))
            {
                errorMessage = "The phone number must contain only digits and cannot exceed 20 characters";                return false;
            }

            if (!string.IsNullOrEmpty(Email) && (Email.Length > 100 || !IsValidEmail(Email)))
            {
                errorMessage = "Invalid email format or email is too long";                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^.+@.+\..+$");
        }

        public SupplierModel Clone()
        {
            return new SupplierModel
            {
                Id = this.Id,
                Name = this.Name,
                ContactPerson = this.ContactPerson,
                Phone = this.Phone,
                Email = this.Email
            };
        }

        public static SupplierModel CreateDefault()
        {
            return new SupplierModel 
            { 
                Name = "New supplier",                ContactPerson = "",
                Phone = "",
                Email = ""
            };
        }
    }
}