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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Y4e6na9Practika.WindowMain
{
    /// <summary>
    /// Логика взаимодействия для UsersInfo.xaml
    /// </summary>
    public partial class UsersInfo : Page
    {
        Entities entities = new Entities();
        int _userid;
        public UsersInfo(int userid)
        {
            InitializeComponent();
            _userid = userid;
            FillDataGrid();
        }
        private void FillDataGrid()
        {
            var posts = from p in entities.Users
                        where p.Users_ID == _userid
                        select new { Id = p.Users_ID, Name = p.Name, SecondName = p.SecondName, Patronymic = p.Patronymic, Phone = p.Phone};
            dGridService.ItemsSource = posts.ToList();
        }
    }
}
