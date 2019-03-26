using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace XNAMachinationisRatio.Rendering {
    abstract class BaseCamera: GameObject , ICamera {
        protected GraphicsDevice mDevice;
        protected ISceneGraph mScene;


        public BaseCamera(string pCameraName, Vector3 pWorldPosition)
            : base(pCameraName, pWorldPosition) {
        }

        public virtual void Init(GraphicsDevice pDevice) {
            mDevice = pDevice;
        }
        public virtual void PresentScene(ISceneGraph pSceneGraph) {
            mScene = pSceneGraph;
        }

        public abstract void RenderScene();

        public abstract Vector3 CameraToWorld(Vector2 pScreenPosition);
    }
}
