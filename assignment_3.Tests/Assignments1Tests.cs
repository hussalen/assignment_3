namespace assignment_3.Tests;

public class Assignments1Tests
{
    [Test]
    public void Constructor_ShouldInitializeCorrectly()
    {
        // Arrange & Act
        var coding = new Coding("Test Topic", DateTime.UtcNow.AddDays(1), "C#", "http://example.com");

        // Assert
        Assert.That(coding.AssignmentID, Is.GreaterThan(0));
        Assert.That(coding.Topic, Is.EqualTo("Test Topic"));
        Assert.That(coding.DueDate, Is.GreaterThanOrEqualTo(DateTime.UtcNow));
        Assert.That(coding.Language, Is.EqualTo("C#"));
        Assert.That(coding.RepositoryUrl, Is.EqualTo("http://example.com"));
    }
    [Test]
    public void Constructor_ShouldThrowException_WhenInvalidValuesProvided()
    {
        Assert.Throws<ArgumentException>(() => new Coding("", DateTime.UtcNow.AddDays(1), "C#", "http://example.com"));
        Assert.Throws<ArgumentException>(() => new Coding("Test", DateTime.UtcNow.AddDays(-1), "C#", "http://example.com"));
        Assert.Throws<ArgumentException>(() => new Coding("Test", DateTime.UtcNow.AddDays(1), "", "http://example.com"));
        Assert.Throws<ArgumentException>(() => new Coding("Test", DateTime.UtcNow.AddDays(1), "C#", "invalid-url"));
    }
    [Test]
    public void CodingConstructor_ShouldThrowArgumentException_WhenTopicIsInvalid()
    {
        var invalidTopic = "No";
        var dueDate = DateTime.UtcNow.AddDays(1);
        var language = "C#";
        var repositoryUrl = "http://example.com/repo";

        Assert.Throws<ArgumentException>(() => new Coding(invalidTopic, dueDate, language, repositoryUrl));
    }
    [Test]
    public void CodingConstructor_ShouldThrowArgumentException_WhenDueDateIsInThePast()
    {
        var topic = "Test Topic";
        var dueDate = DateTime.UtcNow.AddDays(-1);
        var language = "C#";
        var repositoryUrl = "http://example.com/repo";

        Assert.Throws<ArgumentException>(() => new Coding(topic, dueDate, language, repositoryUrl));
    }
    [Test]
    public void EditAssignment_ShouldModifyAssignmentWhenValid()
    {
        var topic = "Test Topic";
        var dueDate = DateTime.UtcNow.AddDays(1);
        var language = "C#";
        var repositoryUrl = "http://example.com/repo";

        var coding = new Coding(topic, dueDate, language, repositoryUrl);

        var newTopic = "Updated Topic";
        var newDueDate = DateTime.UtcNow.AddDays(2);
        var newLanguage = "Java";
        var newRepositoryUrl = "http://example.com/newrepo";

        coding.EditAssignment(coding.AssignmentID, newTopic, newDueDate, newLanguage, newRepositoryUrl);

        Assert.AreEqual(newTopic, coding.Topic);
        Assert.AreEqual(newDueDate, coding.DueDate);
        Assert.AreEqual(newLanguage, coding.Language);
        Assert.AreEqual(newRepositoryUrl, coding.RepositoryUrl);
    }
    [Test]
    public void EditAssignment_ShouldThrowArgumentException_WhenModifyingDefaultAssignment()
    {
        var topic = "Test Topic";
        var dueDate = DateTime.UtcNow.AddDays(1);
        var language = "C#";
        var repositoryUrl = "http://example.com/repo";

        var coding = new Coding(topic, dueDate, language, repositoryUrl);

        Assert.Throws<ArgumentException>(() => coding.EditAssignment(Defaults.DEFAULT_CODING.AssignmentID, "New Topic", DateTime.UtcNow, "Java", "http://example.com/newrepo"));
    }
    [Test]
    public void EditAssignment_ShouldThrowInvalidOperationException_WhenAssignmentIsAlreadySubmitted()
    {
        var topic = "Test Topic";
        var dueDate = DateTime.UtcNow.AddDays(1);
        var language = "C#";
        var repositoryUrl = "http://example.com/repo";

        var coding = new Coding(topic, dueDate, language, repositoryUrl);
        coding.Submit(new Student(ClassLevel.Freshman, "John Doe", "john.doe@example.com", new string[] { "123 Street", "City" }, "password123"));

        Assert.Throws<InvalidOperationException>(() => coding.EditAssignment(coding.AssignmentID, "New Topic", DateTime.UtcNow, "Java", "http://example.com/newrepo"));
    }

