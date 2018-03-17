using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
//using WpfAppDataGrid.Models; //Can use 'using' instead of Models.User

namespace WpfAppDataGrid.ViewModels
{
    class UserViewModel
    {
        //ICommand buttons property, so accessible from View
        public ICommand ButtonCommand_AddUser { get; set; }
        public ICommand ButtonCommand_UpdateSQL { get; set; }
        //ObservableCollection list used for datacontect in View
        public ObservableCollection<Models.User> Users_obsColl { get; set; }

        private DataTable Users_datatable { get; set; }

        ObservableCollection<Models.User> users = new ObservableCollection<Models.User>();

        public UserViewModel()
        {
            ButtonCommand_AddUser = new RelayCommand(new Action<object>(AddNewUser));
            ButtonCommand_UpdateSQL = new RelayCommand(new Action<object>(ObsCollection2SQL));

            //Load Users from SQL database via Datatable into Observable Collection
            Datatable2List();
        }

        //Adds new user. Property in UserModel will fill out if null.
        private void AddNewUser(object obj)
        {
            MessageBox.Show(obj.ToString());
            users.Add(new Models.User { LoginName = null, Password = null });
        }

        //Add user and updates Users_oc with datatable from SQL2Datatable()
        private void Datatable2List()
        {
            SQL2Datatable();
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

        //Creates and opens a connection to SQL database. Fills datatable from SQLDataAdapter
        private void SQL2Datatable()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(local); Initial Catalog = UserDatabase; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("select * from Users", con);

            con.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            Users_datatable = new DataTable();
            adp.Fill(Users_datatable);
            con.Close();
        }

        //Connects to SQL, truncates table and refills it with data from Observable Collection list
        private void ObsCollection2SQL(object obj)
        {
            MessageBox.Show(obj.ToString());
            SqlConnection con = new SqlConnection(@"Data Source=(local); Initial Catalog = UserDatabase; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("TRUNCATE TABLE Users", con);
            con.Open();
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("INSERT INTO Users (LoginName,Password) VALUES (@LoginName,@Password)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@LoginName", DbType.String);
            cmd.Parameters.AddWithValue("@Password", DbType.String);

            foreach (var item in Users_obsColl)
            {
                cmd.Parameters[0].Value = item.LoginName;
                cmd.Parameters[1].Value = item.Password;
                cmd.ExecuteNonQuery();
            }
            con.Close();
            SQL2Datatable();
        }
    }

    // ICommand
    class RelayCommand : ICommand
    {
        private Action<object> _action;
        public RelayCommand(Action<object> action) { _action = action; }

        #region ICommand Members
        public bool CanExecute(object parameter) { return true; }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        { if (parameter != null) { _action(parameter); } else { _action("If Command parameter is not set, this will be the object"); } }
        #endregion
    }
}
