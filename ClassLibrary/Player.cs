using System;
using System.Drawing;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace ChessLibrary
{
	/// <summary>
	/// The class stores info about the chess player. Like 
	/// his type, name, image and side.
	/// </summary>
    [Serializable]
	public class Player
	{
		private Type MovType;		// Type of the player i.e. computer or human
		private Side PieceSide;		// Player side i.e. white or black
		private string PlayerName;		// Name of the Player
		private Image PlayerImage;		// Image of the player
		private Rules PlayerRules;		// A reference to the chess rules
		private TimeSpan MaximumThinkingTime;		// Maximum think time in seconds

		private TimeSpan TotalThinkingTime;	// Stores total think time of the player
		private DateTime StartingTime;		// User turn time starts
		private int	AllAnalyzedMoves;	// Total no. of moves analzyed by the player
		private bool GameAlmostOver;			// True when the game is near the end
		public enum Type{Human, Computer};

        // Empty Constructor for XML serialization
        internal Player()
        {
            PieceSide = PlayerSide;
            MovType = PlayerType;
            MaximumThinkingTime = new TimeSpan(0, 0, 4);	// maximum think for 4 seconds
            TotalThinkingTime = (DateTime.Now - DateTime.Now);	// Reset the time
        }

		// Constructor for the new player
		public Player(Side PlayerSide, Type PlayerType)
		{
			PieceSide=PlayerSide;
			MovType=PlayerType;
			MaximumThinkingTime = new TimeSpan(0,0,4);	// maximum think for 4 seconds
			TotalThinkingTime = (DateTime.Now - DateTime.Now);	// Reset the time
		}

		// Constructor for the new player
		public Player(Side PlayerSide, Type PlayerType, Rules rules) : this(PlayerSide,PlayerType)
		{
			PlayerRules=rules;	
		}

		// User turn/thinking time starts
		public void TimeStart()
		{
			StartingTime=DateTime.Now;
		}

		// User turn/thinking time ends
		public void TimeEnd()
		{
			TotalThinkingTime+= (DateTime.Now - StartingTime);
		}

		// Update the user time
		public void UpdateTime()
		{
		}

		// Returns true if the current player is computer
		public bool IsComputer()
		{
			return (MovType==Type.Computer);
		}

		// Reset the player timers
		public void ResetTime()
		{
			TotalThinkingTime = (DateTime.Now - DateTime.Now);	// Reset time
		}

        // Get or Set the game rules
        [XmlIgnore]
        internal Rules GameRules
        {
            set { PlayerRules = value; }
        }

		// Get the best move available to the player
		public Move GetFixBestMove()
		{
			int alfa, beta;
			int depth;					// depth to which to do the search
			TimeSpan ElapsedTime= new TimeSpan(1);		// Total elpased time
			Move BestMove=null;		// The best move for the current position

			// Initialize constants
			const int MIN_SCORE= -1000000;		// Minimum limit of negative for integer
			const int MAX_SCORE= 1000000;		// Maximum limit of positive integer

			ArrayList TotalMoves=PlayerRules.GenerateAllLegalMoves(PieceSide); // Get all the legal moves for the current side
			ArrayList PlayerCells = PlayerRules.ChessBoard.GetSideCell(PieceSide.type);

			alfa = MIN_SCORE;	// The famous Alfa & Beta are set to their initial values
			beta  = MAX_SCORE;	// at the start of each increasing search depth iteration

			depth=3;

			// Loop through all the legal moves and get the one with best score
			foreach (Move move in TotalMoves)
			{
				// Now to get the effect of this move; execute this move and analyze the board
				PlayerRules.ExecuteMove(move);
				move.Score = -AlphaBeta(PlayerRules.ChessGame.EnemyPlayer(PieceSide).PlayerSide,depth - 1, -beta, -alfa);
				PlayerRules.UndoMove(move);	// undo the move

				// If the score of the move we just tried is better than the score of the best move we had 
				// so far, at this depth, then make this the best move.
				if (move.Score > alfa)
				{
					BestMove = move;
					alfa = move.Score;
				}
			}		
			return BestMove;
		}


		// Get the best move available to the player
		public Move GetBestMove()
		{
			int alfa, beta;
			int depth;					// depth to which to do the search
			TimeSpan ElapsedTime= new TimeSpan(1);		// Total elpased time
			Move BestMove=null;		// The best move for the current position

			// Initialize constants
			const int MIN_SCORE= -10000000;		// Minimum limit of negative for integer
			const int MAX_SCORE= 10000000;		// Maximum limit of positive integer

			ArrayList TotalMoves=PlayerRules.GenerateAllLegalMoves(PieceSide); // Get all the legal moves for the current side

			// Now we use the Iterative deepening technique to search the best move
			// The idea is just simple, we will keep searching in the more and more depth
			// as long as we don't time out.
			// So, it means that when we have less moves, we can search more deeply and which means
			// better chess game.
			DateTime ThinkStartTime=DateTime.Now;
			int MoveCounter;
			Random RandGenerator= new Random();

			// Game is near the end, or the current player is under check
			if (PlayerRules.ChessBoard.GetSideCell(PieceSide.type).Count<=5 || TotalMoves.Count <= 5 )
				GameAlmostOver = true;

			// Game is near the end, or the Enemy player is under check
			Side EnemySide;

			if (PieceSide.isBlack())
				EnemySide = PlayerRules.ChessGame.WhitePlayer.PlayerSide;
			else
				EnemySide = PlayerRules.ChessGame.BlackPlayer.PlayerSide;

			if (PlayerRules.ChessBoard.GetSideCell(PieceSide.Enemy()).Count<=5 || PlayerRules.GenerateAllLegalMoves(EnemySide).Count <= 5 )
				GameAlmostOver = true;

			AllAnalyzedMoves=0;		// Reset the total moves anazlye counter

			for (depth = 1;; depth++)	// Keep doing a depth search 
			{
				alfa = MIN_SCORE;	// The famous Alfa & Beta are set to their initial values
				beta  = MAX_SCORE;	// at the start of each increasing search depth iteration
				MoveCounter = 0;	// Initialize the move counter variable

				// Loop through all the legal moves and get the one with best score
				foreach (Move move in TotalMoves)
				{
					MoveCounter++;

					// Now to get the effect of this move; execute this move and analyze the board
					PlayerRules.ExecuteMove(move);
					move.Score = -AlphaBeta(PlayerRules.ChessGame.EnemyPlayer(PieceSide).PlayerSide,depth - 1, -beta, -alfa);
					AllAnalyzedMoves++;	// Increment move counter
					PlayerRules.UndoMove(move);	// undo the move

					// If the score of the move we just tried is better than the score of the best move we had 
					// so far, at this depth, then make this the best move.
					if (move.Score > alfa)
					{
						BestMove = move;
						alfa = move.Score;
					}

					PlayerRules.ChessGame.NotifyComputerThinking(depth, MoveCounter, TotalMoves.Count,AllAnalyzedMoves, BestMove );

					// Check if the user time has expired
					ElapsedTime=DateTime.Now - ThinkStartTime;
					if ( ElapsedTime.Ticks > (MaximumThinkingTime.Ticks) )	// More than 75 percent time is available
						break;							// Force break the loop
				}

				// Check if the user time has expired
				ElapsedTime=DateTime.Now - ThinkStartTime;
				if ( ElapsedTime.Ticks > (MaximumThinkingTime.Ticks*0.25))	// More than 75 percent time is available
					break;							// Force break the loop
			}
		
			PlayerRules.ChessGame.NotifyComputerThinking(depth, MoveCounter, TotalMoves.Count,AllAnalyzedMoves, BestMove );
			return BestMove;
		}

		// Alfa and beta search to recursively travers the tree to calculate the best move
		private int AlphaBeta(Side PlayerSide, int depth, int alfa, int beta)
		{
			int val;
			System.Windows.Forms.Application.DoEvents();

			// Before we do anything, let's try the null move. It's like giving the opponent
			// a free shot and see if he can damage us. If he can't, we are in a better position and 
			// can nock down him

			// "Adaptive" Null-move forward pruning
			int R = (depth>6 ) ? 3 : 2; //  << This is the "adaptive" bit
			// The rest is normal Null-move forward pruning
			if (depth >= 2 && !GameAlmostOver && PlayerRules.ChessGame.DoNullMovePruning)	// disable null move for now
			{
				val = -AlphaBeta(PlayerRules.ChessGame.EnemyPlayer(PlayerSide).PlayerSide,depth  - R - 1, -beta, -beta + 1); // Try a Null Move
				if (val >= beta) // All the moves can be skipped, i.e. cut-off is possible
					return beta;
			}

			// This variable is set to true when we have found at least one Principle variation node.
			// Principal variation (PV) node is the one where One or more of the moves will return a score greater than alfa (a PV move), but none will return a score greater than or equal to beta. 
			bool bFoundPv = false;

			// Check if we have reached at the end of the search
			if (depth <= 0)
			{
				// Check if need to do queiscent search to avoid horizontal effect
				if (PlayerRules.ChessGame.DoQuiescentSearch)
					return QuiescentSearch(PlayerSide, alfa, beta);
				else
					return PlayerRules.Evaluate(PlayerSide);	// evaluate the current board position
			}	
			// Get all the legal moves for the current side
			ArrayList TotalMoves=PlayerRules.GenerateAllLegalMoves(PlayerSide); 

			// Loop through all the legal moves and get the one with best score
			foreach (Move move in TotalMoves)
			{
				// Now to get the effect of this move; execute this move and analyze the board
				PlayerRules.ExecuteMove(move);

				// Principle variation node is found
				if (bFoundPv && PlayerRules.ChessGame.DoPrincipleVariation) 
				{
					val = -AlphaBeta(PlayerRules.ChessGame.EnemyPlayer(PlayerSide).PlayerSide, depth - 1, -alfa - 1, -alfa);
					if ((val > alfa) && (val < beta)) // Check for failure.
						val=-AlphaBeta(PlayerRules.ChessGame.EnemyPlayer(PlayerSide).PlayerSide,depth - 1, -beta, -alfa); // Do normal Alfa beta pruning
				} 
				else
					val = -AlphaBeta(PlayerRules.ChessGame.EnemyPlayer(PlayerSide).PlayerSide,depth - 1, -beta, -alfa); // Do normal Alfa beta pruning

				AllAnalyzedMoves++;	// Increment move counter
				PlayerRules.UndoMove(move);	// undo the move
			
				// This move will never played by the opponent, as he has already better options
				if (val >= beta)
					return beta;
				// This is the best move for the current side (found so far)
				if (val > alfa)
				{
					alfa = val;
					bFoundPv = true;		// we have found a principle variation node
				}
			}
			return alfa;			
		}


		// Do the queiscent search to avoid horizontal effect
		int QuiescentSearch(Side PlayerSide, int alfa, int beta)
		{
			int val = PlayerRules.Evaluate(PlayerSide);

			if (val >= beta) // We have reached beta cutt off
				return beta;
			
			if (val > alfa) // found alfa cut-off
				alfa = val;

			// Get all the legal moves for the current side
			ArrayList TotalMoves=PlayerRules.GenerateGoodCaptureMoves(PlayerSide); 

			// Loop through all the legal moves and get the one with best score
			foreach (Move move in TotalMoves)
			{
				// Now to get the effect of this move; execute this move and analyze the board
				PlayerRules.ExecuteMove(move);
				val = -QuiescentSearch(PlayerRules.ChessGame.EnemyPlayer(PlayerSide).PlayerSide, -beta, -alfa);
				PlayerRules.UndoMove(move);	// undo the move

				if (val >= beta) // We have reached beta cutt off
					return beta;
			
				if (val > alfa) // found alfa cut-off
					alfa = val;
			}

			return alfa;
		}


		//--------------------------------------------------
		#region Properties for the player class
        [XmlAttribute("Type=PlayerType")]
		public Type PlayerType
		{
			get
			{
				return MovType;
			}
			set
			{
				MovType=value;
			}
		}
		//--------------------------------------------------
		public Side PlayerSide
		{
			get
			{
				return PieceSide;
			}
			set
			{
				PieceSide=value;
			}
		}
		//--------------------------------------------------
		public string Name
		{
			get
			{
				return PlayerName;
			}
			set
			{
				PlayerName=value;
			}
		}
		//--------------------------------------------------
        [XmlIgnore]
		public Image Image
		{
			get
			{
				return PlayerImage;
			}
			set
			{
				PlayerImage=value;
			}
		}

		// Set and Get the total think time. Used for the computer player
		public int TotalThinkTime
		{
			get
			{
				return MaximumThinkingTime.Seconds;	// returns back total think time of the user
			}
			set
			{
				MaximumThinkingTime=new TimeSpan(0,0,value);	// returns back total think time of the user
			}
		}

		// Get the time used by player to make a move
        [XmlIgnore]
		public TimeSpan ThinkSpanTime
		{
			get
			{
				return TotalThinkingTime;	// returns back total think time of the user
			}
            set
            {
                TotalThinkingTime = value;
            }
		}

        /// <summary>
        /// Get or set the total think time in seconds
        /// </summary>
        public long ThinkSpanTimeInSeconds
        {
            get
            {
                return (long)TotalThinkingTime.TotalSeconds;	// returns back total think time of the user
            }
            set
            {
                TotalThinkingTime = new TimeSpan(0,0, (int)value);
            }
        }

		// Get user total think time in time format
		public string ThinkTime
		{
			get
			{
				string strThinkTime;

                // If the Start time is not yet set, initialize it
                if (StartingTime.Year == 1)
                    StartingTime = DateTime.Now;

				TimeSpan timespan = TotalThinkingTime+(DateTime.Now - StartingTime);
				strThinkTime =  timespan.Hours.ToString("00")+":"+timespan.Minutes.ToString("00")+":"+timespan.Seconds.ToString("00");
				return strThinkTime;	// returns back total think time of the user
			}
		}
		#endregion
	}
}
