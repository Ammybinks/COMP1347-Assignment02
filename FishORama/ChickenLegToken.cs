using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;      // Required to use XNA features.
using XNAMachinationisRatio;        // Required to use the XNA Machinationis Ratio Engine.

/* LERNING PILL: XNAMachinationisRatio Engine
 * XNAMachinationisRatio is an engine that allows implementing
 * simulations and games based on XNA, simplifying the use of XNA
 * and adding features not directly available in XNA.
 * XNAMachinationisRatio is a work in progress.
 * The engine works "under the hood", taking care of many features
 * of an interactive simulation automatically, thus minimizing
 * the amount of code that developers have to write.
 * 
 * In order to use the engine, the application main class (Kernel, in the
 * case of FishO'Rama) creates, initializes and stores
 * an instance of class Engine in one of its data members.
 * 
 * The classes comprised in the  XNA Machinationis Ratio engine and the
 * related functionalities can be accessed from any of your XNA project
 * source code files by adding appropriate 'using' statements at the beginning of
 * the file. 
 * 
 */

/*LEARNING PILL: Game Tokens in the XNA Machinationis Ratio Engine.
 * The XNA Machinationis Ratio engine models games as systems
 * comprising entities called tokens. Tokens have a visual representation
 * and interactive behaviors. Tokens are implemented in C# as instances
 * of the class Token or one of its derived classes.
 * 
 * Tokens have attributes that allow implementing simulation features.
 * Tokens can be associated to a 'mind' object, in order to implement their behaviors.
 */

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

/* LEARNING PILL: placing tokens in a scene.
 * In order to be managed by the Machinationis Ratio engine, tokens must be placed
 * in a scene.
 * 
 * In FishORama the procedure for the creation and placement of tokens that must be
 * placed in the scene at startup is carried out byn the method LoadContent of
 * class Kernel.
 * 
 * Tokens can also be created in runtime by any method of any class, provided that
 * the method has access to the simulation scene object encapsulated in class Kernel.
 * This object can be accessed through the property Scene of class Kernel.
 */

namespace FishORama
{
    /// <summary>
    /// Abstraction to represent the chicken leg used to feed the piranha.
    /// 
    /// This class is derived from class X2DToken. In the XNA Machinationis Ratio engine
    /// class X2DToken is a base class for all classes representing objects which
    /// have a visual representation and interactive behaviors in a 2D simulation.
    /// X2DToken implements a number of functionalities that make it easy for developers
    /// to add interactivity to objects minimizing the amount of coded required.
    /// 
    /// Hence, whenever we want to create a new type of object, we must create a new
    /// class derived from X2DToken.
    /// </summary>
    /// 
    class ChickenLegToken : X2DToken
    {
        #region Data Members
        //// This token needs to interact with the aquarium to swim in it (it needs information
        //// regarding the aquarium's boundaries). Hence, it needs a "link" to the aquarium,
        //// which is why it stores in an instance variable a reference to its aquarium.
        //private AquariumToken mAquarium;  // Reference to the aquarium in which the creature lives.
        //private ChickenLegMind mMind;       // Explicit reference to the mind the token is using to enact its behaviors.
        #endregion

        #region Properties
        // No custom properties yet.
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for the chicken leg.
        /// Uses base class to initialize the token name, and adds code to
        /// initialize custom members.
        /// </summary
        /// <param name="pTokenName">Name of the token.</param>
        /// <param name="pAquarium">Reference to the aquarium in which the token lives.</param>
        public ChickenLegToken(String pTokenName)
            : base(pTokenName) {
        }

        #endregion

        #region Methods

        /* LEARNING PILL: Token default properties.
         * In the XNA Machinationis Ratio engine tokens have properties that define
         * how the behave and are visualized. Using the DefaultProperties method 
         *  it is possible to assign deafult values to the token's properties,
         * after the token has been created.
         */

        /// <summary>
        /// Setup default values for this token's porperties.
        /// </summary>
        protected override void DefaultProperties()
        {
            // Specify which image should be associated to this token, assigning
            // the name of the graphic asset to be used ("ChickenLegVisuals" in this case)
            // to the property 'GraphicProperties.AssetID' of the token.
            this.GraphicProperties.AssetID = "ChickenLegVisuals";
        }

        #endregion
    }
}
