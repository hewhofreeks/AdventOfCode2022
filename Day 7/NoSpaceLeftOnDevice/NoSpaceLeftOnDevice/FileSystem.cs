using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSpaceLeftOnDevice
{
    class BaseFile
    {
        public BaseFile(string name)
        {
            Name = name;
        }
        public BaseFile(string name, int size)
        {
            Name = name;
            Size = size;
        }

        public string Name { get; private set; }

        public virtual int Size { get; }
    }

    class Directory : BaseFile
    {
        public Directory(string name, Directory parent) : base(name)
        {
            Files = new List<BaseFile>();
            Parent = parent;
        }

        public Directory(string name) : base(name)
        {
            Files = new List<BaseFile>();
        }

        public Directory Parent { get; set; }

        public IList<BaseFile> Files { get; }

        public override int Size
        {
            get
            {
                return Files.Sum(f => f.Size);
            }
        }

        public void AddFile(BaseFile file)
        {
            Files.Add(file);
        }
    }

    class FileSystem
    {
        private Directory _rootDirectory;
        private Directory _currentDirectory;

        public FileSystem()
        {
            _rootDirectory = new Directory("/");
            _currentDirectory = _rootDirectory;
        }

        public Directory GetRoot()
        {
            return _rootDirectory;
        }

        public void ChangeDirectory(string input)
        {
            switch (input)
            {
                case "..":
                    if (_currentDirectory.Parent != null)
                        _currentDirectory = _currentDirectory.Parent;
                    break;
                case "/":
                    _currentDirectory = _rootDirectory;
                    break;
                default:
                    var directoryToNavigateTo = _currentDirectory.Files.OfType<Directory>().FirstOrDefault(f => f.Name == input);

                    if(directoryToNavigateTo != null)
                        _currentDirectory = directoryToNavigateTo;
                    
                    break;
            }
        }

        public void AddFileToCurrentDirectory(string name, int size)
        {
            if (_currentDirectory.Files.Any(f => f.Name == name))
                return;

            _currentDirectory.AddFile(new BaseFile(name, size));
        }

        public void AddDirectoryToCurrentDirectory(string name)
        {
            if (_currentDirectory.Files.Any(f => f.Name == name))
                return;

            _currentDirectory.AddFile(new Directory(name, _currentDirectory));
        }
    }
}
