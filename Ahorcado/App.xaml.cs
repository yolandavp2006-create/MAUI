using Microsoft.Extensions.DependencyInjection;

namespace Práctica_2._Ahorcado {
    public partial class App : Application {
        public App() {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState) {
            return new Window(new PantallaAhorcado());
        }
    }
}