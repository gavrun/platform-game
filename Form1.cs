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
                jumpSpeed = -8;
                //how high player jumps
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }
            
            
            //control rules for objects in game
            foreach (Control x in this.Controls)
            {
                //all defined by a type of object
                if (x is PictureBox)
                {

                    //platform
                    if ((string)x.Tag == "platform")
                    {
                        //platform collision
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            //player jumps again
                            force = 8;
                            //player repositions on top of a platform
                            player.Top = x.Top - player.Height;

                            if ((string)x.Name == "horizontalPlatform" && goLeft == false || (string)x.Name == "horizontalPlatform" && goRight == false) //fix, move player with the horizontal platform
                            {
                                player.Left -= horizontalSpeed;
                            }

                        }
                        //platform is closest to viewer
                        //covers the players blinking side effect causes by the timer
                        x.BringToFront();


                    }

                    //coin
                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true) //fix, increment score only once
                        {
                            //player collects a coin
                            x.Visible = false;
                            //score increases
                            score++;
                        }


                    }

                    //enemy
                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            //player looses when collides with enemy
                            gameTimer.Stop();
                            isGameOver = true;
                            //adds a message to score box
                            txtScore.Text = "Score: 0" + score + Environment.NewLine + "You were killed!"; //fix ??
                        }
                    }



                }
            }


            //horizontal platform motion
            horizontalPlatform.Left -= horizontalSpeed;
            //reverse motion when touch the form boundary
            if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width) //reverse motion by location ??
            {
                horizontalSpeed = -horizontalSpeed;
            }

            //vertical platform motion
            verticalPlatform.Top += verticalSpeed;
            //reverse motion by location
            if (verticalPlatform.Top < 150 || verticalPlatform.Top > 546)
            {
                verticalSpeed = -verticalSpeed;
            }


            //first enemy motion
            enemyOne.Left -= enemyOneSpeed;
            if (enemyOne.Left < pictureBox2.Left || enemyOne.Left + enemyOne.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }
            
            //second enemy motion opposite
            enemyTwo.Left += enemyTwoSpeed;
            if (enemyTwo.Left < pictureBox5.Left || enemyTwo.Left + enemyTwo.Width > pictureBox5.Left + pictureBox5.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            //player looses when jumps off the bottom (if botton not equals the form width)
            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You fell off!"
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
                    x.Visible = true;
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
