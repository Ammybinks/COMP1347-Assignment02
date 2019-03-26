using System;
using Microsoft.Xna.Framework;

namespace XNAMachinationisRatio.Physics {
    public interface IPhysicsActuator {
        void AddForce(Vector3 pForce);
    }
}
