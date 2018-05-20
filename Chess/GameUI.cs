using System;
using System.Collections;
using System.Windows.Forms;
using ChessLibrary;

namespace Chess
{
	/// <summary>
	/// Summary description for ChessBoard.
	/// </summary>
	public class GameUI
	{
		private ArrayList Squares;	// Picture control array for storing the place holders
		public Images ChessImages;	// Contains reference of chess images
		private string ResourceFolder;		// Contain the locaiton of resource folder
		private int LogCounter;			// Stores the entries in the log

		public Game ChessGame;		    // Backend chess game engine
		public string	SelectedSquare;	// Contains name of the selected square
        public string LastSelectedSquare;// The last selected square
		public ChessMain ParentForm;	// Reference of the parent form 
		public bool ShowMoveHelp;		// Show possible move by colors
		public bool IsRunning;			// Return true when the game is running
		public bool IsOver;				// Set to true when the game is over
        public bool ShowComputerThinkingProgres = true;    // Set wether to show the progress of the computer thinking
        public bool LastMoveByClick;    // Stores true if the last move was made by mouse click (instead of drag and drop)

		public GameUI(ChessMain form)
		{
			this.ParentForm = form;	// get and store reference of parent form

			// Load all the chess images in a list
			ChessImages = new Images();

            #if DEBUG
			    ResourceFolder = "..\\..\\Resources\\";
            #else
                ResourceFolder = "Resources\\";
            #endif


            ResourceFolder = "Resources\\";
			ChessImages.LoadImages(ResourceFolder);
			BuildBoard();
			
			ParentForm.ChessCaptureBar.InitializeBar(ChessImages);	// Initialize chess bar

			// Show legal moves for selected piece
			ShowMoveHelp = true; // 
		}

		// Builds the chess pieces place holder images controls
		public void BuildBoard()
		{
			Squares = new ArrayList();	// Initialize place holder pictures

			// Now dynamically draw all the chess pieces place holder images
			for (int row=1; row<=8; row++)		// repeat for every row in the chess board
				for (int col=1; col<=8; col++)	// repeat for every column in the chess board row
				{
					Square ChessSquare = new Square(row, col, this);
					ChessSquare.SetBackgroundSquare(ChessImages);	// Set the chess square background
					Squares.Add(ChessSquare);
					ParentForm.Controls.Add(ChessSquare);
				}
		}

		// returns board square for the given name
		private Square GetBoardSquare(string strCellName)
		{
			foreach (Square ChessSquare in Squares)
			{
				if (ChessSquare.Name == strCellName)
					return ChessSquare;
			}
			return null;
		}

		// Redraw the visible board from the internal chess board
		public void RedrawBoard()
		{
			foreach (Square ChessSquare in Squares)
			{
				if (ChessSquare.BackgroundImage==null) // background image doesn't exists
				{
					ChessSquare.SetBackgroundSquare(ChessImages);
				}

				if (ChessGame.Board[ChessSquare.Name] != null)	// Valid board square
					ChessSquare.DrawPiece(ChessImages.GetImageForPiece(ChessGame.Board[ChessSquare.Name].piece )); // draw the chess piece image
				
				if (ChessSquare.Name == SelectedSquare && ShowMoveHelp==true) // selected check box
				{
					ChessSquare.BackgroundImage = null;
					ChessSquare.BackColor = System.Drawing.Color.Thistle;
				}
			}

			// Check if need to show the possible legal moves for the current selected piece
			if (SelectedSquare != null && SelectedSquare != "" && ShowMoveHelp==true && ChessGame.Board[SelectedSquare].piece != null && !ChessGame.Board[SelectedSquare].piece.IsEmpty() &&  ChessGame.Board[SelectedSquare].piece.Side.type == /*ChessGame.*/Game.GameTurn )
			{
                // Get all legal moves for the current selected item
                ArrayList moves = ChessGame.GetLegalMoves(ChessGame.Board[SelectedSquare]);
			
				// Hightlight all the possible moves for the current player
				foreach (Cell cell in moves)
				{
					Square sqr=GetBoardSquare(cell.ToString());	// get the board by cell position
					sqr.BackgroundImage = null;
                    // Show a semi-transparent, saddle color
                    sqr.BackColor = System.Drawing.Color.FromArgb(200, System.Drawing.Color.SaddleBrown);
				}
			}
			SelectedSquare="";	// Reset the selected square position
		}

		// Show current player turn visual clue
		public void ShowPlayerTurn()
		{
			ChessGame.UpdateTime();	// Update the chess thinking times

			if (ChessGame.BlackTurn())
			{
				ParentForm.BlackPlayerTime.Text = ChessGame.BlackPlayer.ThinkTime;
				//ParentForm.WhitePlayerName.Visible = true;
				//ParentForm.BlackPlayerName.Visible = !ParentForm.BlackPlayerName.Visible; // Blink the player name
			}
			else
			{
				ParentForm.WhitePlayerTime.Text = ChessGame.WhitePlayer.ThinkTime;
				//ParentForm.BlackPlayerName.Visible = true;
				//ParentForm.WhitePlayerName.Visible = !ParentForm.WhitePlayerName.Visible; // Blink the player name
			}
		}

