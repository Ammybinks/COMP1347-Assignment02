using System;
using System.Collections.Generic;
using XNAMachinationisRatio.Systems;
using Microsoft.Xna.Framework;

namespace XNAMachinationisRatio.AI {
    class AISystem : IAISystem {
        private AISceneGraph mSceneGraph;

        public AISystem() {
            mSceneGraph = new AISceneGraph();
        }

        public void Assemble(IScene pScene) {
            
            pScene.AddSceneListener("AIScene",mSceneGraph);
        }

        public void Assemble(GameObject pGameObject) {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        public void Load(IScene pScene) {
            mSceneGraph = (AISceneGraph)pScene.GetRepresentation("AIScene");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pGameTime"></param>
        public void Update(ref GameTime pGameTime) {
            mSceneGraph.Update(ref pGameTime);
        }
    }
}
