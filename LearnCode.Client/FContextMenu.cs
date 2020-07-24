using System.Windows;
using System.Windows.Controls;

namespace LearnCode.Client
{
    public class FContextMenu : ContextMenu
    {
        static FContextMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FContextMenu), new FrameworkPropertyMetadata(typeof(FContextMenu)));
        }
    }
}
