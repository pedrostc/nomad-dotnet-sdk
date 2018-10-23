using System;
using System.Collections.Generic;
using System.Text;

namespace Nomad.DotNet.Model
{
    public class FileContent
    {
        public string Data { get; set; }
        public string File { get; set; }
        public int Offset { get; set; }

        public string GetDecodedData()
        {
            byte[] data = Convert.FromBase64String(Data);
            string decodedString = Encoding.UTF8.GetString(data);

            return decodedString;
        }
    }
}
