using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
//using WpfAppDataGrid.Models;

namespace WpfAppDataGrid.ViewModels
{
    class UserViewModel
    {
        public ICommand ButtonCommand { get; set; }
        public ICommand ButtonCommandSQL { get; set; }
        public ObservableCollection<Models.User> Users_obsColl { get; set; }
        public DataTable Users_datatable { get; set; }

        ObservableCollection<Models.User> users = new ObservableCollection<Models.User>();

        public UserViewModel()
        {
            ButtonCommand = new RelayCommand(new Action<object>(ShowMessage));

            ButtonCommandSQL = new RelayCommand(new Action<object>(InsertDataToDb));

            //Load Users from adding them to an Observable Collection
            LoadUsers();

            //Load Users directly into Datatable from SQL Database (table: Users)
            LoadSQLDataTable();

            //Load Users from SQL database via Datatable into Observable Collection
            LoadDatabaseUsers();
        }

        public void InsertDataToDb(object obj)
        {
            
            //var records = obj;

            SqlConnection con = new SqlConnection(@"Data Source=(local); Initial Catalog = UserDatabase; Integrated Security = True");
            con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Users VALUES (@param1, @param2)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                //cmd.Parameters.Add("@param1", DbType.String);
                //cmd.Parameters.Add("@param2", DbType.String);
                //cmd.Parameters.Add("@param3", DbType.String);

                foreach (var item in Users_obsColl)
                {
                    cmd.Parameters[0].Value = item.LoginName;
                    cmd.Parameters[1].Value = item.Password;
                    //cmd.Parameters[2].Value = item.param3;

                    cmd.ExecuteNonQuery();
                }

                con.Close();
            
        }

        private void SaveSQL(object obj)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(local); Initial Catalog = UserDatabase; Integrated Security = True");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT Users VALUES (5, 'NorthWestern')";
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void LoadUsers()
        {
            //Adds Users to Users_oc (Observable Collection)
            users.Add(new Models.User { LoginName = "Ace", Password = "123" });
            users.Add(new Models.User { LoginName = "Tec", Password = "Pa$$w0rd" });
            users.Add(new Models.User { LoginName = "BruceWayne", Password = "Batman" });
            users.Add(new Models.User { LoginName = "DonaldTrump", Password = "BestPasswords" });

            Users_obsColl = users;
        }

        void LoadSQLDataTable()
        {
            //Creates and opens a connection to SQL database. Fills datatable from SQLDataAdapter
            SqlConnection con = new SqlConnection(@"Data Source=(local); Initial Catalog = UserDatabase; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("select * from Users", con);

            con.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            Users_datatable = new DataTable();
            adp.Fill(Users_datatable);
            con.Close();
        }

        void LoadDatabaseUsers()
        {
            //Adds a User to Users_oc per row in the datatable from above
            for (int i = 0; i < Users_datatable.Rows.Count; i++)
            {
                users.Add(new Models.User
                {
                    LoginName = Users_datatable.Rows[i]["LoginName"].ToString(),
                    Password = Users_datatable.Rows[i]["Password"].ToString()
                });
            }

            Users_obsColl = users;
        }

        public void ShowMessage(object obj)
        {
            MessageBox.Show(obj.ToString());
        }

    }

    class RelayCommand : ICommand
    {
        private Action<object> _action;

        public RelayCommand(Action<object> action) { _action = action; }

        #region ICommand Members
        public bool CanExecute(object parameter) { return true; }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                _action(parameter);
            }
            else
            {
                _action("Done!");
            }
        }

        #endregion
    }

}
