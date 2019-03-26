using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using XNAMachinationisRatio.Rendering;
namespace XNAMachinationisRatio
{
    public interface ICamera {
        /// <summary>
        /// Camera Position
        /// </summary>
        Vector3 Position { get; set; }
        /// <summary>
        /// Render the scene, from the camera point of view
        /// </summary>
        void RenderScene();
        void Init(GraphicsDevice pDevice);
        void PresentScene(ISceneGraph pSceneGraph);
        Vector3 CameraToWorld(Vector2 pScreenPosition);
    }
}
