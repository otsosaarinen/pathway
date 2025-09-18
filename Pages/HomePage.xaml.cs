using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pathway
{
	public partial class HomePage : Page
	{
		public ObservableCollection<FileTransferData> Transfers { get; set; } = [];

		public HomePage()
		{
			InitializeComponent();

			TransfersListBox.ItemsSource = Transfers;
		}
		private void NewFileTransfer_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			var newPage = new NewFileTransferPage();

			newPage.FileTransfersAdded += (data, page) =>
			{
				Transfers.Add(data);
				page.NavigationService?.GoBack();
			};

			NavigationService.Navigate(newPage);
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			if (TransfersListBox.SelectedIndex >= 0)
			{
				Transfers.RemoveAt(TransfersListBox.SelectedIndex);
			}
		}

		private void RunButton_Click(object sender, RoutedEventArgs e)
		{

			if (TransfersListBox.SelectedItem is FileTransferData selectedTransfer)
			{
				string sourceFile = selectedTransfer.SourceFile;
				string sourceFolder = selectedTransfer.SourceFolder;
				string destinationFolder = selectedTransfer.DestinationFolder;

				// Full path to the source file
				string sourcePath = System.IO.Path.Combine(sourceFolder, sourceFile);

				// Full path to the destination file
				string destinationPath = System.IO.Path.Combine(destinationFolder, sourceFile);

				try
				{
					// Check if source file exists
					if (!File.Exists(sourcePath))
					{
						MessageBox.Show($"Source file does not exist: {sourcePath}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						return;
					}

					// Ensure destination folder exists
					if (!Directory.Exists(destinationFolder))
						Directory.CreateDirectory(destinationFolder);

					// Overwrite if destination file exists
					if (File.Exists(destinationPath))
						File.Delete(destinationPath);

					// Move the file
					File.Move(sourcePath, destinationPath);

					MessageBox.Show($"File moved successfully:\n{sourceFile}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Error moving file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}

		}
	}

}
