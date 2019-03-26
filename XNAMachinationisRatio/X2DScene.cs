using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XNAMachinationisRatio;
using XNAMachinationisRatio.Rendering;
using XNAMachinationisRatio.Systems;

namespace XNAMachinationisRatio {
    /// <summary>
    /// Game 2D Scene
    /// </summary>
    internal class X2DScene : Scene, I2DScene {
        public X2DScene(int pWidth, int pHeight, int pDepth) : base(pWidth,pHeight,pDepth) {  
            mSceneDimension = Dimension.X2D;
        }
        /// <summary>
        /// Create a 2D camera at a given position
        /// </summary>
        /// <param name="pCameraName">Camera name</param>
        /// <param name="pCameraPosition">Camera position</param>
        /// <returns>2D Camera</returns>
        public I2DCamera CreateCameraAt(String pCameraName, Vector3 pCameraPosition) {
            X2DCamera camera = CameraFactory.Create2DCamera(pCameraName);
            this.Place(camera, pCameraPosition);
            return camera;
        }
    }
}