		// Called when it's the next player turn to play the move
		// We handle the computer move here
		public void NextPlayerTurn()
		{
			if (ChessGame.ActivePlay.IsComputer()) // If the active player is a computer
			{
                if (ShowComputerThinkingProgres)
                    ParentForm.ChessCaptureBar.Visible = false;
                else
                    ParentForm.ChessCaptureBar.Visible = true;

				Move nextMove = ChessGame.ActivePlay.GetBestMove();	// get the best move for the player

				if (nextMove!=null)	// a valid move is available
					UserMove(nextMove.StartCell.ToString(), nextMove.EndCell.ToString());
				
				ParentForm.ChessCaptureBar.Visible = true; // show the capture bar
			}
		}

		// Initialize the Chess player objects
		private void InitPlayers()
		{
            // Show the images depending on the selected player types
            if (ChessGame.BlackPlayer.PlayerType == Player.Type.Human && ChessGame.WhitePlayer.PlayerType == Player.Type.Human)
            {
                ChessGame.BlackPlayer.Image = System.Drawing.Image.FromFile(ResourceFolder + "user.jpg");
                ChessGame.WhitePlayer.Image = System.Drawing.Image.FromFile(ResourceFolder + "user_2.jpg");
            }
            else if (ChessGame.BlackPlayer.PlayerType == Player.Type.Computer && ChessGame.WhitePlayer.PlayerType == Player.Type.Human)
            {
                ChessGame.BlackPlayer.Image = System.Drawing.Image.FromFile(ResourceFolder + "laptop.jpg");
                ChessGame.WhitePlayer.Image = System.Drawing.Image.FromFile(ResourceFolder + "user_2.jpg");
            }
            else if (ChessGame.BlackPlayer.PlayerType == Player.Type.Computer && ChessGame.WhitePlayer.PlayerType == Player.Type.Computer)
            {
                ChessGame.BlackPlayer.Image = System.Drawing.Image.FromFile(ResourceFolder + "laptop.jpg");
                ChessGame.WhitePlayer.Image = System.Drawing.Image.FromFile(ResourceFolder + "laptop_2.png");
            }

			// Initialize other board objects
			ParentForm.WhitePlayerName.Text = ChessGame.WhitePlayer.Name;
			ParentForm.BlackPlayerName.Text = ChessGame.WhitePlayer.Name;

			ParentForm.WhitePlayerImage.Image = ChessGame.WhitePlayer.Image;
			ParentForm.BlackPlayerImage.Image = ChessGame.BlackPlayer.Image;

			ParentForm.WhitePlayerName.Text = ChessGame.WhitePlayer.Name;
			ParentForm.BlackPlayerName.Text = ChessGame.BlackPlayer.Name;

			// Set the time 
			ParentForm.BlackPlayerTime.Text = "00:00:00";
			ParentForm.WhitePlayerTime.Text = "00:00:00";

			ParentForm.lstHistory.Items.Clear();
		}

