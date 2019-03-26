using System;
using XNAMachinationisRatio;

namespace XNAMachinationisRatio.Physics {
    public interface ICollisionListener {
        void OnTouch(GameObject pOther);
    }
}
