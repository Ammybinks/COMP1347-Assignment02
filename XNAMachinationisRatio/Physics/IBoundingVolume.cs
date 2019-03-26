using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAMachinationisRatio.Physics {
    interface IBoundingVolume {
        Contact collideWith(BoundingSphere pBV, long pGameTime);
    }
}
