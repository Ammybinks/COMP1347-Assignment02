using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAMachinationisRatio {
    public interface IPhysicsProperties {
        float Mass { set; get; }


        IPhysicsProperties SetCollisionRadius(float pCollisionRadius);


        IPhysicsProperties SetBlockable(bool pIsBlockable);

        IPhysicsProperties SetMovable(bool pIsMovable);
    }
}
