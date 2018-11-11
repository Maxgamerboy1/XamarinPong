using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinPong
{
    public partial class MainPage : ContentPage
    {
        private Timer gameTimer;
        private double ballXSpeed = 1.75;
        private double ballYSpeed = 1.75;
        private Random rng;

        public MainPage()
        {
            GameState = new GameState();
            gameTimer = new Timer(UpdateGame, GameState, 300, 16);
            rng = new Random();
            InitializeComponent();
            BindingContext = this;
        }

        private void UpdateGame(object state)
        {
            if (state is GameState _GameState)
            {
                UpdateBall(_GameState);
            }
        }

        private void UpdateBall(GameState gameState)
        {
            // Check if hit top or bottom of screen
            if (gameState.BallYPos + ball.Y + ball.Width < Y || gameState.BallYPos + ball.Y + ball.Width > Height)
            {
                // Invert the ball's y-direction
                ballYSpeed *= -1;
            }
            double _PaddleWidth = (double)Resources["paddleWidthD"];
            double _GoalWidth = (double)Resources["scoreZoneWidthD"];
            double _ActualBallXPos = gameState.BallXPos + ball.X;
            double _ActualBallYPos = gameState.BallYPos + ball.Y;
            //Check for PlayerOne's paddle collision with ball
            if (_ActualBallXPos < (_GoalWidth + _PaddleWidth) &&
                _ActualBallYPos >= leftPaddle.PaddleYOrigin + gameState.LeftPaddleYPos &&
                _ActualBallYPos <= (leftPaddle.PaddleYOrigin + gameState.LeftPaddleYPos + leftPaddle.HeightRequest))
            {
                // Invert ball's x direction
                ballXSpeed *= -1;

                //increase speed
                ballXSpeed *= 1.25;

                //Set new scale for YAxis for random incoming angle
                ballYSpeed *= 1 + rng.NextDouble();
            }

            //Check for PlayerOne's paddle collision with ball
            if (_ActualBallXPos > (Width - _GoalWidth - _PaddleWidth - ball.Width) &&
                _ActualBallYPos >= rightPaddle.PaddleYOrigin + gameState.RightPaddleYPos &&
                _ActualBallYPos <= (rightPaddle.PaddleYOrigin + gameState.RightPaddleYPos + rightPaddle.HeightRequest))
            {
                // Invert ball's x direction
                ballXSpeed *= -1;

                //increase speed
                ballXSpeed *= 1.25;

                //Set new scale for YAxis for random incoming angle
                ballYSpeed /= 1 + rng.NextDouble();
            }
            gameState.BallXPos += ballXSpeed;
            gameState.BallYPos += ballYSpeed;
        }

        public GameState GameState { get; set; }
    }
}
