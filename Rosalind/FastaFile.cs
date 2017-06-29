using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.SqlServer.Server;

namespace Rosalind
{
    class FastaFile : IEnumerable
    {
        private Dictionary<string, string> _data;

        public FastaFile(Dictionary<string, string> data)
        {
            _data = data;
        }

        public string this[string index]
        {
            get { return _data[index]; }
        }

        

        public FastaFile()
        {
            _data = new Dictionary<string, string>();
        }

        public static FastaFile Parse(string filename)
        {
            var result = new Dictionary<string, string>();
            using (var fs = new FileStream(filename, FileMode.Open))
            {
                var reader = new StreamReader(fs);
                var currentId = "";
                while (true)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) break;
                    if (line[0] == '>')
                    {
                        currentId = string.Concat(line.Skip(1));
                        result.Add(currentId, "");
                    }
                    else if (currentId != "")
                    {
                        result[currentId] += line;
                    }
                }
            }
            return new FastaFile(result);
        }

        public Dictionary<string,string>.Enumerator GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}