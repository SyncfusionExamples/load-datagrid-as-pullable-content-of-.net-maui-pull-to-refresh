﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PullToRefreshTemplate
{
    public class OrderInfoViewModel : INotifyPropertyChanged
    {
        #region private variables

        private List<DateTime>? orderedDates;
        private Random random = new Random();
        private ObservableCollection<OrderInfo>? ordersInfo;

        #endregion

        #region MainGrid DataSource
        private string[] genders = new string[]
        {
            "Male",
            "Female",
            "Female",
            "Female",
            "Male",
            "Male",
            "Male",
            "Male",
            "Male",
            "Male",
            "Male",
            "Male",
            "Female",
            "Female",
            "Female",
            "Male",
            "Male",
            "Male",
            "Female",
            "Female",
            "Female",
            "Male",
            "Male",
            "Male",
            "Male"
        };

        private string[] firstNames = new string[]
        {
            "Kyle",
            "Gina",
            "Irene",
            "Katie",
            "Michael",
            "Oscar",
            "Ralph",
            "Torrey",
            "William",
            "Bill",
            "Daniel",
            "Frank",
            "Brenda",
            "Danielle",
            "Fiona",
            "Howard",
            "Jack",
            "Larry",
            "Holly",
            "Jennifer",
            "Liz",
            "Pete",
            "Steve",
            "Vince",
            "Zeke",
            "Gary",
            "Maciej",
            "Shelley",
            "Linda",
            "Carla",
            "Carol",
            "Shannon",
            "Jauna",
            "Michael",
            "Terry",
            "John",
            "Gail",
            "Mark",
            "Martha",
            "Julie",
            "Janeth",
            "Twanna",
            "Frank",
            "Crowley",
            "Waddell",
            "Irvine",
            "Keefe",
            "Ellis",
            "Gable",
            "Mendoza",
            "Rooney",
            "Lane",
            "Landry",
            "Perry",
            "Perez",
            "Newberry",
            "Betts",
            "Fitzgerald",
            "Adams",
            "Owens",
            "Thomas",
            "Doran",
            "Jefferson",
            "Spencer",
            "Vargas",
            "Grimes",
            "Edwards",
            "Stark",
            "Cruise",
            "Fitz",
            "Chief",
            "Blanc",
            "Stone",
            "Williams",
            "Jobs",
            "Holmes"
        };
        private string[] lastNames = new string[]
        {
            "Adams",
            "Crowley",
            "Ellis",
            "Gable",
            "Irvine",
            "Keefe",
            "Mendoza",
            "Owens",
            "Rooney",
            "Waddell",
            "Thomas",
            "Betts",
            "Doran",
            "Holmes",
            "Jefferson",
            "Landry",
            "Newberry",
            "Perez",
            "Spencer",
            "Vargas",
            "Grimes",
            "Edwards",
            "Stark",
            "Cruise",
            "Fitz",
            "Chief",
            "Blanc",
            "Perry",
            "Stone",
            "Williams",
            "Lane",
            "Jobs"
        };

        private string[] customerID = new string[]
        {
            "Alfki",
            "Frans",
            "Merep",
            "Folko",
            "Simob",
            "Warth",
            "Vaffe",
            "Furib",
            "Seves",
            "Linod",
            "Riscu",
            "Picco",
            "Blonp",
            "Welli",
            "Folig"
        };

        private string[] shipCountry = new string[]
        {
            "Argentina",
            "Austria",
            "Belgium",
            "Brazil",
            "Canada",
            "Denmark",
            "Finland",
            "France",
            "Germany",
            "Ireland",
            "Italy",
            "Mexico",
            "Norway",
            "Poland",
            "Portugal",
            "Spain",
            "Sweden",
            "UK",
            "USA",
        };

        public event PropertyChangedEventHandler? PropertyChanged;

        private Dictionary<string, string[]> shipCity = new Dictionary<string, string[]>();

        #endregion

        /// <summary>
        /// Initializes a new instance of the OrderInfoRepository class.
        /// </summary>
        public OrderInfoViewModel()
        {
            this.OrdersInfo = this.GetOrderDetails(100);
        }

        #region GetOrderDetails

        /// <summary>
        /// Generates record rows with given count
        /// </summary>
        /// <param name="count">integer type of count parameter</param>
        /// <returns>stored Items Values</returns>
        public ObservableCollection<OrderInfo> GetOrderDetails(int count)
        {
            this.SetShipCity();
            this.orderedDates = this.GetDateBetween(2000, 2014, count);
            ObservableCollection<OrderInfo> orderDetails = new ObservableCollection<OrderInfo>();
            int index = 0;
            for (int i = 10001; i <= count + 10000; i++)
            {
                index = index + 1;
                var shipcountry = this.shipCountry[this.random.Next(5)];
                var shipcitycoll = this.shipCity[shipcountry];

                var ord = new OrderInfo()
                {
                    OrderID = i,
                    CustomerID = this.customerID[this.random.Next(15)],
                    EmployeeID = i - 10000 + 2700,
                    FirstName = index > 72 ? this.firstNames[this.random.Next(40)] : this.firstNames[index],
                    LastName = this.lastNames[this.random.Next(15)],
                    Gender = this.genders[this.random.Next(5)],
                    ShipCountry = shipcountry,
                    ShippingDate = this.orderedDates[i - 10001],
                    Freight = Math.Round(this.random.Next(1000) + this.random.NextDouble(), 2),
                    Price = Math.Round(this.random.Next(1000) + this.random.NextDouble(), 3),
                    IsClosed = (i % this.random.Next(1, 10) > 2) ? true : false,
                    ShipCity = shipcitycoll[this.random.Next(shipcitycoll.Length - 1)],
                };
                orderDetails.Add(ord);
            }

            return orderDetails;
        }

        /// <summary>
        /// Used this method to add the More Items in View while view was refreshing
        /// </summary>
        /// <param name="count">record rows count</param>
        /// <returns>added Items Value</returns>
        internal void RefreshItemsource(int count)
        {
            for (int i = 10101; i <= count + 10100; i++)
            {
                var ordeshipcity = this.shipCity[this.shipCountry[this.random.Next(0, 5)]];
                var order = new OrderInfo()
                {
                    OrderID = i,
                    CustomerID = this.customerID[this.random.Next(15)],
                    EmployeeID = i - 10100 + 2800,
                    FirstName = this.firstNames[this.random.Next(15)],
                    LastName = this.lastNames[this.random.Next(15)],
                    Gender = this.genders[this.random.Next(5)],
                    ShipCountry = this.shipCountry[this.random.Next(5)],
                    ShippingDate = DateTime.Now,
                    Freight = Math.Round(this.random.Next(1000) + this.random.NextDouble(), 2),
                    IsClosed = (i % this.random.Next(1, 10) > 5) ? true : false,
                    ShipCity = ordeshipcity[0],
                };

                this.OrdersInfo!.Insert(0, order);
            }
        }


        #endregion

        /// Gets or sets the value of OrdersInfo and notifies user when value gets changed
        public ObservableCollection<OrderInfo>? OrdersInfo
        {
            get
            {
                return this.ordersInfo;
            }

            set
            {
                this.ordersInfo = value;
                this.RaisePropertyChanged("OrdersInfo");
            }
        }

        private void RaisePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Used to generate DateTime and returns the value
        /// </summary>
        /// <param name="startYear">integer type of parameter startYear</param>
        /// <param name="endYear">integer type of parameter endYear</param>
        /// <param name="count">integer type of parameter count</param>
        /// <returns>returns the generated DateTime</returns>
        private List<DateTime> GetDateBetween(int startYear, int endYear, int count)
        {
            List<DateTime> date = new List<DateTime>();
            Random d = new Random(1);
            Random m = new Random(2);
            Random y = new Random(startYear);
            for (int i = 0; i < count; i++)
            {
                int year = y.Next(startYear, endYear);
                int month = m.Next(3, 13);
                int day = d.Next(1, 31);

                date.Add(new DateTime(year, month, day));
            }

            return date;
        }

        /// <summary>
        /// This method used to store the string items collections Value
        /// </summary>
        private void SetShipCity()
        {
            string[] argentina = new string[]
            {
                "Rosario"
            };

            string[] austria = new string[]
            {
                "Graz",
                "Salzburg"
            };

            string[] belgium = new string[]
            {
                "Bruxelles",
                "Charleroi"
            };

            string[] brazil = new string[]
            {
                "Campinas",
                "Resende",
                "Recife",
                "Manaus"
            };

            string[] canada = new string[]
            {
                "Montréal",
                "Tsawassen",
                "Vancouver"
            };

            string[] denmark = new string[]
            {
                "Århus",
                "København"
            };

            string[] finland = new string[]
            {
                "Helsinki",
                "Oulu"
            };

            string[] france = new string[]
            {
                "Lille",
                "Lyon",
                "Marseille",
                "Nantes",
                "Paris",
                "Reims",
                "Strasbourg",
                "Toulouse",
                "Versailles"
            };

            string[] germany = new string[]
            {
                "Aachen",
                "Berlin",
                "Brandenburg",
                "Cunewalde",
                "Frankfurt",
                "Köln",
                "Leipzig",
                "Mannheim",
                "München",
                "Münster",
                "Stuttgart"
            };

            string[] ireland = new string[]
            {
                "Cork"
            };

            string[] italy = new string[]
            {
                "Bergamo",
                "Reggio",
                "Torino"
            };

            string[] mexico = new string[]
            {
                "México D.F."
            };

            string[] norway = new string[]
            {
                "Stavern"
            };

            string[] poland = new string[]
            {
                "Warszawa"
            };

            string[] portugal = new string[]
            {
                "Lisboa"
            };

            string[] spain = new string[]
            {
                "Barcelona",
                "Madrid",
                "Sevilla"
            };

            string[] sweden = new string[]
            {
                "Bräcke",
                "Luleå"
            };

            string[] switzerland = new string[]
            {
                "Bern",
                "Genève"
            };

            string[] uk = new string[]
            {
                "Colchester",
                "Hedge End",
                "London"
            };

            string[] usa = new string[]
            {
                "Albuquerque",
                "Anchorage",
                "Boise",
                "Butte",
                "Elgin",
                "Eugene",
                "Kirkland",
                "Lander",
                "Portland",
                "San Francisco",
                "Seattle",
            };

            string[] venezuela = new string[]
            {
                "Barquisimeto",
                "Caracas", "I. de Margarita",
                "San Cristóbal"
            };

            if (this.shipCity.Count == 0)
            {
                this.shipCity.Add("Argentina", argentina);
                this.shipCity.Add("Austria", austria);
                this.shipCity.Add("Belgium", belgium);
                this.shipCity.Add("Brazil", brazil);
                this.shipCity.Add("Canada", canada);
                this.shipCity.Add("Denmark", denmark);
                this.shipCity.Add("Finland", finland);
                this.shipCity.Add("France", france);
                this.shipCity.Add("Germany", germany);
                this.shipCity.Add("Ireland", ireland);
                this.shipCity.Add("Italy", italy);
                this.shipCity.Add("Mexico", mexico);
                this.shipCity.Add("Norway", norway);
                this.shipCity.Add("Poland", poland);
                this.shipCity.Add("Portugal", portugal);
                this.shipCity.Add("Spain", spain);
                this.shipCity.Add("Sweden", sweden);
                this.shipCity.Add("Switzerland", switzerland);
                this.shipCity.Add("UK", uk);
                this.shipCity.Add("USA", usa);
                this.shipCity.Add("Venezuela", venezuela);
            }
        }
    }
}