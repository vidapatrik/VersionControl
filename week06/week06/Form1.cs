using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using week06.Entities;
using week06.MnbServiceReference;

namespace week06
{
    public partial class Form1 : Form
    {
        private BindingList<RateData> Rates = new BindingList<RateData>();
        private BindingList<string> Currencies;
        public Form1()
        {
            InitializeComponent();
            RefreshData();
        }

       
        private void GetXml()
        {
            // A változó deklarációk jobb oldalán a "var" egy dinamikus változó típus.
            // A "var" változó az első értékadás pillanatában a kapott érték típusát veszi fel, és később nem változtatható.
            // Jelen példa első sora tehát ekvivalens azzal, ha a "var" helyélre a MNBArfolyamServiceSoapClient-t írjuk.
            // Ebben a formában azonban olvashatóbb a kód, és változtatás esetén elég egy helyen átírni az osztály típusát.
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.SelectedItem.ToString(),
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };
            // Ebben az esetben a "var" a GetExchangeRates visszatérési értékéből kapja a típusát.
            // Ezért a response változó valójában GetExchangeRatesResponseBody típusú.
            var response = mnbService.GetExchangeRates(request);

            // Ebben az esetben a "var" a GetExchangeRatesResult property alapján kapja a típusát.
            // Ezért a result változó valójában string típusú.
            var result = response.GetExchangeRatesResult;
            var xml = new XmlDocument();
            xml.LoadXml(result);
            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                Rates.Add(rate);
                rate.Date = DateTime.Parse(element.GetAttribute("date"));
                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }
        }

        private void Showing()
        {
            chartRateData.DataSource = Rates;
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;


        }

        private void RefreshData()
        {
            Rates.Clear();
            Showing();
            GetXml();
            GetCurrencies();
            Rates = new BindingList<RateData>();
            Currencies = new BindingList<string>();
           dataGridView1.DataSource = Rates;

        }
        private void GetCurrencies()
        {
            MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();
            GetCurrenciesRequestBody request = new GetCurrenciesRequestBody();
            var response = mnbService.GetCurrencies(request);
            var result = response.GetCurrenciesResult;
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);
            foreach (XmlElement item in xml.DocumentElement.ChildNodes[0])
            {
                string newItem = item.InnerText;
                Currencies.Add(newItem);
            }
            comboBox1.DataSource = Currencies;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }

       


    }

