using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace XNAMachinationisRatio.Rendering {
    internal class X2DCamera : BaseCamera, I2DCamera {

        private Vector3 mTopLeftCornerWorldPosition;
        private SpriteBatch mBatch;
        private Rectangle mVisibleField;

        public X2DCamera(string pCameraName, Vector3 pWorldPosition)
            : base(pCameraName, pWorldPosition) {
        }

        public override void Init(GraphicsDevice pDevice) {
            base.Init(pDevice);
            mVisibleField = new Rectangle((int)mTopLeftCornerWorldPosition.X, (int)mTopLeftCornerWorldPosition.Y, pDevice.Viewport.Width, pDevice.Viewport.Height);
            mBatch = new SpriteBatch(mDevice);
        }

        public override void RenderScene() {
            mTopLeftCornerWorldPosition = new Vector3(base.Position.X - (mDevice.Viewport.Width / 2), base.Position.Y + (mDevice.Viewport.Height / 2), base.Position.Z);
            IList<IRenderableObject> visibleList = mScene.GetVisibleSet(mVisibleField);
            mDevice.Clear(Color.CornflowerBlue);

            mBatch.Begin();
            // render opaque near to far
            // render skybox
            // render transparent
            foreach (IRenderableObject element in visibleList) {
                I2DRenderableObject render2d = (I2DRenderableObject)element;
                // Mach : I assume all token assets are created facing in the right direction.
                if (render2d.Orientation.X < 0) {
                    //Adds a sprite to a batch of sprites for rendering using the specified 
                    // texture, position, source rectangle, color, rotation, origin, scale, effects and layer.
                    mBatch.Draw(render2d.SpriteMap, WorldToCameraSpace(element), render2d.SourceRegion, Color.White, 0.0f, render2d.Origin, element.Scale, SpriteEffects.FlipHorizontally, 1);
                }
                else {
                    mBatch.Draw(render2d.SpriteMap, WorldToCameraSpace(element), render2d.SourceRegion, Color.White, 0.0f, render2d.Origin, element.Scale, SpriteEffects.None, 1);
                }

            }
            // SpriteBatch is in Deferred Mode, all sprites are drawn only when End method is called.
            mBatch.End();
        }

        private Vector2 WorldToCameraSpace(IRenderableObject pRenderable) {
            Vector3 cameraBaseVector = Vector3.Subtract(pRenderable.WorldPosition, mTopLeftCornerWorldPosition);
            return new Vector2(cameraBaseVector.X, -cameraBaseVector.Y); ;
        }
        public override Vector3 CameraToWorld(Vector2 pScreenPosition) {
            return Vector3.Add(mTopLeftCornerWorldPosition, new Vector3(pScreenPosition.X,-pScreenPosition.Y,0));
        }
        public override Dimension Type { get { return Dimension.X2D; } }
    }
}