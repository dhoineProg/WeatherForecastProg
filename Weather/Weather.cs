using System;
using System.Windows.Forms;
using Weather.Classes;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Weather
{
    public partial class Weather : Form
    {
        Regex r = new Regex(@"[\d.!№;%:?*()&^@,/|!#]");
        const string weatherAPI = "47590611fd856269d88abe3536e1df74"; // Ключ API

        public Weather()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string cityName = textBox1.Text.Trim(); // Запись и удаление лишних символов до и после в поле для названия города

            Match m = r.Match(cityName);
            if (m.Success || cityName.Length < 2) // Проверка на корректность введенных данных
            {
                Alert form2 = new Alert();
                form2.Show(); // Вывод сообщения об ошибке
            }
            else
            {
                HttpClient client = new HttpClient(); // Создание экземпляра класса 
                string link = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={weatherAPI}&units=metric"; //Обращение к данным OpenWeather API
                string response = await client.GetStringAsync(link); // Получение данных от OpenWeather API

                var json = JObject.Parse(response); // Перебор json объекта
                string temperature = json["main"]["temp"].ToString(); // Вывод температуры из данных OpenWeather API 
                string windSpeed = json["wind"]["speed"].ToString(); // Вывод скорости ветра из данных OpenWeather API 
                string description = json["weather"][0]["description"].ToString(); // Вывод описания погоды из данных OpenWeather API 

                textBox2.Text = temperature; // Запись данных о температуре в textBox2
                textBox3.Text = windSpeed; // Запись данных о скорости ветра в textBox3
                textBox4.Text = description; // Запись описания погоды в textBox4
            }
        }

        private void button2_Click(object sender, EventArgs e) // Кнопка закрытия программы
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

    }
}
