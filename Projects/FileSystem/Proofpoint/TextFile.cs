using System;
using System.Collections.Generic;
using System.Text;

namespace Proofpoint
{
    class TextFile : FileSystemObjects
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
        /// Holds the content.
        /// </summary>
        private string content;

        /// <summary>
        /// Holds the size.
        /// </summary>
        private int size;

        /// <summary>
        /// Constructs a new text file.
        /// 
        /// n - file name
        /// p - file path
        /// c - file content
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="p"></param>
        /// <param name="c"></param>
        public TextFile(string n, string p, string c)
        {
            type = "textfile";
            name = n;
            path = p;
            content = c;
            size = c.Length;
        }

        public string Type => type;

        public string Name => name;

        public string Path { get => path; set => path = value; }

        public int Size{ get => size; set => size = value; }

        /// <summary>
        /// Writes the given content to the text file.
        /// </summary>
        /// <param name="s"></param>
        public void Write(string s)
        {
            content = s;
            size = s.Length;
        }
    }
}

