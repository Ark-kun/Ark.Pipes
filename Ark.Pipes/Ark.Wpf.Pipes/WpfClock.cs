using System;
using System.Windows.Media;

namespace Ark.Animation.Pipes.Wpf {
    public class WpfClock : Clock, IDisposable {
        public WpfClock() {
            CompositionTarget.Rendering += RenderingHandler;
        }

        void RenderingHandler(object sender, EventArgs e) {
            OnTick();
        }

        public void Dispose() {
            CompositionTarget.Rendering -= RenderingHandler;
        }
    }
}
