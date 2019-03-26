using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAMachinationisRatio.Util {
    public class TreeNode<T> {
        protected List<TreeNode<T>> mChildList;
        protected TreeNode<T> mParentNode;

        public TreeNode(TreeNode<T> pParentNode) {
            mParentNode = pParentNode;
            mChildList = new List<TreeNode<T>>();
        }

        public void Attach(TreeNode<T> pChild) {

            if (!mChildList.Contains(pChild))
                mChildList.Add(pChild);
        }

        public void Detach(TreeNode<T> pChild) {
            // reallocate in Tree structure
            if (mParentNode != null) {
                this.Remove(pChild);
                mParentNode.ReallocateDetachedChild(pChild);
            }
        }

        public void Remove(TreeNode<T> pChild) {
            if (!mChildList.Contains(pChild))
                mChildList.Remove(pChild);
        }

        protected void ReallocateDetachedChild(TreeNode<T> pChild) {
            // root Node automatically accept detached child
            if (mParentNode == null) {
                this.Attach(pChild);
            }
        }

        public void SetParent(TreeNode<T> pParent) {
            mParentNode = pParent;
        }

        public IList<TreeNode<T>> GetChildList() {
            return mChildList;
        }
    }
}