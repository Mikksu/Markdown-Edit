﻿using System.Windows;
using System.Windows.Input;
using MarkdownEdit.Controls;
using MarkdownEdit.Models;

namespace MarkdownEdit.Commands
{
    internal static class InsertHyperlinkCommand
    {
        public static readonly RoutedCommand Command = new RoutedCommand();

        static InsertHyperlinkCommand()
        {
            Application.Current.MainWindow.CommandBindings.Add(new CommandBinding(Command, Execute));
        }

        private static void Execute(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null) return;
            var editor = ((MainWindow)sender).Editor;
            editor.IfNotReadOnly(() => EditorUtilities.InsertHyperlink(editor.EditBox, e.Parameter as string));
        }
    }
}
