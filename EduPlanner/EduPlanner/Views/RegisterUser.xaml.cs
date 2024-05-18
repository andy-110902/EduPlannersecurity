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
                if (name.Length > 50)
                {
                    await DisplayAlert("Aviso", "El nombre debe ser menor a 50 caracteres.", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    await DisplayAlert("Alerta", "El email no es valido.", "OK");
                    return;
                }
                if (email.Length > 50)
                {
                    await DisplayAlert("Aviso", "El email debe ser menor a 50 caracteres.", "OK");
                    return;
                }
                if (password.Length < 6)
                {
                    await DisplayAlert("Aviso", "La contraseña debe ser de 6 digitos.", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(password) || !Regex.IsMatch(password, "^[0-9]+$"))
                {
                    await DisplayAlert("Alerta", "Ingrese una contraseña numerica mayor a 5 digitos.", "OK");
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
                    await DisplayAlert("Registro", "Usuario registrado", "OK");
                    await Navigation.PopModalAsync();
                    TxtName.Text = "";
                    TxtEmail.Text = "";
                    TxtPassword.Text = "";
                    TxtConfirmPass.Text = "";
                }
                else
                {
                    await DisplayAlert("Error", "El usuario no se pudo registrar.", "OK");
                }
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("Warning", "Email exist", "OK");
                }
                else if (exception.Message.Contains("weak_password"))
                {
                    await DisplayAlert("Aviso", "El usuario se ha registrado exitosamente", "Ok");
                }
                else
                        {
                    await DisplayAlert("Aviso", "El usuario se ha registrado exitosamente", "Ok");
                }

            }

        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

//namespace EduPlanner.Views
//{
//    [XamlCompilation(XamlCompilationOptions.Compile)]
//    public partial class RegisterUser : ContentPage
//    {
//        UserRepository _userRepository = new UserRepository();
//        public RegisterUser()
//        {
//            InitializeComponent();
//        }

//        private async void ButtonRegister_Clicked(object sender, EventArgs e)
//        {
//            try
//            {
//                string name = TxtName.Text;
//                string email = TxtEmail.Text;
//                string password = TxtPassword.Text;
//                string confirmPassword = TxtConfirmPass.Text;

//                // Validaciones del nombre
//                if (string.IsNullOrEmpty(name) || !Regex.IsMatch(name, "^[a-zA-Z]+$"))
//                {
//                    await DisplayAlert("Alerta", "El campo nombre está vacío o contiene algún número o caracter especial", "OK");
//                    return;
//                }

//                // Validaciones del email
//                if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
//                {
//                    await DisplayAlert("Alerta", "El email no es válido.", "OK");
//                    return;
//                }

//                // Validaciones de la contraseña
//                //if (string.IsNullOrEmpty(password))
//                //{
//                //    await DisplayAlert("Aviso", "Ingrese una contraseña.", "OK");
//                //    return;
//                //}

//                //// Contraseña con al menos 6 caracteres, una letra mayúscula, una minúscula y un número.
//                //if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$"))
//                //{
//                //    await DisplayAlert("Aviso", "La contraseña debe tener al menos 6 caracteres, incluyendo una letra mayúscula, una letra minúscula y un número.", "OK");
//                //    return;
//                //}


//                //// Validación de la confirmación de la contraseña
//                //if (string.IsNullOrEmpty(confirmPassword))
//                //{
//                //    await DisplayAlert("Aviso", "Confirme la contraseña.", "OK");
//                //    return;
//                //}

//                //if (password != confirmPassword)
//                //{
//                //    await DisplayAlert("Alerta", "La contraseña no coincide.", "OK");
//                //    return;
//                //}
//                if (string.IsNullOrEmpty(password))
//                {
//                    await DisplayAlert("Cambiar la contraseña", "Porfavor ingresa contraseña", "OK");
//                    return;
//                }
//                //if (password.Length < 6)
//                //{
//                //    await DisplayAlert("Aviso", "La contraseña debe ser mayor a 6 caracteres.", "OK");
//                //    return;
//                //}
//                //// Ajuste en la validación de la contraseña
//                //if (!Regex.IsMatch(password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).+$"))
//                //{
//                //    await DisplayAlert("Aviso", "La contraseña debe contener al menos una letra minúscula, una letra mayúscula y un número.", "OK");
//                //    return;
//                //}
//                if (string.IsNullOrEmpty(confirmPassword))
//                {
//                    await DisplayAlert("Cambiar la contraseña", "Porfavor ingresa confirma contraseña", "OK");
//                    return;
//                }
//                if (password != confirmPassword)
//                {
//                    await DisplayAlert("Cambiar la contraseña", "La contraseña no coincide", "OK");
//                    return;
//                }
//                // Registro del usuario
//                bool isSave = await _userRepository.Register(email, name, password);
//                if (isSave)
//                {
//                    await DisplayAlert("Registro", "Usuario registrado", "OK");
//                    await Navigation.PopModalAsync();
//                    TxtName.Text = "";
//                    TxtEmail.Text = "";
//                    TxtPassword.Text = "";
//                    TxtConfirmPass.Text = "";
//                }
//                else
//                {
//                    await DisplayAlert("Error", "El usuario no se pudo registrar.", "OK");
//                }
//            }
//            catch (Exception exception)
//            {
//                if (exception.Message.Contains("EMAIL_EXISTS"))
//                {
//                    await DisplayAlert("Advertencia", "El email ya está registrado", "OK");
//                }
//                else if (exception.Message.Contains("weak_password"))
//                {
//                    await DisplayAlert("error", "la cuenta o la contraseña no es válida. se espera una contraseña numérica de al menos 6 caracteres.", "ok");
//                }
//                else
//                {
//                    await DisplayAlert("Error", exception.Message, "OK");
//                }
//            }
//        }
//    }
//}
