using System;
using System.StubHelpers;
using Microsoft.Xna.Framework;
using XNAMachinationisRatio.AI;

namespace XNAMachinationisRatio.AI.Behaviour {
    /// <summary>
    /// Base Movement Behaviour move an object according to position and delta position as desired direction
    /// </summary>
    public class BaseMovementBehaviour : IMovementBehaviour {
        /// <summary>
        /// Move a GameObject in a desired direction, updating his position
        /// </summary>
        /// <param name="pTime">Game Time</param>
        /// <param name="pObjectToMove">Object to move</param>
        /// <param name="pDesiredDirection">Desired Direction</param>
        public void Move(GameTime pGameTime,GameObject pObjectToMove, Vector3 pDesiredDirection) {
            pObjectToMove.Orientation = pDesiredDirection;
            pObjectToMove.Position = Vector3.Add(pObjectToMove.Position, pDesiredDirection);
        }
    }
}
