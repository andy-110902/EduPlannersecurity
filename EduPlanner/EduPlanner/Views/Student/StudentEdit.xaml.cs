using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EduPlanner.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentEdit : ContentPage
    {
        MediaFile file;
        StudentRepository studentRepository = new StudentRepository();
        public StudentEdit(StudentModel student)
        {
            InitializeComponent();
            TxtEmail.Text = student.Email;
            TxtName.Text = student.Name;
            TxtId.Text = student.Id;
            //Esto es mio 
        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            string email = TxtEmail.Text;
            if (string.IsNullOrEmpty(name) || !Regex.IsMatch(name, "^[a-zA-Z]+$"))
            {
                await DisplayAlert("Alerta", "El campo nombre esta vacio o contiene algun numero o caracter especial", "OK");
                return;
            }
            if (name.Length > 50)
            {
                await DisplayAlert("Aviso", "El nombre debe ser menos de 50 caracteres.", "OK");
                return;
            }
            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                await DisplayAlert("Alerta", "El email no es valido.", "OK");
                return;
            }
            if (email.Length > 50)
            {
                await DisplayAlert("Aviso", "El email debe ser menos de 50 caracteres.", "OK");
                return;
            }

            StudentModel student = new StudentModel();
            student.Id = TxtId.Text;
            student.Name = name;
            student.Email = email;
            if (file != null)
            {
                string image = await studentRepository.Upload(file.GetStream(), Path.GetFileName(file.Path));
                student.Image = image;
            }
            if (file != null)
            {
                string image = await studentRepository.Upload(file.GetStream(), Path.GetFileName(file.Path));
                student.Image = image;
            }
            bool isUpdated = await studentRepository.Update(student);
            if (isUpdated)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo actualizar la información", "Ok");
            }
        }

        //private async void ImageTap_Tapped(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();
        //    try
        //    {
        //        file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
        //        {
        //            PhotoSize = PhotoSize.Medium
        //        });
        //        if (file == null)
        //        {
        //            return;
        //        }
        //        StudentImage.Source = ImageSource.FromStream(() =>
        //        {
        //            return file.GetStream();
        //        });
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

    }
}