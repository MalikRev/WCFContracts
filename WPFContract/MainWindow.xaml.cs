using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WCFContarcts;

namespace WPFContract
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string PATH = $"{Environment.CurrentDirectory}\\myJson.json";
        private FileIOServiceContract _fileIOService;
        private BindingList<ClassJsonContract> _classJsons;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ContractService client = new ContractService();

            _fileIOService = new FileIOServiceContract(PATH);

            string dataBase = client.LoadBase();

            try
            {
                _classJsons = _fileIOService.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            contracts.ItemsSource = _classJsons;

            _classJsons.ListChanged += _classJsons_ListChanged;
        }
        private void _classJsons_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {

                try
                {
                    _fileIOService.SaveData(sender);
                    _classJsons = _fileIOService.LoadData();
                    contracts.ItemsSource = _classJsons;
                    _classJsons.ListChanged += _classJsons_ListChanged;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }

        private void contracts_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

        private void Window_Closed(object sender, EventArgs e)
        {
            ContractService client = new ContractService();

            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                client.SaveBase(fileText);
            }
        }
    }
}
