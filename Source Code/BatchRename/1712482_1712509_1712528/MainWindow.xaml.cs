using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace _1712482_1712509_1712528
{

    public partial class MainWindow : Window
    {
        public class Action
        {
            public string Name { set; get; }
        }
        public MainWindow()
        {

            InitializeComponent();


            filenameList = new BindingList<Filename>();
            foldernameList = new BindingList<Foldername>();
            FileListView.ItemsSource = filenameList;
            FolderListView.ItemsSource = foldernameList;


        }
        List<StringOperation> _prototypes = new List<StringOperation>();

        BindingList<StringOperation> _actions = new BindingList<StringOperation>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Nạp CSDL để biết những khả năng đổi tên mình có thể

            var prototype1 = new ReplaceOperation()
            {
                Args = new ReplaceArgs()
                {
                    From = "From",
                    To = "To"
                }
            };

            var prototype2 = new NewCaseOperation()
            {
                Args = new NewCaseArgs()
                {
                    Value = NewCaseStringValue.Lowercase
                }
            };
            var prototype3 = new NameNormalizeOperation()
            {
                Args = new NameNormalizeArg()
                {
                    From = "From",
                    To = "To"
                }
            };
            var prototype4 = new MoveOperation()
            {
              Args=new MoveArgs()
              {
                  Length=" ",
                  Des=" ",
                  Start=" ",
              }
            };
            var prototype5 = new UniqueNameOperation()
            {
                Args = new UniqueNameArgs()
                {
                    From="",
                    To="",
                }
            };

            _prototypes.Add(prototype1);
            _prototypes.Add(prototype2);
            _prototypes.Add(prototype3);
            _prototypes.Add(prototype4);
            _prototypes.Add(prototype5);
            ActionsListView.ItemsSource = _prototypes;
            operationsListBox.ItemsSource = _actions;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var item = operationsListBox.SelectedItem as StringOperation;

            item.Config();
        }

        BindingList<Filename> filenameList;
        private void AddFileButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();

            // show dialog
            System.Windows.Forms.DialogResult result = folderDlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // get all filenames in path
                string path = folderDlg.SelectedPath + "\\";
                string[] filenames = Directory.GetFiles(path);

                // add all to filenameList
                foreach (var filename in filenames)
                {
                    string newFilename = filename.Remove(0, path.Length);
                    filenameList.Add(new Filename() { Value = newFilename, Path = path });
                }
            }
        }

        private void FileListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            var x = sender as CheckBox;

            //ReplaceArgs Args = new ReplaceArgs()
            //{
            //    From = "",
            //    To = ""
            //};
          

            //var checkcb = true;
            //var actionUI = ActionsListView.SelectedItem as StringOperation;

            //actionUI.Config();

            var action = ActionsListView.SelectedItem as StringOperation;
            action.Config();
            _actions.Add(action.Clone());
            


            //var screen = new Replace(Args);
            //if (screen.ShowDialog() == true)
            //{
            //}
            //x.IsChecked = false;
        }

        private void Replace_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartBatchButton_click(object sender, RoutedEventArgs e)
        {
            if (e1.controlTab == "file")
            {
                foreach (var filename in filenameList)
                {
                    bool x = true;
                    string NewName = filename.Value;
                    filename.BatchState = "Success";
                    filename.FailedActions = "Failed";
                    bool State = true;
                    foreach (var temp in _actions)
                    {
                        try
                        {
                            NewName = temp.Process(NewName);
                        }
                        catch
                        {
                            State = false;
                        }


                    }
                    if (State == false)
                    {
                        filename.BatchState = "Failed";
                        x = false;
                    }
                    else
                    {
                        filename.BatchState = "Success";
                    }
                    if (x == true)
                    {
                        filename.NewFilename = NewName;
                        var NewNameBackup = NewName;
                        string oldFilename = filename.Path + filename.Value;
                        NewName = filename.Path + filename.NewFilename;
                        // Nếu file đã tồn tại thì đổi tên file thêm đuôi -1 vào cuối file
                        if (NewName == oldFilename)
                        {
                            filename.NewFilename = filename.Value;
                        }
                        else
                        {
                            if (File.Exists(NewName))
                            {
                                NewNameBackup = "(1)" + NewNameBackup;
                                filename.NewFilename = NewNameBackup;
                                NewName = filename.Path + NewNameBackup;
                            }
                            if (NewName != oldFilename)
                            {
                                try
                                {
                                    System.IO.File.Move(oldFilename, NewName);
                                    System.IO.File.Delete(oldFilename);
                                }
                                catch
                                {
                                    filename.BatchState = "Failed";
                                    filename.NewFilename = filename.Value;

                                }
                            }
                        }
                        
                    }
                    
                }
            }
            if(e1.controlTab=="folder")
            {
                
                foreach (var foldername in foldernameList)
                {
                    bool x = true;
                    string NewName = foldername.Value;
                    foldername.BatchState = "Success";
                    foldername.FailedActions = "Failed";
                    bool State = true;
                    foreach (var temp in _actions)
                    {
                        try
                        {
                            NewName = temp.Process(NewName);
                        }
                        catch
                        {
                            State = false;
                        }


                    }
                    if (State == false)
                    {
                        foldername.BatchState = "Failed";
                        x = false;
                    }
                    else
                    {
                        foldername.BatchState = "Success";
                    }

                    if (x == true)
                    {
                        foldername.NewFoldername = NewName;
                        var NewNameBackup = NewName;
                        string oldFilename = foldername.Path + foldername.Value;
                        NewName = foldername.Path + foldername.NewFoldername;
                        if (NewName == oldFilename)
                        {
                            foldername.NewFoldername = foldername.Value;
                        }
                        else
                        {
                            if(File.Exists(NewName))
                            {
                                NewNameBackup = "(1)" + NewNameBackup;
                                foldername.NewFoldername = NewNameBackup;
                                NewName = foldername.Path + NewNameBackup;        
                            }
                            if (NewName != oldFilename)
                            {
                                try
                                {
                                    System.IO.Directory.Move(oldFilename, NewName);
                                    System.IO.File.Delete(oldFilename);
                                }
                                catch
                                {
                                    foldername.NewFoldername = foldername.Value;
                                }
                            }
                        }
                    }
                   else 
                    {
                        foldername.NewFoldername = foldername.Value;
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (e1.controlTab == "file")
            {
                if (filenameList.Count > 0)
                {
                    filenameList.Clear();
                }
            }
            if(e1.controlTab=="folder")
            {
                if (foldernameList.Count > 0)
                {
                    foldernameList.Clear();
                }
            }
        }
        BindingList<Foldername> foldernameList;
        private void AddFolderButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();

            // show dialog
            System.Windows.Forms.DialogResult result = folderDlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // get all foldernames
                string path = folderDlg.SelectedPath + "\\";
                string[] foldernames = Directory.GetDirectories(path);

                // add all to foldername list
                foreach (var foldername in foldernames)
                {
                    string newFoldername = foldername.Remove(0, path.Length);
                    var a = new Foldername();
                    a.Value = newFoldername;
                    a.Path = path;
                    foldernameList.Add(a);
                }
            }

        }
        

        public class Event1
        {
            public string controlTab { get; set; }
        }
        Event1 e1 = new Event1();
        private void TabSelectionChanged_Event(object sender, SelectionChangedEventArgs e)
        {
            
            if (Tab1.IsSelected)
            {
                e1.controlTab = "file";
            }            
            if(Tab2.IsSelected)
            {
                e1.controlTab = "folder";
            }
        }

        private void LenNgang_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var n = 0;
                if (e1.controlTab == "file")
                {
                    var a = FileListView.SelectedItem as Filename;
                    foreach (Filename b in filenameList)
                    {
                        if (b.Value == a.Value)
                        {
                            n = filenameList.IndexOf(b);
                        }
                    }
                    var temp = filenameList[0];
                    filenameList.RemoveAt(n);
                    filenameList.Insert(0, a);

                }
                else if(e1.controlTab=="folder")
                {
                    var b = FolderListView.SelectedItem as Foldername;
                    foreach(Foldername c in foldernameList)
                    {
                        if(c.Value==b.Value)
                        {
                            n = foldernameList.IndexOf(c);
                        }
                    }
                    var temp = foldernameList[0];
                    foldernameList.RemoveAt(n);
                    foldernameList.Insert(0, b);
                }
            }
            catch
            {
               
            }
        }

        private void Len_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if(e1.controlTab=="file")
                {
                    var n = 0;
                    var a = FileListView.SelectedItem as Filename;
                    foreach(Filename c in filenameList)
                    {
                         if(c.Value==a.Value)
                        {
                            n = filenameList.IndexOf(c);      
                        }
                    }
                    if (n > 0)
                    {
                        filenameList.RemoveAt(n);
                        filenameList.Insert(n - 1, a);
                    }
                }
                if (e1.controlTab == "folder")
                {
                    var n = 0;
                    var b = FolderListView.SelectedItem as Foldername;
                    foreach (Foldername c in foldernameList)
                    {
                        if (c.Value == b.Value)
                        {
                            n = foldernameList.IndexOf(c);
                        }
                    }
                    if (n > 0)
                    {
                        foldernameList.RemoveAt(n);
                        foldernameList.Insert(n - 1, b);
                    }
                }
            }
            catch
            {

            }
        }

        private void Xuong_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (e1.controlTab == "file")
                {
                    var n = 0;
                    var a = FileListView.SelectedItem as Filename;
                    foreach (Filename c in filenameList)
                    {
                        if (c.Value == a.Value)
                        {
                            n = filenameList.IndexOf(c);
                        }
                    }
                    if (n < filenameList.Count-1)
                    {
                        filenameList.RemoveAt(n);
                        filenameList.Insert(n + 1, a);
                    }
                    
                }
                if (e1.controlTab == "folder")
                {
                    var n = 0;
                    var b = FolderListView.SelectedItem as Foldername;
                    foreach (Foldername c in foldernameList)
                    {
                        if (c.Value == b.Value)
                        {
                            n = foldernameList.IndexOf(c);
                        }
                    }
                    if (n<foldernameList.Count-1)
                    {
                        foldernameList.RemoveAt(n);
                        foldernameList.Insert(n + 1, b);
                    }
                }
            }
            catch
            {

            }
        }

        private void XuongNgang_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (e1.controlTab == "file")
                {
                    var n = 0;
                    var a = FileListView.SelectedItem as Filename;
                    foreach (Filename c in filenameList)
                    {
                        if (c.Value == a.Value)
                        {
                            n = filenameList.IndexOf(c);
                        }
                    }
                   
                        filenameList.RemoveAt(n);
                        filenameList.Insert(filenameList.Count, a);
                    

                }
                if (e1.controlTab == "folder")
                {
                    var n = 0;
                    var b = FolderListView.SelectedItem as Foldername;
                    foreach (Foldername c in foldernameList)
                    {
                        if (c.Value == b.Value)
                        {
                            n = foldernameList.IndexOf(c);
                        }
                    }
                   
                        foldernameList.RemoveAt(n);
                        foldernameList.Insert(foldernameList.Count, b);
                   
                }
            }
            catch
            {

            }
        }

        private void SaveList_ButtonClick(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog()
            {
                Title = "Save text Files",
                CheckPathExists = true,
                DefaultExt = "txt",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,
            };
            string filename = "";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = saveFileDialog.FileName;
            }
            InputOutput.WriteToFile(filename, new List<string>() { "" });


            foreach (var action in _actions)
            {
                StringBuilder builder = new StringBuilder();
                List<string> keywords = action.Copy();

                for (int i = 0; i < keywords.Count; i++)
                {
                    builder.Append(keywords[i]);
                    if (i < keywords.Count - 1)
                    {
                        builder.Append(",");
                    }
                }
                string line = builder.ToString();
                InputOutput.AppendToFile(filename, new List<string>() { line });
            }


        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            _actions.Clear();
            // open file dialog
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog()
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // get filename
                string filename = openFileDialog1.FileName;

                // read all lines in filename
                List<string> lines = new List<string>();
                InputOutput.ReadFromFile(filename, ref lines);

                // each line, parse arguments to corresponding action and create new action
                foreach (var line in lines)
                {
                    try
                    {
                        List<string> tokens = new List<string>();
                        if (InputOutput.SplitString(line, ref tokens) == 0)
                        {
                            string actionName = tokens[0];
                            tokens.Remove(tokens[0]);
                            List<string> actionArguments = tokens;
                            if (actionName == "Move")
                            {
                                var action3 = new MoveOperation()
                                {
                                    Args = new MoveArgs()
                                    {
                                        Start = tokens[0],

                                        Length = tokens[1],
                                        Des = tokens[2],
                                    }
                                };
                                var action4 = action3 as StringOperation;

                                _actions.Add(action4.Clone());

                            }
                            if (actionName == "NameNormalize")
                            {
                                var action3 = new NameNormalizeOperation()
                                {
                                    Args = new NameNormalizeArg()
                                    {
                                        From = "",
                                        To = "",
                                    }
                                };
                                var action4 = action3 as StringOperation;

                                _actions.Add(action4.Clone());
                            }
                            if (actionName == "UniqueName")
                            {
                                var action3 = new UniqueNameOperation()
                                {
                                    Args = new UniqueNameArgs()
                                    {
                                        From = "",
                                        To = "",
                                    }
                                };
                                var action4 = action3 as StringOperation;

                                _actions.Add(action4.Clone());
                            }
                            if(actionName=="Replace")
                            {
                                var Action3 = new ReplaceOperation()
                                {
                                    Args = new ReplaceArgs()
                                    {
                                        From = tokens[0],
                                        To = tokens[1],
                                        Area = tokens[2],
                                        
                                    }
                                };
                                var action4 = Action3 as StringOperation;
                                _actions.Add(action4.Clone());
                                    
                            }
                            if(actionName=="NewCase")
                            {
                                var Action3 = new NewCaseOperation()
                                {
                                    Args = new NewCaseArgs()
                                    {
                                        Value = tokens[0],
                                    }
                                };
                                var action4 = Action3 as StringOperation;
                                _actions.Add(action4.Clone());
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private void Preview_Click(object sender, RoutedEventArgs e)
        {
            if (e1.controlTab == "file")
            {
                foreach (var filename in filenameList)
                {

                    string NewName = filename.Value;
                    foreach (var temp in _actions)
                    {
                        try
                        {
                            NewName = temp.Process(NewName);
                        }
                        catch
                        {

                        }
                    }
                    filename.NewFilename = NewName;
                }
            }
            else if (e1.controlTab == "folder")
            {

                    foreach (var foldername in foldernameList)
                    {
                        
                        string NewName = foldername.Value;
                        
                        
                        foreach (var temp in _actions)
                        {
                            try
                            {
                                NewName = temp.Process(NewName);
                            }
                            catch
                            {

                            }
                        }
                        foldername.NewFoldername = NewName;
                    }
                }
            }

       

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog()
            {
                Title = "Save text Files",
                CheckPathExists = true,
                DefaultExt = "txt",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,
            };
            string filename = "";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = saveFileDialog.FileName;
            }
            InputOutput.WriteToFile(filename, new List<string>() { "" });


            foreach (var action in _actions)
            {
                StringBuilder builder = new StringBuilder();
                List<string> keywords = action.Copy();

                for (int i = 0; i < keywords.Count; i++)
                {
                    builder.Append(keywords[i]);
                    if (i < keywords.Count - 1)
                    {
                        builder.Append(",");
                    }
                }
                string line = builder.ToString();
                InputOutput.AppendToFile(filename, new List<string>() { line });
            }

        }

        private void ClearButton_click(object sender, RoutedEventArgs e)
        {
            var item = operationsListBox.SelectedItem as StringOperation;
            var index = 0;
            foreach(var a in _actions)
            {
                if(item.Name==a.Name)
                {
                    index = _actions.IndexOf(a);
                }
            }
            _actions.RemoveAt(index);
        }

        private void OperationsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Đồ án được thực hiện bởi 1712482-Nguyễn Tấn Hưng(Hưng thơm tho), 1712509-Đặng Hồ Hoàng Kha(Kha đẹp trai), 1712528-Ngô Trường Khiêm(Khiêm nọng)");
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 50; i++)
            {
                System.Threading.Thread.Sleep(100);
                MyTextBlock.Text = i.ToString();
                System.Windows.Forms.Application.DoEvents();
            }
        }
    }
    
}



