using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TradeManagementSystem.Models;

namespace TradeManagementSystem
{
    public partial class MainWindow : Window
    {
        private List<Counterparty> _counterparties = new List<Counterparty>();
        private List<Invoice> _invoices = new List<Invoice>();
        private List<Order> _orders = new List<Order>();
        private List<Product> _products = new List<Product>();

        private const string CounterpartiesFilePath = "counterparties.json";
        private const string InvoicesFilePath = "invoices.json";
        private const string OrdersFilePath = "orders.json";
        private const string ProductsFilePath = "products.json";

        public MainWindow()
        {
            InitializeComponent();
        }


        // Контрагенты (Заказчики и продавцы)
        private void AddCounterparty_Click(object sender, RoutedEventArgs e)
        {
            var AddCounterpartyWindow = new AddCounterpartyWindow(_counterparties);
            AddCounterpartyWindow.ShowDialog();

            UpdateCounterpartyList();
        }

        private void UpdateCounterpartyList()
        {
            CounterpartyList.ItemsSource = null;
            CounterpartyList.ItemsSource = _counterparties;
            var collectionView = (ICollectionView)CollectionViewSource.GetDefaultView(CounterpartyList.ItemsSource);
            collectionView.Refresh();
        }

        private void DeleteCounterparty_Click(object sender, RoutedEventArgs e)
        {
            if (CounterpartyList.SelectedItem is Counterparty selected)
            {
                _counterparties.Remove(selected);
                UpdateCounterpartyList();
            }
        }

        private void LoadCounterparties_Click(object sender, RoutedEventArgs e)
        {
            _counterparties = JsonHelper.LoadFromFile<Counterparty>(CounterpartiesFilePath);
            UpdateCounterpartyList();
        }

        private void SaveCounterparties_Click(object sender, RoutedEventArgs e)
        {
            if (!_counterparties.Any())
            {
                MessageBox.Show("Список контрагентов пуст. Добавьте хотя бы одного контрагента перед сохранением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            JsonHelper.SaveToFile(CounterpartiesFilePath, _counterparties);
            MessageBox.Show("Список контрагентов сохранен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }




        // Товары
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var suppliers = _counterparties.Where(c => c.TypeView.ToLower() == "supplier").ToList();

            if (!suppliers.Any())
            {
                MessageBox.Show("Добавьте хотя бы одного поставщика перед заказом товаров.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            var addProductWindow = new AddProductWindow(_products, _counterparties); // Открываем новое окно для добавления товара
            addProductWindow.ShowDialog();

            UpdateProductList();
        }

        private void UpdateProductList()
        {
            ProductList.ItemsSource = null;
            ProductList.ItemsSource = _products;
            var collectionView = (ICollectionView)CollectionViewSource.GetDefaultView(ProductList.ItemsSource);
            collectionView.Refresh();
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductList.SelectedItem is Product selected)
            {
                _products.Remove(selected);
                UpdateProductList();
            }
        }

        private void LoadProducts_Click(object sender, RoutedEventArgs e)
        {
            _products = JsonHelper.LoadFromFile<Product>(ProductsFilePath);
            UpdateProductList();
        }

        private void SaveProducts_Click(object sender, RoutedEventArgs e)
        {
            if (!_products.Any())
            {
                MessageBox.Show("Список товаров пуст. Добавьте хотя бы один товар перед сохранением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            JsonHelper.SaveToFile(ProductsFilePath, _products);
            MessageBox.Show("Список пациентов сохранен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }




        // Накладные
        private void AddInvoice_Click(object sender, RoutedEventArgs e)
        {
            var suppliers = _counterparties.Where(c => c.TypeView.ToLower() == "supplier").ToList();

            if (!suppliers.Any())
            {
                MessageBox.Show("Добавьте хотя бы одного поставщика перед заказом товаров.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var AddInvoiceWindow = new AddInvoiceWindow(_invoices, _counterparties);
            AddInvoiceWindow.ShowDialog();

            UpdateInvoiceList();
        }

        private void UpdateInvoiceList()
        {
            InvoiceList.ItemsSource = null;
            InvoiceList.ItemsSource = _invoices;
            var collectionView = (ICollectionView)CollectionViewSource.GetDefaultView(InvoiceList.ItemsSource);
            collectionView.Refresh();
        }

        private void DeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (InvoiceList.SelectedItem is Invoice selected)
            {
                _invoices.Remove(selected);
                UpdateInvoiceList();
            }
        }


        private void LoadInvoices_Click(object sender, RoutedEventArgs e)
        {
            _invoices = JsonHelper.LoadFromFile<Invoice>(InvoicesFilePath);
            UpdateInvoiceList();
        }

        private void SaveInvoices_Click(object sender, RoutedEventArgs e)
        {
            if (!_invoices.Any())
            {
                MessageBox.Show("Список накладных пуст. Добавьте хотя бы одну накладную перед сохранением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            JsonHelper.SaveToFile(InvoicesFilePath, _invoices);
            MessageBox.Show("Список пациентов сохранен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }




        // Заказы
        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            var clients = _counterparties.Where(c => c.TypeView.ToLower() == "client").ToList();

            if (!clients.Any())
            {
                MessageBox.Show("Добавьте хотя бы одного заказчика перед продажей товаров.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var AddOrderWindow = new AddOrderWindow(_orders, _counterparties);
            AddOrderWindow.ShowDialog();

            UpdateOrderList();
        }


        private void UpdateOrderList()
        {
            OrderList.ItemsSource = null;
            OrderList.ItemsSource = _orders;
            var collectionView = (ICollectionView)CollectionViewSource.GetDefaultView(OrderList.ItemsSource);
            collectionView.Refresh();
        }
        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrderList.SelectedItem is Order selected)
            {
                _orders.Remove(selected);
                UpdateOrderList();
            }
        }

        private void LoadOrders_Click(object sender, RoutedEventArgs e)
        {
            _orders = JsonHelper.LoadFromFile<Order>(OrdersFilePath);
            UpdateOrderList();
        }

        private void SaveOrders_Click(object sender, RoutedEventArgs e)
        {
            if (!_orders.Any())
            {
                MessageBox.Show("Список заказов пуст. Добавьте хотя бы один заказ перед сохранением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            JsonHelper.SaveToFile(OrdersFilePath, _orders);
            MessageBox.Show("Список пациентов сохранен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }





        // Сортировка по столбцам
        private void Sorting(object sender, MouseEventArgs e)
        {
            try { 
                var header = e.OriginalSource as GridViewColumnHeader;
                var listView = (ListView)sender;
                var collectionView = (ICollectionView)CollectionViewSource.GetDefaultView(listView.ItemsSource);
                string sortBy = ((Binding)header.Column.DisplayMemberBinding).Path.Path;
                bool isAscending = collectionView.SortDescriptions.Count == 0 || collectionView.SortDescriptions[0].Direction == ListSortDirection.Descending;
                collectionView.SortDescriptions.Clear();
                collectionView.SortDescriptions.Add(new SortDescription(sortBy, isAscending ? ListSortDirection.Ascending : ListSortDirection.Descending));
            }
        catch { }
        }

        
    }
}
