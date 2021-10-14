using SpaceMachineMovementApp.Interface;

namespace SpaceMachineMovementApp.Model
{
    public class Plateu:IPlateu
    {
        private int EdgeXCoordinate { get; set; }
        private int EdgeYCoordinate { get; set; }
        public Plateu(int X, int Y)
        {
            this.EdgeXCoordinate = X;
            this.EdgeYCoordinate = Y;
        }

        public int GetXCornerCoordinates() => EdgeXCoordinate;
        public int GetYCornerCoordinates() => EdgeYCoordinate;
    }
}
