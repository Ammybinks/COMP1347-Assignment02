using System;
using System.Collections.Generic;
using XNAMachinationisRatio.Rendering;
using XNAMachinationisRatio.Physics;
using Microsoft.Xna.Framework;

namespace XNAMachinationisRatio {

    /// <summary>
    /// Game Object is an Object that can be added on a scene
    /// </summary>
    public abstract class GameObject : IComponentContainer {

        protected Dictionary<ComponentType, IGameObjectComponent> mComponentSet = null;
        
        private Vector3 mWorldPosition;
        private Vector3 mOrientation;

        private Vector3 mVelocity;
        private Vector3 mAceleration;

        private Quaternion mRotation;
        private Vector3 mMaxVelocity;
        private Vector3 mMaxRotation;

        /// <summary>
        /// GameObject unique name
        /// </summary>
        private String mName = null;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="pTokenName">Token Name</param>
        /// <param name="pWorldPosition">Initial World Position</param>
        protected GameObject(String pTokenName, Vector3 pWorldPosition) {
            mComponentSet = new Dictionary<ComponentType, IGameObjectComponent>();
            mName = pTokenName;
            Position = pWorldPosition;
        }
        
        /// <summary>
        /// Name of the Game Object
        /// </summary>
        public String Name { 
            get { return mName; } 
            private set { mName = value; }
        }
        /// <summary>
        /// World position of the Game Object
        /// </summary>
        public virtual Vector3 Position { 
            get { return mWorldPosition; } 
            set { mWorldPosition = value; }
        }
        /// <summary>
        /// Orientation of the Game Object
        /// </summary>
        public Vector3 Orientation {
            get { return mOrientation; }
            set { mOrientation = value; mOrientation.Normalize(); }
        }
        /// <summary>
        /// Velocity : how fast game object position is changing
        /// </summary>
        public Vector3 Velocity {
            get { return mVelocity; }
            set { mVelocity = value; }
        }
        /// <summary>
        /// Velocity : how fast game object position is changing
        /// </summary>
        public Vector3 Acceleration {
            get { return mAceleration; }
            set { mAceleration = value; }
        }
        /// <summary>
        /// Max Velocity : how fast game object position is changing
        /// </summary>
        public Vector3 MaxVelocity {
            get { return mMaxVelocity; }
            set { mMaxVelocity = value; }
        }
        /// <summary>
        /// Angular velocity : how fast game object orientation is changing
        /// </summary>
        public Quaternion Rotation {
            get { return mRotation; }
            set { mRotation = value; }
        }

        /// <summary>
        /// Max Angular velocity : how fast game object orientation is changing
        /// </summary>
        public Vector3 MaxRotation {
            get { return mMaxRotation; }
            set { mMaxRotation = value; }
        }

        /// <summary>
        /// Set Game Object Position
        /// </summary>
        /// <param name="pX">X coordinate</param>
        /// <param name="pY">Y coordinate</param>
        /// <param name="pZ">Z coordinate</param>
        public void SetPosition(float pX, float pY, float pZ) {
            mWorldPosition.X = pX;
            mWorldPosition.Y = pY;
            mWorldPosition.Z = pZ;
        }
        #region Component Management
        /// <summary>
        /// Inject component
        /// </summary>
        /// <param name="pType">Component Type</param>
        /// <param name="pComponentToAdd">Component to Inject</param>
        public void Inject(ComponentType pType, IGameObjectComponent pComponentToAdd) {
            if (pComponentToAdd != null) {
                pComponentToAdd.OnBind(this);
                mComponentSet.Add(pType, pComponentToAdd);
            }
        }

        /// <summary>
        /// Eject a component
        /// </summary>
        /// <param name="pType">Component Type to Eject</param>
        public virtual void Eject(ComponentType pType) {
            mComponentSet.Remove(pType);
        }

        /// <summary>
        /// Get if object is Renderable
        /// </summary>
        /// <returns></returns>
        public bool IsRenderable() {
            return mComponentSet.ContainsKey(ComponentType.GRAPHICS);
        }

        /// <summary>
        /// Get if object has a physical body
        /// </summary>
        /// <returns></returns>
        public bool HasAPhysicalBody() {
            return mComponentSet.ContainsKey(ComponentType.PHYSICS);
        }


        internal IRenderableObject GetGraphicComponent() {
            IGameObjectComponent graphic = null;
            mComponentSet.TryGetValue(ComponentType.GRAPHICS, out graphic);
            return (IRenderableObject)graphic;
        }

        internal PhysicalBody GetPhysicsComponent() {
            IGameObjectComponent phys = null;
            mComponentSet.TryGetValue(ComponentType.PHYSICS, out phys);
            return (PhysicalBody)phys;
        }

        internal IAIController GetAIComponent() {
            IGameObjectComponent ai = null;
            mComponentSet.TryGetValue(ComponentType.AI, out ai);
            return (IAIController)ai;
        }

        #endregion
        
        #region Editing Properties Accessors

        protected internal IGraphicProperties GraphicProperties {
            get { return GetGraphicComponent(); }

        }
        protected internal IPhysicsProperties PhysicsProperties {
            get { return GetPhysicsComponent(); }
        }

        #endregion
        /// <summary>
        /// Get Object dimension
        /// </summary>
        public abstract Dimension Type { get; }
    }
}
