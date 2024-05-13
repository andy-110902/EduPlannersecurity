using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EduPlanner.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiPerfil : ContentPage
    {
        public MiPerfil()
        {
            InitializeComponent();

            // Obtener los valores del nombre de usuario y correo electrónico desde las propiedades de la aplicación
            string userEmail = Application.Current.Properties.ContainsKey("UserEmail") ? Application.Current.Properties["UserEmail"].ToString() : "";

            // Instanciar el ViewModel con los valores del usuario
            BindingContext = new UserProfileViewModel(userEmail);
        }
    }

    public class UserProfileViewModel
    {
        public string UserEmail { get; set; }

        public UserProfileViewModel(string userEmail)
        {
            UserEmail = userEmail;
        }
    }
}