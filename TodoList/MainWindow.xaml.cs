using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TodoList.Models;
using TodoList.Service;

namespace TodoList
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Коллекция для хранения задач
        private BindingList<TodoModel> _todoDataList;  
        
        // Объект для работы с данными чтение/запись
        private FileIO _fileIO;

        // Путь к файлу
        private readonly string _path = $"{Environment.CurrentDirectory}\\todoDataList.json";

        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик события загрузки
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIO = new FileIO(_path);
            try
            {
                _todoDataList = _fileIO.LoadData();     // Загрузка данных из файла
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);        // В случае ошибки
                Close();
            }

            dgTodoList.ItemsSource = _todoDataList; // Привязка данных к DataGrid
            _todoDataList.ListChanged += _todoDataList_ListChanged;  // Подписка на событие изменения

        }

        // Обработчик изменения списка задач
        private void _todoDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            // Проверка типа изменения (добавление, удаление, изменение)
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted ||
                e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIO.SaveData(sender);       // Сохранение изменения
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);    // В случае ошибки 
                    Close();
                }

            }
        }
    }
}
