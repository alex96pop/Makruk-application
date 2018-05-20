using System;
using System.Collections;
using System.Xml;

namespace ChessLibrary
{
	/// <summary>
	/// The main chess board. Board contains the chess cells
	/// which will contain the chess pieces. Board also contains the methods
	/// to get and set the user moves.
	/// </summary>
    [Serializable]
	public class Board
	{
		private Side m_WhiteSide, m_BlackSide;	// Chess board site object 
		public Cells m_cells;	// collection of cells in the board
        public static double maxMoves; // max allowed moves until stalemale

		public Board()
		{
            m_WhiteSide = new Side(Side.SideType.White);	// Makde white side
            m_BlackSide = new Side(Side.SideType.Black);	// Makde white side

			m_cells = new Cells();					// Initialize the chess cells collection
		}

		// Initialize the chess board and place piece on thier initial positions
		public void Init()
		{
			m_cells.Clear();		// Remove any existing chess cells

			// Build the 64 chess board cells
			for (int row=1; row<=8; row++)
				for (int col=1; col<=8; col++)
				{
					m_cells.Add(new Cell(row,col));	// Initialize and add the new chess cell
				}

            // Now setup the board for black side
            m_cells["a1"].piece = new Piece(Piece.PieceType.Rook, m_BlackSide);
            m_cells["h1"].piece = new Piece(Piece.PieceType.Rook, m_BlackSide);
            m_cells["b1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide);
            m_cells["g1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide);
            m_cells["c1"].piece = new Piece(Piece.PieceType.Bishop, m_BlackSide);
            m_cells["f1"].piece = new Piece(Piece.PieceType.Bishop, m_BlackSide);
            m_cells["e1"].piece = new Piece(Piece.PieceType.King, m_BlackSide);
            m_cells["d1"].piece = new Piece(Piece.PieceType.Queen, m_BlackSide);
            for (int col = 1; col <= 8; col++)
                m_cells[3, col].piece = new Piece(Piece.PieceType.Pawn, m_BlackSide);

            // Now setup the board for white side
            m_cells["a8"].piece = new Piece(Piece.PieceType.Rook, m_WhiteSide);
            m_cells["h8"].piece = new Piece(Piece.PieceType.Rook, m_WhiteSide);
            m_cells["b8"].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide);
            m_cells["g8"].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide);
            m_cells["c8"].piece = new Piece(Piece.PieceType.Bishop, m_WhiteSide);
            m_cells["f8"].piece = new Piece(Piece.PieceType.Bishop, m_WhiteSide);
            m_cells["e8"].piece = new Piece(Piece.PieceType.King, m_WhiteSide);
            m_cells["d8"].piece = new Piece(Piece.PieceType.Queen, m_WhiteSide);
            for (int col = 1; col <= 8; col++)
                m_cells[6, col].piece = new Piece(Piece.PieceType.Pawn, m_WhiteSide);
        }

		// get the new item by rew and column
		public Cell this[int row, int col]
		{
			get
			{
				return m_cells[row, col];
			}
		}

		// get the new item by string location
		public Cell this[string strloc]
		{
			get
			{
				return m_cells[strloc];	
			}
		}

		// get the chess cell by given cell
		public Cell this[Cell cellobj]
		{
			get
			{
				return m_cells[cellobj.ToString()];	
			}
		}

        /// <summary>
        /// Serialize the Game object as XML String
        /// </summary>
        /// <returns>XML containing the Game object state XML</returns>
        public XmlNode XmlSerialize(XmlDocument xmlDoc)
        {
            XmlElement xmlBoard = xmlDoc.CreateElement("Board");

            // Append game state attributes
            xmlBoard.AppendChild(m_WhiteSide.XmlSerialize(xmlDoc));
            xmlBoard.AppendChild(m_BlackSide.XmlSerialize(xmlDoc));

            xmlBoard.AppendChild(m_cells.XmlSerialize(xmlDoc));

            // Return this as String
            return xmlBoard;
        }

        /// <summary>
        /// DeSerialize the Board object from XML String
        /// </summary>
        /// <returns>XML containing the Board object state XML</returns>
        public void XmlDeserialize(XmlNode xmlBoard)
        {
            // Deserialize the Sides XML
            XmlNode side = XMLHelper.GetFirstNodeByName(xmlBoard, "Side");

            // Deserialize the XML nodes
            m_WhiteSide.XmlDeserialize(side);
            m_BlackSide.XmlDeserialize(side.NextSibling);

            // Deserialize the Cells
            XmlNode xmlCells = XMLHelper.GetFirstNodeByName(xmlBoard, "Cells");
            m_cells.XmlDeserialize(xmlCells);
        }

