using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using System.Xml.Serialization;

namespace _2gis_test2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public class ComKDic<TId, TName>
    {
        private readonly Dictionary<string, TName> _Name = new Dictionary<string, TName>();
        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(TId));

        public void Add(TId key, TName value)
        {
            var keySer = SerializeKey(key);
            _Name.Add(keySer, value);
        }

        public bool Remove(TId key)
        {
            var keySer = SerializeKey(key);
            return _Name.Remove(keySer);
        }

        public TName Get(TId key)
        {
            var keySer = SerializeKey(key);
            return _Name[keySer];
        }


        private string SerializeKey(TId obj)
        {
            using (var textWriter = new StringWriter(CultureInfo.InvariantCulture))
            {
                _serializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }

    }

    public class UserType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public partial class MainWindow : Window
    {
        int id_r = -1;
        string name_r, value_r;
        bool del = false;
        ComKDic<UserType, string> Users = new ComKDic<UserType, string>();
        public MainWindow()
        {
            InitializeComponent();


           
            Users.Add(new UserType { Id = 1, Name = "First_Name" }, "first_value");
            Users.Add(new UserType { Id = 2, Name = "Second_Name" }, "second_value");
            Users.Add(new UserType { Id = 3, Name = "Third_Name" }, "third_value");
            Users.Add(new UserType { Id = 4, Name = "Fourth_Name" }, "fourth_value");
            Users.Add(new UserType { Id = 5, Name = "Fifth_Name" }, "fifth_value");
            listView.Items.Add(Users.Get(new UserType { Id = 1, Name = "First_Name" }));
            listView.Items.Add(Users.Get(new UserType { Id = 2, Name = "Second_Name" }));
            listView.Items.Add(Users.Get(new UserType { Id = 3, Name = "Third_Name" }));
            listView.Items.Add(Users.Get(new UserType { Id = 4, Name = "Fourth_Name" }));
            listView.Items.Add(Users.Get(new UserType { Id = 5, Name = "Fifth_Name" }));
           
        }

       
        private void button_remove_Click(object sender, RoutedEventArgs e)
        {
         
            try
            {
                id_r = Convert.ToInt32(textBox_id.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Вы ввели неправильное значение, id - может принимать целочисленные значения");
            }
            name_r = textBox_name.Text;
            string delval = Users.Get(new UserType { Id = id_r, Name = name_r });
            try
            {
                del = Users.Remove(new UserType { Id = id_r, Name = name_r });
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка удаления! Неверные данные!");
               
            }
            if (del)
            {
                listView.Items.Remove(delval);
                MessageBox.Show("Удаление прошло успешно");
                
            }
            else
            {
                MessageBox.Show("Удаление не удалось");
            }

        }

        private void button_get_Click(object sender, RoutedEventArgs e)
        {
           // var Users = new ComKDic<UserType, string>();
            try
            {
                id_r = Convert.ToInt32(textBox_id.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Вы ввели неправильное значение, id - может принимать целочисленные значения");
            }
            name_r = textBox_name.Text;
            try
            {
                MessageBox.Show("Вывод запрошенного значения: "+ Users.Get(new UserType { Id = id_r, Name = name_r }));
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка вывода! Данные неверны!");
               
            }

        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
           // var Users = new ComKDic<UserType, string>();
            try
            {
                id_r = Convert.ToInt32(textBox_id.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Вы ввели неправильное значение, id - может принимать целочисленные значения");
            }
            name_r = textBox_name.Text;

            value_r = textBox_val.Text;
            try
            {
                Users.Add(new UserType { Id = id_r, Name = name_r }, value_r);
                listView.Items.Add(Users.Get(new UserType { Id = id_r, Name = name_r }));

            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка добавления! Неверные данные!");

            }
        }
    }
}
