﻿#pragma checksum "..\..\FormAssembly.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A2F71F935B9DEABE6DB81B6DC12650BC62541F16F217C01210F88B12560725F2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using De.TorstenMandelkow.MetroChart;
using LiveCharts.Wpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TKAC_Application;


namespace TKAC_Application {
    
    
    /// <summary>
    /// FormAssembly
    /// </summary>
    public partial class FormAssembly : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\FormAssembly.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblTieuDe;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\FormAssembly.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDong;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\FormAssembly.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.PieChart ChtSanLuongNgayTrim;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\FormAssembly.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.PieChart ChtSanLuongNgayChassis;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\FormAssembly.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.PieChart ChtSanLuongNgayFinal;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TKAC Application;component/formassembly.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FormAssembly.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lblTieuDe = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.btnDong = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\FormAssembly.xaml"
            this.btnDong.Click += new System.Windows.RoutedEventHandler(this.btnDong_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ChtSanLuongNgayTrim = ((LiveCharts.Wpf.PieChart)(target));
            return;
            case 4:
            this.ChtSanLuongNgayChassis = ((LiveCharts.Wpf.PieChart)(target));
            return;
            case 5:
            this.ChtSanLuongNgayFinal = ((LiveCharts.Wpf.PieChart)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
