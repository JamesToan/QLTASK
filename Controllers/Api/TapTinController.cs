using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using coreWeb.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TapTinController : ControllerBase
    {
        private ApplicationDbContext _context;


        public TapTinController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult UploadDoc([FromForm] TapTin doc)
        {
            
            // Getting Name
            string name = doc.Name;
            // Getting Image
            var file = doc.File;
            // Saving Image on Server

            var length = file.Length; //10485760 10M
            var contentType = file.ContentType;

            string vTimeFile = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fileName = vTimeFile + "_" + file.FileName;
            string type = fileName.Split('.').Last();

            if (type.ToLower() != "pdf" && type.ToLower() != "doc" && type.ToLower() != "docx" && type.ToLower() != "xls" && type.ToLower() != "xlsx" && type.ToLower() != "xlsm" && type.ToLower() !=  "jpg" && type.ToLower() != "png")
            {
                return Ok(new { status = 0, message = "Không đúng định dạng quy định" });
            }
            if (length > 10485760)
            {
                return Ok(new { status = 0, message = "Dung lượng vượt quá 10M" });
            }

            var filePath = Path.Combine("wwwroot/uploads", fileName);
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return Ok(new { status = 1, message = "Upload thành công", file = fileName });
        }
        [HttpPost]
        public IActionResult UploadImage([FromForm] TapTin image)
        {
            // Getting Name
            string name = image.Name;
            // Getting Image
            var file = image.File;
            // Saving Image on Server

            var length = file.Length; //10485760 10M
            var contentType = file.ContentType;

            string vTimeFile = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fileName = vTimeFile + "_" + file.FileName;
            string type = fileName.Split('.').Last();

            if (type.ToLower() != "png" && type.ToLower() != "jpg" && type.ToLower() != "jpeg")
            {
                return Ok(new { status = 0, message = "Không đúng định dạng quy định" });
            }
            if (length > 10485760)
            {
                return Ok(new { status = 0, message = "Dung lượng vượt quá 10M" });
            }

            var filePath = Path.Combine("wwwroot/images", fileName);
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return Ok(new { status = 1, message = "Upload thành công", file = fileName });
        }
        [HttpPost]
        public IActionResult UploadVideo([FromForm] TapTin video)
        {
            // Getting Name
            string name = video.Name;
            // Getting Image
            var file = video.File;
            // Saving Image on Server

            var length = file.Length; //104857600 100M
            var contentType = file.ContentType;

            string vTimeFile = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fileName = vTimeFile + "_" + file.FileName;
            string type = fileName.Split('.').Last();

            if (type.ToLower() != "mp4" && type.ToLower() != "webm")
            {
                return Ok(new { status = 0, message = "Không đúng định dạng quy định" });
            }
            if (length > 104857600)
            {
                return Ok(new { status = 0, message = "Dung lượng vượt quá 100M" });
            }

            var filePath = Path.Combine("wwwroot/videos", fileName);
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return Ok(new { status = 1, message = "Upload thành công", file = fileName });
        }

        [HttpPost]
        public IActionResult CKUpload([FromForm] IFormFile upload)
        {
            // Getting Name
            //string name = upload;
            // Getting Image
            var file = upload;
            // Saving Image on Server

            var length = file.Length; //10485760 10M
            var contentType = file.ContentType;

            string vTimeFile = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fileName = vTimeFile + "_" + file.FileName;
            string type = fileName.Split('.').Last();

            if (type.ToLower() != "png" && type.ToLower() != "jpg" && type.ToLower() != "jpeg")
            {
                return Ok(new { status = 0, message = "Không đúng định dạng quy định" });
            }
            if (length > 10485760)
            {
                return Ok(new { status = 0, message = "Dung lượng vượt quá 10M" });
            }

            var filePath = Path.Combine("wwwroot/images", fileName);
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return Ok(new
            {
                url = "/images/" + fileName,
                fileName = fileName,
                uploaded = 1
            });
        }
    }
}
