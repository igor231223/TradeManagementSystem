using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TradeManagementSystem.Models;

namespace TradeManagementSystem
{
    public partial class AddProductWindow : Window
    {
        private List<Product> _products; // Список товаров
        private List<Counterparty> _suppliers; // Список поставщиков

        public AddProductWindow(List<Product> products, List<Counterparty> counterparties)
        {
            InitializeComponent();
            _products = products;

            // Заполняем ComboBox поставщиками
            _suppliers = counterparties.Where(c => c.TypeView.ToLower() == "supplier").ToList();
            SupplierComboBox.ItemsSource = _suppliers;
            SupplierComboBox.DisplayMemberPath = "Name";
        }

        // Обработчик изменения категории товара
        private void CategoryComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedItem = CategoryComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem; // Получаем выбранный элемент
                
            if (selectedItem != null && selectedItem.Content.ToString() == "Другое")
            {
                OtherCategoryTextBox.Visibility = Visibility.Visible; // Если выбрано "Другое", показываем текстовое поле для ввода
            }
            else
            {
                OtherCategoryTextBox.Visibility = Visibility.Collapsed; // В противном случае скрываем поле для ввода
            }
        }

        // Обработчик сохранения данных
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            
            var category = (CategoryComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
            var otherCategory = OtherCategoryTextBox.Text;
            var selectedSupplier = (SupplierComboBox.SelectedItem as Counterparty)?.Name;

            if (category == "Другое" && !string.IsNullOrEmpty(otherCategory))
            {
                category = otherCategory;
            }

            var newProduct = new Product
            {
                Name = ProductNameTextBox.Text,
                Category = category,
                Supplier = selectedSupplier,
                DeliveryDate = DeliveryDatePicker.SelectedDate ?? DateTime.Now,
                ShelfLife = int.TryParse(ShelfLifeTextBox.Text, out int shelfLife) ? shelfLife : 0,
                Unit = (UnitComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString(),
                Quantity = int.TryParse(QuantityTextBox.Text, out int quantity) ? quantity : 0,
                PurchasePrice = decimal.TryParse(PriceTextBox.Text, out decimal purchasePrice) ? purchasePrice : 0
            };

            // Добавляем товар в список
            _products.Add(newProduct); 

            // Закрытие окна после добавления
            MessageBox.Show("Товар успешно добавлен!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

        }
    }
}