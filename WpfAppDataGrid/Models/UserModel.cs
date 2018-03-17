using System.ComponentModel;

namespace WpfAppDataGrid.Models
{
    class UserModel
    {

    }

    class User : INotifyPropertyChanged
    {
        //Properties
        private string loginName;
        public string LoginName
        {
            get { return loginName; }

            set
            {
                if (loginName != value)
                {
                    loginName = value;
                    OnPropertyChanged("LoginName");
                    OnPropertyChanged("Combined");
                }
                else { loginName = "Login"; }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }

            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
                    OnPropertyChanged("Combined");
                }
                else { password = "Pass"; }
            }
        }

        public string Combined { get { return loginName + " " + password; } }


        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

}
