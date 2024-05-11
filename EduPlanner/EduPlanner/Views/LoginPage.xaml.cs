using EduPlanner.Views.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EduPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            string email = TxtEmail.Text;
            string password = TxtPassword.Text;
            string token = await _userRepository.SignIn(email, password);
            if (!string.IsNullOrEmpty(token))
            {
                await Navigation.PushAsync(new StudentListPage());
            }
            else
            {
                await DisplayAlert("Sign In", "Sign in failed", "OK");
            }
        }
    }
}