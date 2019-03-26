using System;
using System.Collections.Generic;
using XNAMachinationisRatio.Util;
using Microsoft.Xna.Framework;

namespace XNAMachinationisRatio.AI {
    class AISceneGraph : ListSceneRepresentation<IAIController> {
       
        public override void OnPlacedObject(GameObject pPlacedObject) {
            AddToScene(pPlacedObject.GetAIComponent());
        }

        public override void OnRemovedObject(GameObject pPlacedObject) {
            RemoveFromScene(pPlacedObject.GetAIComponent());
        }

        public void Update(ref GameTime pGameTime) {
            //foreach (IAIController controller in mList) {
                IAIController controller;
                for (int i = 0 ; i < mList.Count ; i++)
                {
                    controller = mList[i];
                    controller.Update(ref pGameTime);
                }
        }
    }
}
