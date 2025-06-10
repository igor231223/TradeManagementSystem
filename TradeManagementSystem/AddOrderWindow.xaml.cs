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

    public partial class AddOrderWindow : Window
    {
        private List<Order> _orders;
        private List<Counterparty> _clients;
        public AddOrderWindow(List<Order> orders, List<Counterparty> counterparties)
        {
            InitializeComponent();
            _orders = orders;

            _clients = counterparties.Where(c => c.TypeView.ToLower() == "client").ToList();
            ClientComboBox.ItemsSource = _clients;
            ClientComboBox.DisplayMemberPath = "Name";
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            var category = (CategoryComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
            var otherCategory = OtherCategoryTextBox.Text;
            var selectedClient = (ClientComboBox.SelectedItem as Counterparty)?.Name;


            if (category == "Другое" && !string.IsNullOrEmpty(otherCategory))
            {
                category = otherCategory;
            }

            var newOrder = new Order()
            {
                Client = selectedClient,
                DeliveryDate = DeliveryDatePicker.SelectedDate ?? DateTime.Now,
                Category = category,
                Quantity = int.TryParse(QuantityTextBox.Text, out int quantity) ? quantity : 0,
                PurchasePrice = decimal.TryParse(PriceTextBox.Text, out decimal purchasePrice) ? purchasePrice : 0,
            };

            _orders.Add(newOrder);

            MessageBox.Show("Заказ успешно добавлен!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
