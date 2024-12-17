using System.Text.Json;

namespace assignment_3.Tests
{
    public class SaveManagerTests
    {
        const string _testFileName = "object_file";
        readonly Student student = new(ClassLevel.Freshman);

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_testFileName + ".json"))
                File.Delete(_testFileName + ".json");
        }

        [Test]
        public void SaveToJson_FileCreated()
        {
            SaveManager.SaveToJson(student, _testFileName);

            Assert.IsTrue(File.Exists(_testFileName + ".json"));

            using StreamReader reader = new(_testFileName + ".json");
            string jsonString = reader.ReadToEnd();

            Assert.That(jsonString, Does.Contain("class_level"));
        }

        [Test]
        public void SaveToJson_CannotPassNullOrEmptyString_Name()
        {
            Assert.Throws<ArgumentNullException>(() => SaveManager.SaveToJson(student, ""));
        }

        [Test]
        public void SaveToJson_CannotPassNullObject()
        {
            Student? nullStudent = null;
            Assert.Throws<NullReferenceException>(
                () => SaveManager.SaveToJson(nullStudent, _testFileName)
            );
        }

        [Test]
        public void LoadToJson_CannotLoadNonExistentFile()
        {
            string invalidFilePath = "non_existent_file.json";

            var ex = Assert.Throws<FileNotFoundException>(
                () => SaveManager.LoadFromJson<Student>(invalidFilePath)
            );
        }

        [Test]
        public void LoadToJson_CannotLoadEmptyFile()
        {
            using (StreamWriter writer = new(_testFileName + ".json"))
            {
                writer.Write("");
            }

            var ex = Assert.Throws<JsonException>(
                () => SaveManager.LoadFromJson<Student>(_testFileName + ".json")
            );
        }

        [Test]
        public void LoadToJson_CannotLoadNonJsonFile()
        {
            using (StreamWriter writer = new(_testFileName + ".json"))
            {
                writer.Write(
                    $"""
                    <?xml version="1.0" encoding="UTF-8"?>
                    <root>Hello World!</root>
                    """
                );
            }

            var ex = Assert.Throws<JsonException>(
                () => SaveManager.LoadFromJson<Student>(_testFileName + ".json")
            );
        }
    }
}
