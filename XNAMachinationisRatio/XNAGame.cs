/*
 * @author : Valerio Provaggi
 * @version : 1.0
 * */
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using XNAMachinationisRatio.Systems;
using XNAMachinationisRatio.Resource;

namespace XNAMachinationisRatio {
    /// <summary>
    /// XNA Game class loaded by the engine
    /// </summary>
    public abstract class XNAGame : Microsoft.Xna.Framework.Game {
        private GraphicsDeviceManager graphics;
        // XNAEngine used for this game
        private Engine mEngine = null;

        public XNAGame(String pGameName) {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            mEngine = Engine.getInstance();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();    
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // initialize Engine and all internal systems
            mEngine.Init(graphics, this.Content);
            ImportAssets();
            base.LoadContent();
        }

        protected virtual void ImportAssets() {
            mEngine.Import(GetAssetLibrary());
        }
        protected abstract AssetLibrary GetAssetLibrary();

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Delegates update to Engine
            mEngine.Update(ref gameTime);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Delegates render to Engine
            mEngine.Render(ref gameTime);            
            base.Draw(gameTime);
        }
        /// <summary>
        /// Start a scene
        /// </summary>
        /// <param name="pScene">Scene To Start</param>
        public void PlayScene(IScene pScene) {
            mEngine.PlayScene(pScene);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pWidth"></param>
        /// <param name="pHeight"></param>
        /// <param name="pDepth"></param>
        /// <returns></returns>
        public static I3DScene CreateA3DScene(int pWidth, int pHeight, int pDepth) {
            I3DScene scene = new X3DScene(pHeight, pHeight, pDepth);
            Engine.getInstance().InitScene(scene);
            return scene;
        }

        public static I2DScene CreateA2DScene(int pWidth, int pHeight, int pDepth) {
            I2DScene scene = new X2DScene(pHeight, pHeight, pDepth);
            Engine.getInstance().InitScene(scene);
            return scene;
        }
    }
}
