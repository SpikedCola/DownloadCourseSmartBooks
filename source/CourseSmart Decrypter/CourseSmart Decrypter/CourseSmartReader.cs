using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CourseSmart_Decrypter
{
    /// <summary>
    /// Helpful BinaryReader wrapper
    /// </summary>
    class CourseSmartReader : BinaryReader
    {
        public CourseSmartReader(string file) : base(new StreamReader(file).BaseStream) { }
        public CourseSmartReader(Stream stream) : base(stream) { }

        /// <summary>
        /// Reads a UTF-8 encoded string. Assumes string is prefixed 
        /// by an unsigned short specifying string length
        /// </summary>
        /// <returns></returns>
        public string ReadUTF8String()
        {
            uint stringLength = this.ReadUInt16();
            char[] chars = this.ReadChars((int)stringLength);
            return new string(chars);
        }
    }
}
