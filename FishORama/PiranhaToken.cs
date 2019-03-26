using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;      // Required to use XNA features.
using XNAMachinationisRatio;        // Required to use the XNA Machinationis Ratio Engine.
using XNAMachinationisRatio.AI;     // Required to use the XNA Machinationis Ratio general AI features

namespace FishORama
{
    /// <summary>
    /// Abstraction to represent the orange fish which moves peacefully in the aquarium.
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
    class PiranhaToken : X2DToken
    {
        #region Data members

        // This token needs to interact with the aquarium to swim in it (it needs information
        // regarding the aquarium's boundaries). Hence, it needs a "link" to the aquarium,
        // which is why it stores in an instance variable a reference to its aquarium.
        private AquariumToken aquarium;    // Reference to the aquarium in which the creature lives.

        private int fishNum;
        private int teamNum;

        private PiranhaMind mind;       // Explicit reference to the mind the token is using to enact its behaviors.

        #endregion

        #region Properties

        /// <summary>
        /// Get aquarium in which the creature lives.
        /// </summary>
        public AquariumToken Aquarium
        {
            get { return aquarium; }
        }

        public int TeamNum
        {
            get { return teamNum; }
        }

        public bool Swimming
        {
            get { return mind.Swimming; }
        }
        
        #endregion

        #region Constructors

        /// Constructor for the orange fish.
        /// Uses base class to initialize the token name, and adds code to
        /// initialize custom members.
        /// <param name="pTokenName">Name of the token.</param>
        /// <param name="pAquarium">Reference to the aquarium in which the token lives.</param>
        public PiranhaToken(String tokenName, AquariumToken aquarium, Random rand, int fishNum, int teamNum)
            : base(tokenName)
        {
            this.aquarium = aquarium;          // Store reference to aquarium in which the creature is living.
            mind.Aquarium = aquarium;     // Provide to the mind a reference to the aquarium, required to swim appropriately.

            this.fishNum = fishNum;
            this.teamNum = teamNum;

            mind.Rand = rand;
        }

        #endregion

        #region Methods 

        /* LEARNING PILL: XNA Machinationis Ration token properties.
         * All tokens created through the XNA Machinationis Ratio engine have standard
         * attributes that define their behavior in a simulation. These standard
         * attributes can be initialized in a very efficient and simple way using
         * the DeafultProperties() method.
         */

        /// <summary>
        /// Setup default properties of the token.
        /// </summary>
        protected override void DefaultProperties()
        {

            // Specify which image should be associated to this token, assigning
            // the name of the graphic asset to be used ("OrangeFishVisuals" in this case)
            // to the property 'GraphicProperties.AssetID' of the token.
            this.GraphicProperties.AssetID = "PiranhaVisuals";

            // Specify mass of the fish. This can be used by
            // physics-based behaviors (work in progress, not functional yet).
            this.PhysicsProperties.Mass = 3;
            
            PiranhaMind myMind = new PiranhaMind(this);   // Create mind, implicitly associating it to the token.

            mind = myMind;     // Store explicit reference to mind being used.
            mind.Aquarium = aquarium;   // Provide to mind explicit reference to Aquarium.
            mind.Radius = 64; // Pass radius of sprite to mind
        }

        public void StartMoving()
        {
            mind.StartMoving();
        }

        public bool StopMoving()
        {
            return mind.StartReturning();
        }

        #endregion

    }
}
