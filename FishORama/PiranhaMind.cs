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

        private float speed; // Stores temporary speed value that the fish will move with every update
        private float baseSpeed = 5; // Default speed the fish moves, before any modifiers
        private int speedModifier = 10; // Amount the speed can vary by, in number of 0.1 intervals
                                        // Because of the way Random.Next() works, speed is first randomised between whole numbers before being divided into smaller increments
        private float speedIncrease = 0.2f; // Amount the speed accelerates by each update

        private bool firstUpdate = true;
        private bool swimming; // Tracks whether the fish is currently swimming off the edge of the screen
        private bool seeking; // Tracks whether the fish is currently moving towards the chicken leg
        private bool returning; // Tracks whether the fish is currently moving back to its original position

        private bool full; // Stores whether the fish has eaten a chicken leg, used to increment the team's score
        
        private float currentSin = 0; // Current progression of the sin pattern
        private float sinIncrement = 0.05f; // How much the pattern progresses each update
        private float sinIntensity = 1; // Intensity of the effect, determining how large the circle of displacement is

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

        /// <summary>
        /// Gets the current swimming state of the fish, returning true if any related booleans evaluate to true
        /// </summary>
        public bool Swimming
        {
            get
            {
                if (seeking || returning)
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
                // Randomise the point the fish will start on along the sin/cos circle
                currentSin += rand.Next(0, 101);
                currentSin /= 100;

                // Face the fish towards the center of the screen
                if ((PossessedToken as PiranhaToken).TeamNum == 1)
                {
                    facingDirectionX = -1;
                }
                else
                {
                    facingDirectionX = 1;

                    // Invert the direction the fish swims along the sin/cos circle
                    sinIncrement *= -1;
                }
                
                PossessedToken.Orientation = new Vector3(facingDirectionX, PossessedToken.Orientation.Y, PossessedToken.Orientation.Z);

                firstUpdate = false;
            }

            tokenPosition = PossessedToken.Position; // Store the current position of the bubble

            // The game has ended and this fish is on the winning team
            if(swimming)
            {
                // Continue swimming off the edge of the screen
                tokenPosition.X += speed;

                speed += speedIncrease;
            }
            // If the chicken leg is in the aquarium
            else if(seeking)
            {
                // Get direction from the token to the chicken leg
                Vector2 direction = new Vector2(chickenLegPosition.X - tokenPosition.X, chickenLegPosition.Y - tokenPosition.Y);

                if (direction.Length() <= radius) // If token has reached the chicken leg
                {
                    if (aquarium.ChickenLeg != null)
                    {
                        aquarium.RemoveChickenLeg();

                        full = true;
                    }
                }
                else
                {
                    // Normalise direction, calculating how far to move this frame
                    direction = Vector2.Normalize(direction);
                    direction *= speed;
                    
                    // Move towards the chicken leg
                    tokenPosition += new Vector3(direction.X, direction.Y, 0);
                }
            }
            else if(returning)
            {
                // Get direction from the token to its original position
                Vector2 direction = new Vector2(originalPosition.X - tokenPosition.X, originalPosition.Y - tokenPosition.Y);

                if (direction.Length() <= speed) // If token would step over its original position on this update
                {
                    // Set the token back to its default pattern

                    // Reset the token's position
                    tokenPosition = originalPosition;

                    facingDirectionX *= -1;
                    PossessedToken.Orientation = new Vector3(facingDirectionX, PossessedToken.Orientation.Y, PossessedToken.Orientation.Z);

                    returning = false;
                }
                else
                {
                    // Normalise direction, calculating how far to move this frame
                    direction = Vector2.Normalize(direction);
                    direction *= speed;

                    // Move towards the token's original position
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

        /// <summary>
        /// Do all initialisation necessary to begin moving the token towards the chicken leg
        /// </summary>
        public void StartMoving()
        {
            // Store the position of the chicken leg
            chickenLegPosition = aquarium.ChickenLeg.Position;

            seeking = true;

            // Store the current position of the token so that it can return later
            originalPosition = tokenPosition;

            // Randomise the token's speed by adding a base speed to a randomised modifier, to reduce variance
            speed = baseSpeed + ((rand.Next(-speedModifier, speedModifier + 1) * 0.1f));
        }

        /// <summary>
        /// Do all initialisation necessary to begin moving the token back to its original position
        /// </summary>
        /// <returns>Whether the fish reached the chicken leg or not</returns>
        public bool StartReturning()
        {
            facingDirectionX *= -1;
            PossessedToken.Orientation = new Vector3(facingDirectionX, PossessedToken.Orientation.Y, PossessedToken.Orientation.Z);

            seeking = false;
            returning = true;

            bool temp = full;
            full = false;
            return temp;
        }

        /// <summary>
        /// Do all initialisation necessary to begin moving the token off the edge of the screen once its team has won
        /// </summary>
        public void Win()
        {
            speed = baseSpeed;

            // Face the fish towards the center of the screen
            if ((PossessedToken as PiranhaToken).TeamNum == 1)
            {
                facingDirectionX = -1;

                PossessedToken.Orientation = new Vector3(facingDirectionX, PossessedToken.Orientation.Y, PossessedToken.Orientation.Z);

                // Invert the direction the fish moves
                speed *= -1;
            }
            else
            {
                facingDirectionX = 1;

                PossessedToken.Orientation = new Vector3(facingDirectionX, PossessedToken.Orientation.Y, PossessedToken.Orientation.Z);

                // Invert the direction the fish accelerates
                speedIncrease *= -1;
            }

            swimming = true;
        }
    }
}
