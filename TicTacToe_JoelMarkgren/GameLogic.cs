using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TicTacToe_JoelMarkgren
{
	public enum CellState
	{
		X,
		O,
		Free
	}
	public class GameLogic : BaseClass
	{
		private CellState[,] Board = new CellState[3, 3];

		private bool YourTurn = true;

		public int NumberOfComputerWins = 0;

		public int NumberOfYourWins = 0;

		public int NumberOfTies = 0;

		private string scoreBoardText;

		public string ScoreBoardText
		{
			get { return scoreBoardText; }
			set { scoreBoardText = value;
				RaisePropertyChanged();
			}
		}



		public GameLogic()
		{
			ResetBoard();
			ScoreBoardText = $"Dina vinster: {NumberOfYourWins} -  Datorns vinster: {NumberOfComputerWins} - Antalet oavgjorda: {NumberOfTies}";
		}


		public void ResetBoard()
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					Board[i, j] = CellState.Free;
				}
			}
			ScoreBoardText = $"Dina vinster: {NumberOfYourWins} -  Datorns vinster: {NumberOfComputerWins} - Antalet oavgjorda: {NumberOfTies}";
		}
		public bool GameIsWon(CellState player)
		{
			//row eller col wins
			for (int i = 0; i < 3; i++)
			{
				if (Board[i, 0] == player && Board[i, 1] == player && Board[i, 2] == player)
				{
					return true;
				}
				if (Board[0, i] == player && Board[1, i] == player && Board[2, i] == player)
				{
					return true;
				}
			}

			//diagonal win

			if (Board[0, 0] == player && Board[1, 1] == player && Board[2, 2] == player)
			{
				return true;
			}
			else if (Board[0, 2] == player && Board[1, 1] == player && Board[2, 0] == player)
			{
				return true;
			}
			return false;
		}

		public void UpdateStatistics(CellState player)
		{
			if (GameIsWon(CellState.X))
			{
				NumberOfYourWins++;
			}
			if (GameIsWon(CellState.O))
			{
				NumberOfComputerWins++;
			}
			if (IsBoardFull())
			{
				NumberOfTies++;
			}
			
		}

		public bool PlayerTurn(int row, int col)
		{

			if (Board[row, col] == CellState.Free)
			{
				Board[row, col] = CellState.X;
				YourTurn = false;
				return true;

			}
			return false;
		}

		public (int, int)? ComputerTurn()
		{
			var random = new Random();

			while (!YourTurn)
			{
				int randomizedRow = random.Next(0, 3);
				int randomizedCol = random.Next(0, 3);

				if (Board[randomizedRow, randomizedCol] == CellState.Free)
				{
					Board[randomizedRow, randomizedCol] = CellState.O;
					YourTurn = true;
					return (randomizedRow, randomizedCol);
				}
			}
			return null;
		}
		public bool IsBoardFull()
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (Board[i, j] == CellState.Free)
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}
