using System;
using Microsoft.Xna.Framework;
using XNAMachinationisRatio.Physics;

namespace XNAMachinationisRatio {
    public interface IAIController : IGameObjectComponent , ICollisionListener {
        void Update(ref GameTime pGameTime);
    }
}
