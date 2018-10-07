﻿using System;
using System.Windows;
using System.Windows.Data;
using EditModule.Commands;
using EditModule.ViewModels;
using ICSharpCode.AvalonEdit;
using Prism.Regions;

namespace EditModule.Views
{
    public partial class EditControl
    {
        private readonly IEditCommandHandler[] _commandHandlers;
        public IRegionManager RegionManager { get; }
        public EditControlViewModel ViewModel => (EditControlViewModel)DataContext;

        public EditControl(IRegionManager regionManager, IEditCommandHandler[] commandHandlers)
        {
            _commandHandlers = commandHandlers;
            RegionManager = regionManager;
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            _border.Child = ViewModel.TextEditor;
            Background = ViewModel.TextEditor.Background;
            ViewModel.Dispatcher = Dispatcher;

            AddCommandHandlers();
            AddPropertyBindings(ViewModel.TextEditor);
            AddEventHandlers(ViewModel.TextEditor);
        }

        private void AddCommandHandlers()
        {
            var shell = Application.Current.MainWindow;
            foreach (var commandHandler in _commandHandlers)
            {
                commandHandler.Initialize(shell, ViewModel);
            }
        }

        private void AddPropertyBindings(DependencyObject textEditor)
        {
            void AddBinding(DependencyProperty dp, string property, BindingMode mode = BindingMode.OneWay) 
                => BindingOperations.SetBinding(textEditor, dp, new Binding(property) { Source = DataContext, Mode = mode });

            AddBinding(FontFamilyProperty, nameof(ViewModel.Font));
            AddBinding(FontSizeProperty, nameof(ViewModel.FontSize));
            AddBinding(TextEditor.WordWrapProperty, nameof(ViewModel.WordWrap), BindingMode.TwoWay);
            AddBinding(TextEditor.IsModifiedProperty, nameof(ViewModel.IsDocumentModified), BindingMode.TwoWay);
        }

        private void AddEventHandlers(IInputElement textEditor)
        {
            Loaded += (sd, ea) => Dispatcher.InvokeAsync(ViewModel.LoadLastFile);
            IsVisibleChanged += (sd, ea) => { if (IsVisible) Dispatcher.InvokeAsync(textEditor.Focus); };
            ViewModel.ThemeChanged += (sd, ea) => Dispatcher.InvokeAsync(() => Background = ViewModel.TextEditor.Background);
        }
    }
}
