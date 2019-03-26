using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using XNAMachinationisRatio;                // Required to use the XNA Machinationis Ratio general features.
using XNAMachinationisRatio.Resource;       // Required to use the MonoGame Machinationis Ratio resource management features.

/* LERNING PILL: XNAMachinationisRatio Engine
 * XNAMachinationisRatio is an engine that allows implementing
 * simulations and games based on MonoGame, simplifying the use of MonoGame
 * and adding features not directly available in MonoGame.
 * XNAMachinationisRatio is a work in progress.
 * The engine works "under the hood", taking care of many features
 * of an interactive simulation automatically, thus minimizing
 * the amount of code that developers have to write.
 * 
 * In order to use the engine, the application main class (Kernel, in the
 * case of FishO'Rama) creates, initializes and stores
 * an instance of class Engine in one of its data members.
 * 
 * The classes comprised in the engine and the related functionalities
 * can be accessed from a new MonoGame project, by:
 * 1) Creating a project reference to the XNAMachinationisRatio project (right-click
 * on your project, select 'Add Reference..." and then select the XNAMachinationisRatio
 * either through the 'Projects' or the 'Browse' tab).
 * 2) Adding appropriate 'using' statements to the source code files from which
 * the XNA Machinationis Ratio classes must be used.
 */

/* SPECIAL THANKS TO OUR SINGAPORE ARTIST CHUCK FOR ALL HIS FINE ARTWORK AND ADDING NEW LIFE TO THE ORANGE FISH! */

/* LEARNING PILL: virtual world space and graphics in FishORama
 * In the Machinationis Ratio engine every object has graphic a position in the
 * virtual world expressed through a 3D vector (represented via a Vector3 object).
 * In 2D simulations the first coordinate of the vector is the horizontal (X)
 * position, the second coordinate (Y) represents the vertical position, and 
 * the third coordinate (Z) represents the depth. All simulation features are
 * based on world coordinates.
 * 
 * At any time, a portion of the scene is visible through a camera object (in FishO'Rama 
 * this is created, initialized and referenced through the Kernel class). For the purpose
 * of visualization, coordinates may also be expressed relative to the camera
 * origin (i.e. the center of the camera). In FishO'Rama the camera is centered on the
 * origin of the world, which makes camera coordinates coinciding with world coordinates.
 * This greatly simplifies all the operations.
 * 
 * The third coordinate of the world position of an object represents the depth, i.e.
 * how close an object is to the camera. This defines which objects are in front of others 
 * (for instance, an object with Z=3 will always be drawn in front of an object with
 * Z=2).
 */

namespace FishORama
{
    /// <summary>
    /// Kernel (orchestrator class) for this application.
    /// </summary>
    public class Kernel : XNAGame
    {

        #region Data Members
        
        I2DScene mScene = null;     // Reference to the FishORama scene, set to null before its initialization.

        I2DCamera mCamera = null;   // Reference to the FishORama camera, set to null before its initialization.

        Random rand = new Random(); // Global random number generator, to be passed to every other object that needs it

        Referee referee; // Referee that manages operation of the game

        #endregion

        #region Properties

        /// <summary>
        /// Get simulation scene.
        /// </summary>
        public I2DScene Scene
        {
            get { return mScene; }
        }

