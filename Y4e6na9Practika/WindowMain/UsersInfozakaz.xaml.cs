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
    /// Логика взаимодействия для UsersInfozakaz.xaml
    /// </summary>
    public partial class UsersInfozakaz : Page
    {
        Entities entities = new Entities();
        int _userid;
        public UsersInfozakaz(int userid)
        {
            InitializeComponent();
            _userid = userid;
            FillDataGrid();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }
        private void FillDataGrid()
        {
            var posts = from p in entities.Application_Submission
                        where p.Users_ID == _userid
                        join d in entities.Educational_Institution on p.Educational_institution_ID equals d.Educational_Institution_ID
                        select new { Id = p.Users_ID, Name = d.Name, Phone =d.Phone, direction = d.direction, Address = d.Address, Date_of_submission = p.Date_of_submission, Status = p.Status};
            dGridService.ItemsSource = posts.ToList();
        }
    }
}
