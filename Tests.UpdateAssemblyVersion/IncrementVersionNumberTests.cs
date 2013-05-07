namespace Tests.UpdateConfigurations
{
    using NUnit.Framework;
    using global::UpdateConfigurations;

    [TestFixture]
    public class IncrementVersionNumberTests : BaseTest
    {
        [Test]
        public void when_increment_version_with_default_increment_value_and_position()
        {
            var sut = new IncrementVersionNumber {CurrentVersion = "1.0.0.0"};

            sut.Execute();

            Assert.AreEqual("1.0.0.1", sut.NewVersion, "Version");
        }

        [Test]
        public void when_increment_version_with_default_increment_value_and_specific_position()
        {
            var sut = new IncrementVersionNumber {CurrentVersion = "1.0.0.0", Position = 2};

            sut.Execute();

            Assert.AreEqual("1.0.1.0", sut.NewVersion, "Version");
        }

        [Test]
        public void when_increment_version_with_specific_increment_value_and_default_position()
        {
            var sut = new IncrementVersionNumber {CurrentVersion = "1.0.0.0", IncrementValue = 3};

            sut.Execute();

            Assert.AreEqual("1.0.0.3", sut.NewVersion, "Version");
        }

        [Test]
        public void when_increment_version_with_default_increment_value_and_invalid_position()
        {
            var sut = new IncrementVersionNumber {CurrentVersion = "1.0.0.0", Position = 120};

            sut.Execute();

            Assert.AreEqual("1.0.0.1", sut.NewVersion, "Version");
        }
    }
}