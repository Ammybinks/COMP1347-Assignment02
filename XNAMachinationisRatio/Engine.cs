using System;
using System.Collections.Generic;
using System.Diagnostics;
using XNAMachinationisRatio.Resource;
using XNAMachinationisRatio.Physics;
using XNAMachinationisRatio.Rendering;
using XNAMachinationisRatio.Systems;
using XNAMachinationisRatio.AI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace XNAMachinationisRatio
{
    public enum Dimension {
        X2D,
        X3D
    }
    /// <summary>
    /// Engine is responsible to handle all
    /// </summary>
    public class Engine {
        private IResourceManager mResourceManager = null;
        private IAISystem mAISystem = null;
        private IRenderingSystem mRenderingSystem = null;
        private IPhysicsSystem mPhysicsSystem = null;

        /// <summary>
        /// Singletone instance
        /// </summary>
        private static Engine mSingletonInstance = new Engine();

        /// <summary>
        /// Ctor
        /// </summary>
        private Engine() {
            mRenderingSystem = new RenderingSystem();
            mAISystem = new AISystem();
            mPhysicsSystem = new PhysicsSystem();
        }
        
        /// <summary>
        /// Singleton method to access the unique instance of the engine
        /// </summary>
        /// <returns>Engine instance</returns>
        public static Engine getInstance() {
            return mSingletonInstance;
        }

        /// <summary>
        /// Initialize the Game Engine
        /// </summary>
        /// <param name="pDeviceManager">GraphicsDeviceManager previously associated to the Game class</param>
        /// <param name="pContentManager">Content Manager</param>
        public void Init(GraphicsDeviceManager pDeviceManager, ContentManager pContentManager) {
            Debug.Assert(pDeviceManager != null, "Device Manager cannot be null");
            Debug.Assert(pContentManager != null, "Content Manager cannot be null");
            
            mResourceManager = new ResourceManager(pContentManager);
            mRenderingSystem.Init(pDeviceManager, mResourceManager);
        }

        /// <summary>
        /// Update Loop
        /// </summary>
        /// <param name="pGameTime">Game Time</param>
        public void Update(ref GameTime pGameTime) {
            mAISystem.Update(ref pGameTime);
            mPhysicsSystem.Update(ref pGameTime);
        }

        /// <summary>
        /// Render the scene
        /// </summary>
        /// <param name="pGameTime">Game time</param>
        public void Render(ref GameTime pGameTime) {
            mRenderingSystem.Render(ref pGameTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pWidth"></param>
        /// <param name="pHeight"></param>
        /// <returns></returns>
        public void InitScene(IScene pScene) {
            mRenderingSystem.Assemble(pScene);
            mAISystem.Assemble(pScene);
            mPhysicsSystem.Assemble(pScene);
            mResourceManager.RegisterTo(pScene);
        }

        /// <summary>
        /// Start a scene
        /// </summary>
        /// <param name="pScene">Scene To Start</param>
        public void PlayScene(IScene pScene) {
            mResourceManager.Load(pScene);
            mAISystem.Load(pScene);
            mRenderingSystem.Load(pScene);
            mPhysicsSystem.Load(pScene);
        }

        public void Import(AssetLibrary pAssetLibrary) {
            mResourceManager.Import(pAssetLibrary);
        }

        internal IPhysicsSystem PhysicsSystem { get { return mPhysicsSystem; } }
        internal IRenderingSystem RenderingSystem { get { return mRenderingSystem; } }
        internal IAISystem AISystem { get { return mAISystem; } }
    }
}
