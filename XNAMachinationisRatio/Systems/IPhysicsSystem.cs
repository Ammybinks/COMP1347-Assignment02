using System;
using Microsoft.Xna.Framework;

namespace XNAMachinationisRatio.Systems {
    interface IPhysicsSystem : IEngineSystem{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pGameTime"></param>
        void Update(ref GameTime pGameTime);
    }
}
