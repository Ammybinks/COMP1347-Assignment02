using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAMachinationisRatio.Resource;

namespace XNAMachinationisRatio.Rendering {
    public interface IRenderableObject : IGameObjectComponent, IGraphicProperties {
        GenericAsset Asset { get; set; }
        Vector3 WorldPosition { get;  }

    }
}
