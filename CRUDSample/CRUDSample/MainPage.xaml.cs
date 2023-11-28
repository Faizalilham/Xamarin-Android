using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CRUDSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            PopulateEmployeeList();
        }
        public void PopulateEmployeeList()
        {
            EmployeeList.ItemsSource = null;
            EmployeeList.ItemsSource = DependencyService.Get<ISQLite>().GetTransaction();
        }

        private void AddEmployee(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddEmployeePage(null));
        }

        private void EditEmployee(object sender, ItemTappedEventArgs e)
        {
            Transactions details = e.Item as Transactions;
            if(details!=null)
            {
                Navigation.PushAsync(new AddEmployeePage(details));
            }
        }

        private async void DeleteEmployee(object sender, EventArgs e)
        {
            bool res=await DisplayAlert("Message", "Do you want to delete transaction?", "Ok", "Cancel");
            if(res)
            {
                try
                {
                    var menu = sender as MenuItem;
                    Transactions details = menu.CommandParameter as Transactions;
                    DependencyService.Get<ISQLite>().DeleteTransaction(details.IdTransactions);
                    Console.WriteLine($"TESTYGY SUUU ${details.IdTransactions}");
                    PopulateEmployeeList();
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"MESSI ${exception}");
                    throw;
                }
            }          
        }
    }
}
