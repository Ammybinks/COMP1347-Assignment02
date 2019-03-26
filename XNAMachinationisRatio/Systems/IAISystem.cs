using System;
using Microsoft.Xna.Framework;

namespace XNAMachinationisRatio.Systems {
    interface IAISystem : IEngineSystem {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pGameTime"></param>
        void Update(ref GameTime pGameTime);
    }
}
