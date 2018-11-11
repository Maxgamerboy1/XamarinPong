using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace XamarinPong
{
    public class Paddle : ContentView
    {

        public static readonly BindableProperty PaddleColorProperty = BindableProperty.Create(nameof(PaddleColor), typeof(Color), typeof(Paddle), Color.Orange);
        public static readonly BindableProperty IsLeftPaddleProperty = BindableProperty.Create(nameof(IsLeftPaddle), typeof(bool), typeof(Paddle), false);
        public static readonly BindableProperty TranslatedYProperty = BindableProperty.Create(nameof(TranslatedY), typeof(double), typeof(Paddle), 0D, BindingMode.OneWayToSource);


        public Paddle()
        {
            ThePaddle = new BoxView
            {
                Color = PaddleColor,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center
            };
            BackgroundColor = Color.Transparent;
            GestureRecognizers.Add(new PaddleDraggedRecognizer());
            Content = ThePaddle;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(PaddleColor))
            {
                ThePaddle.Color = PaddleColor;
            }
            else if (propertyName == nameof(HeightRequest))
            {
                ThePaddle.HeightRequest = HeightRequest;
            }
            else if (propertyName == nameof(WidthRequest))
            {
                ThePaddle.WidthRequest = WidthRequest;
            }
            else if (propertyName == nameof(IsLeftPaddle))
            {
                ThePaddle.HorizontalOptions = IsLeftPaddle ? LayoutOptions.End : LayoutOptions.Start;
            }
            else
            {
                base.OnPropertyChanged(propertyName);
            }
        }

        public bool IsLeftPaddle { get => (bool)GetValue(IsLeftPaddleProperty); set => SetValue(IsLeftPaddleProperty, value); }
        public Color PaddleColor { get => (Color)GetValue(PaddleColorProperty); set => SetValue(PaddleColorProperty, value); }
        public double PaddleYOrigin => ThePaddle.Y;

        private BoxView ThePaddle { get; set; }
        public double TranslatedY { get => (double)GetValue(TranslatedYProperty); set => SetValue(TranslatedYProperty, value); }


        class PaddleDraggedRecognizer : PanGestureRecognizer
        {
            public PaddleDraggedRecognizer()
            {
                PanUpdated += PaddleDragged;
            }

            private void PaddleDragged(object sender, PanUpdatedEventArgs e)
            {
                if (sender is Paddle _Paddle)
                {
                    switch (e.StatusType)
                    {
                        case GestureStatus.Started:
                            break;
                        case GestureStatus.Running:
                            _Paddle.ThePaddle.TranslationY = _Paddle.TranslatedY + e.TotalY;
                            break;
                        case GestureStatus.Completed:
                            _Paddle.TranslatedY = _Paddle.ThePaddle.TranslationY;
                            break;
                        case GestureStatus.Canceled:
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }


}
