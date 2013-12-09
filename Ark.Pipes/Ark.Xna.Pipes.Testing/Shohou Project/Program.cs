using Microsoft.Xna.Framework;

namespace Ark.Shohou {
    static class Program {
        static void Main(string[] args) {
            //Ark.Pipes.Tests.Test();

            //using (Game game = new TestGame1()) {
            //using (Game game = new TestGame2()) {
            using (Game game = new TestGame3()) {
                game.Run();
            }
        }
    }
}