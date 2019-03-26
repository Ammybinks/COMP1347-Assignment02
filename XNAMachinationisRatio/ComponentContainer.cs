using System;
using System.Collections.Generic;
using XNAMachinationisRatio.Rendering;
using XNAMachinationisRatio.Physics;

namespace XNAMachinationisRatio {

    public interface IComponentContainer {

        /// <summary>
        /// Inject a Component in the container
        /// </summary>
        /// <param name="pType">Type of the component</param>
        /// <param name="pComponentToAdd">Component to Inject</param>
        void Inject(ComponentType pType, IGameObjectComponent pComponentToAdd);

        /// <summary>
        /// Eject a Component from the container
        /// </summary>
        /// <param name="pType">Type of the component to eject</param>
        void Eject(ComponentType pType);
    }
}
