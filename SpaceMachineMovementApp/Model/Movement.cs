using SpaceMachineMovementApp.Interface;
using SpaceMachineMovementApp.Model.Enum;
using System.Collections.Generic;

namespace SpaceMachineMovementApp.Model
{
    public class Movement : IMovement
    {
        private IPlateu PlateuCoordinate { get; set; }
        private Direction Direction { get; set; }
        public bool arriveBorder { get; set; }
        private int XCoordinate { get; set; }
        private int YCoordinate { get; set; }
        public Movement(int X, int Y, Direction direction, Plateu plateu)
        {
            this.XCoordinate = X;
            this.YCoordinate = Y;
            this.Direction = direction;
            SetPlateu(plateu);
        }
        void SetPlateu(IPlateu plateu)
        {
            if (this.PlateuCoordinate == null)
                this.PlateuCoordinate = plateu;

        }
        public void MoveX(int stepForward)
        {
            if (Direction == Direction.E && this.PlateuCoordinate.GetXCornerCoordinates() == this.XCoordinate)
                this.arriveBorder = true;
            else
                this.XCoordinate = Direction == Direction.E ? (this.XCoordinate + stepForward) : (this.XCoordinate - stepForward);
        }
        public void MoveY(int stepForward)
        {
            if (Direction == Direction.N && this.PlateuCoordinate.GetYCornerCoordinates() == this.YCoordinate)
                this.arriveBorder = true;
            else
                this.YCoordinate = Direction == Direction.N ? (this.YCoordinate + stepForward) : (this.YCoordinate - stepForward);
        }
        public void Turn(char way)
        {
            this.Direction = way == 'L' ? (Direction)((int)(this.Direction + 3) % 4) :
                             way == 'R' ? (Direction)((int)(this.Direction + 1) % 4) :
                             this.Direction;
        }
        public string ApplyCommand(List<char> commands)
        {
            foreach (char command in commands)
            {
                if (command == 'M' && (Direction == Direction.E || Direction == Direction.W))
                    MoveX(1);
                else if (command == 'M' && (Direction == Direction.N || Direction == Direction.S))
                    MoveY(1);
                else
                    Turn(command);
            }
            return XCoordinate + " " + YCoordinate + " " + Direction;
        }
    }
}
