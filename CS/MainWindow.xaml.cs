using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DXSample {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
    }

    public class SaveWidthContentControl : ContentControl {
        
        public static readonly DependencyProperty ContentWidthProperty = DependencyProperty.Register("ContentWidth", typeof(double), typeof(SaveWidthContentControl), new UIPropertyMetadata(double.NaN, new PropertyChangedCallback(OnContentWidthChanged)));

        private static void OnContentWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((SaveWidthContentControl)d).OnContentWidthChanged();
        }
        protected virtual void OnContentWidthChanged() {
            SetContentWidth(ContentWidth);
        }

        public double ContentWidth {
            get { return (double)GetValue(ContentWidthProperty); }
            set { SetValue(ContentWidthProperty, value); }
        }

        void SetContentWidth(double width) {
            FrameworkElement controlFromContent = Content as FrameworkElement;
            if(controlFromContent == null)
                return;

            controlFromContent.Width = width;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e) {
            base.OnPropertyChanged(e);
            if(e.Property.Name == "Width") {
                double newWidth = (double)e.NewValue;
                if((newWidth != 0) && !double.IsNaN(newWidth)) {
                    SetContentWidth(newWidth);
                    Width = double.NaN;
                }
            } else if(e.Property.Name == "Content") {
                SetContentWidth(ContentWidth);
            }
        }
    }
}
