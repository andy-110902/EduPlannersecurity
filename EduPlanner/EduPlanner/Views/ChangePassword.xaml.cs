using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions; // Añade esta línea para usar Regex
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EduPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePassword : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        public ChangePassword()
        {
            InitializeComponent();
        }

        private async void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
            try
            {
                string password = TxtPassword.Text;
                string confirmPass = TxtConfirm.Text;

                if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Cambiar la contraseña", "Porfavor ingresa contraseña", "OK");
                    return;
                }
                if (password.Length < 6)
                {
                    await DisplayAlert("Aviso", "La contraseña debe ser mayor a 6 caracteres.", "OK");
                    return;
                }
                // Ajuste en la validación de la contraseña
                if (!Regex.IsMatch(password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).+$"))
                {
                    await DisplayAlert("Aviso", "La contraseña debe contener al menos una letra minúscula, una letra mayúscula y un número.", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(confirmPass))
                {
                    await DisplayAlert("Cambiar la contraseña", "Porfavor ingresa confirma contraseña", "OK");
                    return;
                }
                if (password != confirmPass)
                {
                    await DisplayAlert("Cambiar la contraseña", "La contraseña no coincide", "OK");
                    return;
                }
                string token = Preferences.Get("token", "");
                string newToken = await _userRepository.ChangePassword(token, password);
                if (!string.IsNullOrEmpty(newToken))
                {
                    await DisplayAlert("Cambiar la contraseña", "la contraseña ha sido cambiada", "OK");
                    Preferences.Set("token", newToken);
                    // Preferences.Clear();
                    // await Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    await DisplayAlert("Cambiar la contraseña", "Cambio de contraseña falló", "OK");
                }
            }
            catch (Exception exception)
            {
                // Manejo de excepción (opcionalmente puedes mostrar un mensaje al usuario)
            }
        }
    }
}
