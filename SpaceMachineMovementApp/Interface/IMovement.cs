using System.Collections.Generic;

namespace SpaceMachineMovementApp.Interface
{
    interface IMovement
    {
        void MoveX(int stepForward);
        void MoveY(int stepForward);
        void Turn(char way);
        string ApplyCommand(List<char> commands);
    }
}
