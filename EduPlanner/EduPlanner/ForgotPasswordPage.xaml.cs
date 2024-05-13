using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EduPlanner
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForgotPasswordPage : ContentPage
	{
		UserRepository _userRepository = new UserRepository();
		public ForgotPasswordPage ()
		{
			InitializeComponent ();
		}

        private async void ButtonSendLink_Clicked(object sender, EventArgs e)
        {
			string email = TxtEmail.Text;
			if (string.IsNullOrEmpty(email) ) 
			{
				await DisplayAlert("Alerta", "Ingresa tu email.", "OK");
				return;
			}
			bool IsSend = await _userRepository.ResetPassword(email);
			if (IsSend) 
			{
                await DisplayAlert("Reinicio contraseña", "Se envio el enlace a tu correo.", "OK");
				await Navigation.PopModalAsync();
            }
			else
			{
                await DisplayAlert("Reinicio contraseña", "Error en el envio del enlace.", "OK");
            }
        }
    }
}