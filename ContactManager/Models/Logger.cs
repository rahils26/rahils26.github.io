using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ContactManager.Models
{
    public static class FileLogger
    {
        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs\\log.txt");
        public static void Log(string message)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }

                File.AppendAllText(filePath, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}