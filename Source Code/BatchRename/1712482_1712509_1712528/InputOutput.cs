using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1712482_1712509_1712528
{
    class InputOutput
    {
        static public int WriteToFile(string filename, List<string> lines)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {

                foreach (string line in lines)
                {
                    sw.WriteLine(line);
                }

            }

            return 0;
        }

        static public int AppendToFile(string filename, List<string> lines)
        {
            using (StreamWriter sw = new StreamWriter(filename, append: true))
            {

                foreach (string line in lines)
                {
                    sw.WriteLine(line);
                }

            }

            return 0;
        }
        static public int ReadFromFile(string filename, ref List<string> lines)
        {

            using (StreamReader sr = new StreamReader(filename))
            {
                string line;

                // Read and display lines from the file until 
                // the end of the file is reached. 
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return 0;
        }
        static public int SplitString(string line, ref List<string> output)
        {
            List<int> index = new List<int>();

            try
            {
                for (int i = 0; i < line.Length; i++)
                {

                    if (line[i] == ',')
                    {
                        index.Add(i);
                    }

                }

                for (int i = 0; i <= index.Count; i++)
                {
                    if(index.Count==0)
                    {
                        output.Add(line);
                        break;
                    }
                    if(i==0)
                    {
                        output.Add(line.Substring(0, index[i]));
                    }
                    else if (i < index.Count )
                    {
                        output.Add(line.Substring(index[i-1]+1, index[i] - index[i-1]-1));
                    }
                    else
                    {
                        output.Add(line.Substring(index[i-1]+1, line.Length - index[i-1]-1));
                    }

                }
            }
            catch
            {
                return 1;
            }
            if(line.Length==0)
            {
                return 1;
            }
            return 0;
        }

    }
}
