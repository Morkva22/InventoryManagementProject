using System;
using ManagementSystem;

namespace DesktopApplication.Services
{
    public interface IWindowService
    {
        bool ShowProductEditDialog(ProductModel product);
        bool ShowCategoryEditDialog(CategoryModel category);
        bool ShowSupplierEditDialog(SupplierModel supplier);
        bool ShowConfirmation(string message, string title = "Подтверждение");
        void ShowError(string message, string title = "Ошибка");
        void ShowInfo(string message, string title = "Информация");
    }
}