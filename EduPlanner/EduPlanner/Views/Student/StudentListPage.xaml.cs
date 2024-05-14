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
    public partial class StudentListPage : ContentPage
    {
        StudentRepository studentRepository = new StudentRepository();
        public StudentListPage()
        {
            InitializeComponent();

            StudentListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }

        protected override async void OnAppearing()
        {
            var students = await studentRepository.GetAll();
            StudentListView.ItemsSource = null;
            StudentListView.ItemsSource = students;
            StudentListView.IsRefreshing = false;
        }

        private void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new StudentEntry());
        }

        private void StudentListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            var studnet = e.Item as StudentModel;
            Navigation.PushModalAsync(new StudentDetails(studnet));
            ((ListView)sender).SelectedItem = null;
        }

        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var student = await studentRepository.GetById(id);
            if (student == null)
            {
                await DisplayAlert("Alerta", "Datos no encontrados.", "Ok");
            }
            student.Id = id;
            await Navigation.PushModalAsync(new StudentEdit(student));
        }

        private async void DeleteTap_Tapped(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Eliminar", "Desea eliminar?", "Sí", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await studentRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Información", "El estudiante se ha eliminado.", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "El estudiante no se ha podido eliminar.", "Ok");
                }
            }
        }

        private async void TxtSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var students = await studentRepository.GetAllByName(searchValue);
                StudentListView.ItemsSource = null;
                StudentListView.ItemsSource = students;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var students = await studentRepository.GetAllByName(searchValue);
                StudentListView.ItemsSource = null;
                StudentListView.ItemsSource = students;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void EditMenuItem_Clicked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var student = await studentRepository.GetById(id);
            if (student == null)
            {
                await DisplayAlert("Alerta", "Datos no encontrados.", "Ok");
            }
            student.Id = id;
            await Navigation.PushModalAsync(new StudentEdit(student));
        }

        private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Eliminar", "Desea eliminar?", "Sí", "No");
            if (response)
            {
                string id = ((MenuItem)sender).CommandParameter.ToString();
                bool isDelete = await studentRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Información", "El estudiante se ha eliminado.", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "El estudiante no se ha podido eliminar.", "Ok");
                }
            }
        }

        private async void EditSwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var student = await studentRepository.GetById(id);
            if (student == null)
            {
                await DisplayAlert("Alerta", "Datos no encontrados.", "Ok");
            }
            student.Id = id;
            await Navigation.PushModalAsync(new StudentEdit(student));
        }

        
    }
}