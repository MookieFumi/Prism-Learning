using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrismLearning.Controls
{
    public partial class Panel : ContentView
    {
        public static readonly BindableProperty MessageTextProperty = BindableProperty.Create(
            nameof(MessageText),
            typeof(object),
            typeof(Panel),
            null,
            BindingMode.OneWay);

        public static readonly BindableProperty AcceptTextProperty = BindableProperty.Create(
            nameof(AcceptText),
            typeof(object),
            typeof(Panel),
            null,
            BindingMode.OneWay);

        public static readonly BindableProperty CancelTextProperty = BindableProperty.Create(
            nameof(CancelText),
            typeof(object),
            typeof(Panel),
            null,
            BindingMode.OneWay);

        public static readonly BindableProperty AcceptCommandProperty = BindableProperty.Create(
            nameof(AcceptCommand),
            typeof(ICommand),
            typeof(Panel),
            null,
            BindingMode.OneWay);

        public static readonly BindableProperty CancelCommandProperty = BindableProperty.Create(
            nameof(CancelCommand),
            typeof(ICommand),
            typeof(Panel),
            null,
            BindingMode.OneWay);

        public Panel()
        {
            InitializeComponent();
        }

        public string MessageText
        {
            get => (string)GetValue(MessageTextProperty);
            set => SetValue(MessageTextProperty, value);
        }

        public string AcceptText
        {
            get => (string)GetValue(AcceptTextProperty);
            set => SetValue(AcceptTextProperty, value);
        }

        public string CancelText
        {
            get => (string)GetValue(CancelTextProperty);
            set => SetValue(CancelTextProperty, value);
        }

        public ICommand AcceptCommand
        {
            get => (ICommand)GetValue(AcceptCommandProperty);
            set => SetValue(AcceptCommandProperty, value);
        }

        public ICommand CancelCommand
        {
            get => (ICommand)GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(AcceptText):
                    _accept.Text = AcceptText;
                    break;
                case nameof(CancelText):
                    _cancel.Text = CancelText;
                    break;
                case nameof(MessageText):
                    _message.Text = MessageText;
                    break;
                case nameof(AcceptCommand):
                    _accept.Command = AcceptCommand;
                    break;
                case nameof(CancelCommand):
                    _cancel.Command = CancelCommand;
                    break;
            }
        }
    }
}
