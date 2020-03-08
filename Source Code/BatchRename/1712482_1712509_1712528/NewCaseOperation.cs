using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
namespace _1712482_1712509_1712528
{
    public class StringProcess
    {
        static public string CapitalizedString(string name)
        {
            string result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
            return result;
        }
        static public string NormalizedString(string name)
        {
            string output = "";
            string word = "";
            string[] tokens = name.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in tokens)
            {
                word = token.Substring(0, 1).ToUpper() + token.Substring(1).ToLower();
                word += " ";
                output += word;

            }
            output = output.Substring(0, output.Length - 1);

            return output;
        }
    }
    public class NewCaseArgs : StringArgs
    {
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }

    public class NewCaseStringValue
    {
        public static string Uppercase => "Uppercase";
        public static string Lowercase => "Lowercase";
        public static string Capitalizedcase => "Capitalizedcase";
    }

    class NewCaseOperation : StringOperation, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Name + "," + Args.ToString();
        }

        public override string Process(string origin)
        {
            var args = Args as NewCaseArgs;
            var value = args.Value;

            string name = Path.GetFileNameWithoutExtension(origin);
            string extension = origin.Remove(0, name.Length);
            if (value == NewCaseStringValue.Lowercase)
            {
                name = name.ToLower();
            }
            else if (value == NewCaseStringValue.Uppercase)
            {
                name = name.ToUpper();
            }
            else
            {
                //capitalize
                name = StringProcess.CapitalizedString(name);
            }

            return name + extension;
        }

        public override StringOperation Clone()
        {
            var oldArgs = Args as NewCaseArgs;
            return new NewCaseOperation()
            {
                Args = new NewCaseArgs()
                {
                    Value = oldArgs.Value
                }
            };
        }
       
        public override void Config()
        {
            var screen = new NewCase(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }

        }

   

        public override List<string> Copy()
        {

            var NewArgs = Args as NewCaseArgs;
            var a = NewArgs.Value;
            
            var c = "NewCase";
            List<string> temp = new List<string>();
            temp.Add(c);
            temp.Add(a);


            return temp;
        }

      

        

        public override string Name => "New Case";
        public override string Description
        {
            get
            {
                var args = Args as NewCaseArgs;
                return $"Make {args.Value}";
            }
        }
    }
}
