using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using XNAMachinationisRatio.Systems;

namespace XNAMachinationisRatio.Rendering
{
    class RenderingSystem : IRenderingSystem  
    {
        private GraphicsDeviceManager mDeviceManager = null;
        private ISceneGraph mSceneGraph = null;
        private ICamera mCamera = null;
        private IResourceManager mResourceManager;

        /// <summary>
        /// Perform Rendering System Initialization
        /// </summary>
        /// <param name="pDeviceManager">GraphicsDeviceManager</param>
        public void Init(GraphicsDeviceManager pDeviceManager, IResourceManager pResourceManager) {
            mResourceManager = pResourceManager;
            mDeviceManager = pDeviceManager;
        }

        public void Assemble(IScene pScene) {
            if(Dimension.X2D == pScene.Dimension) 
                pScene.AddSceneListener("2DGraphicScene",new BaseSceneGraph(mResourceManager,this));
            if(Dimension.X3D == pScene.Dimension)
                pScene.AddSceneListener("3DGraphicScene", new BaseSceneGraph(mResourceManager, this));
        }

        public void Assemble(GameObject pGameObject) {
            if(Dimension.X2D == pGameObject.Type)
                // inject a graphic component on token
                pGameObject.Inject(ComponentType.GRAPHICS, new X2DRenderableObject("NoAssetSpecifed"));
            else
                pGameObject.Inject(ComponentType.GRAPHICS, new X3DRenderableObject("NoAssetSpecified"));
        }

        public void Load(IScene pScene) {
            Debug.Assert(null != mCamera, "No camera. A Camera shall be placed in the scene");
            mSceneGraph = (ISceneGraph)pScene.GetRepresentation("2DGraphicScene");
            mCamera.PresentScene(mSceneGraph);
        }

        public void SetCamera(ICamera pCamera) {
            Debug.Assert(null != pCamera, "Invalid Parameter, trying to set a null camera");
            mCamera = pCamera;
            mCamera.Init(mDeviceManager.GraphicsDevice);
        }

       
        /// <summary>
        /// Render the scene
        /// </summary>
        /// <param name="pSimTime">Time structure</param>
        public void Render(ref GameTime pSimTime) {
            if (mCamera != null)
                mCamera.RenderScene();
            else {
                mDeviceManager.GraphicsDevice.Clear(Color.Black);
            }
        }


    }
}
