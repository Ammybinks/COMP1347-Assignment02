using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAMachinationisRatio.Resource;
namespace XNAMachinationisRatio.Rendering {
    internal class X3DCamera : BaseCamera, I3DCamera {
        private Matrix mView;
        private Vector3 mLookTarget;
        private float mFOV;
        private float mNearPlaneDistance;
        private float mFarPlaneDistance;
        
        private FillMode mFillMode;

        private Vector3 mCameraUp = Vector3.Up;
        private Matrix mProjection;

        public X3DCamera(string pCameraName, Vector3 pWorldPosition)
            : base(pCameraName, pWorldPosition) {
        }

        public override void Init(GraphicsDevice pDevice) {
            base.Init(pDevice);
        }
        public override Dimension Type { get { return Dimension.X3D; } }
        public override void RenderScene() {

            Vector3 position = this.Position;
            Matrix.CreateLookAt(ref position, ref mLookTarget, ref mCameraUp, out mView);
            mProjection = Matrix.CreatePerspectiveFieldOfView(mFOV, mDevice.Viewport.AspectRatio, mNearPlaneDistance, mFarPlaneDistance);

            RasterizerState s = new RasterizerState();
            s.FillMode = FillMode.Solid;
            mDevice.RasterizerState = s;

            mDevice.Clear(Color.AliceBlue);
            IList<IRenderableObject> objectList = mScene.GetVisibleSet(new Rectangle());
            X3DRenderableObject actualToken;
            foreach (IRenderableObject model in objectList) {
                actualToken = (X3DRenderableObject)model;
                foreach (ModelMesh mesh in ((X3DAsset)model.Asset).Model.Meshes) {

                    foreach (BasicEffect be in mesh.Effects) {
                        be.EnableDefaultLighting();
                        be.Projection = mProjection;
                        be.View = mView;
                        be.DiffuseColor = Color.White.ToVector3();
                        be.World = Matrix.CreateTranslation(model.WorldPosition);
                    }
                    mesh.Draw();
                }
            }
        }

        public I3DCamera LookAt(Vector3 pPosition) {
            mLookTarget = pPosition;
            return this;
        }
        public I3DCamera SetFOV(float pFOVAngle) {
            mFOV = pFOVAngle;
            return this;
        }
        public I3DCamera SetNearPlaneDistance(float pNearPlaneDistance) {
            mNearPlaneDistance = pNearPlaneDistance;
            return this;
        }
        public I3DCamera SetFarPlaneDistance(float pFarPlaneDistance) {
            mFarPlaneDistance = pFarPlaneDistance;
            return this;
        }
        public I3DCamera SetRasterState(FillMode pFillMode) {
            mFillMode = pFillMode;
            return this;
        }
        public override Vector3 CameraToWorld(Vector2 pScreenPosition) {
            Matrix ProjInv = Matrix.Invert(mProjection);
            // TODO : da implementares
            return new Vector3(0,0,0);
        }
    }
}
