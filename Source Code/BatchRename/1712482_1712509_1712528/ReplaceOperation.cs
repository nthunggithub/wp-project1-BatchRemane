using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace _1712482_1712509_1712528
{
    public class ReplaceOperation : StringOperation, INotifyPropertyChanged
    {
        public static string NameArea => "Name";

        public static string ExtensionArea => "Extension";
        public override string ToString()
        {
            return Name + "," + Args.ToString();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public static string NamePos => "Name";

        public static string ExtensionPos => "Extension";
        

        public override StringOperation Clone()
        {
            var oldArgs = Args as ReplaceArgs;
            return new ReplaceOperation()
            {
                Args = new ReplaceArgs()
                {
                    From = oldArgs.From,
                    To = oldArgs.To,
                    Area = oldArgs.Area
                }
            };
        }

       

        public override void Config()
        {
            var screen = new Replace(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }

        }

        public override string Process(string origin)
        {
            var args = Args as ReplaceArgs;
            var from = args.From;
            var to = args.To;
            var area = args.Area;
            string name ="";
            string extension = "";
            try
            {
                 name = Path.GetFileNameWithoutExtension(origin);
                 extension = origin.Remove(0, name.Length + 1);
                extension = "." + extension;
            }
            catch
            {
                name = origin;
            }

            if (area == ReplaceOperation.NameArea)
                name = name.Replace(from, to);
            else
            {
                try
                {
                    extension = extension.Replace(from, to);
                   
                }
                catch{

                }
               
            }
            string filename = name + extension;
            return filename;
        }

        public override List<string> Copy()
        {
            List<string> temp = new List<String>();
            var NewArgs = Args as ReplaceArgs;
            var a = NewArgs.From;
            var b = NewArgs.To;
            var c = NewArgs.Area;
            var d = "Replace";
            temp.Add(d);
            temp.Add(a);
            temp.Add(b);
            temp.Add(c);
            return temp;
        }

     

        public override string Name => "Replace";
        public override string Description
        {
            get
            {
                var args = Args as ReplaceArgs;
                return $"Replace from {args.From} to {args.To} area {args.Area}";
            }
        }
    }
}
