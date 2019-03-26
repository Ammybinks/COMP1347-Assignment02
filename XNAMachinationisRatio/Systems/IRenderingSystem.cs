using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAMachinationisRatio.Rendering;

namespace XNAMachinationisRatio.Systems
{
    /// <summary>
    /// Class responsible for rendering on screen
    /// </summary>
    interface IRenderingSystem : IEngineSystem
    {
        /// <summary>
        /// Initializa the Renderer
        /// </summary>
        /// <param name="pDevice">DeviceManager to modify window and device attributes</param>
        void Init(GraphicsDeviceManager pDevice, IResourceManager pResourceManager);
        
        /// <summary>
        /// Render the scene
        /// </summary>
        /// <param name="pSimTime">Time structure</param>
        void Render(ref GameTime pGameTime);
        void SetCamera(ICamera pCamera);

    }
}
