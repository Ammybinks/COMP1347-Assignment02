using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XNAMachinationisRatio.Util;

namespace XNAMachinationisRatio.Physics {
    class PhysicsSceneGraph : ListSceneRepresentation<PhysicalBody> {

        IList<CollisionData> mCollisionList = new List<CollisionData>(30);

        public override void OnPlacedObject(GameObject pPlacedObject) {
            if (pPlacedObject.HasAPhysicalBody()) {
                this.AddToScene(pPlacedObject.GetPhysicsComponent());
            }
        }

        public override void OnRemovedObject(GameObject pRemovedObject) {
            if (pRemovedObject.IsRenderable()) {
                this.RemoveFromScene(pRemovedObject.GetPhysicsComponent());
            }
        }

        public void Update(ref GameTime pGameTime) {
            // resolve forces
            foreach (PhysicalBody body in mList) {

                body.Integrate(ref pGameTime);
            }
            
            // detect collisions
            for (int i = 0; i < mList.Count; i++) {
                for (int j = (i + 1); j < mList.Count; j++) {
                    Contact contact = mList[i].CollideWith(mList[j], pGameTime.ElapsedGameTime.Milliseconds);
                    if (contact != null) {
                        mCollisionList.Add(new CollisionData(mList[i], mList[j], contact));
                    }
                }
            }
            // adjust positions
            CollisionManager.ResolvePenetrations(mCollisionList);

            // notify events to Token AI
            NotifyCollisionsToAI();
            mCollisionList.Clear();
        }
        private void NotifyCollisionsToAI() {

            for (int i = 0; i < mCollisionList.Count; i++) {
                mCollisionList[i].First.NotifyCollision(mCollisionList[i].Second);
                mCollisionList[i].Second.NotifyCollision(mCollisionList[i].First);
            }
        }

    }
}