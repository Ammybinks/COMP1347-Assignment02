using System;
using Microsoft.Xna.Framework;

namespace XNAMachinationisRatio.Physics {
   
    class BoundingSphere {
        
        private PhysicalBody mBody; 
	    private float mRadius;
      
        public BoundingSphere(PhysicalBody body, float pRadius) {
            mBody = body;
            mRadius = pRadius;
        }
        public Vector3 Center {
            get { return mBody.WorldPosition; }
        }
        public Contact CollideWith(BoundingSphere other, long pGameTime) {
            Vector3 distanceVect = Vector3.Subtract(other.Center,this.Center);
            float distance = distanceVect.Length();

            if ((this.mRadius + other.mRadius) > distance) {
                float penetrationDepth = (this.mRadius + other.mRadius) - distance;
                Vector3 normal = (Vector3.Normalize(distanceVect));
                return new Contact(pGameTime, penetrationDepth, normal);
      		}
            return null;
        }
    }
}
