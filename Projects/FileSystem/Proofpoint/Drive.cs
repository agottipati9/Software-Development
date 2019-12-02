using System;
using System.Collections.Generic;
using System.Text;

namespace Proofpoint
{
    class Drive : FileSystemObjects
    {

        private string type;
        private string name;
        private string path;
        private string content;
        private int size;
        private Dictionary<string, FileSystemObjects> drive;

        public Drive(string n)
        {
            type = "drive";
            name = n;
            path = name + "/";
            size = 0;
            drive = new Dictionary<string, FileSystemObjects>();
        }

        public string Type => type;

        public string Name => name;

        public string Path { get => path; set => path = value; }

        public int Size { get => size; set => size = value; }

        public void AddEntity(FileSystemObjects obj)
        {
            if (obj.Type == "drive")
                throw new ArgumentException("Illegal File System Operation");

            String temp = obj.Name;

            if (drive.ContainsKey(temp))
                throw new ArgumentException("Path already exists.");
            else
            {
                drive.Add(temp, obj);
                obj.Path = path + "\\" + obj.Name;
            }
        }

        /// <summary>
        /// Returns the files located locally in this drive.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, FileSystemObjects> getFiles()
        {
            return new Dictionary<string, FileSystemObjects>(drive);
        }
    }

}

