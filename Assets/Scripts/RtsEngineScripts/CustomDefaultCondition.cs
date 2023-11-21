using UnityEngine;

using RTSEngine.Game;
using RTSEngine.Determinism;
using RTSEngine;
using RTSEngine.Entities;
using RTSEngine.UnitExtension;
using RTSEngine.EntityComponent;
using RTSEngine.ResourceExtension;
using RTSEngine.Event;

namespace DemonOverwhelming
{
    /// <summary>
    /// ��������������ʵ���������ʱ������Ϸʧ��
    /// </summary>
    public class CustomDefaultCondition : EntityComponentBase, IPostRunGameService
    {
   
        protected IGameManager gameManager { private set; get; }
        protected IGlobalEventPublisher globalEvent { private set; get; }
        public void Init(IGameManager gameMgr)
        {
            //this.gameManager = gameMgr;
            //this.globalEvent = gameMgr.GetService<IGlobalEventPublisher>();

        }
        protected override void OnInit()
        {
            this.gameManager = gameMgr;
            this.globalEvent = gameMgr.GetService<IGlobalEventPublisher>();
        }

        protected override void OnDisabled()
        {
            TriggerDefeatCondition();
        }
        private void TriggerDefeatCondition()
        {
            Debug.Log(gameManager);
            Debug.Log(globalEvent);
            globalEvent.RaiseFactionSlotDefeatConditionTriggeredGlobal(
                gameMgr.LocalFactionSlot,
                new DefeatConditionEventArgs(DefeatConditionType.custom));
        }
    }
}