using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EduPlanner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChangePassword : ContentPage
	{
		UserRepository _userRepository =new UserRepository();
		public ChangePassword ()
		{
			InitializeComponent ();
		}

        private async void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
			try
			{
				string password = TxtPassword.Text;
				string confirmPass= TxtConfirm.Text;
				if(string.IsNullOrEmpty(password))
				{
					 await DisplayAlert("Change Password", "Porfavor ingresa contraseña", "OK");
					return;
				}
                if (string.IsNullOrEmpty(confirmPass))
                {
                    await DisplayAlert("Change Password", "Porfavor ingresa confirma contraseña", "OK");
					return;
                }
                if (password!= confirmPass)
				{
                     DisplayAlert("Change Password", "La contraseña no coincide", "OK");
					return;
                }
				string token = Preferences.Get("token", "") ;
				bool IsChanged = await _userRepository.ChangePassword(token, password);
				if (IsChanged) 
				{
                    DisplayAlert("Change Password", "Password has been changed", "OK");
                }
				else
				{
                    DisplayAlert("Change Password", "Password Change failed", "OK");
                }
			}
			catch (Exception exception)
			{
			}
        }
    }
}