		// get all the cell locations on the chess board
		public ArrayList GetAllCells()
		{
			ArrayList CellNames = new ArrayList();

			// Loop all the squars and store them in Array List
			for (int row=1; row<=8; row++)
				for (int col=1; col<=8; col++)
				{
					CellNames.Add(this[row,col].ToString()); // append the cell name to list
				}

			return CellNames;
		}

		// get all the cells containing pieces of given side
        public ArrayList GetSideCell(Side.SideType PlayerSide)
		{
			ArrayList CellNames = new ArrayList();

			// Loop all the squares and store them in Array List
			for (int row=1; row<=8; row++)
				for (int col=1; col<=8; col++)
				{
					// check and add the current type cell
					if (this[row,col].piece!=null && !this[row,col].IsEmpty() && this[row,col].piece.Side.type == PlayerSide)
						CellNames.Add(this[row,col].ToString()); // append the cell name to list
				}

			return CellNames;
		}

		// Returns the cell on the top of the given cell
		public Cell TopCell(Cell cell)
		{
			return this[cell.row-1, cell.col];
		}

		// Returns the cell on the left of the given cell
		public Cell LeftCell(Cell cell)
		{
			return this[cell.row, cell.col-1];
		}

		// Returns the cell on the right of the given cell
		public Cell RightCell(Cell cell)
		{
			return this[cell.row, cell.col+1];
		}

		// Returns the cell on the bottom of the given cell
		public Cell BottomCell(Cell cell)
		{
			return this[cell.row+1, cell.col];
		}

		// Returns the cell on the top-left of the current cell
		public Cell TopLeftCell(Cell cell)
		{
			return this[cell.row-1, cell.col-1];
		}

		// Returns the cell on the top-right of the current cell
		public Cell TopRightCell(Cell cell)
		{
			return this[cell.row-1, cell.col+1];
		}

		// Returns the cell on the bottom-left of the current cell
		public Cell BottomLeftCell(Cell cell)
		{
			return this[cell.row+1, cell.col-1];
		}

		// Returns the cell on the bottom-right of the current cell
		public Cell BottomRightCell(Cell cell)
		{
			return this[cell.row+1, cell.col+1];
		}

        // Verify conditions for white side to start counting down until stalemale
        public int VerifyCountforWhite()
        {
            // board verification starts at a1 (top left of the board)
            Cell newcell= m_cells["a1"];
            // counters for enemy rooks, bishops, knights, queens and all the pieces on the board
            int counterRook = 0, counterBishop = 0, counterKnight = 0, counterPieces = 0;

            // sum of total value of the white / black pieces
            int totalWhiteWeight=0, totalBlackWeight=0;

            // go through all the pieces to check for pawns.
            // if they exist, no stalemale can take place
            while(newcell!=null)
            {
                Cell newNewcell = newcell;
                while(newNewcell!=null)
                {
                    if (!newNewcell.IsEmpty())
                    {
                        // if any pawn is found no stalemale can happen
                        if (newNewcell.piece.IsPawn())
                            return -1;
                        // sum up all the player specific pieces to find who is at disatvantage
                        if (newNewcell.piece.Side.isWhite())
                            totalWhiteWeight += newNewcell.piece.GetWeight();
                        if (newNewcell.piece.Side.isBlack())
                            totalBlackWeight += newNewcell.piece.GetWeight();
                    }
                    newNewcell = this.RightCell(newNewcell);
                }
                newcell = this.BottomCell(newcell);
            }
            // if the side at advantage calls for stalemale countdown, it cannot happen
            if (totalWhiteWeight >= totalBlackWeight)
                return -1;

            newcell = m_cells["a1"];
            while (newcell != null)
            {
                Cell newNewcell = newcell;
                while (newNewcell != null)
                {
                    if (!newNewcell.IsEmpty())
                    {
                        counterPieces++; // count all the pieces on the board

                        if (newNewcell.piece.Side.isBlack() && newNewcell.piece.IsRook())
                            counterRook++; // count black rooks
                        if (newNewcell.piece.Side.isBlack() && newNewcell.piece.IsBishop())
                            counterBishop++;// count black bishops
                        if (newNewcell.piece.Side.isBlack() && newNewcell.piece.IsKnight())
                            counterKnight++;// count black knights
                    
                        // if there is any white piece other than the king, stalemale in 64 moves
                        if (newNewcell.piece.Side.isWhite() && newNewcell.piece.IsRook())
                            return 64;
                        if (newNewcell.piece.Side.isWhite() && newNewcell.piece.IsBishop())
                            return 64;
                        if (newNewcell.piece.Side.isWhite() && newNewcell.piece.IsKnight())
                            return 64;
                        if (newNewcell.piece.Side.isWhite() && newNewcell.piece.IsQueen())
                            return 64;
                    }
                    newNewcell = this.RightCell(newNewcell);
                }
                newcell = this.BottomCell(newcell);
            }

            // if there are 2 enemy rooks, stalemale in (8 minus number of pieces) moves
            if (counterRook == 2)
            {
                return 8 - counterPieces; 
            }
            // if there is 1 enemy rook, stalemale in (16 minus number of pieces) moves
            if (counterRook == 1)
            {
                return 16 - counterPieces;
            }
            // if there are 2 enemy bishops, stalemale in (22 minus number of pieces) moves
            if (counterBishop == 2)
            {
                return 22-counterPieces;
            }
            // if there are 2 enemy knights, stalemale in (32 minus number of pieces) moves
            if (counterKnight == 2)
            {
                return 32-counterPieces;
            }
            // if there is 1 enemy bishop, stalemale in (44 minus number of pieces) moves
            if (counterBishop == 1)
            {
                return 44-counterPieces;
            }
            // otherwise stalemale in (64 minus number of pieces) moves
            return 64-counterPieces;
        }

