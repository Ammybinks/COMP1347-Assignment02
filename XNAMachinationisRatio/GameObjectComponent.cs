using System;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace XNAMachinationisRatio {
    /// <summary>
    /// Object's component
    /// </summary>
    public abstract class GameObjectComponent : IGameObjectComponent {
        /// <summary>
        /// Reference to GameObject
        /// </summary>
        protected GameObject Self;
        /// <summary>
        /// On binding component to a game object
        /// </summary>
        /// <param name="pGameObject">object binded</param>
        public virtual void OnBind(GameObject pGameObject) {
            Debug.Assert(pGameObject != null, "GameObject shall not be null");
            Self = pGameObject;
        } 
        public void SetWorldPosition(Vector3 pPosition) {
            Self.SetPosition(pPosition.X, pPosition.Y, pPosition.Z);
        }
        public Vector3 WorldPosition {
            get {
                return Self.Position;
            }
        }
    }
}
