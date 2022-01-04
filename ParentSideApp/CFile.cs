using System.IO;

namespace ParentSideApp
{
    public class CFile
    {
        public CFolder DirectoryPath { get; set; }
        public string FileName { get; set; }

        public string FullFileNamePath => DirectoryPath.Path + @"\" + FileName;

        public CFile(FileInfo fileInfo)
        {
            DirectoryPath = new CFolder
            {
                Path = fileInfo.DirectoryName
            };
            FileName = fileInfo.Name;
        }

    }
}
