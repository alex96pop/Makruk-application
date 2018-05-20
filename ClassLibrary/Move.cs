using System;

namespace ChessLibrary
{
	/// <summary>
	/// This class stores info about a single chess game move.
	/// It contains source and target chess squars/cells and also the type
	/// of move and related info.
	/// </summary>
    [Serializable]
	public class Move
	{
		public enum MoveType {NormalMove, CaputreMove, TowerMove, PromotionMove, EnPassant};	// Type of the move

		private Cell StartingCell;	// start cell
		private Cell EndingCell;		// end cell
		private Piece PieceValue;			// Piece being moved
		private Piece CapturePiece;	// Piece captured by this mov
		private Piece PromotedPiece;		// Piece selected after pawn promotion
		private Piece EnPessantPiece;	// Piece captured during enpassant move
		private MoveType MovType;		// Type of the move
		private bool CheckCause;		// if cause or leave the user under check
		private int	MoveScore;			// Score of the move from the board analyze routine

        // Emptry internal constructor for XML Serialization support
        internal Move()
        {
            MoveScore = 0;
        }

		public Move(Cell begin, Cell end)
		{
			StartingCell=begin;
			EndingCell=end;
			PieceValue=begin.piece;
			CapturePiece=end.piece;
			MoveScore=0;
		}

		// Returns the move start cell
		public Cell StartCell
		{
			get
			{
				return StartingCell;
			}
            set
            {
                StartingCell = value;
            }
		}

		// Returns the move end cell
		public Cell EndCell
		{
			get
			{
				return EndingCell;
			}
            set
            {
                EndingCell = value;
            }
		}

		// Returns the piece which was moved
		public Piece Piece
		{
			get
			{
				return PieceValue;
			}
            set
            {
                PieceValue = value;
            }
		}

		// Returns the captured piece 
		public Piece CapturedPiece
		{
			get
			{
				return CapturePiece;
			}
            set
            {
                CapturePiece = value;
            }
		}

		// Get and Set the move type property
		public MoveType Type
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

		// Return true if the move when executed, place or leave user under check
		public bool CauseCheck
		{
			get
			{
				return CheckCause;
			}
			set
			{
				CheckCause=value;
			}
		}

		// Set and get the promo piece
		public Piece PromoPiece
		{
			get
			{
				return PromotedPiece;
			}
			set
			{
				PromotedPiece=value;
			}
		}

		// Set and get the EnPassant piece
		public Piece EnPassantPiece
		{
			get
			{
				return EnPessantPiece;
			}
			set
			{
				EnPessantPiece=value;
			}
		}

		// Set and get the move Score
		public int Score
		{
			get
			{
				return MoveScore;
			}
			set
			{
				MoveScore=value;
			}
		}

		// Return true if the move was promo move
		public bool IsPromoMove()
		{
			return MovType==MoveType.PromotionMove;
		}

		// Return true if the move was capture move
		public bool IsCaptureMove()
		{
			return MovType==MoveType.CaputreMove;
		}

		//Return a descriptive move text
		public override string ToString()
		{
			if (MovType == Move.MoveType.CaputreMove)	// It's a capture move
				return PieceValue + " " + StartingCell.ToString2() + "x" + EndingCell.ToString2();
			else
				return PieceValue + " " + StartingCell.ToString2() + "-" + EndingCell.ToString2();
		}
	}

	// This class is used to compare two Move type objects
	public class MoveCompare : System.Collections.IComparer
	{
		// Empty constructore
		public MoveCompare()
		{
		}

		public int Compare(Object firstObj, Object SecondObj)
		{
			Move firstMove = (Move)firstObj;
			Move secondMove = (Move)SecondObj;

			return -firstMove.Score.CompareTo(secondMove.Score);
		}
	}
}
