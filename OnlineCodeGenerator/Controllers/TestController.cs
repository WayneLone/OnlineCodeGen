using Microsoft.AspNetCore.Mvc;
using OnlineCodeGenerator.Models;
using OnlineCodeGenerator.Services;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodeGenerator.Controllers
{
    public class TestController : Controller
    {
        private readonly IViewRenderService _viewRenderService;

        public TestController(IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
        }

        public async Task<IActionResult> Build()
        {
            Poco model = new Poco
            {
                ClassComment = "用户信息",
                ClassName = "UserInfo",
                NameSpace = "App.Entity",
                Properties = new List<PocoProperty>
                {
                    new PocoProperty { Comment = "用户ID", Name = "ID", TypeName = "int" },
                    new PocoProperty { Comment = "用户名称", Name = "Name", TypeName = "string" },
                    new PocoProperty { Comment = "用户年龄", Name = "Age", TypeName = "int" },
                    new PocoProperty { Comment = "用户性别", Name = "Gender", TypeName = "bool" }
                }
            };
            string result = await _viewRenderService.RenderToStringAsync("Test/Poco", model);

            byte[] fileBuffer = BuildZipFile(new List<(string fileName, string fileContent)> {  ("UserInfo.cs", result) });

            return File(fileBuffer, "application/zip", "code.zip");
        }

        private byte[] BuildZipFile(List<(string fileName, string fileContent)> fileList)
        {
            using (MemoryStream zipStream = new MemoryStream())
            {
                using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create))
                {
                    foreach (var item in fileList)
                    {
                        ZipArchiveEntry entry = zip.CreateEntry(item.fileName);
                        using (Stream entryStream = entry.Open())
                        {
                            byte[] entryFileBuffer = Encoding.UTF8.GetBytes(item.fileContent);
                            entryStream.Write(entryFileBuffer, 0, entryFileBuffer.Length);
                        }
                    }
                }
                return zipStream.ToArray();
            }
        }
    }
}
