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
    /// ��Ҳ���������������ս����������ͳ�ƵĲ��󿨲���һ��������ĵ�λ
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
        /// ��ȡս��������������������в��󿨣���ȡ�������Ӱ��λ�ã����ݻ�Ӱ��λ�����ɶ�Ӧ�ĵ�λ
        /// </summary>
        public void CreatePlayerSoliders()
        {
            foreach (FormatCard formatCard in BattleManager.instance.allSelectedCards)
            {
                //��ȡҪ���ɵĵ�λ
                IUnit unitPrefab = formatCard.parentParameter.soldierPrefab.GetComponent<IUnit>();
                //��ȡ��Ӱλ��
                List<Vector3> shadowPositions = new List<Vector3>();
                //�������󿨵Ļ�Ӱ���Ӷ���õ�����ÿ����Ӱ��λ��
                foreach (Transform t in formatCard.shadowObject.transform)
                    shadowPositions.Add(t.position);
                //�����õ�������λ�ã��������������µ�λ
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
        /// ����һ����λ��������Ӫ����Ҫ�Ķ���id
        /// ������Ӫ��ȡ��Ҫ���ɵ�λ�ã�����id��ȡ��Ҫ���ɵĶ���Ȼ������
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
        /// ����һ����λ��������Ӫ����Ҫ�Ķ���id������λ�ã�������λ�ô�����λ
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
        /// ����һ�鵥λ��������Ӫ������id������id��ƫ��
        /// �ȸ��ݴ������Ӫ��ƫ��ȷ����������λ�ã�Ȼ���ȡ��Ӧ�����еĸ�������λ�ã����õ�����λ�����ɷ�����������λ�����ɾ���ĵ�λ
        /// </summary>
        /// <param name="camp"></param>
        /// <param name="soldierId"></param>
        /// <param name="formationId"></param>
        public void CreateOneTeamUnit(Camp camp, string soldierId, string formationId, Vector3 offset)
        {
            SceneObjectsManager objectsManager = SceneObjectsManager.instance;
            //��ȡ����λ��
            Vector3 generateCenrerPos = (camp == Camp.demon ? objectsManager.playerEntityGeneratePoint.position : objectsManager.enemyEntityGeneratePoint.position) + offset;
            //��ȡ��������
            SoldierFormation formation = GameDataManager.instance.GetFormationById(formationId);
            //���������еĸ���ƫ��λ�ã����ɾ���ĵ�λ����
            foreach(Vector3 v in formation.soldierOffsets)
            {
                CreateOneUnit(camp, soldierId, generateCenrerPos + v);
            }
        }

    }
}