using System;
using System.Collections.Generic;
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

namespace DragDropAutoScroll
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            const int topLevelItemsCount = 100;
            const int childrenPerTopLevelItem = 3;
            for (int i = 0; i < topLevelItemsCount; i++)
            {
                var treeViewItem = new TreeViewItem() { Header = $"Item {i}" };
                for (int j = 0; j < childrenPerTopLevelItem; j++)
                {
                    var child = new TreeViewItem() { Header = $"SubItem {j}" };
                    treeViewItem.Items.Add(child);
                }
                treeView.Items.Add(treeViewItem);
            }
        }
    }
}
