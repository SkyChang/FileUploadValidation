using FileUploadValidation.Utility;
using FileUploadValidation.Utility.Lib;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadValidation.Utility.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        private readonly IExtensionCheck _extensionCheck;

        public AllowedExtensionsAttribute(params string[] Extensions)
        {
            _extensionCheck = new ExtensionCheck();
            _extensions = Extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            // 判斷有無資料
            if (file == null)
            {
                return new ValidationResult($"副檔名不允許");
            }

            var extension = Path.GetExtension(file.FileName);

            if( string.IsNullOrEmpty(extension))
            {
                return new ValidationResult($"副檔名有誤");
            }
            
            // 判斷是否允許的副檔名
            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult($"副檔名不允許");
            }

            byte[] fileBytes;

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            // 判斷簽章
            if (!_extensionCheck.IsValidFileExtension(extension.ToLower(), fileBytes))
            {
                return new ValidationResult($"副檔名驗證失敗");
            }

            return ValidationResult.Success;
        }

        
    }

}
