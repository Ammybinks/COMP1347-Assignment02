using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;              // Required to use XNA features.
using XNAMachinationisRatio;                // Required to use the XNA Machinationis Ratio Engine general features.
using XNAMachinationisRatio.AI;             // Required to use the XNA Machinationis Ratio general AI features.

namespace FishORama
{
    class AquariumMind : AIPlayer
    {
        #region Data Members

        // This mind needs to interact with the token which it possesses, 
        // since it needs to know where are the aquarium's boundaries.
        // Hence, the mind needs a "link" to the aquarium, which is why it stores in
        // an instance variable a reference to its aquarium.
        private AquariumToken mAquarium = null;         // Reference to the aquarium in which the creature lives.

        private Random rand; // Stores the random number generator passed from the kernel

        private bool gameOver;

        #endregion

        #region Properties

        /// <summary>
        /// Set Aquarium in which the mind's behavior should be enacted.
        /// </summary>
        public AquariumToken Aquarium
        {
            set { mAquarium = value; }
        }

        public Random Rand
        {
            set { rand = value; }
        }

        public bool GameOver
        {
            set { gameOver = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="pToken">Token to be associated with the mind.</param>
        public AquariumMind(X2DToken pToken)
        {
            this.Possess(pToken);       // Possess token.           
        }

        #endregion

        #region Methods

        /* LEARNING PILL: The AI update method.
         * Mind objects enact behaviors through the method Update. This method is
         * automatically invoked by the engine, periodically, 'under the hood'. This can be
         * be better understood that the engine asks to all the available AI-based tokens:
         * "Would you like to do anything at all?" And this 'asking' is done through invoking
         * the Update method of each mind available in the system. The response is the execution
         * of the Update method of each mind , and all the methods possibly triggered by Update.
         * 
         * Although the Update method could invoke other methods if needed, EVERY
         * BEHAVIOR STARTS from Update. If a behavior is not directly coded in Updated, or in
         * a method invoked by Update, then it is IGNORED.
         * 
         * In this case a simple motion behavior has been encapsulated in the update method.
         * The fish swims horizontally, towards its current facing direction. When it reaches the
         * boundary of the acquarium, it reverses its direction and starts swimming again.
         */

        /// <summary>
        /// AI Update method.
        /// </summary>
        /// <param name="pGameTime">Game time</param>
        public override void Update(ref GameTime pGameTime)
        {
            if(!gameOver)
            {
                if (rand.Next(0, 60) == 0)
                {
                    PlaceLeg();
                }
            }
        }

        private void PlaceLeg()
        {
            if (mAquarium.ChickenLeg == null)
            {

                // Position the leg in the center of the screen
                Vector3 legPos = new Vector3(rand.Next(-50, 51), rand.Next(-200, 201), 3);

                // Create and insert chicken leg in the scene.
                mAquarium.ChickenLeg = new ChickenLegToken("ChickenLeg");

                this.mAquarium.Kernel.Scene.Place(mAquarium.ChickenLeg, legPos);
            }
        }

        #endregion
    }
}
