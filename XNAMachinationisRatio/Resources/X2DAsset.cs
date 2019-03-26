using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNAMachinationisRatio.Resource {
    /// <summary>
    /// 2D Asset
    /// </summary>
    public class X2DAsset : GenericAsset {
        
        private Vector2 mOrigin;
        private Rectangle mImageDimension;
        private Texture2D mSpriteMap;
        private Vector2 mTopLeftCorner;


        public X2DAsset(String pAssetIdentifier, String pAssetName) : base(pAssetIdentifier,pAssetName){
        }
        
        /// <summary>
        /// Origin coordinates in UV coordinates.
        /// </summary>
        Vector2 UWOriginCoordinates { get { return mOrigin; } }

        #region External Interface Method
        /// <summary>
        /// Set origin at
        /// </summary>
        /// <param name="pU"></param>
        /// <param name="pV"></param>
        /// <returns></returns>
        public X2DAsset UVOriginAt(uint pU, uint pV) {
            mOrigin.X = pU;
            mOrigin.Y = pV;
            return this;
        }
        
        public X2DAsset UVTopLeftCornerAt(uint pX, uint pY) {
            mTopLeftCorner.X = pX;
            mTopLeftCorner.Y = pY;
            return this;
        }
        
        public X2DAsset Width(uint pWidth) {
            mImageDimension.Width = (int)pWidth;
            return this;
        }
        
        public X2DAsset Height(uint pHeight) {
            mImageDimension.Height = (int)pHeight;
            return this;
        }
        #endregion


        internal Texture2D SpriteMap { get { return mSpriteMap; } set { mSpriteMap = value; } }
        internal Vector2 UVOrigin { get { return mOrigin; } }
        internal Vector2 UVTopLeftCorner { get { return mTopLeftCorner; } }
        internal int SpriteWidth { get { return mImageDimension.Width; } }
        internal int SpriteHeight { get { return mImageDimension.Height; } }

    }
}
