namespace UpdateConfigurations
{
    using System.IO;
    using System.Xml;
    using Microsoft.Build.Framework;

    public class UpdateEndpointTask : ITask
    {
        public bool Execute()
        {
            if (!File.Exists(WebConfig))
                return false;

            var fileContents = new XmlDocument();
            fileContents.Load(WebConfig);

            var nodes = fileContents.SelectNodes("/configuration/system.serviceModel/client/endpoint");

            if (nodes == null || nodes.Count == 0)
                return false;

            foreach(var node in nodes)
            {
                var address = (node as XmlNode).Attributes["address"].Value;

                var splitAddress = address.Split('/');

                var newAddress = Url + "/" + address[address.Length - 1];

                (node as XmlNode).Attributes["address"].Value = newAddress;
            }

            using (var writer = XmlWriter.Create(WebConfig))
            {
                fileContents.WriteContentTo(writer);
            }

            return true;
        }

        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        [Required]
        public string WebConfig { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
