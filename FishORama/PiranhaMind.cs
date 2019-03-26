using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;              // Required to use XNA features.
using XNAMachinationisRatio;                // Required to use the XNA Machinationis Ratio Engine general features.
using XNAMachinationisRatio.AI;             // Required to use the XNA Machinationis Ratio general AI features.

namespace FishORama
{
    class PiranhaMind : AIPlayer
    {
        #region Data Members

        // This mind needs to interact with the token which it possesses, 
        // since it needs to know where are the aquarium's boundaries.
        // Hence, the mind needs a "link" to the aquarium, which is why it stores in
        // an instance variable a reference to its aquarium.
        private AquariumToken aquarium;        // Reference to the aquarium in which the creature lives.

        private Random rand;

        private Vector3 tokenPosition; // Stores the temporary position of the fish
        private Vector3 originalPosition; // Stores the position the fish started swimming from, to return to after moving
        private Vector3 chickenLegPosition;

        private float facingDirectionX;         // Direction the fish is facing (1: right; -1: left).

        private float speed; // Speed the fish moves regularly
        private float baseSpeed = 3; // Default speed the fish moves, before any modifiers
        private int speedModifier = 10; // Amount the speed can vary by, in number of 0.1 intervals

        private bool firstUpdate = true;
        private bool isMoving; // Tracks whether the fish is currently moving towards the chicken leg
        private bool hasMoved; // Tracks whether the fish is currently moving back to its original position

        private bool full; // Stores whether the fish has eaten a chicken leg
        
        private float currentSin = 0;
        private float sinIncrement = 0.05f;
        private float sinIntensity = 1;

        private float radius; // Size of the circle surrounding the piranha, checked against for collisions

        #endregion

        #region Properties

        /// <summary>
        /// Set Aquarium in which the mind's behavior should be enacted.
        /// </summary>
        public AquariumToken Aquarium
        {
            set { aquarium = value; }
        }
        
        public Random Rand
        {
            set { rand = value; }
        }

        public int Radius
        {
            set { radius = value; }
        }

        public bool Swimming
        {
            get
            {
                if (isMoving || hasMoved)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="pToken">Token to be associated with the mind.</param>
        public PiranhaMind(X2DToken pToken)
        {
            this.Possess(pToken);       // Possess token.
            facingDirectionX = 1;       // Current direction the fish is facing.            
        }

        #endregion

        /// <summary>
        /// AI Update method.
        /// </summary>
        /// <param name="pGameTime">Game time</param>


        public override void Update(ref GameTime pGameTime)
        {
            if(firstUpdate)
            {
                // Face the fish towards the center of the screen
                if((PossessedToken as PiranhaToken).TeamNum == 1)
                {
                    facingDirectionX = -1;
                }
                else
                {
                    facingDirectionX = 1;
                    sinIncrement *= -1;
                }
                
                PossessedToken.Orientation = new Vector3(facingDirectionX, PossessedToken.Orientation.Y, PossessedToken.Orientation.Z);

                firstUpdate = false;
            }

            tokenPosition = PossessedToken.Position; // Store the current position of the bubble

            // If the chicken leg is in the aquarium
            if(isMoving)
            {
                // Get direction and distance to swim, based on the fish's speed
                Vector2 direction = new Vector2(chickenLegPosition.X - tokenPosition.X, chickenLegPosition.Y - tokenPosition.Y);

                if (direction.Length() <= radius) // If token has reached the chicken leg
                {
                    if(aquarium.ChickenLeg != null)
                    {
                        aquarium.RemoveChickenLeg();

                        full = true;
                    }
                }
                else
                {
                    direction = Vector2.Normalize(direction);
                    direction *= speed;

                    tokenPosition += new Vector3(direction.X, direction.Y, 0);
                }
            }
            else if(hasMoved)
            {
                // Get direction and distance to swim, based on the fish's speed
                Vector2 direction = new Vector2(originalPosition.X - tokenPosition.X, originalPosition.Y - tokenPosition.Y);

                if (direction.Length() <= speed) // If token would step over its original position on this update
                {
                    tokenPosition = originalPosition;

                    facingDirectionX *= -1;

                    PossessedToken.Orientation = new Vector3(facingDirectionX, PossessedToken.Orientation.Y, PossessedToken.Orientation.Z);

                    hasMoved = false;
                }
                else
                {
                    direction = Vector2.Normalize(direction);
                    direction *= speed;

                    tokenPosition += new Vector3(direction.X, direction.Y, 0);
                }
            }
            else
            {
                tokenPosition.X += ((float)Math.Sin(currentSin) * sinIntensity); // Move the bubble in a sine pattern along the X axis
                tokenPosition.Y += ((float)Math.Cos(currentSin) * sinIntensity); // Move the bubble in a cosine pattern along the y axis

                currentSin += sinIncrement; // Increase the evolution of the sin/cos pattern
            }

            PossessedToken.Position = tokenPosition; // Set the token's current position to the new one, after all movements

        }

        public void StartMoving()
        {
            // Store the position of the chicken leg
            chickenLegPosition = aquarium.ChickenLeg.Position;

            isMoving = true;

            originalPosition = tokenPosition;

            speed = baseSpeed + ((rand.Next(0, speedModifier) * 0.1f));
        }

        public bool StartReturning()
        {
            facingDirectionX *= -1;

            PossessedToken.Orientation = new Vector3(facingDirectionX, PossessedToken.Orientation.Y, PossessedToken.Orientation.Z);

            isMoving = false;
            hasMoved = true;

            bool temp = full;
            full = false;
            return temp;
        }
    }
}
