using System;
using Microsoft.Xna.Framework;

namespace XNAMachinationisRatio.Rendering {
    internal class CameraFactory {
        /// <summary>
        /// Create a Base fixed camera with a name
        /// </summary>
        /// <param name="pCameraName">Name of created camera</param>
        /// <returns>2D Camera</returns>
        public static X2DCamera Create2DCamera(string pCameraName) {
            return new X2DCamera(pCameraName, Vector3.Zero);
        }

        public static X3DCamera Create3DCamera(string pCameraName) {
            return new X3DCamera(pCameraName, Vector3.Zero);
        }
    }
}
