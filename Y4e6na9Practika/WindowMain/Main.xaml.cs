using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Y4e6na9Practika.WindowMain
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        Entities ent = new Entities();
        public Users CurrentUser { get; private set; }
        int _userid;
        UserActivityLogger logger = new UserActivityLogger();

        public Main(int userid)
        {
            InitializeComponent();
            LabelName.Content = Profile.lograz;
            LabelRole.Content = Profile.role;
            ListViewWithImages.ItemsSource = ent.Educational_Institution.ToList();
            Comboserch.Items.Add("Строительное");
            Comboserch.Items.Add("Информационное");
            Manager.MainFrame = Framey4b;
            Manager.zakaz = Framey4b;
            Manager.Redy = Framey4b;
            Manager.History = Framey4b;
            Manager.usersinfo = Framey4b;
            _userid = userid;
            if (Profile.role != "Администратор")
            {
                HistoryButton.Visibility = Visibility.Collapsed;
                AddButton.Visibility = Visibility.Collapsed;
            }
        }
        private void RedButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Educational_Institution selectedEI = (Educational_Institution)button.DataContext;
            EditPage editPage = new EditPage(selectedEI);
            editPage.EduIntitUpdated += (s, updateEduInst) =>
            {
                
                var itemToUpdate = ListViewWithImages.Items.Cast<Educational_Institution>().FirstOrDefault(item => item.Educational_Institution_ID == updateEduInst.Educational_Institution_ID);
                if (itemToUpdate != null)
                {
                    itemToUpdate.Name = updateEduInst.Name;
                    itemToUpdate.Address = updateEduInst.Address;
                    itemToUpdate.Phone = updateEduInst.Phone;
                    itemToUpdate.direction = updateEduInst.direction;
                    itemToUpdate.CountMect = updateEduInst.CountMect;
                }
                ListViewWithImages.Items.Refresh();
            };
            Manager.usersinfo.Navigate(editPage);
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            ApplyFilter();
        }

        private void Comboserch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Educational_Institution educational_Institution = button.DataContext as Educational_Institution;

            // Создание объекта InfoAplicant с передачей объектов Educational_Institution и Users
            InfoAplicant infoAplicant = new InfoAplicant(educational_Institution, _userid); // замените `currentUser` на ваш объект Users

            infoAplicant.EduIntitUpdated += (s, updateEduInst) =>
            {
                educational_Institution.CountMect = updateEduInst.CountMect;
                ListViewWithImages.Items.Refresh();
            };
            var nameEdu = ent.Educational_Institution.FirstOrDefault(p => p.Educational_Institution_ID == educational_Institution.Educational_Institution_ID);
            var NameUser = ent.Users.FirstOrDefault(p => p.Users_ID == _userid);
            logger.LogUserActivity(_userid, $"Просмотрена запись {nameEdu.Name}", NameUser.Name);
            Manager.MainFrame.Navigate(infoAplicant);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.zakaz.Navigate(new UsersInfozakaz(_userid));
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.History.Navigate(new AdminWindow.viewdatauser());
        }

        private void UsersDataInfo_Click(object sender, RoutedEventArgs e)
        {
            Manager.usersinfo.Navigate(new UsersInfo(_userid));
        }
        private void refreshfiltres_Click_1(object sender, RoutedEventArgs e)
        {
            Comboserch.Text = "";
            ListViewWithImages.Items.Refresh();
        }
        private void ApplyFilter()
        {
            string searchText = SearchTextBox.Text.ToLower();
            string selectedSort = (string)Comboserch.SelectedItem;

            var filteredList = ent.Educational_Institution
                .Where(s => string.IsNullOrEmpty(searchText) ||
                            s.Name.ToLower().StartsWith(searchText) ||
                            s.Address.ToLower().StartsWith(searchText) ||
                            s.direction.StartsWith(searchText) ||
                            s.Phone.StartsWith(searchText))
                .ToList();

            if (selectedSort == "Информационное")
            {
                filteredList = filteredList
                    .Where(s => s.direction == selectedSort)
                    .ToList();

            }
            else if (selectedSort == "Строительное")
            {
                filteredList = filteredList
                    .Where(s => s.direction == selectedSort)
                    .ToList();
            }
            ListViewWithImages.ItemsSource = filteredList;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            EditPage editPage = new EditPage();
            editPage.EduIntitUpdated += (s, updateEduInst) =>
            {
                (ListViewWithImages.ItemsSource as List<Educational_Institution>).Add(updateEduInst);
                //ListViewWithImages.ItemsSource = null;
                //ListViewWithImages.ItemsSource = ent.Educational_Institution.ToList();
                //ListViewWithImages.Items.Add(updateEduInst);
                ListViewWithImages.Items.Refresh();
            };
            Manager.Redy.Navigate(editPage);
        }
    }
}
