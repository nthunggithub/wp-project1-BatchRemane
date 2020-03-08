using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;


namespace _1712482_1712509_1712528
{
    public class UniqueNameOperation : StringOperation,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

       
        public override string Process(string FileName)
        {

            FileName = Guid.NewGuid().ToString();

            return FileName;
        }
        public override StringOperation Clone()
        {
            var oldArgs = Args as UniqueNameArgs;
            return new UniqueNameOperation
            {
                Args = new UniqueNameArgs()
                {
                    From = oldArgs.From,
                    To = oldArgs.To
                }
            };
        }

        public override void Config()
        {
            //var screen = new NameNormalizeDialog(Args);
            //if (screen.ShowDialog() == true)
            //{

            //}
        }

        public override List<string> Copy()
        {
            List<string> temp = new List<string>();
            string a = "UniqueName";
            temp.Add(a);
            return temp;
        }

       

        public override string Name => "UniqueName";
        public override string Description
        {
            get
            {
                var args = Args as UniqueNameArgs;
                return "Unique Name";
            }
        }
    }
}

