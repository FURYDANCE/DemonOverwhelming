using UnityEngine;
using DemonOverwhelming;
using RTSEngine;
using RTSEngine.EntityComponent;
using RTSEngine.UI;
using RTSEngine.ResourceExtension;
using RTSEngine.Game;
using RTSEngine.Determinism;

namespace DemonOverwhelming
{
    /// <summary>
    /// 结合RTSEngine的金钱、血液管理器
    /// </summary>

    public class Glod_and_Boold_Manager : MonoBehaviour, IPostRunGameService
    {
        [SerializeField]
        private ResourceInput[] sellResources = new ResourceInput[0];
        [SerializeField]
        private float addTimer;
        private TimeModifiedTimer timer;
        protected IResourceManager resourceMgr { private set; get; }
        public void Init(IGameManager manager)
        {
            resourceMgr = manager.GetService<IResourceManager>();
            timer = new TimeModifiedTimer(addTimer);
            Debug.Log(resourceMgr);
        }

        private void Update()
        {
            if (timer.ModifiedDecrease())
            {
                timer.Reload();
                resourceMgr.UpdateResource(0, sellResources, add: true);
            }
        }
    }
}