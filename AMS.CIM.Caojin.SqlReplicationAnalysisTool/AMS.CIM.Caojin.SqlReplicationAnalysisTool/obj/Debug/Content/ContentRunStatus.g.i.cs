﻿#pragma checksum "..\..\..\Content\ContentRunStatus.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8E07C105EFF9260AD1153A009EF15470117CBB39"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using AMS.CIM.Caojin.SqlReplicationAnalysisTool.Content;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Converters;
using FirstFloor.ModernUI.Windows.Navigation;
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


namespace AMS.CIM.Caojin.SqlReplicationAnalysisTool.Content {
    
    
    /// <summary>
    /// ContentRunStatus
    /// </summary>
    public partial class ContentRunStatus : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Content\ContentRunStatus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_file;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Content\ContentRunStatus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_selectfile;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Content\ContentRunStatus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tb_TargetTime;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Content\ContentRunStatus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DG1;
        
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
            System.Uri resourceLocater = new System.Uri("/SqlReplicationAnalysisTool;component/content/contentrunstatus.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Content\ContentRunStatus.xaml"
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
            this.tb_file = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.button_selectfile = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\Content\ContentRunStatus.xaml"
            this.button_selectfile.Click += new System.Windows.RoutedEventHandler(this.button_selectfile_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tb_TargetTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.DG1 = ((System.Windows.Controls.DataGrid)(target));
            
            #line 19 "..\..\..\Content\ContentRunStatus.xaml"
            this.DG1.LoadingRow += new System.EventHandler<System.Windows.Controls.DataGridRowEventArgs>(this.DG1_LoadingRow);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
