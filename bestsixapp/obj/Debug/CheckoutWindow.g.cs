﻿#pragma checksum "..\..\CheckoutWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F813313AE583B6CEA00ABFBB6E9756F9C29A3812"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using bestsixapp;


namespace bestsixapp {
    
    
    /// <summary>
    /// CheckoutWindow
    /// </summary>
    public partial class CheckoutWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\CheckoutWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTEditCheckin;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\CheckoutWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxRoomNo;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\CheckoutWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxBedType;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\CheckoutWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxNoOfBeds;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\CheckoutWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxPrice;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\CheckoutWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxSmoking;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\CheckoutWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTRefund;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\CheckoutWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTCheckout;
        
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
            System.Uri resourceLocater = new System.Uri("/bestsixapp;component/checkoutwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CheckoutWindow.xaml"
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
            this.BTEditCheckin = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\CheckoutWindow.xaml"
            this.BTEditCheckin.Click += new System.Windows.RoutedEventHandler(this.ButtonEdit_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TextBoxRoomNo = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.TextBoxBedType = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.TextBoxNoOfBeds = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.TextBoxPrice = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.TextBoxSmoking = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.BTRefund = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\CheckoutWindow.xaml"
            this.BTRefund.Click += new System.Windows.RoutedEventHandler(this.ButtonRefund_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.BTCheckout = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\CheckoutWindow.xaml"
            this.BTCheckout.Click += new System.Windows.RoutedEventHandler(this.ButtonCheckout_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
