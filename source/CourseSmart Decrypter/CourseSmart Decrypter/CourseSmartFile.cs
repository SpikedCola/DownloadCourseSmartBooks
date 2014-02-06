using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CourseSmart_Decrypter
{
    class CourseSmartFile : IDisposable
    {
        /// <summary>
        /// Name of the file we are parsing
        /// </summary>
        string Filename = null;

        /// <summary>
        /// First byte of the file specifies the operation:
        /// 1 - no restrictions, load file (UNTESTED)
        /// 2 - restricted to preview size, first byte followed by 3 unsigned shorts specifying width, height, and offset (UNTESTED)
        /// 3 - load page information from data in key value pairs, load file
        /// 4 - retry request from server (CURRENTLY UNSUPPORTED)
        /// 5 - load page information from data in key value pairs, trigger error handler (CURRENTLY UNSUPPORTED)
        /// </summary>
        byte OPERATION = 0;

        /// <summary>
        /// Special restriction information, in the case of OPERATION 2
        /// </summary>
        uint WidthRestriction = 0;
        uint HeightRestriction = 0;
        uint Offset = 0;

        /// <summary>
        /// Dictionary to hold key/value pairs loaded from files, in the case of OPERATION 3
        /// </summary>
        Dictionary<string, string> Data = new Dictionary<string, string>();

        /// <summary>
        /// Byte array to hold SWF data
        /// </summary>
        public byte[] MovieData = null;

        /// <summary>
        /// Underlying binary reader to read the file
        /// </summary>
        CourseSmartReader Reader = null;

        /// <summary>
        /// Constructor creates the underlying reader and handles loading the file
        /// </summary>
        /// <param name="file"></param>
        public CourseSmartFile(string file)
        {
            this.Filename = file;
            this.Reader = new CourseSmartReader(file);
            this.Load();
        }

        /// <summary>
        /// Clean up reader & any data kicking around
        /// </summary>
        public void Dispose()
        {
            this.Data.Clear();
            this.Data = null;
            if (this.MovieData != null)
            {
                this.MovieData = null;
            }
            if (this.Reader != null)
            {
                this.Reader.Close();
                this.Reader.Dispose();
                this.Reader = null;
            }
        }

        /// <summary>
        /// Attempts to return a helpful filename
        /// </summary>
        /// <returns></returns>
        public string GetFilename()
        {
            // if we have the page name in our data dict, use that
            if (this.Data.ContainsKey("debugPage"))
            {
                return this.Data["debugPage"];
            }

            // if we have nothing else, return the original file name
            return Path.GetFileNameWithoutExtension(this.Filename);
        }

        /// <summary>
        /// Loads the OPERATION from the file, and any specific information
        /// </summary>
        private void Load()
        {
            OPERATION = this.Reader.ReadByte();
            switch (OPERATION)
            {
                case 2:
                    this.WidthRestriction = this.Reader.ReadUInt16();
                    this.HeightRestriction = this.Reader.ReadUInt16();
                    this.Offset = this.Reader.ReadUInt16();
                    break;
                case 3:
                    this.LoadKeyValuePairs();
                    break;
            }

            this.LoadMovie();
        }

        /// <summary>
        /// Loads key/value pairs from the file. An unsigned short
        /// specifies how many pairs should be expected
        /// </summary>
        private void LoadKeyValuePairs()
        {
            uint numberOfPairs = this.Reader.ReadUInt16();
            for (int i = 0; i < numberOfPairs; i++)
            {
                string key = this.Reader.ReadUTF8String();
                string value = this.Reader.ReadUTF8String();
                this.Data.Add(key, value);
            }
        }

        /// <summary>
        /// Loads movie data from the remainder of the file. Will attempt to decrypt
        /// non-SWF-looking data
        /// </summary>
        private void LoadMovie()
        {
            // size of movie is total length minus current position
            int movieSize = (int)this.Reader.BaseStream.Length - (int)this.Reader.BaseStream.Position;

            // create a buffer for the movie data
            byte[] buffer = new byte[movieSize];

            // copy data from current position to end of file into buffer
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                this.Reader.BaseStream.CopyTo(ms);
            }

            // grab first 3 bytes of data to check for magic string
            byte[] header = new byte[3];
            Array.Copy(buffer, header, 3);
            string magicString = Encoding.ASCII.GetString(header);

            if (magicString != "CWS")
            {
                // didnt find standard header - probably encrypted
                this.DecryptMovieData(ref buffer);

                // double check magic string after decrypting
                Array.Copy(buffer, header, 3);
                magicString = Encoding.ASCII.GetString(header);
                if (magicString != "CWS")
                {
                    throw new Exception("Didnt find CWS magic string after decrypting movie data");
                }
            }

            this.MovieData = buffer;
        }

        /// <summary>
        /// Decryption routine - extracted from offline viewer code
        /// </summary>
        /// <param name="data">Byte List containing encrypted movie data</param>
        private void DecryptMovieData(ref byte[] data)
        {
            // load encrypted data into cs reader
            using (MemoryStream dataStream = new MemoryStream(data))
            using (CourseSmartReader dataReader = new CourseSmartReader(dataStream))
            {
                byte XORKeyLength = dataReader.ReadByte();
                byte SPECIAL = dataReader.ReadByte(); // not 100% on what this value is
                byte swapTableLength = dataReader.ReadByte();

                // read XOR key & swap table
                byte[] XORKey = dataReader.ReadBytes(XORKeyLength);
                byte[] swapTable = dataReader.ReadBytes(swapTableLength);

                // calculate how much movie data remains after reading xor key and swap table
                int movieDataLength = data.Length - (int)dataReader.BaseStream.Position;

                // not sure yet
                int smallerBytesToSwap = movieDataLength / SPECIAL; // smaller
                int largerBytesToSwap = movieDataLength - (smallerBytesToSwap * (SPECIAL - 1)); // larger

                // stage 1 - read remaining data from file and decrypt using XOR key
                List<byte> decryptedDataByteList = new List<byte>();
                for (int i = 0; i < movieDataLength; i++)
                {
                    decryptedDataByteList.Add((byte)(dataReader.ReadByte() ^ XORKey[i % XORKey.Length]));
                }
                byte[] decryptedData = decryptedDataByteList.ToArray();

                // stage 2 - load decrypted data into cs reader & perform byte-swapping to finish decrypting data
                using (MemoryStream decryptedDataStream = new MemoryStream(decryptedData))
                using (CourseSmartReader decryptedDataReader = new CourseSmartReader(decryptedDataStream))
                {
                    for (int i = swapTableLength - 1; i >= 0; i--)
                    {
                        byte swapValue = swapTable[i];
                        // swap is only performed if value from SwapTable is different from current position in table
                        if (swapValue != i)
                        {
                            // not exactly sure what this is about
                            int bytesToSwap1 = (swapValue == (SPECIAL - 1)) ? largerBytesToSwap : smallerBytesToSwap;
                            int bytesToSwap2 = (i == (SPECIAL - 1)) ? largerBytesToSwap : smallerBytesToSwap;
                            int bytesToSwap = Math.Min(bytesToSwap1, bytesToSwap2);

                            int secondSwapPosition = swapValue * smallerBytesToSwap; 
                            int firstSwapPosition = i * smallerBytesToSwap; 

                            // read two sets of bytes to swap
                            decryptedDataReader.BaseStream.Position = firstSwapPosition;
                            byte[] firstSwapBuffer = decryptedDataReader.ReadBytes(bytesToSwap);

                            decryptedDataReader.BaseStream.Position = secondSwapPosition;
                            byte[] secondSwapBuffer = decryptedDataReader.ReadBytes(bytesToSwap);

                            // write bytes back to decrypted data array
                            Buffer.BlockCopy(secondSwapBuffer, 0, decryptedData, firstSwapPosition, bytesToSwap);
                            Buffer.BlockCopy(firstSwapBuffer, 0, decryptedData, secondSwapPosition, bytesToSwap);
                        }
                    }
                }

                // overwrite reference to data with our freshly decrypted movie data
                data = decryptedData;
            }
        }

        #region Static Functions

        /// <summary>
        /// (Crudely) determines if file is a CourseSmart file
        /// </summary>
        /// <param name="file">Path to file to check</param>
        /// <returns>True if successful; null otherwise</returns>
        public static bool IsCourseSmartFile(string file)
        {
            CourseSmartReader reader = new CourseSmartReader(file);

            byte firstByte = reader.ReadByte();
            if (firstByte == 0 || firstByte > 5)
            {
                // first byte in file must be 1-5
                return false;
            }

            switch (firstByte)
            {
                case 1:
                    // no restrictions, no key/value pairs, just file data
                    break;
                case 2:
                    // check for width/height/offset 
                    uint width = reader.ReadUInt16();
                    uint height = reader.ReadUInt16();
                    uint offset = reader.ReadUInt16();

                    if (width == 0 || height == 0 || offset == 0)
                    {
                        // should have some value here
                        return false;
                    }
                    break;
                case 3:
                    uint numberOfKeyValuePairs = reader.ReadUInt16();
                    if (numberOfKeyValuePairs == 0 || numberOfKeyValuePairs > 20)
                    {
                        // sanity check on how many key value pairs we will read
                        // should be more than 0 as we need a book name & page #,
                        // but there shouldnt be more than 10ish (most I've seen 
                        // is 8, but lets call it 20)
                        return false;
                    }
                    break;
                case 4:
                case 5:
                    // both of these are error cases, no sense loading them as 
                    // there shouldnt be anything here
                    return false;
            }

            return true;
        }

        #endregion
    }
}
