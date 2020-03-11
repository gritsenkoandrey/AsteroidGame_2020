﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lesson_5_HomeWork.Work;
using System.ComponentModel;

namespace Lesson_5_HomeWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<WorkClass> _workClasses;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _workClasses = new BindingList<WorkClass>()
            {
                new WorkClass(){ Employee = "Andrey Gritsenko", Department = "GeekBrains"},
                new WorkClass(){ Employee = "Irina Gritsenko", Department = "GeekBrains"},
                new WorkClass(){ Employee = "Arina Gritsenko", Department = "GeekBrains"}
            };
            ed_Homework.ItemsSource = _workClasses;
            _workClasses.ListChanged += _workClassesChanged;
        }
        private void _workClassesChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    break;
                case ListChangedType.ItemDeleted:
                    break;
                case ListChangedType.ItemChanged:
                    break;
            }
        }
    }
}