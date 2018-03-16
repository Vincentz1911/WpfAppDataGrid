using System.ComponentModel;

namespace WpfAppDataGrid.Models
{
    class UserModel
    {
    }

    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private string loginName;
        public string LoginName
        {
            get { return loginName; }

            set
            {
                if (loginName != value)
                {
                    loginName = value;
                    NotifyPropertyChanged("LoginName");
                    NotifyPropertyChanged("Combined");
                }
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
                    NotifyPropertyChanged("Password");
                    NotifyPropertyChanged("Combined");
                }
            }
        }

        public string Combined { get { return loginName + " " + password; } }
    }
}
