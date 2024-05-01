using incomeExpensTrckAVALONIA.Models;
using MongoDB.Bson;
using Realms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace incomeExpensTrckAVALONIA.Services
{
    public class ExpenseService
    {
        // Start a realm connection
        private Realm realm;
        public string StatusMessage;

        private void Init()
        {
            if (realm != null) return;

            realm = Realm.GetInstance(); // Opens a local realm instance
            //Console.WriteLine($"Realm is located at: {realm.Config.DatabasePath}");
             Debug.WriteLine("Realm initialized.");

        }

        public List<Expense> GetExpenses()
        {
            try
            {
                Init();
                var expenses = realm.All<Expense>().ToList();
                Console.WriteLine($"Fetched {expenses.Count}  {expenses} expenses from the database.");
                foreach (var expense in expenses)
                {
                    Console.WriteLine($"Expense: {expense.Day}");
                }
                return expenses;
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrieve expenses data.";
            }
            return new List<Expense>();
        }

        public async void AddExpense(Expense expense)
        {
            try
            {
                Init(); // make sure the realm connection is open

                if (expense == null)
                    throw new ArgumentNullException("Invalid record");

                await realm.WriteAsync(() =>
                {
                    realm.Add(expense);
                });
                Debug.WriteLine("Expense added successfully: " + expense.ToString());
                StatusMessage = "Expense added successfully.";
                Debug.WriteLine(StatusMessage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to add expense. Error: {ex.Message}");
                StatusMessage = $"Failed to add expense. {ex}";
            }
        }

        public static Expense GetExpense(string id)
        {

            try
            {
                var realm = Realm.GetInstance();
                if (realm == null) return null;
                var expenseToShow = realm.All<Expense>().FirstOrDefault(d => d.Id == id);
                Console.WriteLine($"Fetched {expenseToShow} expense from the database.");
                if (expenseToShow != null)
                {
                    Console.WriteLine($"The one Expense: {expenseToShow.Day}");
                }
                return expenseToShow;
            }
            catch (Exception)
            {
                //StatusMessage = "Failed to retrieve expense data.";
                Debug.WriteLine("Error", "Failed to retrieve expense data.", "Ok");
            }
            return null;
        }

        public string UpdateExpense(Expense editedExpense)
        {
            Init();
            // open a thread-safe transaction
            using var transaction = realm.BeginWrite();
            try
            {
                var expenseToUpdate = realm.All<Expense>().FirstOrDefault(d => d.Id == editedExpense.Id);
                if (expenseToUpdate != null)
                {
                    try
                    {
                        realm.WriteAsync(() =>
                        {
                            //expenseToUpdate.Date = editedExpense.Date;
                            expenseToUpdate.Day = editedExpense.Day;
                            expenseToUpdate.Month = editedExpense.Month;
                            expenseToUpdate.Year = editedExpense.Year;
                            expenseToUpdate.Amount = editedExpense.Amount;
                            expenseToUpdate.Category = editedExpense.Category;
                            expenseToUpdate.Account = editedExpense.Account;
                            expenseToUpdate.Location = editedExpense.Location;
                            expenseToUpdate.Note = editedExpense.Note;
                            expenseToUpdate.Description = editedExpense.Description;
                        });



                        if (transaction.State == TransactionState.Running)
                        {
                            transaction.Commit();
                            StatusMessage = "Expense updated successfully.";
                            //return expenseToDelete.Id; // this is so that we can use the id to remove the item from the list
                        }
                    }
                    catch (Exception ex)
                    {
                        StatusMessage = "Update Failed";
                        Console.WriteLine(ex.Message);
                        // Something went wrong; roll back the transaction
                        if (transaction.State != TransactionState.RolledBack &&
                            transaction.State != TransactionState.Committed)
                        {
                            transaction.Rollback();
                        }
                    }

                }
                else
                {
                    StatusMessage = "Expense not found.";
                }
            }
            catch (Exception)
            {
                StatusMessage = "Failed to update the expense.";
            }
            return string.Empty;
        }

        //public List<Expense> GetExpenses()
        //{
        //    var expense1 = new Expense()
        //    {
        //        Id = ObjectId.GenerateNewId().ToString(),
        //        Day = "15",
        //        Month = "2",
        //        Year = "2024",
        //        Amount = 10.00,
        //        Category = "Culture",
        //        Account = "Cash",
        //        Location = "Starbucks",
        //        Note = "Food",
        //        Description = "Coffee"
        //    };

        //    var expense2 = new Expense()
        //    {
        //        Id = ObjectId.GenerateNewId().ToString(),
        //        Day = "10",
        //        Month = "5",
        //        Year = "2024",
        //        Amount = 120.00,
        //        Category = "Groceries",
        //        Account = "Card",
        //        Location = "Local Supermarket",
        //        Note = "Weekly groceries",
        //        Description = "Food and supplies"
        //    };

        //    var expense3 = new Expense()
        //    {
        //        Id = ObjectId.GenerateNewId().ToString(),
        //        Day = "25",
        //        Month = "1",
        //        Year = "2024",
        //        Amount = 350.00,
        //        Category = "Clothes",
        //        Account = "Card",
        //        Location = "H&M",
        //        Note = "Clothes",
        //        Description = "Supplies"
        //    };

        //    var expense4 = new Expense()
        //    {
        //        Id = ObjectId.GenerateNewId().ToString(),
        //        Day = "11",
        //        Month = "9",
        //        Year = "2024",
        //        Amount = 50.00,
        //        Category = "Health",
        //        Account = "Bank Accounts",
        //        Location = "Hospital",
        //        Note = "Clothes",
        //        Description = "Supplies"
        //    };

        //    var expense5 = new Expense()
        //    {
        //        Id = ObjectId.GenerateNewId().ToString(),
        //        Day = "15",
        //        Month = "2",
        //        Year = "2024",
        //        Amount = 900.00,
        //        Category = "Education",
        //        Account = "Bank Accounts",
        //        Location = "HAMK",
        //        Note = "Clothes",
        //        Description = "Supplies"
        //    };

        //return new List<Expense> { expense1, expense2, expense3, expense4, expense5 };

        //}
    }
}