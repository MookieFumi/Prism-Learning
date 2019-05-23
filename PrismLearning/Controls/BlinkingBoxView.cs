using System;
using Xamarin.Forms;

namespace PrismLearning.Controls
{
    public class BlinkingBoxView : BoxView
    {
        volatile bool isBlinking;

        public static readonly BindableProperty BlinkProperty = BindableProperty.Create(
           nameof(Blink),
           typeof(bool),
           typeof(BlinkingBoxView),
           default(bool),
           BindingMode.OneWay);

        public bool Blink
        {
            get
            {
                return (bool)GetValue(BlinkProperty);
            }
            set
            {
                SetValue(BlinkProperty, value);
            }
        }

        public static readonly BindableProperty BlinkDurationProperty = BindableProperty.Create(
          nameof(BlinkDuration),
          typeof(uint),
          typeof(BlinkingBoxView),
          default(uint),
          BindingMode.OneWay);

        public uint BlinkDuration
        {
            get
            {
                return (uint)GetValue(BlinkDurationProperty);
            }
            set
            {
                SetValue(BlinkDurationProperty, value);
            }
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == BlinkProperty.PropertyName)
            {
                SetBlinking(Blink);
            }
            if (propertyName == BlinkDurationProperty.PropertyName)
            {
                if (isBlinking)
                {
                    SetBlinking(false);
                    SetBlinking(Blink);
                }
            }
        }

        void SetBlinking(bool shouldBlink)
        {
            if (shouldBlink && !isBlinking)
            {
                isBlinking = true;

                var blinkAnimation = new Animation(((d) =>
                {
                    Opacity = d;
                }), 0f, 1f, Easing.SinInOut);

                this.Animate("BlinkingBoxViewBlink", blinkAnimation, length: BlinkDuration, repeat: () => isBlinking);
            }
            else if (!shouldBlink && isBlinking)
            {
                isBlinking = false;
            }
        }
    }
}
