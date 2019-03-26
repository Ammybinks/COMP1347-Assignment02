using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAMachinationisRatio {
    /// <summary>
    /// Scene Representation Model an abstract scene scheme, every systems model one scene with one or more scene representations
    /// </summary>
    /// <typeparam name="T">Type of the element in the scene</typeparam>
    public interface ISceneRepresentationTrait<T> : ISceneListener {
        /// <summary>
        /// Add an element to scene representation
        /// </summary>
        /// <param name="pElement">element to add</param>
       void AddToScene(T pElement);
        /// <summary>
        /// Remove an element from scene representation
        /// </summary>
        /// <param name="pElement">element to remove</param>
        void RemoveFromScene(T pElement);

    }
}
