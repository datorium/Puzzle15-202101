﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle15_202101
{
    public partial class Puzzle : Form
    {

        List<Button> tiles = new List<Button>();
        List<Point> initialLocations = new List<Point>(); 
        Random rand = new Random();

        public Puzzle()
        {
            InitializeComponent();
            InitializePuzzle();
            ShuffleTiles();
        }

        private void InitializePuzzle()
        {
            int tileCounter = 1;
            Button tile = null;
            for(int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    tile = new Button();
                    tile.BackColor = Color.SteelBlue;
                    tile.Font = new Font(tile.Font.FontFamily, 22);
                    tile.ForeColor = Color.White;
                    tile.FlatStyle = FlatStyle.Flat;
                    tile.Width = 80;
                    tile.Height = 80;
                    tile.Top = 20 + j * 90;
                    tile.Left = 20 + i * 90;
                    initialLocations.Add(tile.Location);

                    tile.Click += Tile_Click;


                    if (tileCounter == 16)
                    {
                        tile.Text = string.Empty;
                        tile.Name = "TileEmpty";
                    }
                    else
                    {
                        tile.Text = tileCounter.ToString();
                    }

                    this.Controls.Add(tile);
                    tiles.Add(tile);

                    tileCounter++;
                }
            }
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            Button tile = (Button)sender;
            if (CanSwap(tile))
            {
                SwapTiles(tile);
                CheckForWin();
            }            
        }
        
        private void SwapTiles(Button tile)
        {
            Button tileEmpty = (Button)this.Controls["TileEmpty"];

            Point tileOldLocation = tile.Location;
            tile.Location = tileEmpty.Location;
            tileEmpty.Location = tileOldLocation;
        }

        private void ShuffleTiles()
        {
            for(int i = 0; i < 100; i++)
            {
                SwapTiles(tiles[rand.Next(0, 15)]);
            }
        }

        private bool CanSwap(Button tile)
        {
            double a = 0, b = 0, c = 0;
            Button tileEmpty = (Button)this.Controls["TileEmpty"];

            a = (double)(tileEmpty.Left - tile.Left);
            b = (double)(tileEmpty.Top - tile.Top);
            c = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));

            if(c <= 90)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        private void CheckForWin()
        {
            bool check = true;
            for(int i = 0; i < 16; i++)
            {
                check = check & (tiles[i].Location == initialLocations[i]);
            }

            if (check)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            MessageBox.Show("You win!");
        }
    }
}
