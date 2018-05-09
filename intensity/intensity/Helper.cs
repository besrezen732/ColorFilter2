using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intensity
{
    public class Helper
    {
        public void FileWriter(string colorString)
        {

            using (FileStream fstream = new FileStream(@"D:\note.txt", FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(colorString);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }
        }

    }
}
