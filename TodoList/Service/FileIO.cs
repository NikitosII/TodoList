using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Service
{
    internal class FileIO
    {
        private readonly string _path;
        public FileIO(string path)
        {
            _path = path;
        }

        // загрузка данных
        public BindingList<TodoModel> LoadData()
        {
            bool isExist = File.Exists(_path);
            if (!isExist)                        // файл не существует
            {
                File.CreateText(_path).Dispose();
                return new BindingList<TodoModel>();
            }
            using (var reader = File.OpenText(_path))  // файл существует
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<TodoModel>>(fileText);
            }
        }

        // сохранение данных в файл
        public void SaveData(object todoDataList)
        {
            using (StreamWriter writer = File.CreateText(_path))  // метод Dispose() автоматич. вызовется 
            {
                string output = JsonConvert.SerializeObject(todoDataList);
                writer.Write(output);
            }

        }


    }
}
