namespace UpdateConfigurations
{
    using System.IO;
    using System.Text;
    using Microsoft.Build.Framework;

    public class AssemblyVersionTask : ITask
    {
        public const string VersionLine = "[assembly: AssemblyVersion(\"{0}\")]";
        public const string FileVersionLine = "[assembly: AssemblyFileVersion(\"{0}\")]";
    
        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        [Required]
        public string[] FileList { get; set; }

        [Required]
        public string AssemblyVersion { get; set; }

        [Required]
        public string AssemblyFileVersion { get; set; }

        public bool Execute()
        {
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
                            if (!string.IsNullOrEmpty(AssemblyVersion))
                                newFile.AppendLine(string.Format(VersionLine, AssemblyVersion));
                        }
                        else
                        if (line.StartsWith("[assembly: AssemblyFileVersion"))
                        {
                            if (!string.IsNullOrEmpty(AssemblyFileVersion))
                                newFile.AppendLine(string.Format(FileVersionLine, AssemblyFileVersion));
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
    }
}
