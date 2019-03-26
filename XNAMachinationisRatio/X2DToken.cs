using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace XNAMachinationisRatio {

    /// <summary>
    /// Token is a game object Renderable and Collidable by default
    /// </summary>
    public abstract class X2DToken : GameObject {
        
        protected X2DToken(String pTokenName)
            : base(pTokenName, Vector3.Zero) {
                InjectComponents(); 
                DefaultProperties();
        }

        public override Dimension Type { get { return Dimension.X2D; } }
        /// <summary>
        /// Initialize Token Default Properties
        /// </summary>
        private void InjectComponents() {
            Engine.getInstance().RenderingSystem.Assemble(this);
            Engine.getInstance().PhysicsSystem.Assemble(this);
        }

        protected abstract void DefaultProperties();
    }
}
