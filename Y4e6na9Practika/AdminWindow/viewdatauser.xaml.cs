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

namespace Y4e6na9Practika.AdminWindow
{
    /// <summary>
    /// Логика взаимодействия для viewdatauser.xaml
    /// </summary>
    public partial class viewdatauser : Page
    {
        public viewdatauser()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)

                dGridService.ItemsSource = Entities.GetContext().LoggerUser.ToList();
        }
    }
}
