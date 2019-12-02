using System;
using System.Collections.Generic;
using System.Text;

namespace Proofpoint
{
    class Folder : FileSystemObjects
    {
        /// <summary>
        /// Holds the file type.
        /// </summary>
        private string type;

        /// <summary>
        /// Holds the file name.
        /// </summary>
        private string name;

        /// <summary>
        /// Holds the path.
        /// </summary>
        private string path;

        /// <summary>
        /// Holds the size.
        /// </summary>
        private int size;

        /// <summary>
        /// Represents a folder.
        /// </summary>
        private Dictionary<string, FileSystemObjects> folder;

        /// <summary>
        /// Constructs a new empty folder.
        /// 
        /// t - file type.
        /// n - file name
        /// p - file path
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="n"></param>
        /// <param name="p"></param>
        public Folder(string t, string n, string p)
        {
            type = t;
            name = n;
            path = p;
            size = 0;
            folder = new Dictionary<string, FileSystemObjects>();
        }

        public string Type => type;

        public string Name => name;

        public string Path { get => path; set => path = value; }

        public int Size { get => size; set => size = value; }

        /// <summary>
        /// Adds an entity to the given file.
        /// </summary>
        /// <param name="obj"></param>
        public void AddEntity(FileSystemObjects obj)
        {
            if (obj.Type == "drive")
                throw new ArgumentException("Illegal File System Operation");

            String temp = obj.Name;

            if (folder.ContainsKey(temp))
                throw new ArgumentException("Path already exists.");
            else
            {
                folder.Add(temp, obj);
                obj.Path = path + "\\" + obj.Name;

                if (type == "folder")
                    size += obj.Size;
                else
                    size += obj.Size / 2;
            }

        }

        /// <summary>
        /// Returns the files located locally in this folder.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, FileSystemObjects> getFiles()
        {
            return new Dictionary<string, FileSystemObjects>(folder);
        }
    }

    class ZipFile : Folder
    {
        public ZipFile(string t, string n, string p, int s) : base(t, n, p) { }
    }

}
