using System;
using System.Windows.Media;

namespace Ark.Animation { //.Pipes.Wpf {
    public class WpfTrigger : TriggerBase, IDisposable {
        public WpfTrigger() {
            CompositionTarget.Rendering += RenderingHandler;
        }

        void RenderingHandler(object sender, EventArgs e) {
            OnTriggered();
        }

        public void Dispose() {
            CompositionTarget.Rendering -= RenderingHandler;
        }
    }
}
