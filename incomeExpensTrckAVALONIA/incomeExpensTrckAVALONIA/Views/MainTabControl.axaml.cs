using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using System;

namespace incomeExpensTrckAVALONIA.Views
{
    public partial class MainTabControl : TabControl, IStyleable
    {
        public Type StyleKey => typeof(TabControl);

        public MainTabControl()
        {
            InitializeComponent();
        }

        //private void InitializeComponent()
        //{
        //    AvaloniaXamlLoader.Load(this);
        //}
    }
}
