using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XNAMachinationisRatio.Systems;
using XNAMachinationisRatio.Rendering;

namespace XNAMachinationisRatio {
    /// <summary>
    /// A Scene from game Point of View
    /// </summary>
    public interface IScene {

        /// <summary>
        /// Spawn a gameObject in the desired position
        /// </summary>
        /// <param name="pGameObject">GameObject</param>
        /// <param name="pPosition">Desired position</param>
        void Place(GameObject pGameObject, Vector3 pPosition);

        void Remove(GameObject pGameObject);
        Dimension Dimension { get; }
        void AddSceneListener(string pIdentifier,ISceneListener pScene);

        ISceneListener GetRepresentation(string pIdentifier);
    }
}
