using System;
using XNAMachinationisRatio.Resource;

namespace XNAMachinationisRatio.Systems {
    /// <summary>
    /// Resource manager
    /// </summary>
    public interface IResourceManager : ISceneListener {
        void RegisterTo(IScene pScene);
        void Import( AssetLibrary pAssetLibrary);
        void Load(IScene pScene);
    }
}
