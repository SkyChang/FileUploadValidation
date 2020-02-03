namespace FileUploadValidation.Utility.Lib
{
    public interface IExtensionCheck
    {
        bool IsValidFileExtension(string extension, byte[] fileData);
    }
}