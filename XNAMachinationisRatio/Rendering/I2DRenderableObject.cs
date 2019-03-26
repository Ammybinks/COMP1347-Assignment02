using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNAMachinationisRatio.Rendering {
    interface I2DRenderableObject : IRenderableObject {
        Texture2D SpriteMap { get; }
        Rectangle SourceRegion { get; }
        Vector2 Origin { get; }
        Vector3 Orientation { get; } 
    }
}
