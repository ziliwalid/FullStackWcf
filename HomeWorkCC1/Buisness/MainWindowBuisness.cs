using Prism.Commands;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using s = ServiceReference1;

namespace UI.Buisness
{
    class MainWindowBuisness : DependencyObject
    {
        s.Service1Client srv = new s.Service1Client();
        public UserLogin User { get; set; }
        public DelegateCommand LoginCommand { get; set; }

        public MainWindowBuisness()
        {
            this.User = new UserLogin();
            this.LoginCommand = new DelegateCommand(Login);
        }

        private async void Login()
        {
            await LoginAsync();
        }

        private async Task LoginAsync()
        {
            var user = await srv.LoginAsync(User.Email, User.Password);
            if(user == null) { MessageBox.Show("not connected"); }
            else
            {
                MessageBox.Show("connected");
            }

        }

    }
}
