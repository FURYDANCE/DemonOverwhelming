using UnityEngine;

using RTSEngine.Game;
using RTSEngine.Determinism;
using RTSEngine;
using RTSEngine.Entities;
using RTSEngine.UnitExtension;

/// <summary>
/// 用RTSEngine生成单位
/// </summary>
public class UnitWaveSpawner : MonoBehaviour, IPostRunGameService
{
    [SerializeField, EnforceType(typeof(IUnit), prefabOnly: true)]
    private GameObject unitPrefabObj = null;
    private IUnit unitPrefab;
    [SerializeField]
    private int spawnAmount = 3;

    [SerializeField]
    private Transform spawnTransform = null;
    [SerializeField]
    private Transform gotoTransform = null;

    protected IUnitManager unitMgr { private set; get; }

    [SerializeField]
    private float spawnPeriod = 5.0f;
    private TimeModifiedTimer timer;

    public void Init(IGameManager gameMgr)
    {
        timer = new TimeModifiedTimer(spawnPeriod);
        if (!spawnTransform.IsValid() || !gotoTransform.IsValid() || !unitPrefabObj.IsValid())
        {
            Debug.LogError("[UnitWaveSpawner] All inspector fields must be assigned!");
            return;
        }

        unitPrefab = unitPrefabObj.GetComponent<IUnit>();
        unitMgr = gameMgr.GetService<IUnitManager>();
    }

    private void Update()
    {
        if (timer.ModifiedDecrease())
        {
            timer.Reload();

            // Spawn Unit Here
            for (int i = 0; i < spawnAmount; i++)
            {
                unitMgr.CreateUnit(unitPrefab, spawnTransform.position, Quaternion.identity, new InitUnitParameters
                {
                    free = true,

                    useGotoPosition = true,
                    gotoPosition = gotoTransform.position,
                });
            }
        }
    }
}