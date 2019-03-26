using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XNAMachinationisRatio;
using XNAMachinationisRatio.Rendering;
using XNAMachinationisRatio.Systems;

namespace XNAMachinationisRatio {
    internal class X3DScene : Scene, I3DScene {
        

        public X3DScene(int pWidth, int pHeight, int pDepth)
            : base(pWidth, pHeight, pDepth) {  
            mSceneDimension = Dimension.X2D;
        }

        public I3DCamera CreateCameraAt(String pCameraName, Vector3 pCameraPosition) {
            X3DCamera camera = CameraFactory.Create3DCamera(pCameraName);
            this.Place(camera, pCameraPosition);
            return camera;
        }
    }
}
