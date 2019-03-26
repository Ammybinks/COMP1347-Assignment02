using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAMachinationisRatio.Resource;

namespace XNAMachinationisRatio.Rendering {
    class X2DRenderableObject : RenderableObject , I2DRenderableObject {
       
        private X2DAsset mAsset = null;
        private Vector3 mFacingDirection;


        public X2DRenderableObject(String pAssetID) : base(pAssetID) {
            mFacingDirection = new Vector3(1,0,0);
        }

       
        public override GenericAsset Asset { get { return mAsset; } set { mAsset = (X2DAsset)value; } }

        public Texture2D SpriteMap {
            get {
                return mAsset.SpriteMap;
            }
        }

        public Rectangle SourceRegion {
            get {
                return new Rectangle((int)mAsset.UVTopLeftCorner.X, (int)mAsset.UVTopLeftCorner.Y, mAsset.SpriteWidth, mAsset.SpriteHeight);
            }
        }

        public Vector2 Origin {
            get {
                return mAsset.UVOrigin;
            }
        }

        public Vector3 Orientation {
            get {
                return Self.Orientation;
            }
        }

    }
}
