using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Chess
{
	/// <summary>
	/// Summary description for CaptureBar.
	/// </summary>
	public class CaptureBar : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private ArrayList Images;		// List of images to display
		private ArrayList Squares;	// Picture control array for storing the place holders
		public Images ChessImages;	// Contains reference of chess images

		public CaptureBar()
		{
			InitializeComponent();
			Squares = new ArrayList();
			Images = new ArrayList();
		}

		public void InitializeBar(Images ImagesList)
		{
			ChessImages = ImagesList;

			
			for (int col=1; col<=12; col++)	// repeat for every column in the chess board row
			{
				Square ChessSquare = new Square(col, 1, null);
				//ChessSquare.SetBackgroundSquare(ChessImages);	// Set the chess square background
				
				if ((col)%2==0) // White cell
					ChessSquare.BackgroundImage = ImagesList["White2"];
				else
					ChessSquare.BackgroundImage = ImagesList["Black2"];
				Squares.Add(ChessSquare);
				this.Controls.Add(ChessSquare);
			}

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// CaptureBar
			// 
			this.Name = "CaptureBar";
			this.Size = new System.Drawing.Size(664, 55);

		}

		// Add the captured piece image to display list
		public void Add(Image CapturedImage)
		{
			Images.Add(CapturedImage);
			RefreshList();		// Redraw image list
		}

		// Remove the last captured piece image from the display list
		public void RemoveLast()
		{
			if (Images.Count > 0)
				Images.RemoveAt(Images.Count-1);	// Remove last added piece
			RefreshList();		// Redraw image list
		}

		// Clear the image list
		public void Clear()
		{
			Images.Clear();
			RefreshList();		// Redraw image list
		}

		// Redraw the image list
		public void RefreshList()
		{
			foreach (Square sqr in Squares)
			{
				sqr.DrawPiece(null);
			}

			// Draw last added images in the available list
			int iStart=0, iIndex=0;

			if (Images.Count > Squares.Count)
				iStart= Images.Count-Squares.Count;

			for (;iStart<Images.Count; iStart++)
			{
				Square sqr = (Square)Squares[iIndex++];
				sqr.DrawPiece( (Image) Images[iStart]);
			}
		}

		#endregion
	}
}
