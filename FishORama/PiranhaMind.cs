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

        private bool full; // Stores whether the fish has eaten a chicken leg
        
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
                    sinIncrement *= -1;
                }
                
                PossessedToken.Orientation = new Vector3(facingDirectionX, PossessedToken.Orientation.Y, PossessedToken.Orientation.Z);

                firstUpdate = false;
            }

            tokenPosition = PossessedToken.Position; // Store the current position of the bubble

            if(swimming)
            {
                tokenPosition.X += speed;

                speed += speedIncrease;
            }
            // If the chicken leg is in the aquarium
            else if(seeking)
            {
                // Get direction and distance to swim, based on the fish's speed
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
                    direction = Vector2.Normalize(direction);
                    direction *= speed;

                    tokenPosition += new Vector3(direction.X, direction.Y, 0);
                }
            }
            else if(returning)
            {
                // Get direction and distance to swim, based on the fish's speed
                Vector2 direction = new Vector2(originalPosition.X - tokenPosition.X, originalPosition.Y - tokenPosition.Y);

                if (direction.Length() <= speed) // If token would step over its original position on this update
                {
                    tokenPosition = originalPosition;

                    facingDirectionX *= -1;

                    PossessedToken.Orientation = new Vector3(facingDirectionX, PossessedToken.Orientation.Y, PossessedToken.Orientation.Z);

                    returning = false;
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

            seeking = true;

            originalPosition = tokenPosition;

            speed = baseSpeed + ((rand.Next(-speedModifier, speedModifier + 1) * 0.1f));
        }

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

        public void Win()
        {
            speed = baseSpeed;
            // Face the fish towards the center of the screen
            if ((PossessedToken as PiranhaToken).TeamNum == 1)
            {
                facingDirectionX = -1;

                PossessedToken.Orientation = new Vector3(facingDirectionX, PossessedToken.Orientation.Y, PossessedToken.Orientation.Z);

                speed *= -1;
            }
            else
            {
                facingDirectionX = 1;

                PossessedToken.Orientation = new Vector3(facingDirectionX, PossessedToken.Orientation.Y, PossessedToken.Orientation.Z);

                speedIncrease *= -1;
            }

            swimming = true;
        }
    }
}