        // Verify conditions for black side to start counting down until stalemale
        public int VerifyCountforBlack()
        {
            // board verification starts at a1 (top left of the board)
            Cell newcell = m_cells["a1"];
            // counters for enemy rooks, bishops, knights, queens and all the pieces on the board
            int counterRook = 0, counterBishop = 0, counterKnight = 0, counterPieces = 0;

            // sum of total value of the white / black pieces
            int totalWhiteWeight = 0, totalBlackWeight = 0;

            // go through all the pieces to check for pawns.
            // if they exist, no stalemale can take place
            while (newcell != null)
            {
                Cell newNewcell = newcell;
                while (newNewcell != null)
                {
                    if (!newNewcell.IsEmpty())
                    {
                        // if any pawn is found no stalemale can happen
                        if (newNewcell.piece.IsPawn())
                            return -1;
                        // sum up all the player specific pieces to find who is at disatvantage
                        if (newNewcell.piece.Side.isWhite())
                            totalWhiteWeight += newNewcell.piece.GetWeight();
                        if (newNewcell.piece.Side.isBlack())
                            totalBlackWeight += newNewcell.piece.GetWeight();
                    }
                    newNewcell = this.RightCell(newNewcell);
                }
                newcell = this.BottomCell(newcell);
            }

            // if the side at advantage calls for stalemale countdown, it cannot happen
            if (totalBlackWeight >= totalWhiteWeight)
                return -1;

            newcell = m_cells["a1"];
            while (newcell != null)
            {
                Cell newNewcell = newcell;
                while (newNewcell != null)
                {
                    if (!newNewcell.IsEmpty())
                    {
                        counterPieces++; // count all the pieces on the board

                        if (newNewcell.piece.Side.isWhite() && newNewcell.piece.IsRook())
                            counterRook++; // count white rooks
                        if (newNewcell.piece.Side.isWhite() && newNewcell.piece.IsBishop())
                            counterBishop++;// count white bishops
                        if (newNewcell.piece.Side.isWhite() && newNewcell.piece.IsKnight())
                            counterKnight++;// count white knights

                        // if there is any white piece other than the king, stalemale in 64 moves
                        if (newNewcell.piece.Side.isBlack() && newNewcell.piece.IsRook())
                            return 64;
                        if (newNewcell.piece.Side.isBlack() && newNewcell.piece.IsBishop())
                            return 64;
                        if (newNewcell.piece.Side.isBlack() && newNewcell.piece.IsKnight())
                            return 64;
                        if (newNewcell.piece.Side.isBlack() && newNewcell.piece.IsQueen())
                            return 64;
                    }
                    newNewcell = this.RightCell(newNewcell);
                }
                newcell = this.BottomCell(newcell);
            }

            // if there are 2 enemy rooks, stalemale in (8 minus number of pieces) moves
            if (counterRook == 2)
            {
                return 8 - counterPieces;
            }
            // if there is 1 enemy rook, stalemale in (16 minus number of pieces) moves
            if (counterRook == 1)
            {
                return 16 - counterPieces;
            }
            // if there are 2 enemy bishops, stalemale in (22 minus number of pieces) moves
            if (counterBishop == 2)
            {
                return 22 - counterPieces;
            }
            // if there are 2 enemy knights, stalemale in (32 minus number of pieces) moves
            if (counterKnight == 2)
            {
                return 32 - counterPieces;
            }
            // if there is 1 enemy bishop, stalemale in (44 minus number of pieces) moves
            if (counterBishop == 1)
            {
                return 44 - counterPieces;
            }
            // otherwise stalemale in (64 minus number of pieces) moves
            return 64 - counterPieces;
        }
    }
}
