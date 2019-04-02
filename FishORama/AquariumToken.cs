using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;              // Required to use XNA features.
using XNAMachinationisRatio;                // Required to use the XNA Machinationis Ratio Engine.
using XNAMachinationisRatio.Resource;       // Required to use the XNA Machinationis Ratio resource management features.

namespace FishORama
{
    class AquariumToken : X2DToken
    {
        #region Data Members
        
        // Reference to the simulation kernel (orchestrator of the whole application).
        private Kernel mKernel;             
        
        // Reference to the mind of the aquarium.
        private AquariumMind mind;         
        
        /*
         * Attributes of the aquarium.
         */
        private int mWidth;                 // Aquarium width.
        private int mHeight;                // Aquarium height.
        
        /*
         * Useful references to entities populating the aquarium.
         */

        // Reference to the chicken leg. Required to allow the piranha
        // reaching it.
        private ChickenLegToken mChickenLeg = null;
        
        #endregion

        #region Properties

        /// <summary>
        /// Get reference to aquarium width.
        /// </summary>
        public int Width
        {
            get { return mWidth; }
        }

        /// <summary>
        /// Get reference to aquarium height.
        /// </summary>
        public int Height
        {
            get { return mHeight; }
        }

        /// <summary>
        /// Get/set reference to chicken leg.
        /// </summary>
        public ChickenLegToken ChickenLeg
        {
            get { return mChickenLeg; }
            set { mChickenLeg = value; }
        }

        /// <summary>
        /// Get reference to Kernel.
        /// </summary>
        public Kernel Kernel
        {
            get { return mKernel; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for the aquarium.
        /// Uses base class to initialize the token name, and adds code to
        /// initialize custom members.
        /// </summary
        /// <param name="pTokenName">Name of the token.</param>
        /// <param name="pKernel">Reference to the simulation kernel.</param>
        /// <param name="pWidth">Width of the aquarium.</param>
        /// <param name="pHeight">Height of the aquarium.</param>
        public AquariumToken(String pTokenName, Kernel pKernel, int pWidth, int pHeight, Random rand)
            : base(pTokenName)
        {
            mKernel = pKernel;      // Store reference to kernel.
            mHeight = pHeight;      // Height of the aquarium.
            mWidth = pWidth;        // Width of the aquarium.

            mind.Rand = rand;
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
            // the name of the graphic asset to be used ("AquariumVisuals" in this case)
            // to the property 'GraphicProperties.AssetID' of the token.
            this.GraphicProperties.AssetID = "AquariumVisuals";

            /* LEARNING PILL: Token behaviors in the XNA Machinationis Ratio engine
             * Some simulation tokens may need to enact specific behaviors in order to
             * participate in the simulation. The XNA Machinationis Ratio engine
             * allows a token to enact a behavior by associating an artificial intelligence
             * mind to it. Mind objects are created from subclasses of the class AIPlayer
             * included in the engine. In order to associate a mind to a token, a new
             * mind object must be created, passing to the constructor of the mind a reference
             * of the object that must be associated with the mind. This must be done in
             * the DefaultProperties method of the token. Upon creation of the mind, XNA
             * Machinationis Ratio automatically "injects" its into the token, establishing
             * a link which is not visible to the programmer (but it there!)
             * 
             * In this case, instances of the class OrangeFishToken can enact a simple swimming
             * behavior. The behavior is implemented through the class SimpleSwimind.
             */

            AquariumMind myMind = new AquariumMind(this);   // Create mind, implicitly associating it to the token.


            mind = myMind;         // Store explicit reference to mind being used.
            mind.Aquarium = this;  // Provide to mind explicit reference to Aquarium.
        }

        /// <summary>
        /// Indicate whether a game object in the aquarium has reached the aquarium's
        /// horizontal boundaries.
        /// </summary>
        /// <param name="pObject">Object to check.</param>
        /// <returns>Result of the check.</returns>
        public bool ReachedHorizontalBoundary(GameObject pObject)
        {
            // Check if the absolute value of the horizontal distance between the center of
            // aquarium and the object is greater than half the width of the aquarium,
            // which means that center of the object has reached the horizontal boundary
            // of the aquarium.
            if (Math.Abs(pObject.Position.X - this.Position.X) >= (this.mWidth / 2))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Indicate whether a game object in the aquarium has reached the aquarium's
        /// horizontal boundaries.
        /// </summary>
        /// <param name="pObject">Object to check.</param>
        /// <returns>Result of the check.</returns>
        public bool ReachedVerticalBoundary(GameObject pObject)
        {
            // Check if the absolute value of the horizontal distance between the center of
            // aquarium and the object is greater than half the width of the aquarium,
            // which means that center of the object has reached the horizontal boundary
            // of the aquarium.
            if (Math.Abs(pObject.Position.Y - this.Position.Y) >= (this.mHeight / 2))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Remove chicken leg from aquarium.
        /// </summary>
        public void RemoveChickenLeg()
        {
            if(mChickenLeg != null)
            {
                this.mKernel.Scene.Remove(mChickenLeg);
                mChickenLeg = null;
            }
        }

        public void End()
        {
            mind.GameOver = true;
        }

        #endregion
    }
}