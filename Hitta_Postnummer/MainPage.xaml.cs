using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Threading;
using Newtonsoft.Json;

namespace Hitta_Postnummer
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //När knappen "Sök" trycks så skickas en GET request
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
            webClient.DownloadStringAsync(new Uri("http://yourmoneyisnowmymoney.com/api/zipcodes/?zipcode=" + TextBoxSearch.Text));
        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Result)) //Om en string återfås
                {
                    ListBox1.DataContext = null; //"Clearar" listboxen
                    var root1 = JsonConvert.DeserializeObject<RootObject>(e.Result);
                    if (root1.status_code == 100) //Om status-koden är 100 vilket betyder OK
                    {
                        SearchResult.Text = "Din sökning efter : " + TextBoxSearch.Text + " genererade " + root1.results.Count + " resultat.";
                        ListBox1.DataContext = root1.results; //Sätt den tillbakafådda listan som data till listboxen
                    }
                    else if (root1.status_code == 900) //900 = inga träffar
                    {
                        SearchResult.Text = "Din sökning genererade 0 träffar.";
                    }
                    else //Vid annat error
                    {
                        SearchResult.Text = "Det uppstod ett fel vid din sökning, använd minst 3 siffror i din sökning.";
                    }
                }

            }
            catch (Exception ex) //Om appen oväntat crashar skriv ut felmeddelande
            {
                MessageBox.Show(ex.ToString());
            }


        }


    }
}