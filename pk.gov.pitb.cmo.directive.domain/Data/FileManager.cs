
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.web
{
    public class FileManager:IFileManager
    {

     
        private readonly IConfiguration configuration;
       // private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        public FileManager( IConfiguration configuration)
        {
           // this.hostingEnvironment = hostingEnvironment;
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
            return (configuration.GetSection("ConnectionStrings").GetSection("RepositoryContext").Value);
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
            var path = configuration.GetSection("Attachments").GetSection(PathName).Value+"/";
            
            string UniqueFileName = ModuleName+DateTime.Now.ToString("yyyy-MM-dd")+ Guid.NewGuid().ToString() + extension;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllBytes(path + UniqueFileName, file);
            return path + UniqueFileName;
        }

        public string UniqueFileName(string fileName, string FileId, string fileExt)
        {
            return (DateTime.Now.Year + "/" + DateTime.Now.ToString("yyyy-MM-dd") + "--" + FileId + "--" + fileName + "--" + Guid.NewGuid().ToString("N") + fileExt);
        }



    }
}
