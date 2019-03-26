using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAMachinationisRatio.Physics {
    class CollisionData {
        public Contact Contact; // dati del contatto
        public PhysicalBody First; // primo collidable
        public PhysicalBody Second; // secondo collidable

        public CollisionData(PhysicalBody pFirst, PhysicalBody pSecond, Contact pContact) {
            Contact = pContact;
            First = pFirst;
            Second = pSecond;
        }
    }
    class CollisionManager {

        public static void ResolvePenetrations(IList<CollisionData> pCollisionList) {

            for (int i = 0; i < pCollisionList.Count; i++) {
                if (pCollisionList[i].Second.IsBlockable() && pCollisionList[i].First.IsBlockable()) {
                    float deltaX = pCollisionList[i].Contact.Penetration.X;
                    float deltaY = pCollisionList[i].Contact.Penetration.Y;
                    float deltaZ = pCollisionList[i].Contact.Penetration.Z;
                    // if both are movable
                    if (pCollisionList[i].First.IsMovable() && pCollisionList[i].Second.IsMovable()) {
                        pCollisionList[i].First.Move((-deltaX * 0.5f), (-deltaY * 0.5f), (-deltaZ * 0.5f));
                        pCollisionList[i].Second.Move(+(deltaX * 0.5f), +(deltaY * 0.5f), +(deltaZ * 0.5f));
                    }
                        //if only one is movable
                    else {
                       
                        if (pCollisionList[i].First.IsMovable())
                            pCollisionList[i].First.Move(-deltaX, -deltaY, -deltaZ);
                        if (pCollisionList[i].Second.IsMovable())
                            pCollisionList[i].Second.Move(deltaX, deltaY, deltaZ);
                    }
                }
            }
        }
    }
}
