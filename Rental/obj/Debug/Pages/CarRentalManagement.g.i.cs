﻿#pragma checksum "..\..\..\Pages\CarRentalManagement.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "54615338D90643E07A9CFED83A3B2B7A1F3DB3BF6B535B6ED9DFD5C7DB0CF9D7"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Rental.Pages;
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


namespace Rental.Pages {
    
    
    /// <summary>
    /// CarRentalManagement
    /// </summary>
    public partial class CarRentalManagement : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\Pages\CarRentalManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid clientsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Pages\CarRentalManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid carsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Pages\CarRentalManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid carPopularityDataGrid;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Pages\CarRentalManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ordersDataGrid;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Pages\CarRentalManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid carAvailabilityDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/Rental;component/pages/carrentalmanagement.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\CarRentalManagement.xaml"
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
            this.clientsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            
            #line 17 "..\..\..\Pages\CarRentalManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnAddClientClick);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 18 "..\..\..\Pages\CarRentalManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnEditClientClick);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 19 "..\..\..\Pages\CarRentalManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnDeleteClientClick);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 21 "..\..\..\Pages\CarRentalManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnShowClientDetailsClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.carsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            
            #line 30 "..\..\..\Pages\CarRentalManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnAddCarClick);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 31 "..\..\..\Pages\CarRentalManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnUpdateCarStatusClick);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 33 "..\..\..\Pages\CarRentalManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnShowCarPopularityClick);
            
            #line default
            #line hidden
            return;
            case 10:
            this.carPopularityDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 11:
            this.ordersDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 42 "..\..\..\Pages\CarRentalManagement.xaml"
            this.ordersDataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ordersDataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 44 "..\..\..\Pages\CarRentalManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnCreateOrderClick);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 45 "..\..\..\Pages\CarRentalManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnEditOrderClick);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 46 "..\..\..\Pages\CarRentalManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnCompleteOrderClick);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 54 "..\..\..\Pages\CarRentalManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnShowCarAvailabilityClick);
            
            #line default
            #line hidden
            return;
            case 16:
            this.carAvailabilityDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

