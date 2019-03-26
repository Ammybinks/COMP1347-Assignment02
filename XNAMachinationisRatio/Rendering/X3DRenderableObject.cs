using System;
using Microsoft.Xna.Framework.Graphics;
using XNAMachinationisRatio.Resource;

namespace XNAMachinationisRatio.Rendering {
    class X3DRenderableObject : RenderableObject, I3DRenderableObject {
        private X3DAsset mAsset = null;
        

        public X3DRenderableObject(String pAssetID) : base(pAssetID) {
           
        }
        

        public override GenericAsset Asset { get { return mAsset; } set { mAsset = (X3DAsset)value; } }
        
        public Model Model { 
            get { 
                return mAsset.Model;
            }
        }
    }
}
