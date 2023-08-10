using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.domain.Contracts
{
    public interface IFileManager
    {

       
        public string UniqueFileName(string fileName, string FileId, string fileExt);

        public string GetConnectionString();

        string Save(byte[] file, string extension, string PathName,string ModuleName);

        string Copy(string name, string PathName);
        string Save(IFormFile file, string FileName, string PathName, string ModuleName);

        void Delete(string name, string PathName);

        byte[] Get(string name, string PathName);
    }
}
