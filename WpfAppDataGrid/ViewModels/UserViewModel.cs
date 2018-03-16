using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using WpfAppDataGrid.Models;

namespace WpfAppDataGrid.ViewModels
{
    class UserViewModel
    {
        public ObservableCollection<User> Users_oc { get; set; }
        public DataTable Users_dt { get; set; }

        ObservableCollection<User> users = new ObservableCollection<User>();

        public UserViewModel()
        {
            //Load Users from adding them to an Observable Collection
            LoadUsers();

            //Load Users directly into Datatable from SQL Database (table: Users)
            LoadSQLDataTable();

            //Load Users from SQL database via Datatable into Observable Collection
            LoadDatabaseUsers();
        }

        void LoadUsers()
        {
            //Adds Users to Users_oc (Observable Collection)
            users.Add(new User { LoginName = "Ace", Password = "123" });
            users.Add(new User { LoginName = "Tec", Password = "Pa$$w0rd" });
            users.Add(new User { LoginName = "BruceWayne", Password = "Batman" });
            users.Add(new User { LoginName = "DonaldTrump", Password = "BestPasswords" });

            Users_oc = users;
        }

        void LoadSQLDataTable()
        {
            //Creates and opens a connection to SQL database. Fills datatable from SQLDataAdapter
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=(local); Initial Catalog = UserDatabase; Integrated Security = True";
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Users";
            cmd.Connection = con;           
            
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dat = new DataTable("Users");
            adp.Fill(dat);

            Users_dt = dat;
        }

        void LoadDatabaseUsers()
        {
            //Adds a User to Users_oc per row in the datatable from above
            for (int i = 0; i < Users_dt.Rows.Count; i++)
            {
                users.Add(new User
                {
                    LoginName = Users_dt.Rows[i]["LoginName"].ToString(),
                    Password = Users_dt.Rows[i]["Password"].ToString()
                });
            }

            Users_oc = users;
        }



    }
}
