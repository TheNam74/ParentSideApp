using System.IO;

namespace ParentSideApp
{
    public class CFolder
    {
        public string Path { get; set; }

        public string Name
        {
            get
            {
                string result = new DirectoryInfo(Path).Name;
                return result;
            }
        }

        public CFolder(string iPath)
        {
            Path = iPath;
        }

        public CFolder()
        {

        }
        public override string ToString()
        {
            return Name;
        }
    }
}
