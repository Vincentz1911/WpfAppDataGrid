using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppDataGrid.Models
{
    class UserProfile
    {
    }

    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private string loginName;
        public string LoginName
        {
            get
            {
                return loginName;
            }

            set
            {
                if (loginName != value)
                {
                    loginName = value;
                    RaisePropertyChanged("LoginName");
                    RaisePropertyChanged("Combined");
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
                    RaisePropertyChanged("Password");
                    RaisePropertyChanged("Combined");
                }
            }
        }

        public string Combined
        {
            get
            {
                return loginName + " " + password;
            }
        }
    }
}
