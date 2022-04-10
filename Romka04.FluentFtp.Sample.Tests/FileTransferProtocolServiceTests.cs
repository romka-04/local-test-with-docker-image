using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Romka04.FluentFtp.Sample;

[TestFixture]
public class FileTransferProtocolServiceTests
{
    [Test]
    public async Task WriteFileAsync_should_copy_file_to_destination()
    {
        // arrange
        var fileName = _fixture.CreateRandomFileName("write-file-test-{0}.txt");
        await using var file = _fixture.CreateStream();
        var sut = _fixture.CreateSut();
        // act
        await sut.WriteFileAsync(fileName, file, false, CancellationToken.None);
        // assert
        AssertFileExists(fileName);
    }

    private void AssertFileExists(string fileName)
    {
        const string ftpData = "c:/pure-ftpd/data";
        var fullPath = Path.Combine(ftpData, fileName);
        var isExists = File.Exists(fullPath);
        Assert.IsTrue(isExists, $"File {fileName} is not found in location specified for FTP data. " +
                                $"Please check FTP settings. FTP data location: '{fullPath}'")
            ;
    }

    #region Test Helpers

    private FileTransferProtocolServiceTestsFixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _fixture = new FileTransferProtocolServiceTestsFixture();
    }

    [TearDown]
    public void TearDown()
    {
    }

    #endregion
}

public class FileTransferProtocolServiceTestsFixture
{
    public string CreateRandomFileName(string fileNameTemplate)
    {
        return string.Format(fileNameTemplate, Guid.NewGuid().ToString("N").Substring(0, 4));
    }

    public Stream CreateStream()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Sir, in my heart there was a kind of fighting");
        sb.AppendLine("That would not let me sleep. Methought I lay");
        sb.AppendLine("Worse than the mutines in the bilboes. Rashly—");
        sb.AppendLine("And prais'd be rashness for it—let us know");
        sb.AppendLine("Our indiscretion sometimes serves us well ...");

        return ObjectToStream(sb);
    }

    private Stream ObjectToStream(object sb)
        => ObjectToStream(sb.ToString());

    private Stream ObjectToStream(string? sb)
        => new MemoryStream(Encoding.UTF8.GetBytes(sb ?? string.Empty));

    public FileTransferProtocolService CreateSut()
    {
        return new();
    }
}