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
            try
            {
                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
                if (string.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Warning", "Enter your email", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Warning", "Enter your password", "OK");
                    return;
                }

                string token = await _userRepository.SignIn(email, password);
                string userEmail = email; // Obtener el correo electrónico desde la autenticación

                if (!string.IsNullOrEmpty(token))
                {

                    // Guardar los valores del nombre de usuario y correo electrónico en las propiedades de la aplicación
                    Application.Current.Properties["UserEmail"] = userEmail;
                    await Application.Current.SavePropertiesAsync();

                    await Navigation.PushAsync(new StudentListPage());
                }
                else
                {
                    await DisplayAlert("Sign In", "Sign in failed", "OK");
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
                //else if(exception.Message.Contains("INVALID_PASSWORD"))
                //{
                //    await DisplayAlert("No Autorizado", "Password invalido", "OK");
                //}
                else
                {
                    await DisplayAlert("Error", exception.Message, "OK");
                }
            }
            
        }

        private async void RegisterTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterUser());

        }
    }
}
