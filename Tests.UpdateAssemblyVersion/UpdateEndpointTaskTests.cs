namespace Tests.UpdateConfigurations
{
    using System.Xml;
    using NUnit.Framework;
    using global::UpdateConfigurations;

    [TestFixture]
    public class UpdateEndpointTaskTests
    {
        private const string InputFile = "./TestAppConfig.xml";
        private const string OutputFile = "./ExpectedAppConfig.xml";

        [Test]
        public void when_replace_endpoint()
        {
            var sut = new UpdateEndpointTask {WebConfig = InputFile, Url = "http://newurl"};

            sut.Execute();

            var input = new XmlDocument();
            var output = new XmlDocument();

            input.Load(InputFile);
            output.Load(OutputFile);

            var inputNodes = input.SelectNodes("/configuration/system.serviceModel/client/endpoint");
            var outputNodes = input.SelectNodes("/configuration/system.serviceModel/client/endpoint");

            Assert.That(inputNodes[0].Attributes["address"].Value == outputNodes[0].Attributes["address"].Value);
        }
    }
}
