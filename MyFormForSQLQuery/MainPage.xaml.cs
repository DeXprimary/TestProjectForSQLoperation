using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MyFormForSQLQuery
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "workstation id=dexogen-db.mssql.somee.com;" +
                "packet size=4096;" +
                "user id=DeXogen_SQLLogin_1;" +
                "pwd=vejgaqdkkp;" +
                "data source=dexogen-db.mssql.somee.com;" +
                "persist security info=False;" +
                "initial catalog=dexogen-db";

            using (var sqlConnect = new SqlConnection(connectionString))
            {
                Console.WriteLine("Connected!");

                sqlConnect.Open();

                string commandString = $"SELECT FirstName as Имя, LastName as Фамилия, Email as Мыло, " +
                    $"Companies.Title as Название, Companies.Country as Страна " +
                    $"FROM Peoples " +
                    $"LEFT JOIN Companies ON Peoples.CompanyId = Companies.Id";

                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnect);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    int currentRow = 0;

                    while (reader.Read())
                    {
                        TextBlock name = new TextBlock() { Text = reader["Имя"].ToString() };
                        Grid.SetColumn(name, 0); Grid.SetRow(name, currentRow);
                        mainGrid.Children.Add(name);

                        TextBlock name2 = new TextBlock() { Text = reader["Фамилия"].ToString() };
                        Grid.SetColumn(name2, 1); Grid.SetRow(name2, currentRow);
                        mainGrid.Children.Add(name2);

                        TextBlock email = new TextBlock() { Text = reader["Мыло"].ToString() };
                        Grid.SetColumn(email, 2); Grid.SetRow(email, currentRow);
                        mainGrid.Children.Add(email);

                        TextBlock title = new TextBlock() { Text = reader["Название"].ToString() };
                        Grid.SetColumn(title, 3); Grid.SetRow(title, currentRow);
                        mainGrid.Children.Add(title);

                        TextBlock country = new TextBlock() { Text = reader["Страна"].ToString() };
                        Grid.SetColumn(country, 4); Grid.SetRow(country, currentRow);
                        mainGrid.Children.Add(country);

                        currentRow++;
                                               
                    }
                }

                sqlConnect.Close();
            }
        }
    }
}
