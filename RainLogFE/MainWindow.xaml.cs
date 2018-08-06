using Newtonsoft.Json;
using RainLogBO;
using RainLogBO.Helpers;
using RainLogBO.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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


namespace RainLogFE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<GaugeResult> GaugeResult { get; set; }
        public ObservableCollection<Selection> DayStart { get; set; }
        public string DayStartSelected { get; set; }
        public ObservableCollection<Selection> DayEnd { get; set; }
        public string DayEndSelected { get; set; }
        public ObservableCollection<Selection> MonthStart { get; set; }
        public string MonthStartSelected { get; set; }
        public ObservableCollection<Selection> MonthEnd { get; set; }
        public string MonthEndSelected { get; set; }
        public ObservableCollection<Selection> YearStart { get; set; }
        public string YearStartSelected { get; set; }
        public ObservableCollection<Selection> YearEnd { get; set; }
        public string YearEndSelected { get; set; }
        public double Latitude { get; set; } = 32.08737414662593;
        public double Longitude { get; set; } = -110.69535881280899;
        public double Radius { get; set; } = 50;
        public string Type { get; set; } = "Circle";

        public MainWindow()
        {
            InitializeComponent();
            InititailizeData();

        }
        /// <summary>
        /// Initialize data
        /// </summary>
        public void InititailizeData()
        {
            var sData = new SelectionData();
            var dayCollection = sData.GetData("days.json");
            var monthCollection = sData.GetData("months.json");
            var yearCollection = sData.GetData("years.json");

            DayStart = dayCollection;
            DayEnd = dayCollection;
            MonthStart = monthCollection;
            MonthEnd = monthCollection;
            YearStart = yearCollection;
            YearEnd = yearCollection;
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //format dates
            var startDate = string.Format("{0}-{1}-{2}", YearStartSelected, MonthStartSelected, DayStartSelected);
            var endDate = string.Format("{0}-{1}-{2}", YearEndSelected, MonthEndSelected, DayEndSelected);
            double totalRain = 0;

            //Set proper endpoint
            //TODO: Add gauge endpoints
            var endpoint = string.Empty;
            var baseUrl = ConfigurationManager.AppSettings["rainLog:BaseUrl"];
            var dailyUrl = ConfigurationManager.AppSettings["rainLog:DailyUrl"];
            var monthlyUrl = ConfigurationManager.AppSettings["rainLog:MonthlyUrl"];
            var operation = ConfigurationManager.AppSettings["rainLog:Operation"];

            endpoint = baseUrl + dailyUrl + operation;



            //Create data object

            var rainQuality = new List<string>();
            rainQuality.Add("Good");
            var center = new Center
            {
                Lat = Latitude,
                Lng = Longitude

            };
            var region = new Region
            {
                Radius = Radius,
                Type = Type,
                Center = center
            };

            var pagination = new Pagination
            {
                Offset = 0,
                Limit = 1000
            };
            
            var locationData = new LocationData
            {
                Pagination = pagination,
                Quality = rainQuality,
                DateRangeStart = startDate,
                DateRangeEnd = endDate,
                Region = region,
            };

            //Get result data
            var apiManager = new APIManager();
            var results = new List<Result>();
            for (int i = 0; i < 10; i++)
            {
                locationData.Pagination.Offset = i * 1000;
                try
                {
                    var result = apiManager.Post(endpoint, locationData);
                    if(result != "")
                    {
                        results.AddRange(JsonConvert.DeserializeObject<ObservableCollection<Result>>(result));
                    }
                }
                catch(Exception exc)
                {
                    //
                }

            }
            //var results = JsonConvert.DeserializeObject<ObservableCollection<Result>>(apiManager.Post(endpoint, locationData));
            

            var gauges = results.Select(r => r.GaugeId).Distinct().ToList();
            var gaugeTotals = results.Select(r => (r.GaugeId, r.RainAmount));
            GaugeResult = new List<GaugeResult>();
            //Totals per gauge
            foreach (var gauge in gauges)
            {
                double totalGaugeAmount = 0;
                foreach(var gTotal in gaugeTotals)
                {
                    if (gTotal.GaugeId == gauge)
                        
                        totalGaugeAmount += gTotal.RainAmount ?? 0;
                }
                GaugeResult.Add(new GaugeResult
                {
                    GaugeId = gauge.Value,
                    TotalRain = totalGaugeAmount
                });
                totalGaugeAmount = 0;
            }

            //Totals for area
            foreach(var result in GaugeResult)
            {
                totalRain += result.TotalRain;
            }
            lstResults.ItemsSource = GaugeResult.OrderBy(g => g.TotalRain);

            lblResult.Content = Math.Round(totalRain, 2); ;

            var report = new Report();
            report.CreateCsv(startDate + "__" + endDate, GaugeResult);


        }

        private void Cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            switch (comboBox.Name)
            {
                case "cmbDayStart":
                    DayStartSelected = comboBox.SelectedValue.ToString();
                    break;
                case "cmbDayEnd":
                    DayEndSelected = comboBox.SelectedValue.ToString();
                    break;
                case "cmbMonthStart":
                    MonthStartSelected = comboBox.SelectedValue.ToString();
                    break;
                case "cmbMonthEnd":
                    MonthEndSelected = comboBox.SelectedValue.ToString();
                    break;
                case "cmbYearStart":
                    YearStartSelected = comboBox.SelectedValue.ToString();
                    break;
                case "cmbYearEnd":
                    YearEndSelected = comboBox.SelectedValue.ToString();
                    break;
                default:
                    break;

            }

        }
    }
}
