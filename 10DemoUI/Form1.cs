namespace _10DemoUI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Core;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Serialization;

    public partial class Form1 : Form
    {
        private const string Path = "..\\..\\..\\cars.csv";
        delegate void SetTextCallback(string text);

        public Form1()
        {
            this.InitializeComponent();
        }

        private void GetDataBtn_Click(object sender, EventArgs e)
        {
            Task task1 = Task.Factory.StartNew(() =>
            {
                this.Log("start to process file");
            }
            ).ContinueWith(ant =>
            {
                var cars = this.ProcessCarsFile(Path).ToList();
                return cars;
            }).ContinueWith(ant => 
                {
                    this.DisplayCars(ant.Result);
                    return ant;
                }
            ).ContinueWith(ant =>
                {
                    this.Log($"finish to process file. {ant.Result} cars downloaded");
                }
            );
            
        }

       

        private void DisplayCars(List<Car> cars)
        {
            foreach (var car in cars)
            {
                this.AppendToContent(car.ToString());
            }
        }

        private IEnumerable<Car> ProcessCarsFile(string filePath)
        {
            var cars = new List<Car>(600);
            var lines = File.ReadAllLines(filePath).Skip(2);

            foreach (var line in lines)
            {
                cars.Add(Car.Parse(line));
            }

            Thread.Sleep(TimeSpan.FromSeconds(3)); // simulate some work

            return cars;
        }

        public void Log(string s)
        {
            logTbx.Invoke(new Action(() => this.logTbx.AppendText($"{DateTime.Now} - {s}{Environment.NewLine}")));
        }

        public void AppendToContent(string s)
        {
            contentTxb.Invoke(new Action(() => this.contentTxb.AppendText($"{s}{Environment.NewLine}")));
        }
    }
}
