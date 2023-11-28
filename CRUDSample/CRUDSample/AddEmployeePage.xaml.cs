using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRUDSample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEmployeePage : ContentPage
	{
        Transactions employeeDetails;
        private Transaction a;
       
		public AddEmployeePage (Transactions details)
		{
			InitializeComponent ();
            pickerProduct.ItemsSource = DependencyService.Get<ISQLite>().GetAllGoods();
            pickerUser.ItemsSource = DependencyService.Get<ISQLite>().GetAllUser();
           
            if(details!=null)
            {
                
                employeeDetails = details;
                a = DependencyService.Get<ISQLite>().TransactionById(employeeDetails.IdTransactions);
                Console.WriteLine($"TESTYGY SUUUIS ${employeeDetails.IdTransactions} ${employeeDetails.name}");
                PopulateDetails(employeeDetails);
            }
		}

        private void PopulateDetails(Transactions details)
        {
            pickerProduct.Title = details.name;
            pickerUser.Title = details.user;
            date.Text = details.DateTransaction;
            quantity.Text = details.Quantity.ToString();
            saveBtn.Text = "Update";
            this.Title = "Edit Employee";
        }

        private void SaveTransaction(object sender, EventArgs e)
        {
            Goods goods = pickerProduct.SelectedItem as Goods;
            User users = pickerUser.SelectedItem as User;
            if(saveBtn.Text=="Save")
            {
                if (int.TryParse(quantity.Text, out int parsedQuantity))
                {
                    
                    Transaction transaction = new Transaction();
                    transaction.IdGoods = goods.IdGoods == null ? 1 : goods.IdGoods ;
                    transaction.IdUser = users.IdUser == null ? 1 : users.IdUser;
                    transaction.Quantity = parsedQuantity;
                    transaction.DateTransaction = date.Text;

                    
                    bool res = DependencyService.Get<ISQLite>().SaveTransaction(transaction);
                    if (res)
                    {
                        Navigation.PopAsync();
                    }
                    else
                    {
                        DisplayAlert("Message", "Data Failed To Save", "Ok");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid quantity input. Please enter a valid integer.");
                }
                
            }
            else
            {
                
               
                if (int.TryParse(quantity.Text, out int parsedQuantity))
                {
                    
                    Console.WriteLine($"RONALDO ${a.IdTransaction} ${a.IdGoods} ${a.IdUser} ${a.Quantity} ${a.DateTransaction}");
                    
                    Transaction transactionUpdate = new Transaction();
                    // update employee
                    if (pickerProduct.SelectedIndex == -1 && pickerUser.SelectedIndex == -1)
                    {
                       
                        transactionUpdate.IdTransaction = employeeDetails.IdTransactions;
                        transactionUpdate.IdGoods = a.IdGoods;
                        transactionUpdate.IdUser = a.IdGoods;
                        transactionUpdate.Quantity = parsedQuantity;
                        transactionUpdate.DateTransaction = date.Text;
                    }else if (pickerProduct.SelectedIndex == -1)
                    {
                        transactionUpdate.IdTransaction = employeeDetails.IdTransactions;
                        transactionUpdate.IdGoods = a.IdGoods;
                        transactionUpdate.IdUser = users.IdUser;
                        transactionUpdate.Quantity = parsedQuantity;
                        transactionUpdate.DateTransaction = date.Text;
                        
                    }else if (pickerUser.SelectedIndex == -1)
                    {
                        transactionUpdate.IdTransaction = employeeDetails.IdTransactions;
                        transactionUpdate.IdGoods = goods.IdGoods;
                        transactionUpdate.IdUser = a.IdUser;
                        transactionUpdate.Quantity = parsedQuantity;
                        transactionUpdate.DateTransaction = date.Text;
                    }
                    else
                    {
                        
                        transactionUpdate.IdTransaction = employeeDetails.IdTransactions;
                        transactionUpdate.IdGoods = goods.IdGoods;
                        transactionUpdate.IdUser = users.IdUser;
                        transactionUpdate.Quantity = parsedQuantity;
                        transactionUpdate.DateTransaction = date.Text;
                    }

                    try
                    {
                        bool res = DependencyService.Get<ISQLite>().UpdateTransaction(transactionUpdate);
                        if (res)
                        {
                            Navigation.PopAsync();
                        }
                        else
                        {
                            DisplayAlert("Message", "Data Failed To Update", "Ok");
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"SUUUUUSS ${exception}");
                        throw;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid quantity input. Please enter a valid integer.");
                }
                
            }
        }
    }
}