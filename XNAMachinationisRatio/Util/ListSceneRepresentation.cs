using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAMachinationisRatio.Util {
    abstract class ListSceneRepresentation<T> : ISceneRepresentationTrait<T> {
        protected IList<T> mList;

        public ListSceneRepresentation() {
            mList = new List<T>();
        }

        public abstract void OnPlacedObject(GameObject pPlacedObject);
        public abstract void OnRemovedObject(GameObject pPlacedObject);

        public virtual void AddToScene(T pElement) {
            if(pElement!=null && !mList.Contains(pElement)) {
                mList.Add(pElement);
            }
        }

        public virtual void RemoveFromScene(T pElement) {
            mList.Remove(pElement);
        }
    }
}
