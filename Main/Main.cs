﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GrblHeightProbe2
{
	public partial class Main : Form
	{
		Settings Set = Settings.Default;

		public Main()
		{
			InitializeComponent();
			HeightMapUpdated += Main_HeightMapUpdated;
			GRBL.OnLineReceived += GRBL_OnLineReceived_Console;
			GRBL.OnLineSent += GRBL_OnLineSent_Console;
		}

		void Main_HeightMapUpdated()
		{
			CurrentMap_RedrawPreview();
		}

		private void pictureBoxPreview_SizeChanged(object sender, EventArgs e)
		{
			CurrentMap_RedrawPreview();
		}

		private void Main_DragEnter(object sender, DragEventArgs e)
		{
			if(e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.All;
			}
		}

		private void Main_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				foreach(string filePath in (string[])e.Data.GetData(DataFormats.FileDrop))
				{
					if (!File.Exists(filePath))
						continue;

					if(new FileInfo(filePath).Extension == "hmap")
					{
						TryLoadHeightMap(filePath);
					}
					else
					{
						TryOpenGCode(filePath);
					}
				}
			}
		}

	}
}
