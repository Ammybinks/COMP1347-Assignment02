using System;

using Microsoft.Xna.Framework;
using XNAMachinationisRatio.Resource;

namespace XNAMachinationisRatio.Rendering {
    abstract class RenderableObject : GameObjectComponent, IRenderableObject {
        protected String mAssetID = null;
        protected float mScaleFactor;

        public RenderableObject(String pAssetID) : base() {
            mAssetID = pAssetID;
            mScaleFactor = 1.0f;
        }
       
        public String AssetID { get { return mAssetID; } set { mAssetID = value; } }

        public float Scale { get { return mScaleFactor;} set { mScaleFactor = value; } }


        public abstract GenericAsset Asset { get; set; }
    }
}
