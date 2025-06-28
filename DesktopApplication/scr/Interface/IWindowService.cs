using System;
using ManagementSystem;

namespace DesktopApplication.Services
{
    public interface IWindowService
    {
        // Изменяем метод - теперь он возвращает отредактированный продукт или null
        ProductModel ShowProductEditDialog(ProductModel product);
        
        CategoryModel ShowCategoryEditDialog(CategoryModel category);
        SupplierModel ShowSupplierEditDialog(SupplierModel supplier);
        bool ShowConfirmation(string message, string title = "Подтверждение");
        void ShowError(string message, string title = "Ошибка");
        void ShowInfo(string message, string title = "Информация");
    }
}