using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Collector.Tests
{
    public class FileResourcesTests
    {
        [Test]
        public void IsCollectionFile()
        {
            var result = File.Exists("D:\\Projekty\\Collector\\Collector\\bin\\Debug\\net7.0\\catalog.txt");
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsCatalogFile()
        {
            var result = File.Exists("D:\\Projekty\\Collector\\Collector\\bin\\Debug\\net7.0\\collection.txt");
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsCoinsFile()
        {
            var result = File.Exists("D:\\Projekty\\Collector\\Collector\\bin\\Debug\\net7.0\\coins.txt");
            Assert.That(result, Is.True);
        }

        [Test]
        public void WhetherItRetrievesCatalogData()
        {
            List<string[]> catalogTable = new();
            var fileName = "D:\\Projekty\\Collector\\Collector\\bin\\Debug\\net7.0\\catalog.txt";
            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText(fileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var dictionary = line.Split(';');
                        catalogTable.Add(dictionary);
                        line = reader.ReadLine();
                    }
                }
            }
            Assert.That(catalogTable, Is.Not.Empty);
        }

        [Test]
        public void WhetherItRetrievesCollectionData()
        {
            List<string[]> coinCollection = new();
            var fileName = "D:\\Projekty\\Collector\\Collector\\bin\\Debug\\net7.0\\collection.txt";
            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText(fileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var dictionary = line.Split(';');
                        coinCollection.Add(dictionary);
                        line = reader.ReadLine();
                    }
                }
            }
            Assert.That(coinCollection, Is.Not.Empty);
        }

        [Test]
        public void WhetherItRetrievesCoinsData()
        {
            List<string[]> coinTable = new();
            var fileName = "D:\\Projekty\\Collector\\Collector\\bin\\Debug\\net7.0\\coins.txt";
            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText(fileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var dictionary = line.Split(';');
                        coinTable.Add(dictionary);
                        line = reader.ReadLine();
                    }
                }
            }
            Assert.That(coinTable, Is.Not.Empty);
        }
    }
}