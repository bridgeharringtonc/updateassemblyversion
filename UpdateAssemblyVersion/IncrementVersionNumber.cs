namespace UpdateConfigurations
{
    using System;
    using Microsoft.Build.Framework;

    public class IncrementVersionNumber : ITask
    {
        [Required]
        public string CurrentVersion { get; set; }

        [Output]
        public string NewVersion { get; set; }

        public int IncrementValue { get; set; }

        public int Position { get; set; }

        public bool Execute()
        {
            NewVersion = IncrementCurrentVersion();
            
            return true;
        }

        public IBuildEngine BuildEngine { get; set; }
        public ITaskHost HostObject { get; set; }

        private string IncrementCurrentVersion()
        {
            if (IncrementValue == default(int))
                IncrementValue = 1;

            var splitProperty = CurrentVersion.Split('.');

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

            return newValue;
        }
    }
}
