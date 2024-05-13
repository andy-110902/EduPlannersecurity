using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                if (string.IsNullOrEmpty(name) || !Regex.IsMatch(name, "^[a-zA-Z]+$"))
                {
                    await DisplayAlert("Alerta", "El campo nombre esta vacio o contiene algun numero o caracter especial", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    await DisplayAlert("Alerta", "El email no es valido.", "OK");
                    return;
                }
                if (password.Length < 6)
                {
                    await DisplayAlert("Aviso", "La contraseña debe ser de 6 digitos.", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(password) || !Regex.IsMatch(password, "^[0-9]+$"))
                {
                    await DisplayAlert("Alerta", "El campo contraseña esta vacio, este solo acepta numeros.", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(confirmPassword))
                {
                    await DisplayAlert("Aviso", "Confirme la contraseña.", "OK");
                    return;
                }
                if (password != confirmPassword)
                {
                    await DisplayAlert("Alerta", "La contraseña no coincide.", "OK");
                    return;
                }
                bool IsSave = await _userRepository.Register(email, name, password);
                if (IsSave)
                {
                    await DisplayAlert("Register user", "Registration completed", "OK");
                    await Navigation.PopModalAsync();
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