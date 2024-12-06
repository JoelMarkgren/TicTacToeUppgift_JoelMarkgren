using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe_JoelMarkgren
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		private GameLogic gameHandler;

		public MainWindow()
		{
			InitializeComponent();
			gameHandler = new GameLogic();
			DataContext = gameHandler;
		}



		private void ButtonClick(object sender, RoutedEventArgs e)
		{
			if (sender is Button btn)
			{
				int row = Grid.GetRow(btn);
				int col = Grid.GetColumn(btn);

				if (gameHandler.PlayerTurn(row, col))
				{
					btn.Content = "X";
					btn.IsEnabled = false;
				}
				if (gameHandler.GameIsWon(CellState.X))
				{		
					MessageBox.Show("Du vann");
					gameHandler.NumberOfYourWins++;
					ResetGame();
					return;
				}
				if (gameHandler.IsBoardFull())
				{
					MessageBox.Show("Oavgjort");
					gameHandler.NumberOfTies++;
					ResetGame();
					return;
				}
				var computerMove = gameHandler.ComputerTurn();

				if (computerMove != null)
				{
					int compRow = computerMove.Value.Item1;
					int compCol = computerMove.Value.Item2;

					var computerButton = FindButtonInGrid(compRow, compCol);
					if (computerButton != null)
					{
						computerButton.Content = "O";
						computerButton.IsEnabled = false;
					}

					if (gameHandler.GameIsWon(CellState.O))
					{
						MessageBox.Show("Datorn vann");
						gameHandler.NumberOfComputerWins++;
						ResetGame();
						return;
					}
				}

			}

		}
		private Button? FindButtonInGrid(int row, int col)
		{
			foreach (var cell in GameBoard.Children)
			{
				if (cell is Button btn && Grid.GetRow(btn) == row && Grid.GetColumn(btn) == col)
				{
					return btn;
				}
			}
			return null;
		}

		private void ResetGame()
		{
			gameHandler.ResetBoard();

			foreach (var child in GameBoard.Children)
			{
				if (child is Button btn)
				{
					btn.Content = string.Empty;
					btn.IsEnabled = true;
				}
			}
		}
	}
}
