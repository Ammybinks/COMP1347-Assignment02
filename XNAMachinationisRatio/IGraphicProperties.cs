using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNAMachinationisRatio {


    public interface IGraphicProperties {
        String AssetID { get; set; }
       
        float Scale { get; set; }
    }
}
