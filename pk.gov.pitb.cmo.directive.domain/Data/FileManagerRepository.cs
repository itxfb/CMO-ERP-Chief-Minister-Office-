using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccessLayer.Shared_Services
{
    public class FileManagerRepository:IFileManager
    {

        private readonly IHostingEnvironment hostingEnvironment;
     //   private readonly IConfiguration configuration;
      //  public FileManagerRepository(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        public FileManagerRepository()
        {
            this.hostingEnvironment = hostingEnvironment;
            this.configuration = configuration;
        }
        public string Copy(string name, string PathName)
        {
            throw new NotImplementedException();
        }

        public void Delete(string name, string PathName)
        {
            var path = configuration.GetSection("Attachments").GetSection("Attachmentpath").Value;
            File.Delete(path + name);
        }

        public byte[] Get(string name, string PathName)
        {

            var path =  configuration.GetSection(PathName).GetSection("Attachmentpath").Value;

            return File.ReadAllBytes(path + name);
        
        }

        public string GetConnectionString()
        {
            return (configuration.GetSection("ConnectionStrings").GetSection("DBConn").Value);
        }

        public string Save(IFormFile file, string FileName, string PathName, string ModuleName)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return this.Save(ms.ToArray(), Path.GetExtension(FileName), PathName, ModuleName);
            }
        }

        public string Save(byte[] file, string extension, string PathName ,string ModuleName)
        {
            var path = configuration.GetSection("Attachments").GetSection("Attachmentpath").Value;
            string UniqueFileName = ModuleName+DateTime.Now.ToString("yyyy-MM-dd")+ Guid.NewGuid().ToString() + extension;
            File.WriteAllBytes(path + UniqueFileName, file);
            return UniqueFileName;
        }

        public string UniqueFileName(string fileName, string FileId, string fileExt)
        {
            return (DateTime.Now.Year + "/" + DateTime.Now.ToString("yyyy-MM-dd") + "--" + FileId + "--" + fileName + "--" + Guid.NewGuid().ToString("N") + fileExt);
        }



    }
}
