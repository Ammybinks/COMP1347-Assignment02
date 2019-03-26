using System;
using Microsoft.Xna.Framework;

namespace XNAMachinationisRatio {
    public interface IGameObjectComponent{
        /// <summary>
        /// Initialize GameObjectComponent
        /// </summary>
        /// <param name="pGameObject">Object to Initialize</param>
        void OnBind(GameObject pGameObject);
    }
    /* #PILL# Enum Type */
    public enum ComponentType {
        GRAPHICS,
        PHYSICS,
        AI,
        ANIMATION
    }
}
