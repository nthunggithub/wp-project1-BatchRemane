using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace _1712482_1712509_1712528
{
  
    public class NameNormalizeOperation : StringOperation, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

       
        public override string Process(string FileName)
        {
            bool check = true;
            while (check==true)
            {
                
                if (FileName[0] != ' ' && FileName[FileName.Length - 1] != ' ')
                {
                    break;
                }
                else
                {
                    if (FileName[0] == ' ')
                    {
                        FileName.Remove(0, 1);
                        check = true;
                    }
                    if (FileName[FileName.Length - 1] == ' ')
                    {
                        FileName.Remove(FileName.Length - 1, 1);
                        check = true;
                    }
                }
            }
            FileName=char.ToUpper(FileName[0])+FileName.Substring(1, FileName.Length - 1);

            for(var i =1;i<FileName.Length;i++)
            {
              if(FileName[i]==' ' && FileName[i+1]==' ')
                {

                    // Xóa 1 khoảng trống khi có 2 khoảng trống liên tiếp giữa các từ
                    FileName.Remove(i, 1);
                }   
            }

            for (var i = 1; i < FileName.Length; i++)
            {
                if (FileName[i] == ' ' )
                {

                    // Xóa 1 khoảng trống khi có 2 khoảng trống liên tiếp giữa các từ
                    FileName = FileName.Substring(0, i + 1) + char.ToUpper(FileName[i + 1]) + FileName.Substring(i + 2, FileName.Length - i - 2);

                }
            }



            return FileName;
        }
        public override StringOperation Clone()
        {
            var oldArgs = Args as NameNormalizeArg;
            return new NameNormalizeOperation
            {
                Args = new NameNormalizeArg()
                {
                    From = oldArgs.From,
                    To = oldArgs.To
                }
            };
        }

        public override void Config()
        {
            var screen = new NameNormalizeDialog(Args);
            if (screen.ShowDialog() == true)
            {

            }
        }

        public override List<string> Copy()
        {
            List<string> temp = new List<string>();
            var a = "NameNormalize";
            temp.Add(a);
            return temp;
        }

        

        public override string Name => "Normalize";
        public override string Description
        {
            get
            {
                var args = Args as NameNormalizeArg;
                return "Normalize Name";
            }
        }
    }
}


