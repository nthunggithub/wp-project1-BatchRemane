using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace _1712482_1712509_1712528
{
    public class MoveOperation : StringOperation, INotifyPropertyChanged
    {

        

        public event PropertyChangedEventHandler PropertyChanged;
        public override string Name => "Move Action";

        public override string Description
        {
            get
            {
                var args = Args as MoveArgs;
                return $"Move {args.Length} character Begin with index: {args.Start} to {args.Des}";
            }
        }

        public override StringOperation Clone()
        {
            var oldArgs = Args as MoveArgs;

            return new MoveOperation()
            {
                Args = new MoveArgs()
                {
                    Length = oldArgs.Length,
                    Start = oldArgs.Start,
                    Des = oldArgs.Des,
                }
            };
        }

        public override void Config()
        {
            var screen = new MoveControl(Args);

            if (screen.ShowDialog() == true)
            {

            }
        }

       

        public override string Process(string FileName)
        {
            var NewArgs = Args as MoveArgs;
            var begin = NewArgs.Start;
            var length = NewArgs.Length;
            var des = NewArgs.Des;
            var b = int.Parse(begin);
            var l = int.Parse(length);
            if ((b == 0 && des == "Begin") || (l == 0))
            {
                return FileName;

            }
            else if (b == 0 && des == "End")
            {
                FileName = FileName.Substring(l - 1, FileName.Length - 1) + FileName.Substring(0, l - 1);
            }
            else
            {
                if (des == "End")
                {
                    FileName = FileName.Substring(0, b - 1) + FileName.Substring(b + l - 1, FileName.Length - b - l) + FileName.Substring(b, l);
                }
                if (des == "Begin")
                {
                    var x = FileName.Substring(b, l);
                    var y = FileName.Substring(0, b);
                    var z = FileName.Substring(b + l, FileName.Length - b - l);

                    FileName = x + y + z;

                }
            }
            return FileName;
        }

        public override List<string> Copy()
        {
            var NewArgs = Args as MoveArgs;
            var a = NewArgs.Start;
            var b = NewArgs.Length;
            var c = NewArgs.Des;
            var d = "Move";
           

            List<string>temp = new List<string>();
            temp.Add(d);
            temp.Add(a);
            temp.Add(b);
            temp.Add(c);

            return temp;
        }

       
    }
}