    [Test]
    public void RemoveAssignment_ShouldRemoveAssignmentSuccessfully()
    {
        var topic = "Test Topic";
        var dueDate = DateTime.UtcNow.AddDays(1);
        var language = "C#";
        var repositoryUrl = "http://example.com/repo";

        var coding = new Coding(topic, dueDate, language, repositoryUrl);

        Coding.RemoveAssignment(coding.AssignmentID);

        Assert.AreEqual(5, Coding.GetCodingExtent().Count);
    }

    [Test]
    public void Submit_ShouldSubmitAssignmentSuccessfully()
    {
        var topic = "Test Topic";
        var dueDate = DateTime.UtcNow.AddDays(1);
        var language = "C#";
        var repositoryUrl = "http://example.com/repo";

        var coding = new Coding(topic, dueDate, language, repositoryUrl);
        var student = new Student(ClassLevel.Freshman, "John Doe", "john.doe@example.com", new string[] { "123 Street", "City" }, "password123");

        coding.Submit(student);

        Assert.NotNull(coding.SubmissionDate);
        Assert.AreEqual(student, coding.SubmittingStudent);
    }

    [Test]
    public void EConstructor_ShouldInitializeCorrectly()
    {
        // Arrange & Act
        var essay = new Essay("Test Topic", DateTime.UtcNow.AddDays(1), 500, 1500);

        // Assert
        Assert.That(essay.AssignmentID, Is.GreaterThan(0));
        Assert.That(essay.Topic, Is.EqualTo("Test Topic"));
        Assert.That(essay.DueDate, Is.GreaterThanOrEqualTo(DateTime.UtcNow));
        Assert.That(essay.MinWordCount, Is.EqualTo(500));
        Assert.That(essay.MaxWordCount, Is.EqualTo(1500));
    }
    [Test]
    public void EditAssignment_ShouldModifyExistingEssayCorrectly()
    {
        // Arrange
        var essay = new Essay("Test Topic", DateTime.UtcNow.AddDays(1), 500, 1500);
        var newTopic = "Updated Topic";
        var newDueDate = DateTime.UtcNow.AddDays(2);
        uint newMinWordCount = 600;
        uint newMaxWordCount = 1600;

        // Act
        Essay.EditAssignment(essay.AssignmentID, newTopic, newDueDate, newMinWordCount, newMaxWordCount);

        // Assert
        Assert.That(essay.Topic, Is.EqualTo(newTopic));
        Assert.That(essay.DueDate, Is.EqualTo(newDueDate));
        Assert.That(essay.MinWordCount, Is.EqualTo(newMinWordCount));
        Assert.That(essay.MaxWordCount, Is.EqualTo(newMaxWordCount));
    }
    [Test]
    public void Submit_ShouldUpdateSubmissionDateAndStudent()
    {
        // Arrange
        var essay = new Essay("Test Topic", DateTime.UtcNow.AddDays(1), 500, 1500);
        var student = new Student(ClassLevel.Freshman, "John Doe", "john.doe@example.com", new string[] { "123 Street", "City" }, "password123");
        essay.WordCount = 1000;

        // Act
        essay.Submit(student);

        // Assert
        Assert.That(essay.SubmissionDate, Is.Not.Null);
        Assert.That(essay.SubmittingStudent, Is.EqualTo(student));
    }
    [Test]
    public void RemoveAssignment_ShouldRemoveEssaySuccessfully()
    {
        // Arrange
        var essay = new Essay("Test Topic", DateTime.UtcNow.AddDays(1), 500, 1500);
        var initialCount = Essay.GetEssayExtent().Count;

        // Act
        Essay.RemoveAssignment(essay.AssignmentID);

        // Assert
        Assert.That(Essay.GetEssayExtent().Count, Is.EqualTo(initialCount-1));
    }
    [Test]
    public void RemoveAssignment_ShouldThrowException_WhenEssayIsDefault()
    {
        // Arrange
        var essay = Defaults.DEFAULT_ESSAY; 

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Essay.RemoveAssignment(essay.AssignmentID));
    }
    


}