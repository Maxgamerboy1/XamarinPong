using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamarinPong
{
    public class GameState : INotifyPropertyChanged
    {
        private double __BallXPos;
        private double __BallYPos;
        private double __LeftPaddleYPos;
        private double __RightPaddleYPos;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public double BallXPos
        {
            get => __BallXPos;
            set
            {
                __BallXPos = value;
                OnPropertyChanged();
            }
        }

        public double BallYPos
        {
            get => __BallYPos;
            set
            {
                __BallYPos = value;
                OnPropertyChanged();
            }
        }

        public double LeftPaddleYPos
        {
            get => __LeftPaddleYPos;
            set
            {
                __LeftPaddleYPos = value;
                OnPropertyChanged();
            }
        }

        public double RightPaddleYPos
        {
            get => __RightPaddleYPos;
            set
            {
                __RightPaddleYPos = value;
                OnPropertyChanged();
            }
        }
    }
}
