using System;
using Microsoft.Xna.Framework;

namespace XNAMachinationisRatio.AI {
    public interface IMovementBehaviour {
        /// <summary>
        /// Move the GameObject in the Desired Direction
        /// </summary>
        /// <param name="pGameTime">Game time</param>
        /// <param name="pObjectToMove">Object to move</param>
        /// <param name="pDesiredDirection">Desired direction</param>
        void Move(GameTime pGameTime,GameObject pObjectToMove, Vector3 pDesiredDirection);
    }
}
