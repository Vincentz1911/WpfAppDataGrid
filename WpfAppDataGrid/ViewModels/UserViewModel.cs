using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
//using WpfAppDataGrid.Models;

namespace WpfAppDataGrid.ViewModels
{
    class UserViewModel
    {
        public ObservableCollection<Models.User> Users_obsColl { get; set; }
        public DataTable Users_datatable { get; set; }

        ObservableCollection<Models.User> users = new ObservableCollection<Models.User>();

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



    }
}
