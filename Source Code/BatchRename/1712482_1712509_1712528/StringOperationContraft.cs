using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;



namespace _1712482_1712509_1712528
{
    public class StringArgs
    {

    }
    public class Key
    {

    }
    public class MoveKeyArgs:Key,INotifyPropertyChanged
    {
        public string NameAction { get; set; }
        public string Start1 { get; set; }
        public string Length1 { get; set; }
        public string Des1 { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class ReplaceArgs : StringArgs, INotifyPropertyChanged
    {
        public string From { get; set; }
        public string To { get; set; }

        public string Area { get; set; }

        public override string ToString()
        {
            return From + "," + To + "," + Area;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class UltraReplaceArgs : ReplaceArgs
    {
        public int Index { get; set; } // Chi xoa phan tu xuat hien thu i
    }
    public class MoveArgs:StringArgs
    {
        public string Length { get; set; }
        public string Start { get; set; }
        public string Des { get; set; }

       
    }
    public class NameNormalizeArg : StringArgs, INotifyPropertyChanged
    {
        public string From { get; set; }
        public string To { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class UniqueNameArgs:StringArgs,INotifyPropertyChanged
    {
        public string From { get; set; }
        public string To { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public abstract class StringOperation : INotifyPropertyChanged
    {
        public StringArgs Args { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

       

        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract StringOperation Clone();

        public abstract void Config();

        public abstract string Process(string FileName);

        public Key KeyArgs { get; set; }

        public  abstract List<string> Copy();

        


    }

}
