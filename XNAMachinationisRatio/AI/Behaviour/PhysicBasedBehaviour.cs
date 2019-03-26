using System;
using Microsoft.Xna.Framework;

namespace XNAMachinationisRatio.AI.Behaviour {
    public class PhysicBasedBehaviour: IMovementBehaviour {

         public void Move(GameTime pGameTime,GameObject pObjectToMove, Vector3 pDesiredDirection) {
            float forceToApply = 40;
            pDesiredDirection.Normalize();
            pObjectToMove.GetPhysicsComponent().AddForce(Vector3.Multiply(pDesiredDirection, forceToApply));   
        }
    }
}
