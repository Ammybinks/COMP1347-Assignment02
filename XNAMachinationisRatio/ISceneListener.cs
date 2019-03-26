using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAMachinationisRatio {
    /// <summary>
    /// Scene listener interested in scene event
    /// </summary>
    public interface ISceneListener {
        /// <summary>
        /// Reacts to "object placed event"
        /// </summary>
        /// <param name="pPlacedObject">Placed object</param>
        void OnPlacedObject(GameObject pPlacedObject);
        /// <summary>
        /// Reacts to "Object removed event"
        /// </summary>
        /// <param name="pPlacedObject">Removed object</param>
        void OnRemovedObject(GameObject pPlacedObject);
    }
}
