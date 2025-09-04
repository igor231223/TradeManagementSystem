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
    public partial class AddCounterpartyWindow : Window
    {
        private List<Counterparty> _counterparties; // Список контрагентов
        public AddCounterpartyWindow(List<Counterparty> counterparties)
        {
            InitializeComponent();
            _counterparties = counterparties;
        }

        private void AddCounterparty_Click(object sender, RoutedEventArgs e)
        {
            var type = (TypeComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
            
            var newCounterparty = new Counterparty
            {
                Type = type,
                Name = NameTextBox.Text,
                Country = CountryTextBox.Text,
                DirectorName = DirectorNameTextBox.Text,
                LegalAddress = LegalAddressTextBox.Text,
                Phone = PhoneTextBox.Text,
                Email = EmailTextBox.Text,
                BankName = BankNameTextBox.Text,
                AccountNumber = AccountNumberTextBox.Text,
                TaxNumber = TaxNumberTextBox.Text
            };

            if (type == "Заказчик") { type = "Client"; }
            else if (type == "Поставщик") { type = "Supplier"; };

            newCounterparty.TypeView = type;

            // Добавляем контрагента в список
            _counterparties.Add(newCounterparty);

            // Закрытие окна после добавления
            MessageBox.Show("Контрагент успешно добавлен!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

    }
}
