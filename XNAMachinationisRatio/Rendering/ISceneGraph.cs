using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNAMachinationisRatio.Rendering
{
    /// <summary>
    /// Scene Graph
    /// </summary>
    public interface ISceneGraph : ISceneRepresentationTrait<IRenderableObject>
    {
        /// <summary>
        /// Return the visible set of graphic objects inside the visible area
        /// </summary>
        /// <param name="pVisibleArea">Rectangle</param>
        /// <returns>List of Graphics</returns>
        List<IRenderableObject> GetVisibleSet(Rectangle pVisibleArea);
    }
}
