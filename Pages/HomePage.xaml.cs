using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	}

}
