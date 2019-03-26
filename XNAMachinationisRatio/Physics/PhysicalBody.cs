using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using XNAMachinationisRatio.Util;

namespace XNAMachinationisRatio.Physics {
    public class PhysicalBody :  GameObjectComponent, IPhysicsProperties, IPhysicsActuator {
        
        private const float DEFAULT_INVERSE_MASS = 1.0f;
        
        // f = f0 + f1 + ... + fn
        private Vector3 mForceAccumulator;
        private float mInverseMass;
        private BoundingSphere mBoundingVolume;
        private bool mIsBlockable;
        private bool mIsMovable;

        public PhysicalBody() {
            mInverseMass = DEFAULT_INVERSE_MASS;
        }
        /// <summary>
        /// Add a force to the game object
        /// </summary>
        /// <param name="pForce"></param>
        public void AddForce(Vector3 pForce) {
            mForceAccumulator= Vector3.Add(mForceAccumulator, pForce);
        }

        internal void Move(float pX, float pY, float pZ) {
            SetWorldPosition( Vector3.Add(Self.Position,new Vector3(pX,pY,pZ)));
        }

        public void Integrate(ref GameTime pGameTime) {
            // update position
            SetWorldPosition(Vector3.Add(Self.Position, Vector3.Multiply(Self.Velocity, (float)pGameTime.ElapsedGameTime.TotalSeconds)));
            // acc = F * 1/M
            Vector3 resultingacceleration = Vector3.Multiply(mForceAccumulator, mInverseMass);
            // update acceleration
            Self.Acceleration = Vector3.Add(Self.Acceleration, resultingacceleration);
            // velocity = velocity + acc * time            
            Self.Velocity = Vector3.Add(Self.Velocity, Vector3.Multiply(mForceAccumulator, (float)pGameTime.ElapsedGameTime.TotalSeconds));
            // reset force accumulator
            mForceAccumulator = Vector3.Zero;
        }

        internal Contact CollideWith(PhysicalBody pOther, long pGameTime) {
            if (mBoundingVolume == null)
                return null;
            return mBoundingVolume.CollideWith(pOther.mBoundingVolume, pGameTime);
        }

        public float Mass {
            get { return 1.0f / mInverseMass; }
            set {
                if (value < 1.0f)
                    mInverseMass = DEFAULT_INVERSE_MASS;
                else
                    mInverseMass = 1 / value;
            }
        }
        public IPhysicsProperties SetCollisionRadius(float pCollisionRadius) {
            mBoundingVolume = new BoundingSphere(this, pCollisionRadius);
            return this;
        }

        public IPhysicsProperties SetBlockable(bool pIsBlockable) {
            mIsBlockable = pIsBlockable;
            return this;
        }

        public IPhysicsProperties SetMovable(bool pIsMovable) {
            mIsMovable = pIsMovable;
            return this;
        }

        public bool IsBlockable() {
            return mIsBlockable;
        }

        internal bool IsMovable() {
            return mIsMovable;
        }


        internal void NotifyCollision(PhysicalBody physicalBody) {
           ICollisionListener collisionListener = Self.GetAIComponent();
           if (null != collisionListener) {
               collisionListener.OnTouch(physicalBody.Self);
           }
        }
    }
}
