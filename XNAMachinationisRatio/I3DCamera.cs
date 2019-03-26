using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNAMachinationisRatio {
    public interface I3DCamera: ICamera {
        I3DCamera LookAt(Vector3 pPosition);
        I3DCamera SetFOV(float pFOVAngle);
        I3DCamera SetNearPlaneDistance(float pNearPlaneDistance);
        I3DCamera SetFarPlaneDistance(float pFarPlaneDistance);
        I3DCamera SetRasterState(FillMode pFillMode);
    }
}
