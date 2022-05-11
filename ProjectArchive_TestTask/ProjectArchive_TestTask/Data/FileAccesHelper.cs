using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
namespace ProjectArchive_TestTask.Data
{
    public class FileAccesHelper
    {
        public static string GetLocalFilePath(string fileName)
        {
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, fileName);
        }
    }
}
