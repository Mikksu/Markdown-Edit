﻿using System.Windows;
using System.Windows.Input;
using MarkdownEdit.Controls;

namespace MarkdownEdit.Commands
{
    internal static class RevertCommand
    {
        public static readonly RoutedCommand Command = new RoutedCommand();

        static RevertCommand()
        {
            Application.Current.MainWindow.CommandBindings.Add(new CommandBinding(Command, Execute));
        }

        private static void Execute(object sender, ExecutedRoutedEventArgs e)
        {
            var editor = ((MainWindow)sender).Editor;
            editor.OpenFile(editor.FileName);
        }
    }
}