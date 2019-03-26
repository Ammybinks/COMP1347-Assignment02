using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XNAMachinationisRatio.Physics;

namespace XNAMachinationisRatio.AI {
    public abstract class AIPlayer : GameObjectComponent ,IAIController {
        protected IMovementBehaviour mActualMovementBehaviour = null;
        /// <summary>
        /// Associate the controller to a specific Token in the Game
        /// </summary>
        /// <param name="pPossessedToken"></param>
        public void Possess(GameObject pPossessedToken) {
            /* injecting the token */
            pPossessedToken.Inject(ComponentType.AI, this);
        }
        /// <summary>
        /// Method inherited as IComponent
        /// </summary>
        /// <param name="pPossessedToken"></param>
        public override void OnBind(GameObject pPossessedToken) {
            if (Self != null && Self != pPossessedToken) {
                Self.Eject(ComponentType.AI);
            }
            base.OnBind(pPossessedToken);
        }

        public GameObject PossessedToken { get { return Self; } }

        protected IPhysicsActuator GetActuator() {
            if (Self == null)
                return null;
            return Self.GetPhysicsComponent();
        }
        public abstract void Update(ref GameTime pGameTime);
        public virtual void OnTouch(GameObject pOther) {
        }
    }
}
