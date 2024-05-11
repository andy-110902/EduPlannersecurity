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
    public partial class StudentEntry : ContentPage
    {
        StudentRepository repository = new StudentRepository();
        public StudentEntry()
        {
            InitializeComponent();
        }

        public async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            string email = TxtEmail.Text;
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Advertencia", "Por favor, ingresa tu nombre.", "Cancel");
            }
            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Advertencia", "Por favor, ingresa tu correo.", "Cancel");
            }

            StudentModel student = new StudentModel();
            student.Name = name;
            student.Email = email;

            var isSaved = await repository.Save(student);
            if (isSaved)
            {
                await DisplayAlert("Information", "El estudiante se ha registrado con éxito.", "Ok");
                Clear();
            }
            else
            {
                await DisplayAlert("Error", "El estudiante no se ha podido registrar.", "Ok");
            }
        }

        public void Clear()
        {
            TxtName.Text = string.Empty;
            TxtEmail.Text = string.Empty;
        }
    }
}