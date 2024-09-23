using l_winapi.Module;
using Shell.IconExtractor;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Threading;
using WIndShellExperienceHost.Module;

namespace WIndShellExperienceHost.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для ListApplications.xaml
    /// </summary>
    public partial class ListApplications : System.Windows.Controls.UserControl
    {
        #region ContextMenu
        private ContextMenu ContextMenuItem;
        #endregion
        private FilePanel SelectFilePanel;
        private Task TaskBackShell;
        public bool status_cl = false;


        #region
        public Action EventStartionApplication;
        #endregion

        public ListApplications()
        {
            InitializeComponent();
            this.Loaded += ListApplications_Loaded;

        }



        private void ListApplications_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ContextMenuItem = (ContextMenu)this.FindResource("_cm_select");

            foreach (MenuItem item in ContextMenuItem.Items)
            {
                item.PreviewMouseLeftButtonDown += Event_ContextMenuItemSelect;
            }
        }
        private void Event_ContextMenuItemSelect(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Context_MenuItem mi = (Context_MenuItem)sender;
            if (SelectFilePanel == null)
                return;
            switch (mi.Tag.ToString().ToLower())
            {
                case "rm":

                    Debug.WriteLine(SelectFilePanel.SystemPath);


                    G_.AllOptions.List_Applications.Remove(SelectFilePanel.SystemPath);


                    for (int i = 0; i < _wrappanel.Children.Count; i++)
                    {
                        var panel_ = (FilePanel)(_wrappanel.Children[i]);
                        if (panel_.SystemPath == SelectFilePanel.SystemPath)
                        {
                            panel_.AnimationRemove(() =>
                            {
                                _wrappanel.Children.RemoveAt(i);

                            });

                            break;
                        }
                    }


                    //this.ShellIconExtractorTask();
                    //this.ShellIconExtractorTask();
                    //await this.SaveData();
                    break;
                case "sh":

                    Util.StartExplorerSelect(SelectFilePanel.SystemPath);
                    break;
                case "ra":

                    Util.local_ProcessStart(SelectFilePanel.SystemPath, isadmin: true);
                    break;
                case "cp":

                    // SelectFilePanel.SystemPath
                    break;
                default:
                    break;
            }

        }



        public void add_new_item_FilePanel(
          Shell.IconExtractor.Strucrure.IcoExtractorOptions stru,
          l_winapi.Module.AppOptions.Application application,
          string SysPathImage

          )
        {
            if (App.Current.Dispatcher.CheckAccess())
            {
                ___add_new_item_FilePanel(stru, application, SysPathImage);
            }
            else
            {
                App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    ___add_new_item_FilePanel(stru, application, SysPathImage);
                }));
            }
        }

        private FilePanel ___add_new_item_FilePanel(
       Shell.IconExtractor.Strucrure.IcoExtractorOptions stru,
       l_winapi.Module.AppOptions.Application application,
       string SysPathImage

       )
        {

            FilePanel c_ = new View.FilePanel();
            c_.SetData(application.SysName, application.SysPath);
            c_.SetImage(SysPathImage);
            c_.Type = stru.type;
            c_.PreviewMouseLeftButtonDown += AppStrtup_MouseLeftButtonDown;
            c_.PreviewMouseRightButtonDown += Event_SelectItem;
            _wrappanel.Children.Add(c_);
            return c_;
        }

        private void Event_SelectItem(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SelectFilePanel = (FilePanel)sender;

            if (ContextMenuItem == null)
                return;

            ContextMenuItem.ShowContextMenu(this);
        }

        public void ShellIconExtractorTask()
        {
            this.Clear();
            if (TaskBackShell != null)
            {

                if ((int)TaskBackShell.Status == 3)
                {
                    status_cl = true;
                    TaskBackShell.Dispose();

                }
            }


            TaskBackShell = new Task(() =>
            {

                var stru = new Shell.IconExtractor.Strucrure.IcoExtractorOptions()
                {
                    iconSize = Shell.IconExtractor.Enumes.IconSize.ExtraLarge,
                    path = "",
                    state = Shell.IconExtractor.Enumes.ItemState.Undefined,
                    type = Shell.IconExtractor.Enumes.ItemType.File,
                };

                foreach (var item in G_.AllOptions.List_Applications.apps)
                {
                    if (status_cl)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            this.Clear();
                        });

                        status_cl = false;
                        break;
                    }

                    Thread.Sleep(5);
                    if (Directory.Exists(item.SysPath))
                    {
                        stru.type = Shell.IconExtractor.Enumes.ItemType.Folder;
                    }
                    else
                    {
                        stru.type = Shell.IconExtractor.Enumes.ItemType.File;
                    }
                    stru.path = item.SysPath;


                    string SysPathImage = Path.GetFullPath(Path.Combine("Data", $"{item.SysName}.png"));
                    if (File.Exists(SysPathImage))
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            this.add_new_item_FilePanel(stru, item, SysPathImage);
                        });


                        continue;
                    }


                    using (IcoExtractor extr = new IcoExtractor(stru))
                    {
                        if (extr.GetIcon != null)
                        {


                            extr.SaveToFile(SysPathImage);
                            extr.Dispose();
                            this.Dispatcher.Invoke(() =>
                            {
                                this.add_new_item_FilePanel(stru, item, SysPathImage);
                            });



                        }
                    }


                }
                status_cl = false;
            });

            TaskBackShell.Start();
        }


        private void AppStrtup_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FilePanel filePanel = (FilePanel)(sender);
            l_winapi.Module.Trycatch.trycatch(() =>
            {
                switch (filePanel.Type)
                {
                    case Shell.IconExtractor.Enumes.ItemType.Drive:

                        break;
                    case Shell.IconExtractor.Enumes.ItemType.Folder:
                        Process.Start("explorer.exe", $"/select, \"{filePanel.SystemPath}\"");


                        break;
                    case Shell.IconExtractor.Enumes.ItemType.File:
                        Util.local_ProcessStart(filePanel.SystemPath, isadmin: false);


                        //Process.Start("explorer.exe", filePanel.SystemPath);
                        break;
                    default:
                        break;
                }


            });

            G_.MainWindow.SetVisibility(false);

        }
        private void _show_contextmenu(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SelectFilePanel = (FilePanel)sender;
            if (ContextMenuItem == null)
                return;
            ContextMenuItem.Background = System.Windows.Media.Brushes.Transparent;
            ContextMenuItem.PlacementTarget = this;
            ContextMenuItem.IsOpen = true;

        }
        public void Clear()
        {
            _wrappanel.Children.Clear();
        }
    }
}
