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

        bool goLeft, goRight, jumping, isGameOver;

        int jumpSpeed;
        int force;
        int score = 0;
        int playerSpeed = 7;

        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;



        public Form1()
        {
            InitializeComponent();
        }




        private void MainGameTimerEvent(object sender, EventArgs e)
        {

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {

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
