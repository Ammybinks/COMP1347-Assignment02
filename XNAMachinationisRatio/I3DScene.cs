using System;
using Microsoft.Xna.Framework;

namespace XNAMachinationisRatio {
    public interface I3DScene : IScene{
        I3DCamera CreateCameraAt(String pCameraName, Vector3 pCameraPosition);
    }
}
