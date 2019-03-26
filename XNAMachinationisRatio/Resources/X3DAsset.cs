using System;
using Microsoft.Xna.Framework.Graphics;

namespace XNAMachinationisRatio.Resource {
    public class X3DAsset : GenericAsset {
        private Model mModel;
        public X3DAsset(String pAssetIdentifier, String pAssetName) : base(pAssetIdentifier,pAssetName) {
        }
        internal Model Model { get { return mModel; } set { mModel = value; } }
    }
}
