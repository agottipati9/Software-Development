using System;
using System.Collections.Generic;
using System.Text;

namespace Proofpoint
{
    interface FileSystemObjects
    {
        /// <summary>
        /// Returns the type of the entity.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// An alphanumeric string. 
        /// Two entities with the same parent cannot have the same name. 
        /// Similarly, two drives cannot have the same name.
        /// Returns the name of the entity.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The concatenation of the names of the containing entities, 
        /// from the drive down to and including the entity.
        /// The names are separated by ‘\’.
        /// Returns the path of the entity.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// For a text file – it is the length of its content.
        /// For a drive or a folder, it is the sum of all sizes of the entities it contains.
        /// For a zip file, it is one half of the sum of all sizes of the entities it contains.
        /// Returns the size of the entity.
        /// </summary>
        int Size { get; set; }

    }
}
