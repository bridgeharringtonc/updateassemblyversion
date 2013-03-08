namespace Tests.UpdateConfigurations
{
    using System;
    using System.IO;
    using NUnit.Framework;
    using global::UpdateConfigurations;

    [TestFixture]
    public class UpdateAssemblyVersionTests
    {
        private const string SourceFile = "./TestAssemblyInfo.txt";
        private const string ExpectedFile = "./ExpectedAssemblyInfo.txt";

        [Test]
        public void when_process_Assembly_with_version()
        {
            var sut = new AssemblyVersionTask {AssemblyVersion = "2.1.3", AssemblyFileVersion = "5.6.7", FileList = new[] {SourceFile}};


            sut.Execute();

            var source = GetFileContents(SourceFile);
            var result = GetFileContents(ExpectedFile);

            Console.WriteLine(source);
            Console.WriteLine(result);

            Assert.That(source == result);

        }

        private string GetFileContents(string filePath)
        {

            using (var reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();

            }
        }
    }
}