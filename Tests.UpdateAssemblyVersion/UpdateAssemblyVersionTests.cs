namespace Tests.UpdateConfigurations
{
    using System;
    using System.IO;
    using NUnit.Framework;
    using global::UpdateConfigurations;

    [TestFixture]
    public class UpdateAssemblyVersionTests : BaseTest
    {
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
    }
}