namespace GrampsView.Data.Model.Tests
{
    using Commons.Xml.Relaxng;

    using global::NUnit.Framework;

    using System.Xml;

    [TestFixture()]
    public class RelaxNGBasicDataTests
    {
        [TearDown]
        public void Cleanup()
        {
        }

        [SetUp]
        public void Init()
        {
        }

        [Test()]
        public void RelaxNGBasicDataTests_Basic()
        {
            XmlReader instance = new XmlTextReader("instance.xml");

            XmlReader grammar = new XmlTextReader("my.rng");

            RelaxngValidatingReader reader =
                new RelaxngValidatingReader(instance, grammar);
        }
    }
}