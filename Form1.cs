using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace platform_game
{
    public partial class Form1 : Form
    {
        //motion and progress
        bool goLeft, goRight, jumping, isGameOver;
        int jumpSpeed;
        int force;
        int score = 0;
        int playerSpeed = 7;
        //platforms
        int horizontalSpeed = 5;
        int verticalSpeed = 3;
        //enemies
        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score " + score;
            //player gravity
            player.Top += jumpSpeed;
            //player speed acceleration
            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true)
            {
                player.Left += playerSpeed;
            }
            //player jump
            if (jumping == true && force < 0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
                jumping = -8;
                //how high player jumps
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }
            
            
            
            foreach (Control x in this.Controls)
            {

                if (x is PictureBox)
                {


                    if ((string)x.Tag == "platform")
                    {
                        //platform collision
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            //player jumps again
                            force = 8;
                            //player repositions on top of a platform
                            player.Top = x.Top - player.Height;
                        }
                        //platform is closest to viewer
                        //covers the players blinking side effect causes by the timer
                        x.BringToFront();


                    }



                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping == true)
            {
                jumping = false;
            }
            //option to reset while playing
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }

        private void RestartGame()
        {
            //reset motion and progress
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;
            txtScore.Text = "Score: " + score;
            //reset coins
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible == true;
                }
            }
            //reset position of player, platforms, and enemies
            player.Left = 23;
            player.Top = 642;
            enemyOne.Left = 266;
            enemyTwo.Left = 280;
            horizontalPlatform.Left = 385;
            verticalPlatform.Top = 546;
            gameTimer.Start();
        }
    }
}
