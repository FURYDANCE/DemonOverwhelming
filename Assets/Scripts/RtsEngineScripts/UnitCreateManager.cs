using UnityEngine;
using System;
using RTSEngine.Game;
using RTSEngine.Determinism;
using RTSEngine;
using RTSEngine.Entities;
using RTSEngine.UnitExtension;
using System.Collections.Generic;

namespace DemonOverwhelming
{
    /// <summary>
    /// 玩家波次生成器，遍历战斗管理器中统计的布阵卡并逐一生成所需的单位
    /// </summary>
    public class UnitCreateManager : MonoBehaviour, IPostRunGameService
    {
        protected IUnitManager unitMgr { private set; get; }

        public void Init(IGameManager manager)
        {
            unitMgr = manager.GetService<IUnitManager>();
        }
        public static UnitCreateManager instance;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(instance);
            }
            instance = this;
        }
        /// <summary>
        /// 读取战斗管理器中所保存的所有布阵卡，读取其各个幻影的位置，根据幻影的位置生成对应的单位
        /// </summary>
        public void CreatePlayerSoliders()
        {
            foreach (FormatCard formatCard in BattleManager.instance.allSelectedCards)
            {
                //获取要生成的单位
                IUnit unitPrefab = formatCard.parentParameter.soldierPrefab.GetComponent<IUnit>();
                //获取幻影位置
                List<Vector3> shadowPositions = new List<Vector3>();
                //遍历布阵卡的幻影的子对象得到布阵卡每个幻影的位置
                foreach (Transform t in formatCard.shadowObject.transform)
                    shadowPositions.Add(t.position);
                //遍历得到的所有位置，在其坐标生成新单位
                foreach (Vector3 v in shadowPositions)
                {
                    unitMgr.CreateUnit(unitPrefab, v, Quaternion.identity, new InitUnitParameters
                    {
                        free = true,

                        useGotoPosition = true,
                        gotoPosition = v + Vector3.right * 500,
                    });
                }
            }
        }


        /// <summary>
        /// 创造一个单位，传入阵营和需要的对象id
        /// 根据阵营获取需要生成的位置，根据id获取需要生成的对象，然后生成
        /// </summary>
        public void CreateOneUnit(Camp camp, string id)
        {
            SceneObjectsManager objectsManager = SceneObjectsManager.instance;
            GameObject findedPrefab = GameDataManager.instance.GetEntityDataById(id).unitPrefab;
            unitMgr.CreateUnit(findedPrefab.GetComponent<IUnit>(), (camp == Camp.demon ? objectsManager.playerEntityGeneratePoint.position : objectsManager.enemyEntityGeneratePoint.position), Quaternion.identity, new InitUnitParameters
            {
                free = true,

                useGotoPosition = true,
                gotoPosition = camp == Camp.demon ? (objectsManager.playerEntityGeneratePoint.position + Vector3.right * 500) : (objectsManager.enemyEntityGeneratePoint.position - Vector3.right * 500),
            });
        }


        /// <summary>
        /// 创造一个单位，传入阵营，需要的对象id和生成位置，在生成位置创建单位
        /// </summary>
        /// <param name="camp"></param>
        /// <param name="id"></param>
        /// <param name="generatePos"></param>
        public void CreateOneUnit(Camp camp, string id, Vector3 generatePos)
        {
            SceneObjectsManager objectsManager = SceneObjectsManager.instance;
            GameObject findedPrefab = GameDataManager.instance.GetEntityDataById(id).unitPrefab;
            unitMgr.CreateUnit(findedPrefab.GetComponent<IUnit>(), generatePos, Quaternion.identity, new InitUnitParameters
            {
                free = true,

                useGotoPosition = true,
                gotoPosition = camp == Camp.demon ? (generatePos + Vector3.right * 500) : (generatePos - Vector3.right * 500),
            });
        }


        /// <summary>
        /// 生成一组单位，传入阵营，对象id，阵型id和偏移
        /// 先根据传入的阵营和偏移确定具体生成位置，然后获取对应阵型中的各个具体位置，调用单个单位的生成方法，传入其位置生成具体的单位
        /// </summary>
        /// <param name="camp"></param>
        /// <param name="soldierId"></param>
        /// <param name="formationId"></param>
        public void CreateOneTeamUnit(Camp camp, string soldierId, string formationId, Vector3 offset)
        {
            SceneObjectsManager objectsManager = SceneObjectsManager.instance;
            //获取中心位置
            Vector3 generateCenrerPos = (camp == Camp.demon ? objectsManager.playerEntityGeneratePoint.position : objectsManager.enemyEntityGeneratePoint.position) + offset;
            //获取阵型数据
            SoldierFormation formation = GameDataManager.instance.GetFormationById(formationId);
            //遍历阵型中的各个偏移位置，生成具体的单位对象
            foreach(Vector3 v in formation.soldierOffsets)
            {
                CreateOneUnit(camp, soldierId, generateCenrerPos + v);
            }
        }

    }
}