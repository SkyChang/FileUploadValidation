using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileUploadValidation.Utility.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Study4.Security.Utility.Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 新增申请病历檔案上傳.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateMAFileUpload([FromForm]UserViewModel vm)
        {
            if (Request.Form.Files.Count <= 0)
            {
                return BadRequest("系統錯誤，請稍後再試");
            }


            var file = Request.Form.Files[0];

            string extension = Path.GetExtension(file.FileName).ToLower();

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var fileBytes = memoryStream.ToArray();
                System.IO.File.WriteAllBytes(@"C:\Users\skchang\Desktop\Temp\123.jpg", fileBytes);
                return Ok();

            }
        }
    }
    public class UserViewModel
    {
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(".jpg", ".png" )]
        public IFormFile Data { get; set; }
    }
}