using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TradeManagementSystem.Models;

namespace TradeManagementSystem
{
    public partial class AddInvoiceWindow : Window
    {
        private List<Invoice> _invoices;
        private List<Counterparty> _suppliers;

        public AddInvoiceWindow(List<Invoice> invoices, List<Counterparty> counterparties)
        {
            InitializeComponent();
            _invoices = invoices;

            // Заполняем ComboBox поставщиками
            _suppliers = counterparties.Where(c => c.TypeView.ToLower() == "supplier").ToList();
            SupplierComboBox.ItemsSource = _suppliers;
            SupplierComboBox.DisplayMemberPath = "Name";
        }

        private void CategoryComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedItem = CategoryComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem;

            if (selectedItem != null && selectedItem.Content.ToString() == "Другое")
            {
                OtherCategoryTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                OtherCategoryTextBox.Visibility = Visibility.Collapsed;
            }
        }

        private void AddInvoice_Click(object sender, RoutedEventArgs e)
        {

            var category = (CategoryComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
            var otherCategory = OtherCategoryTextBox.Text;
            var selectedSupplier = (SupplierComboBox.SelectedItem as Counterparty)?.Name;


            if (category == "Другое" && !string.IsNullOrEmpty(otherCategory))
            {
                category = otherCategory;
            }

            var newInvoice = new Invoice()
            {
                Supplier = selectedSupplier,
                DeliveryDate = DeliveryDatePicker.SelectedDate ?? DateTime.Now,
                Category = category,
                Quantity = int.TryParse(QuantityTextBox.Text, out int quantity) ? quantity : 0,
            };

            _invoices.Add(newInvoice);

            MessageBox.Show("Накладная успешно добавлена!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