        /// <summary>
        /// Get simulation camera.
        /// </summary>
        public I2DCamera Camera
        {
            get { return mCamera; }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Kernel(): base("FishO'Rama")
        {
            this.IsMouseVisible = true;     // Display mouse cursor.
        }
        
        #endregion


        #region Methods

        /* LEARNING PILL: XNA Machinationis Ratio asset library
         * In order to work, simulations may require media assets such as
         * images used to display tokens. In order to load and manage assets
         * XNA Machinationis Ratio uses the class AssetLibrary, and the related
         * methods.
         */

        /// <summary>
        /// Create library of graphic assets.
        /// </summary>
        /// <returns>Library.</returns>
        protected override AssetLibrary GetAssetLibrary()
        {
            AssetLibrary lib = AssetLibrary.CreateAnEmptyLibrary();             // New asset library.
            X2DAsset A = null;                                                  // Temporary variable used to create graphic assets.

            /* LEARNING PILL: Creation of new graphic assets with XNA Machinationis ratio
             * Tokens with a visual representation must have an associated visual asset.
             * In the XNA Machinationis Ratio engine, graphic assets are created using the
             * class X2DAsset, based on images imported in the MonoGame contents pipeline.
             * After creation assets are imported in the simulation assets library.
             * Assets are created specifying:
             * - ID of the asset and ID of MonoGame resource in which the asset is contained (useful to support spritesheets).
             * - The local origin of the asset (in this case, the center of the image). It will be used to
             *   position and move it in the scene.
             * - The position of the asset in the MonoGame resource (from top-left corner of the image).
             * - The width of the asset.
             * - The height of the asset.
             * 
             * NOTE: all the required assets have already been loaded in the solution,
             * and are available to create graphic library assets using their assigned ID.
             */

            // Create a new graphic asset  for the aquarium visuals using class X2DAsset.
            A = new X2DAsset("AquariumVisuals", "AquariumBackground"). 
                UVOriginAt(400, 300).
                UVTopLeftCornerAt(0, 0).
                Width(800).
                Height(600); 
            
            // Import aquarium visual asset in the library.
            lib.ImportAsset(A);

            // Create a new graphic asset for the first progress marker visuals using class X2DAsset.
            A = new X2DAsset("ChickenLegVisuals", "ChickenLeg").
                UVOriginAt(64, 64).
                UVTopLeftCornerAt(0, 0).
                Width(128).
                Height(128);

            // Import first marker visual asset in the library
            lib.ImportAsset(A);

            // Create a new graphic asset  for the orange fish visuals using class X2DAsset.
            A = new X2DAsset("OrangeFishVisuals", "OrangeFish").
                UVOriginAt(64, 64).
                UVTopLeftCornerAt(0, 0).
                Width(128).
                Height(84);

            // Import orange fish visual asset in the library
            lib.ImportAsset(A);

            // Create a new graphic asset  for the orange fish visuals using class X2DAsset.
            A = new X2DAsset("PiranhaVisuals", "Piranha1").
                UVOriginAt(66, 64).
                UVTopLeftCornerAt(0, 0).
                Width(132).
                Height(128);

            // Import orange fish visual asset in the library
            lib.ImportAsset(A);

            // Return library.
            return lib;
        }

        /// <summary>
        /// Load contents for the simulation.
        /// LoadContent will be called only once, at the beginning of the simulation,
        /// is the place to load all of your content (e.g. graphics and sounds).
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();

            // Instantiate and initialize scene, specifying its horizontal size (800)
            // and vertical size (600).
            // Note, the third parameter is set to 0 because unused in FishORama.
            mScene = XNAGame.CreateA2DScene(800, 600, 0);

            /*
             * Create Tokens
             */
            Console.WriteLine("Welcome to the FishORama Framework");

            AquariumToken aquarium = new AquariumToken("Aquarium", this, 800, 600, rand);         // Create aquarium token.
                        
             /* LEARNING PILL: placing tokens in a scene.
             * In order to be managed by the Machinationis Ratio engine, tokens must be placed
             * in a scene.
             * 
             * In FishORama the procedure for the creation and placement of tokens that must be
             * placed in the scene at startup is carried out byn the method LoadContent of
             * class Kernel.
             * Tokens can also be created in runtime by any method of any class, provided that
             * the method has access to the simulation scene object encapsulated in class Kernel.
             * This object can be accessed through the property Scene of class Kernel.
             */

            /*
             * Place tokens in the scene.
             */

            Vector3 tokenPos;        // Empty Vector3 object to be used to position tokens.

            tokenPos = new Vector3(0, 0, 0);            // Define scene position for the aquarium.
            mScene.Place(aquarium, tokenPos);           // Place token in scene.

            PiranhaToken piranha;
            string fishName = "Piranha";

            Team[] teams = new Team[2];

            for(int i = 0; i < teams.Length; i++)
            {
                PiranhaToken[] fish = new PiranhaToken[3];

                for(int o = 0; o < fish.Length; o++)
                {
                    piranha = new PiranhaToken(fishName, aquarium, rand, o + 1, i + 1);

                    // Initialise upper left point, for fish (0, 0)
                    tokenPos = new Vector3(-300, 150, 1);
                    // Increment the X position of the fish based on the team number
                    tokenPos.X += 600 * i;
                    // Increment the Y position of the fish based on the fish number
                    tokenPos.Y -= 150 * o;

                    mScene.Place(piranha, tokenPos);

                    fish[o] = piranha;
                }

                teams[i] = new Team(fish);
            }

            referee = new Referee(aquarium, rand, teams, 3);

            /*
             * Create and Initialize camera
             */

            // Define the position for the camera as a 3D vector object, created as a new
            // instance of class Vector3, and initialized to (0, 0, 1),
            // which means that in FishORama it is centered on the origin of the world.
            Vector3 camPosition = new Vector3(0, 0, 1);

            // Instantiate and initialize camera, specifying its ID ("Camera 01"
            // in this case), and its position (camPosition in this case).
            mCamera = mScene.CreateCameraAt("Camera 01", camPosition);

            //Startup the visualization, giving the "...and ACTION!" directive.
            this.PlayScene(mScene);
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Perform standard update operations.
            base.Update(gameTime);

            // Have the referee update the game state
            referee.Update();
        }

        #endregion
    }
}
