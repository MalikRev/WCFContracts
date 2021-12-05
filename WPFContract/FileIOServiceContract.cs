using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;

namespace WPFContract
{
    class FileIOServiceContract
    {
        private readonly string PATH;

        public FileIOServiceContract(string path) { PATH = path; }

        public BindingList<ClassJsonContract> LoadData()
        {
            //проверка на наличие файла с помощью метода Exists, если нет то создает пустой
            var fileExist = File.Exists(PATH);
            if (!fileExist)
            {
                //создание файла + освобождение ресурсов
                File.CreateText(PATH).Dispose();
                //возвращает пустой список
                return new BindingList<ClassJsonContract>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                //десериализация в данных в BindigList
                return JsonConvert.DeserializeObject<BindingList<ClassJsonContract>>(fileText);
            }
        }

        public void SaveData(object _classJsons)
        {
            //открывает файл по пути PATH, using для освобождения ресурсов по записи в файл
            using (StreamWriter writer = File.CreateText(PATH))
            {
                //десериализация object в string
                string output = JsonConvert.SerializeObject(_classJsons);
                //запись строчки в файл
                writer.Write(output);
            }
        }
    }
}
