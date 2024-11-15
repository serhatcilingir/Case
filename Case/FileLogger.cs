using System.IO;

namespace Case.Utilities
{
    public class FileLogger
    {
        private readonly string _filePath;

        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void WriteLog(string content)
        {
            // Dosyayı aç ve yaz
            using (var writer = new StreamWriter(_filePath, true)) // Append mode
            {
                writer.WriteLine(content); // Her bir içerik yeni satıra eklenir
            }
        }
    }
}
