using Microsoft.Xna.Framework.Graphics;
public class SharedSpriteBatch : SpriteBatch {
    int level = 0;
    object lockObject = new object();

    public SharedSpriteBatch(GraphicsDevice device)
        : base(device) {
    }

    public void SharedBegin() {
        lock (lockObject) {
            if (level == 0) {
                this.Begin();
            }
            level++;
        }
    }
    public void SharedEnd() {
        lock (lockObject) {
            level--;
            if (level == 0) {
                this.End();
            }
        }
    }

}