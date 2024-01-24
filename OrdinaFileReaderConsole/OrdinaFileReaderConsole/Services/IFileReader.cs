namespace OrdinaFileReaderConsole
{
    public interface IFileReader
    {
        string ReadFileContent(string path);
        string DecryptText(string content);
    }
}
