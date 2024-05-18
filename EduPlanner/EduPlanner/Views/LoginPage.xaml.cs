using EduPlanner.Views.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EduPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        UserRepository _userRepository = new UserRepository();

        //public ICommand TapCommand => new Command(async() => await)
        public LoginPage()
        {
            InitializeComponent();


            //bool haskey = Preferences.ContainsKey("token");
            //if (haskey)
            //{
            //    string token = Preferences.Get("token", "");
            //    if (!string.IsNullOrEmpty(token))
            //    {
            //        Navigation.PushAsync(new RegisterUser());
            //    }
            //}
        }

        private async void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
                if (string.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Advertencia", "Ingresa tu correo.", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Advertencia", "Ingresa tu contraseña.", "OK");
                    return;
                }

                string token = await _userRepository.SignIn(email, password);

                if (!string.IsNullOrEmpty(token))
                {
                    Preferences.Set("token", token);
                    Preferences.Set("userEmail", email);
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    await DisplayAlert("Inicio de sesion", "Inicio fallido", "OK");
                }
            }
            catch (Exception exception)
            {
                if /*(exception.Message.Contains("EMAIL_NOT_FOUND"))*/
                    (exception.Message.Contains("INVALID_LOGIN_CREDENTIALS"))
                {
                    await DisplayAlert("Error", "La cuenta o la contraseña no es válida. Vuelve a intentarlo.", "OK");
                    //await DisplayAlert("No Autorizado", "Email invalido", "OK");
                }
                else if (exception.Message.Contains("INVALID_PASSWORD"))
                {
                    await DisplayAlert("No Autorizado", "Password invalido", "OK");
                }
                else if (exception.Message.Contains("INVALID_EMAIL"))
                {
                    await DisplayAlert("No Autorizado", "cORREO invalido", "OK");
                }
                else
                {
                    await DisplayAlert("Error", exception.Message, "OK");
                }
            }
            TxtEmail.Text = "";
            TxtPassword.Text = "";

        }

        private async void RegisterTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterUser());

        }

        private async void ForgotTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ForgotPasswordPage());
        }
    }
}