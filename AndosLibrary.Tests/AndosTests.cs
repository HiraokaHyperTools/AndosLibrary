using NUnit.Framework;
using System;
using System.IO;

namespace AndosLibrary.Tests
{
    public class AndosTests
    {
        private readonly Andos _andos = new Andos(new Uri(Environment.GetEnvironmentVariable("AndosLibrary.Tests.URL")));

        [OneTimeSetUp]
        public void Login()
        {
            _andos.Login(
                Environment.GetEnvironmentVariable("AndosLibrary.Tests.User"),
                Environment.GetEnvironmentVariable("AndosLibrary.Tests.Pass")
            );
        }

        [OneTimeTearDown]
        public void Logout()
        {
            _andos.Logout();
        }

        private string AddNewRecord(string title)
        {
            var result = _andos.AddJs($"REG=1&group_id=1&title={Uri.EscapeDataString(title)}&project=AndosLibrary.Tests");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.id, Does.Match("^\\d{8}$"));

            return result.id;
        }

        [Test]
        [TestCase(false, false, false)]
        [TestCase(false, false, true)]
        [TestCase(false, true, false)]
        [TestCase(false, true, true)]
        [TestCase(true, false, false)]
        [TestCase(true, false, true)]
        [TestCase(true, true, false)]
        [TestCase(true, true, true)]
        public void ReplaceJsTest(bool keepOwner, bool alwaysrename, bool newmt)
        {
            string id = AddNewRecord($"ReplaceJsTest {keepOwner} {alwaysrename} {newmt}");
            Console.WriteLine(id);

            var textFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".txt");
            File.WriteAllText(textFile, $"{DateTime.Now}");

            var multipart = new Andos.MPFDGen();
            multipart.FileList.Add(
                new Andos.MPFDGen.FileEntry
                {
                    Name = "contents",
                    FilePath = textFile,
                }
            );
            _andos.ReplaceJs(id, multipart, keepOwner, alwaysrename, newmt);
        }

        [Test]
        public void ReplaceFile2Test()
        {
            string id = AddNewRecord("ReplaceFile2Test");
            Console.WriteLine(id);

            var textFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".txt");
            File.WriteAllText(textFile, $"{DateTime.Now}");

            _andos.ReplaceFile2(id, textFile);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void ReplaceFile3Test(bool alwaysrename)
        {
            string id = AddNewRecord($"ReplaceFile3Test {alwaysrename}");
            Console.WriteLine(id);

            var textFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".txt");
            File.WriteAllText(textFile, $"{DateTime.Now}");

            _andos.ReplaceFile3(id, textFile, alwaysrename);
        }

        [Test]
        public void SearchTests()
        {
            void Verify(Search result)
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.f, Is.EqualTo("thumbnail"));
                Assert.That(result.cntNext, Is.GreaterThanOrEqualTo(0));
                Assert.That(result.cntPrev, Is.GreaterThanOrEqualTo(0));
                Assert.That(result.cntRecords, Is.GreaterThanOrEqualTo(0));
                Assert.That(result.cntTotal, Is.GreaterThanOrEqualTo(0));
                Assert.That(result.start, Is.GreaterThanOrEqualTo(0));
                Assert.That(result.recs, Is.Not.Null);
            }

            {
                var result = _andos.SearchDoc("test");
                Verify(result);
            }
            {
                var result = _andos.SearchDocWithfp2("test");
                Verify(result);
            }
            {
                var result = _andos.SearchCustom(new Wcqb().Str("test").Skw());
                Verify(result);
            }
            {
                var result = _andos.SearchCustomWithfp2(new Wcqb().Str("test").Skw());
                Verify(result);
            }
        }
    }
}
