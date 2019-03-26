using System;
using Microsoft.Xna.Framework;
namespace XNAMachinationisRatio {

    public interface I2DScene : IScene {
        I2DCamera CreateCameraAt(String pCameraName, Vector3 pCameraPosition);
    }
}
