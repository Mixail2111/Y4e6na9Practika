using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    /// Логика взаимодействия для InfoAplicant.xaml
    /// </summary>
    public partial class InfoAplicant : Page
    {
        public Users CurrentUser { get; set; }

        Educational_Institution selected { get; set; }
        public event EventHandler<Educational_Institution> EduIntitUpdated;
        Entities ent = new Entities();
        int _userid;

        UserActivityLogger logger = new UserActivityLogger();
        public InfoAplicant(Educational_Institution selected, int userid)
        {
            InitializeComponent();
            this.selected = selected;
            DataContext = this.selected;
            _userid = userid;
        }

        private void btn_application_submit_Click(object sender, RoutedEventArgs e)
        {

            // Проверка, что текущий пользователь авторизован
            if (_userid.ToString() != null)
            {
                var result = MessageBox.Show("Хотите подать заявку?", "Подача заявки", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Application_Submission application_Submission = new Application_Submission();
                    application_Submission.Status = "ожидает";
                    application_Submission.Date_of_submission = DateTime.Now;
                    application_Submission.Educational_institution_ID = selected.Educational_Institution_ID;
                    application_Submission.Users_ID = _userid;
                    Educational_Institution edu = ent.Educational_Institution.Find(selected.Educational_Institution_ID);
                    edu.CountMect--;
                    ent.Entry(edu).State = EntityState.Modified;
                    selected = edu;
                    //selected.CountMect--;
                    ent.Application_Submission.Add(application_Submission);
                    DataContext = selected;
                    EduIntitUpdated?.Invoke(this, selected);
                    ent.SaveChanges();

                    var nameEdu = ent.Educational_Institution.FirstOrDefault(p => p.Educational_Institution_ID == selected.Educational_Institution_ID);
                    var NameUser = ent.Users.FirstOrDefault(p => p.Users_ID == _userid);
                    logger.LogUserActivity(_userid, $"Подача заявки в {nameEdu.Name} ", NameUser.Name);
                    MessageBox.Show("Заявка подана! Ожидайте рассмотрения");
                }
            }
            else
            {
                MessageBox.Show("Вы не авторизованы для подачи заявки.");
            }
        }

    }
}
