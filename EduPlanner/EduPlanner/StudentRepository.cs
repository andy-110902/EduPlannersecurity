﻿using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Firebase.Database.Query;
using Firebase.Storage;
using System.Linq;

namespace EduPlanner

{
    public class StudentRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://eduplanner-ffe8c-default-rtdb.firebaseio.com/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("eduplanner-ffe8c.appspot.com");
        public async Task<bool> Save(StudentModel student)
        {
            var data = await firebaseClient.Child(nameof(StudentModel)).PostAsync(JsonConvert.SerializeObject(student));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<List<StudentModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(StudentModel)).OnceAsync<StudentModel>()).Select(item => new StudentModel
            {
                Email = item.Object.Email,
                Name = item.Object.Name,
                Image = item.Object.Image,
                Id = item.Key
            }).ToList();
        }

        public async Task<List<StudentModel>> GetAllByName(String name)
        {
            return (await firebaseClient.Child(nameof(StudentModel)).OnceAsync<StudentModel>()).Select(item => new StudentModel
            {
                Email = item.Object.Email,
                Name = item.Object.Name,
                Image = item.Object.Image,
                Id = item.Key
            }).Where(c => c.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public async Task<StudentModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(StudentModel) + "/" + id).OnceSingleAsync<StudentModel>());
        }

        public async Task<bool> Update(StudentModel student)
        {
            await firebaseClient.Child(nameof(StudentModel) + "/" + student.Id).PutAsync(JsonConvert.SerializeObject(student));
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(StudentModel) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<string> Upload(Stream img, string fileName)
        {
            var image = await firebaseStorage.Child("Images").Child(fileName).PutAsync(img);
            return image;
        }
    }
}
