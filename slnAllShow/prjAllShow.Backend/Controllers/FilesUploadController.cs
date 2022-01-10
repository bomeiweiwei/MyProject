using Microsoft.AspNetCore.Mvc;
using AllShow.Data;
using AllShow.Models;
using Microsoft.AspNetCore.Authorization;

namespace prjAllShow.Backend.Controllers
{
    [Authorize]
    public class FilesUploadController : Controller
    {
        private string _path;
        private AllShowDBContext _context;
        public FilesUploadController(IWebHostEnvironment hostEnvironment, AllShowDBContext context)
        {
            _path = $"{hostEnvironment.WebRootPath}/Images";
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        private FileInfo[] GetFiles()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_path);
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            return fileInfos;
        }
        #region 單圖檔案上傳
        public IActionResult TestFileUpload()
        {
            return View(GetFiles());
        }
        [HttpPost]
        public async Task<IActionResult> TestFileUpload(IFormFile formFile)
        {
            if (formFile != null)
            {
                if (formFile.Length > 0)
                {
                    string savePath = $"{_path}\\{formFile.FileName}";
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return View(GetFiles());
        }
        #endregion
        #region 多圖檔案上傳
        public IActionResult TestFilesUpload()
        {
            return View(GetFiles());
        }
        [HttpPost]
        public async Task<IActionResult> TestFilesUpload(List<IFormFile> formFiles)
        {
            foreach (var formFile in formFiles)
            {
                if (formFile != null)
                {
                    if (formFile.Length > 0)
                    {
                        string savePath = $"{_path}\\{formFile.FileName}";
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }
            }
            return View(GetFiles());
        }
        #endregion
        /*
        [HttpPost]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }
        */

        public IActionResult TestShowImages()
        {
            int count = _context.DbFiles.Count();
            ViewBag.Count = count;
            return View();
        }

        [HttpPost]
        public IActionResult UpImages(IFormFile formFile)
        {
            byte[] buffer = new byte[formFile.Length];
            var resultInBytes = ConvertToBytes(formFile);
            Array.Copy(resultInBytes, buffer, resultInBytes.Length);

            DbFiles dbFiles = new DbFiles()
            {
                Name = Path.GetRandomFileName(),
                MimeType = formFile.ContentType,
                Size = (int)formFile.Length,
                Contnet = buffer
            };
            try
            {
                SaveDbFiles(dbFiles);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return RedirectToAction("TestShowImages");
        }

        public IActionResult ShowImages(int id)
        {
            if (id > 0)
            {
                var file = _context.DbFiles.FirstOrDefault(m => m.Id == id);
                if (file != null)
                {
                    byte[] buffer = file.Contnet;
                    return File(buffer, file.MimeType, file.Name);
                }
                else
                {
                    return Content("");
                }
            }
            else
            {
                return Content("");
            }
        }       

        private byte[] ConvertToBytes(IFormFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.OpenReadStream().CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public void SaveDbFiles(DbFiles dbFiles)
        {
            _context.Add(dbFiles);
            _context.SaveChanges();
        }
    }
}
