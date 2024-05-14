using EduPlanner.Views.Student;
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
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
			LblUser.Text = Preferences.Get("userEmail","default");
		}

        private void BtnStudentList_Clicked(object sender, EventArgs e)
        {
			Navigation.PushModalAsync(new NavigationPage(new StudentListPage()));
        }

        private void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new ChangePassword()));
        }
    }
}