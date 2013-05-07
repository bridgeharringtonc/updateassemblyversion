namespace UpdateConfigurations
{
    using System;
    using System.IO;
    using System.Text;
    using Microsoft.Build.Framework;

    public class IncrementAssemblyVersionTask : ITask
    {
        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        [Required]
        public string[] FileList { get; set; }

        public int IncrementValue { get; set; }

        public int Position { get; set; }

        public bool Execute()
        {
            if (IncrementValue == default(int))
                IncrementValue = 1;

            foreach (var file in FileList)
            {
                if (!File.Exists(file)) continue;

                var newFile = new StringBuilder();
                using (var reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("[assembly: AssemblyVersion"))
                        {
                            newFile.AppendLine(GetReplacementLine(AssemblyVersionTask.VersionLine, GetPropertyValue(line)));
                        }
                        else if (line.StartsWith("[assembly: AssemblyFileVersion"))
                        {
                            newFile.AppendLine(GetReplacementLine(AssemblyVersionTask.FileVersionLine, GetPropertyValue(line)));
                        }
                        else
                        {
                            newFile.AppendLine(line);
                        }
                    }
                }

                using (var writer = new StreamWriter(file))
                {
                    writer.Write(newFile.ToString());
                }
            }
            
            return true;
        }

        private static string GetPropertyValue(string line)
        {
            var startIndex = line.IndexOf('"') + 1;
            var endIndex = line.LastIndexOf('"');

            return line.Substring(startIndex, endIndex - startIndex);
        }

        private string GetReplacementLine(string defaultText, string propertyValue)
        {
            if (string.IsNullOrEmpty(propertyValue))
                return string.Empty;

            var splitProperty = propertyValue.Split('.');

            if (Position == default(int))
            {
                Position = splitProperty.Length - 1;
            }

            if (Position < 0 || Position > splitProperty.Length - 1)
            {
                Position = splitProperty.Length - 1;
            }

            var newValue = string.Empty;
            for (var i = 0; i < splitProperty.Length; i++)
            {
                if (i == Position)
                    newValue += (Convert.ToInt32(splitProperty[i]) + IncrementValue).ToString();
                else
                    newValue += splitProperty[i];

                if (i < splitProperty.Length - 1)
                    newValue += ".";
            }

            return string.Format(defaultText, newValue);
        }
    }
}