		// A move is made by the player
		public bool UserMove(string source, string dest)
		{
            bool success = true;
			int MoveResult=ChessGame.DoMove(source, dest);
			RedrawBoard();	// Refresh the board

			switch (MoveResult)
			{
				case 0:	// move was successfull;
					// check if the last move was promo move
					Move move=ChessGame.GetLastMove();	// get the last move 


					// Add to the capture list
					if ( move.IsCaptureMove() )
						ParentForm.ChessCaptureBar.Add(ChessImages.GetImageForPiece(move.CapturedPiece));

					// If last move was a pawn promotion move, get the new selected piece from user
					if (move.IsPromoMove() && !ChessGame.ActivePlay.IsComputer())
						ChessGame.SetPromoPiece(GetPromoPiece(move.EndCell.piece.Side));	// Set the promo piece as selected by user
					
					// check for the checkmate situation
					if (ChessGame.IsCheckMate(/*ChessGame.*/Game.GameTurn))
					{
						IsOver=true;
						MessageBox.Show(ChessGame.GetPlayerBySide(/*ChessGame.*/Game.GameTurn).Name + " is checkmate.", "Game Over",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					}
					// check for the statemate situation
					if (ChessGame.IsStaleMate(/*ChessGame.*/Game.GameTurn))
					{
						IsOver=true;
						MessageBox.Show(ChessGame.GetPlayerBySide(/*ChessGame.*/Game.GameTurn).Name + " is stalmate.", "Game Over",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					}
					LogUserMove(move.ToString());	// Log the user action
					NextPlayerTurn();
					break;

				default:
                    success = false;
					break;
			}

            return success;
		}

		// Choose which piece to promote to -> changed to queen only
		public Piece GetPromoPiece(Side PlayerSide)
		{
			return new Piece(Piece.PieceType.Queen,PlayerSide);
		}

		// Display the user move in the history log
		public void LogUserMove(string movestring)
		{
			LogCounter++;
			ListViewItem newItem = new ListViewItem(new string[] { LogCounter.ToString(), movestring}, -1);
			
			if (LogCounter % 2 == 0)	// even entry
				newItem.ForeColor = System.Drawing.Color.Blue;

			ParentForm.lstHistory.Items.Add(newItem);
			ParentForm.lstHistory.Items[ParentForm.lstHistory.Items.Count-1].EnsureVisible();	// Scroll down
		
            // If the log has more than 16 items, reduce the width of the detail column to avoid 
            // horizontal scrollbar
            if (ParentForm.lstHistory.Items.Count > 16)
                ParentForm.lstHistory.Columns[1].Width = 90;
        }

		// Undo the last move
		public void UndoMove()
		{
			IsOver=false;				// Reset the is running variable

            // check if the last move was promo move
            Move move = ChessGame.GetLastMove();	// get the last move 

			if (ChessGame.UnDoMove())
			{
				LogUserMove("Undo Move");	// Log the user action

                // Only remove the item from capture bar, if it was a capture move
                if (move.IsCaptureMove())
                    ParentForm.ChessCaptureBar.RemoveLast();
			}

			// If computer is playing do the double undo
			if (ChessGame.ActivePlay.IsComputer())
			{
                move = ChessGame.GetLastMove();	// get the last move 
				ChessGame.UnDoMove();

                // Only remove the item from capture bar, if it was a capture move
                if (move.IsCaptureMove())
				    ParentForm.ChessCaptureBar.RemoveLast();
			}

			RedrawBoard();	// Refresh the board
		}

		// Handle the computer thinking event
		public void ComputerThinking(int depth, int currentMove, int TotalMoves, int TotalAnalzyed, Move BestMove)
		{
            if (ShowComputerThinkingProgres)
            {
                // Update label and progress bar to display the computer think status
                ParentForm.PrgComputerThinkDepth.Maximum = TotalMoves;
                ParentForm.PrgComputerThinkDepth.Value = currentMove;
                ParentForm.LblComuterThinkLabel.Text = "Computer thinking at depth " + depth.ToString() + ". Total moves analyzed: " + TotalAnalzyed + ". ";

                if (BestMove != null)
                    ParentForm.LblComuterThinkLabel.Text += "Best move found so far is :" + BestMove.ToString();
            }
		}


        /// <summary>
        /// Save the current game state to the given file path
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveGame()
        {
            // Show the File Save as dialog and get the target file path
            SaveFileDialog saveAsDialog = new SaveFileDialog();
            saveAsDialog.Title = "Save file as...";
            saveAsDialog.Filter = "Quick Chess File (*.qcf)|*.qcf";
            saveAsDialog.RestoreDirectory = true;

            if (saveAsDialog.ShowDialog() == DialogResult.OK)
            {
                // Save the file at the given path
                ChessGame.SaveGame(saveAsDialog.FileName);
            }
        }

		// Initialize a new game and set the pieces on the board
		public void NewGame()
		{
			ParentForm.ChessCaptureBar.Clear();
			NewGame NewGameDlg = new NewGame();
            NewGameDlg.ResourceFolderPath = ResourceFolder;
			NewGameDlg.ShowDialog();

			// Start the new game
			if (NewGameDlg.bStartGame)
			{
				ChessGame = new Game();

				// Handle the events fired by the library
				ChessGame.ComputerThinking += new ChessLibrary.Game.ChessComputerThinking(ComputerThinking);

				ChessGame.Reset();	// Reset the game board
				IsRunning = true;
				LogCounter = 0;

				ChessGame.WhitePlayer.Name = NewGameDlg.WhitePlayerName.Text;
				ChessGame.BlackPlayer.Name = NewGameDlg.BlackPlayerName.Text;

				// Start Human Vs. Computer Game
                if (NewGameDlg.PlayersHvC.Checked)
                {
                    ChessGame.BlackPlayer.PlayerType = Player.Type.Computer;	// Set the black player as computer
                    ChessGame.WhitePlayer.PlayerType = Player.Type.Human;	    // Set the white player as computer (as he has the first move)
                }


				// Beginner Player
				if (NewGameDlg.PlayerLevel1.Checked)
				{
					ChessGame.WhitePlayer.TotalThinkTime = 4;	// set maximum thinking time
					ChessGame.BlackPlayer.TotalThinkTime = 4;	// set maximum thinking time
				}

				// Intermediate Player
				if (NewGameDlg.PlayerLevel2.Checked)
				{
					ChessGame.WhitePlayer.TotalThinkTime = 7;	// set maximum thinking time
					ChessGame.BlackPlayer.TotalThinkTime = 7;	// set maximum thinking time
				}

				// Chess Master Player
				if (NewGameDlg.PlayerLevel3.Checked)
				{
					ChessGame.WhitePlayer.TotalThinkTime = 10;	// set maximum thinking time
					ChessGame.BlackPlayer.TotalThinkTime = 10;	// set maximum thinking time
				}

				InitPlayers();
				RedrawBoard();		// Make the chess board visible on screen
				NextPlayerTurn();		// When the both players are computer this start the game 
			
            }
		}
	}
}
