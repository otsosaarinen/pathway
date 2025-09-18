using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
	public partial class NewFileTransferPage : Page
	{
		public event Action<FileTransferData, Page> FileTransfersAdded;

		public NewFileTransferPage()
		{
			InitializeComponent();
		}
		private void BackToHome_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NavigationService.GoBack();
		}

		private void AddFileTransfer_Click(object sender, RoutedEventArgs e)
		{
			var data = new FileTransferData
			{
				SourceFile = SourceFileTextBox.Text,
				SourceFolder = SourceFolderTextBox.Text,
				DestinationFolder = DestinationFolderTextBox.Text
			};

			FileTransfersAdded?.Invoke(data, this);
		}
	}
	public class FileTransferData
	{
		public required string SourceFile { get; set; }
		public required string SourceFolder { get; set; }
		public required string DestinationFolder { get; set; }

		public override string ToString()
		{
			return $"Moving {SourceFile} from {SourceFolder} to {DestinationFolder}";
		}
	}
}
