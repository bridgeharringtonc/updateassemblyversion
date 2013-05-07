namespace Tests.UpdateConfigurations
{
    using System.IO;
    using NUnit.Framework;

    public class BaseTest
    {
        protected const string SourceFile = "./TestAssemblyInfo.txt"; 
        protected  const string ExpectedFile = "./ExpectedAssemblyInfo.txt";

        [SetUp]
        public void Setup()
        {
            if (File.Exists(SourceFile))
                File.Delete(SourceFile);

            File.Copy(Path.Combine("../../", SourceFile), SourceFile);
        }

        public static string GetFileContents(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }

        public static string GetPropertyValue(string filePath, string propertyName)
        {
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(propertyName))
                    {
                        var startIndex = line.IndexOf('"') + 1;
                        var endIndex = line.LastIndexOf('"');

                        return line.Substring(startIndex, endIndex - startIndex);
                    }
                }
            }

            return string.Empty;
        }
    }
}