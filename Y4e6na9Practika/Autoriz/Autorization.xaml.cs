using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Y4e6na9Practika.WindowMain;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;
using System.Security.Cryptography;
using System.Text;

namespace Y4e6na9Practika.Autoriz
{
    public partial class Autorization : Window
    {
        Entities entities = new Entities();
        private int loginAttempts = 0;
        private bool loginBlocked = false;
        private Educational_Institution selected;

        UserActivityLogger logger = new UserActivityLogger();

        public Autorization()
        {
            InitializeComponent();
            Capcha();
        }

        private async void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            Roles userR = entities.Roles.FirstOrDefault(p => p.Roles_Id == 1);
            Users user = entities.Users.FirstOrDefault(p => p.Users_ID == 1);
            LoginSuccessful(user, userR);
            //if (loginBlocked)
            //{
            //    MessageBox.Show("Подождите несколько секунд перед следующей попыткой входа.");
            //    return;
            //}
            //// Проверка на пустые поля
            //if (string.IsNullOrWhiteSpace(LoginTextBox.Text) || string.IsNullOrWhiteSpace(PasswordTextBox.Password) || string.IsNullOrWhiteSpace(CaptchaTextBox.Text))
            //{
            //    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            //try
            //{
            //    string login = LoginTextBox.Text.Trim();
            //    string password = PasswordTextBox.Password.Trim();
            //    string passwotdHash = Encrypt(password);
            //    Roles role = new Roles();
            //    Users user = new Users();
            //    user = entities.Users.FirstOrDefault(p => p.Login == login && p.Password == passwotdHash);

            //    if (CaptchaTextBox.Text != Captchatext.Text)
            //    {
            //        MessageBox.Show("Неверная CAPTCHA", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //        CaptchaTextBox.Text = "";
            //        Capcha();
            //    }
            //    else
            //    {
            //        if (user != null && user.Roles_ID == 1)
            //        {
            //            LoginSuccessful(user, role);
            //        }
            //        else if (user != null)
            //        {
            //            LoginSuccessful(user, role);
            //        }
            //        else
            //        {
            //            loginAttempts++;
            //            if (loginAttempts >= 3)
            //            {
            //                loginBlocked = true;
            //                MessageBox.Show("Вы ввели неверные логин или пароль. Попробуйте снова через 10 секунд.");
            //                await Task.Delay(10000); // 10 seconds delay
            //                loginAttempts = 0;
            //                loginBlocked = false;
            //            }
            //            else
            //            {
            //                MessageBox.Show("Вы ввели неверные логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //            }
            //        }
            //    }
            //}
            //catch
            //{
            //}
        }
        private void LoginSuccessful(Users user, Roles role)
        {
            Profile.lograz = user.Name;
            var infoAplicantPage = new InfoAplicant(selected, user.Users_ID);
            // Получаем роль пользователя из базы данных
            role = entities.Roles.FirstOrDefault(r => r.Roles_Id == user.Roles_ID);

            if (role != null)
            {
                logger.LogUserActivity(user.Users_ID, "Успешный вход в систему", user.Name);
                Profile.role = role.NameRoles;
                MessageBox.Show("Вы вошли как " + user.Name);
                MessageBox.Show("Ваша роль " + role.NameRoles);
                var winmain = new Main(user.Users_ID);
                winmain.Show();
            }
            else
            {
                MessageBox.Show("Ошибка при получении информации о роли пользователя.");
            }
        }
        private string Encrypt(string input)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(input));
                return Convert.ToBase64String(data);
            }
        }

        public void Capcha()
        {
            string allowchar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            Random r = new Random();
            string pwd = new string(Enumerable.Repeat(allowchar, 6).Select(s => s[r.Next(s.Length)]).ToArray());
            CaptchaLabel.Content = pwd;
            Captchatext.Text = pwd;
        }

        private void RestarntCapthca_Click(object sender, RoutedEventArgs e)
        {
            Capcha();
        }
    }
}
