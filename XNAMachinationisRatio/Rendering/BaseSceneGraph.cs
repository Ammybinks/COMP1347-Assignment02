using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAMachinationisRatio.Util;
using XNAMachinationisRatio.Systems;

namespace XNAMachinationisRatio.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    class BaseSceneGraph : ListSceneRepresentation<IRenderableObject>,ISceneGraph
    {
       
        private IResourceManager mResourceManager;
        private IRenderingSystem mRenderingSystem;

        public BaseSceneGraph(IResourceManager pResourceManager, IRenderingSystem pRenderingSystem)
        {
            mRenderingSystem = pRenderingSystem;
            mResourceManager = pResourceManager;
          
        }
        
        public override void OnPlacedObject(GameObject pPlacedObject) {
            if (pPlacedObject.GetType() == typeof(X2DCamera)) {
                mRenderingSystem.SetCamera((X2DCamera)pPlacedObject);
            }
            if (pPlacedObject.GetType() == typeof(X3DCamera)) {
                mRenderingSystem.SetCamera((X3DCamera)pPlacedObject);
            }
            if (pPlacedObject.IsRenderable()) {
                this.AddToScene(pPlacedObject.GetGraphicComponent());
            }
        }

        public override void OnRemovedObject(GameObject pRemovedObject) {
            if (pRemovedObject.IsRenderable()) {
                this.RemoveFromScene(pRemovedObject.GetGraphicComponent());
            }
        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pRenderableObject"></param>
        public override void AddToScene(IRenderableObject pRenderableObject)
        {
            if (!mList.Contains(pRenderableObject))
            {
                int i = 0;
                for (i = 0; i < mList.Count; i++) {
                    if (!(mList.ElementAt(i).WorldPosition.Z < pRenderableObject.WorldPosition.Z)) {
                        break;
                    }
                }
                mList.Insert(i, pRenderableObject);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pVisibleArea"></param>
        /// <returns></returns>
        public List<IRenderableObject> GetVisibleSet(Rectangle pVisibleArea)
        {
            return mList.ToList();
        }

    }
}
