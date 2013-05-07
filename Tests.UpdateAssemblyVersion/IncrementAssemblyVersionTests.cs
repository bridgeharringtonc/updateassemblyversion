namespace Tests.UpdateConfigurations
{
    using NUnit.Framework;
    using global::UpdateConfigurations;

    [TestFixture]
    public class IncrementAssemblyVersionTests : BaseTest
    {
        [Test]
        public void when_process_Assembly_with_default_increment_value_and_position()
        {
            var sut = new IncrementAssemblyVersionTask { FileList = new[] { SourceFile } };

            sut.Execute();

            var version = BaseTest.GetPropertyValue(SourceFile, "AssemblyVersion");
            var fileVersion = BaseTest.GetPropertyValue(SourceFile, "AssemblyFileVersion");

            Assert.AreEqual("1.0.0.1", version, "Version");
            Assert.AreEqual("1.0.0.1", fileVersion, "File version");
        }

        [Test]
        public void when_process_Assembly_with_default_increment_value_and_specific_position()
        {
            var sut = new IncrementAssemblyVersionTask { FileList = new[] { SourceFile }, Position = 2 };

            sut.Execute();

            var version = BaseTest.GetPropertyValue(SourceFile, "AssemblyVersion");
            var fileVersion = BaseTest.GetPropertyValue(SourceFile, "AssemblyFileVersion");

            Assert.AreEqual("1.0.1.0", version, "Version");
            Assert.AreEqual("1.0.1.0", fileVersion, "File version");
        }

        [Test]
        public void when_process_Assembly_with_specific_increment_value_and_default_position()
        {
            var sut = new IncrementAssemblyVersionTask { FileList = new[] { SourceFile }, IncrementValue = 3 };

            sut.Execute();

            var version = BaseTest.GetPropertyValue(SourceFile, "AssemblyVersion");
            var fileVersion = BaseTest.GetPropertyValue(SourceFile, "AssemblyFileVersion");

            Assert.AreEqual("1.0.0.3", version, "Version");
            Assert.AreEqual("1.0.0.3", fileVersion, "File version");
        }

        [Test]
        public void when_process_Assembly_with_default_increment_value_and_invalid_position()
        {
            var sut = new IncrementAssemblyVersionTask { FileList = new[] { SourceFile }, Position = 120 };

            sut.Execute();

            var version = BaseTest.GetPropertyValue(SourceFile, "AssemblyVersion");
            var fileVersion = BaseTest.GetPropertyValue(SourceFile, "AssemblyFileVersion");

            Assert.AreEqual("1.0.0.1", version, "Version");
            Assert.AreEqual("1.0.0.1", fileVersion, "File version");
        }
    }
}