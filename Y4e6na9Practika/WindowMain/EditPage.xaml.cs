using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        Entities entities = new Entities();
        private Educational_Institution selectTovar;
        private Educational_Institution tempediting;
        public event EventHandler<Educational_Institution> EduIntitUpdated;
        int count = 0;
        public EditPage(Educational_Institution selectTovar)
        {
            InitializeComponent();
            tb_header.Text = "Редактирование заведения";
            this.selectTovar = selectTovar;
            tempediting = CloneEI(selectTovar);
            DataContext = tempediting;
        }
        public EditPage()
        {
            InitializeComponent();
            tb_header.Text = "Добавление заведения";
            selectTovar = new Educational_Institution();
            DataContext = selectTovar;
        }
        private Educational_Institution CloneEI(Educational_Institution ed)
        {
            return new Educational_Institution
            {
                Educational_Institution_ID = ed.Educational_Institution_ID,
                Name = ed.Name,
                Address = ed.Address,
                Phone = ed.Phone,
                Photo = ed.Photo,
                direction = ed.direction,
                CountMect = ed.CountMect,
            };
        }
        private bool IsEIModified(Educational_Institution original, Educational_Institution modified)
        {
            // Сравниваем каждое поле объектов
            if (original.Name != modified.Name ||
                original.Address != modified.Address ||
                original.Photo != modified.Photo ||
                original.direction != modified.direction ||
                original.Phone != modified.Phone ||
                original.CountMect != modified.CountMect)
            {
                return true; // Если хотя бы одно поле отличается, возвращаем true
            }
            return false; // Если все поля идентичны, возвращаем false
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tempediting == null)
                {
                    if (!ValidateMedicationData(selectTovar))
                    {
                        return;
                    }
                    entities.Educational_Institution.Add(selectTovar);
                    EduIntitUpdated?.Invoke(this, selectTovar);
                    MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    entities.SaveChanges();
                }
                else
                {
                    // Проверка ввода для строковых полей
                    if (IsEIModified(selectTovar, tempediting))
                    {
                        Educational_Institution existingProduct = entities.Educational_Institution.Find(selectTovar.Educational_Institution_ID);
                        entities.Entry(existingProduct).CurrentValues.SetValues(tempediting);
                        EduIntitUpdated?.Invoke(this, existingProduct);
                        entities.SaveChanges();
                        MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                        MessageBox.Show("Данные не изменены! Сохранять нечего");
                }
            }
            catch (Exception ex)
            {
                // Показать сообщение об ошибке
                MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool ValidateMedicationData(Educational_Institution newEI)
        {
            // Проверка наличия обязательных полей
            if (string.IsNullOrWhiteSpace(newEI.Name) ||
                string.IsNullOrWhiteSpace(newEI.Address) ||
                string.IsNullOrWhiteSpace(newEI.Phone) ||
                string.IsNullOrWhiteSpace(newEI.direction) ||
                newEI.CountMect == null 
                )
            {
                MessageBox.Show("Ошибка: Пожалуйста, заполните все обязательные поля.");
                return false;
            }

            // Проверка корректности числовых значений
            if (newEI.CountMect < 0 || !int.TryParse(SeatsTextBox.Text, out int count))
            {
                MessageBox.Show("Ошибка: Количество мест должно быть положительным числом!");
                return false;
            }
            else
                this.count = count;

            return true;
        }

    }
}
