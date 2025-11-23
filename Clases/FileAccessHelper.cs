using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACollahuazoS5.Clases
{
    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            //se define le ruta y el nombre de la base de datos(Archivo)
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);
        }
    }
}
