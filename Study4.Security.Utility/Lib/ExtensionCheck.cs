using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadValidation.Utility.Lib
{
    public class ExtensionCheck : IExtensionCheck
    {
        private static Dictionary<string, List<byte[]>> _fileSignature =
            new Dictionary<string, List<byte[]>>
            {
                //https://www.filesignatures.net/index.php?search=PDF&mode=EXT
                {
                    ".png", new List<byte[]>
                    {
                        new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }
                    }
                },
                {
                    ".jpg", new List<byte[]>
                    {
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 }
                    }
                },
                {
                    ".jpeg", new List<byte[]>
                    {
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 }
                    }
                },
                {
                    ".docx", new List<byte[]>
                    {
                        new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                        new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00  }
                    }
                },
                {
                    ".pdf", new List<byte[]>
                    {
                        new byte[] { 0x25, 0x50, 0x44, 0x46 }
                    }
                },
                {
                    ".xlsx", new List<byte[]>
                    {
                        new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                        new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 }

                    }
                },
                {
                    ".xml", new List<byte[]>
                    {
                        new byte[]
                        {
                            0x3C, 0x3F, 0x78, 0x6D,
                            0x6C, 0x20, 0x76, 0x65,
                            0x72, 0x73, 0x69, 0x6F,
                            0x6E, 0x3D, 0x22, 0x31,
                            0x2E, 0x30, 0x22, 0x3F,
                            0x3E
                        }
                    }
                }
            };

        public bool IsValidFileExtension(string extension, byte[] fileData)
        {
            if (string.IsNullOrEmpty(extension) || fileData == null || fileData.Length == 0)
            {
                return false;
            }

            // 找不到副檔名
            if (!_fileSignature.ContainsKey(extension.ToLower()))
            {
                return false;
            }

            var sig = _fileSignature[extension.ToLower()];
            foreach (byte[] b in sig)
            {
                var curFileSig = new byte[b.Length];
                Array.Copy(fileData, curFileSig, b.Length);
                if (curFileSig.SequenceEqual(b))
                {
                    // 比對正確
                    return true;
                }
            }

            // 比對不正確
            return false;
        }
    }
}
