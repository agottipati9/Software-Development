using System;
using System.Collections.Generic;

namespace Proofpoint
{
    public class FileSystem
    {
        /// <summary>
        /// Represents the file system.
        /// </summary>
        private Dictionary<String, FileSystemObjects> system;

        /// <summary>
        /// Constructs a new filesystem.
        /// </summary>
        public FileSystem()
        {
            system = new Dictionary<String, FileSystemObjects>();
        }

        /// <summary>
        /// Creates a new entity in the file system.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        public void Create(String type, String name, String path)
        {
            // All Files must be contained within drives
            if(system.Count == 0 && type != "drive")
            {
                throw new ArgumentException("Illegal File System Operation.");
            }

            // Drives are the highest level of storage
            else if(system.Count >= 0 && type == "drive" && path == "")
            {
                system.Add(path, new Drive(name));
                return;
            }

            // Other files may now be created
            else
            {
                // Traverses to given path
                String[] fileNames = GetPathTokens(path);
                bool bottomFile = false;
                FileSystemObjects file = TraverseToPath(fileNames, bottomFile);

                if(file.Type != "folder")
                    throw new ArgumentException("Illegal File System Operation.");

                // Creates a file in the given path
                switch (type)
                {
                    case "folder":
                        ((Folder)file).AddEntity(new Folder(type, name, path));
                        break;
                    case "zipfile":
                        ((Folder)file).AddEntity(new Folder(type, name, path));
                        break;
                    case "textfile":
                        ((Folder)file).AddEntity(new TextFile(type, name, ""));
                        break;
                    default:
                        throw new ArgumentException("Not a valid file system type.");
                }

            }
        }

        /// <summary>
        /// Deletes a file from the file system.
        /// </summary>
        /// <param name="path"></param>
        public void Delete(String path)
        {
            // Traverses to path
            String[] tokens = GetPathTokens(path);
            FileSystemObjects obj = TraverseToPath(tokens, true);
            Dictionary<string, FileSystemObjects> file = ((Folder)obj).getFiles();
            String delName = tokens[tokens.Length - 1];

            // Removes file (if exists) and decrements the size of the related files
            if (file.ContainsKey(delName))
            {
                AlterPathSize(tokens, "dec", obj.Size);
                file.Remove(delName);
            }
            else
                throw new ArgumentException("Path not found");
        }

        /// <summary>
        /// Moves a file within the filesystem.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public void Move(String src, String dst)
        {
            // Traverses to src path and enforces file system properties
            String[] tokens = GetPathTokens(src);
            FileSystemObjects obj = TraverseToPath(tokens, true);
            Dictionary<string, FileSystemObjects> file = ((Folder)obj).getFiles();
            String delName = tokens[tokens.Length - 1];

            String[] dstTokens = GetPathTokens(dst);
            if (dstTokens.Length == 0)
                throw new ArgumentException("Illegal FileSystem Operation");

            if (file.ContainsKey(delName))
            {
               // Grabs object to be moved and decreases the size along the path
                FileSystemObjects temp = file[delName];
                AlterPathSize(tokens, "dec", temp.Size);
                file.Remove(delName);

                // Moves object and updates path
                FileSystemObjects dstObj = TraverseToPath(dstTokens, true);
                AlterPathSize(dstTokens, "inc", temp.Size);
                ((Folder)dstObj).AddEntity(temp);
            }
            else
                throw new ArgumentException("Path not found");
        }

        /// <summary>
        /// Writes the given content to the textfile dictated by the path.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public void WriteToFile(String path, String content)
        {
            String[] tokens = GetPathTokens(content);
            FileSystemObjects obj = TraverseToPath(tokens, false);

            if (obj.Type != "textfile")
                throw new ArgumentException("Cannot write to file");
            else
            {
                // Alters the file path's size based on the new content
                String op = "";
                int size = obj.Size - content.Length;

                if (content.Length > obj.Size)
                    op = "inc";
                else if (content.Length < obj.Size)
                    op = "dec";

                ((TextFile)obj).Write(content);
                AlterPathSize(tokens, op, size);
            }
        }

        /// <summary>
        /// Driver method for Travesing file system.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        private FileSystemObjects TraverseToPath(string[] tokens, bool bottom)
        {
            return Traverse(tokens, bottom, 0);
        }

        /// <summary>
        /// Alters each file's path along the traversal. If op = "inc", 
        /// then all files will increase in size. Otherwise, all files will
        /// decrease in size.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        private FileSystemObjects AlterPathSize(String[] tokens, string op, int size)
        {
            bool bottomFile = false;

            if (op == "inc")
               size = Math.Abs(size);
            else if (op == "dec")
                size = Math.Abs(size) * -1;

            return Traverse(tokens, bottomFile, size);
        }

        /// <summary>
        /// Traverses the file path and returns either the deepest or parent file.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private FileSystemObjects Traverse(string[] tokens, bool bottom, int size)
        {
            // Ensure the path exists
            if (tokens.Length == 0)
                throw new ArgumentException("Path not found");

            FileSystemObjects obj = system[tokens[0]];

            // Ensure the path exists
            if (ReferenceEquals(obj, null))
                throw new ArgumentException("Path not found");

            if (obj.Type == "folder" || obj.Type == "drive")
            {
                // Parent File
                if (bottom)
                {
                    for (int i = 1; i < tokens.Length - 1; i++)
                    {
                        string s = tokens[i];

                        if (ReferenceEquals(obj, null))
                            throw new ArgumentException("Path not found");

                        if (obj.Type == "textfile" || obj.Type == "ZipFile")
                            throw new ArgumentException("Cannot create entities for this type.");

                        obj.Size += size;
                        obj = ((Drive)obj).getFiles()[s];
                    }

                }

                // Deepest File
                else
                {
                    for (int i = 1; i < tokens.Length; i++)
                    {
                        string s = tokens[i];

                        if (ReferenceEquals(obj, null))
                            throw new ArgumentException("Path not found");

                        if (obj.Type == "textfile" || obj.Type == "ZipFile")
                            throw new ArgumentException("Cannot create entities for this type.");

                        if (i < tokens.Length - 1)
                            obj.Size += size;

                        obj = ((Drive)obj).getFiles()[s];
                    }
                }
            }

            // Ensure the path exists
            if(ReferenceEquals(obj, null))
                throw new ArgumentException("Path not found");

            return obj;
        }

        /// <summary>
        /// Returns each name along a given file path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private String[] GetPathTokens(string path)
        {
            // rejex to split on /
            return null;
        }

        /// <summary>
        /// For testing purposes.
        /// </summary>
        /// <returns></returns>
        //public Dictionary<String, FileSystemObjects> GetFileSystem()
        //{
        //    return system;
        //}

    }
}
