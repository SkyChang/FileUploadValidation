namespace FileUploadValidation.Utility
{
    public interface IExtensionCheck
    {
        bool IsValidFileExtension(string extension, byte[] fileData);
    }
}