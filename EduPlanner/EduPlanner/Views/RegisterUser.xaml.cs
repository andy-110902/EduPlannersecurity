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
    public partial class RegisterUser : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        public RegisterUser()
        {
            InitializeComponent();
        }

        private async void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            try
            {
                string name = TxtName.Text;
                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
                string confirmPassword = TxtConfirmPass.Text;
                if (string.IsNullOrEmpty(name))
                {
                    await DisplayAlert("Warning", "Type name", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Warning", "Type email", "OK");
                    return;
                }
                if (password.Length < 6)
                {
                    await DisplayAlert("Warning", "Password should be 6 digit.", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Warning", "Type password", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(confirmPassword))
                {
                    await DisplayAlert("Warning", "Type Confirm password", "OK");
                    return;
                }
                if (password != confirmPassword)
                {
                    await DisplayAlert("Warning", "Password not match.", "OK");
                    return;
                }
                bool IsSave = await _userRepository.Register(email, name, password);
                if (IsSave)
                {
                    await DisplayAlert("Register user", "Registration completed", "OK");
                }
                else
                {
                    await DisplayAlert("Register user", "Registration filed", "OK");
                }
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("Warning", "Email exist", "OK");
                }
                else
                {
                    await DisplayAlert("Error", exception.Message, "OK");
                }

            }

        }
    }
}