using System.Collections.ObjectModel;
using System.Windows;
using Lesson_5_HomeWork.Work;
using System.Collections.Specialized;

namespace Lesson_5_HomeWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<WorkClass> _workClasses;

        //private BindingList<WorkClass> _workClasses;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //_workClasses = new BindingList<WorkClass>()
            _workClasses = new ObservableCollection<WorkClass>()
            {
                new WorkClass(){ Employee = "Andrey Gritsenko", Department = "GeekBrains"},
                new WorkClass(){ Employee = "Irina Gritsenko", Department = "GeekBrains"},
                new WorkClass(){ Employee = "Arina Gritsenko", Department = "GeekBrains"}
            };
            
            ed_Homework.ItemsSource = _workClasses;
            //_workClasses.ListChanged += _workClassesChanged;
            _workClasses.CollectionChanged += _workClasses_CollectionChanged;

        }
        private void _workClasses_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    MessageBox.Show("Вы добавили");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    MessageBox.Show("Вы удалили");
                    break;
                case NotifyCollectionChangedAction.Replace:
                    MessageBox.Show("Вы заменили");
                    break;
            }
        }
        //private void _workClassesChanged(object sender, ListChangedEventArgs e)
        //{
        //    switch (e.ListChangedType)
        //    {
        //        case ListChangedType.ItemAdded:
        //            MessageBox.Show("Вы добавили элемент");
        //            break;
        //        case ListChangedType.ItemDeleted:
        //            MessageBox.Show("Вы удалили элемент");
        //            break;
        //        case ListChangedType.ItemChanged:
        //            MessageBox.Show("Вы изменили элемент");
        //            break;
        //    }
        //}
    }
}