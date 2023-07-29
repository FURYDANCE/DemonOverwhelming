using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region 战斗烈度计算相关变量

    /// <summary>
    /// 战斗烈度（总）
    /// </summary>
    [Header("战斗烈度（总）")]
    public float battleIntensity;

    /// <summary>
    /// 当前的战斗烈度等级
    /// </summary>
    [Header("当前的战斗烈度等级和其最大等级")]
    public int battleIntensityLevel;
    /// <summary>
    /// 最大的战斗烈度等级
    /// </summary>
    public int maxIntensityLevel;

    /// <summary>
    /// 生成一波士兵的间隔时间
    /// </summary>
    [Header("生成一波士兵的间隔时间")]
    public float createWaitTime;
    [SerializeField]
    private float createWaitTimer;

    /// <summary>
    /// true代表使用同样的烈度变化程度，false则代表使用不同样的
    /// </summary>
    [Header("勾选后使用同样的烈度变化程度，不勾选则使用不同样的")]
    [Header("若使用不同样的，则需要在烈度等级划分中填好对应的等级所需的烈度")]
    public bool usingSameLevelCaculation;

    //这种是每种战斗烈度的等级需要的烈度变化是相同的
    /// <summary>
    /// 战斗烈度等级划分，战斗烈度（总）除以等级划分得到当前的烈度等级
    /// </summary>
    [Header("战斗烈度等级划分，战斗烈度（总）除以等级划分得到当前的烈度等级")]
    public int intensityLevelClassification_Same;

    //这种是每种战斗烈度的等级需要不同的烈度
    /// <summary>
    /// 战斗烈度等级划分，当战斗烈度（总）减去其中的等级划分时大于零，则战斗烈度等级为对应的等级
    /// </summary>
    [Header("战斗烈度等级划分，当战斗烈度（总）减去其中的等级划分时大于零，则战斗烈度等级为对应的等级")]
    public List<int> intensityLevelClassification_Different;

    /// <summary>
    /// 战斗烈度的自然增长速度
    /// </summary>
    [Header("战斗烈度的自然增长速度")]
    public float intensityIncreaseSpeed;

    #endregion
    /// <summary>
    /// 每个烈度等级对应的可能出现的波次s
    /// 【要和最大烈度等级的数量一样】
    /// </summary>
    [Header("每个烈度等级对应的可能出现的波次s【要和最大烈度等级的数量一样】")]
    public WaveCorrespondingToIntensityLevel[] wavesEveryIntensityLevel;



    [Header("调试：勾选后不自然生成波次")]
    public bool debug_DontWork;
    private void Start()
    {
        createWaitTimer = createWaitTime;
    }
    private void FixedUpdate()
    {
        BattleIntensityCaculate();
        if (debug_DontWork)
            return;
        createWaitTimer -= Time.deltaTime;
        if (createWaitTimer <= 0)
        {
            createWaitTimer = createWaitTime;
            CreateWaveEnemy();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CreateWaveEnemy();
        }

    }

    /// <summary>
    /// 战斗烈度的相关计算
    /// </summary>
    public void BattleIntensityCaculate()
    {
        //每秒增加的战斗烈度
        battleIntensity += Time.deltaTime * intensityIncreaseSpeed;
        //计算当前的烈度等级
        if (usingSameLevelCaculation)
        {
            battleIntensityLevel = (int)(battleIntensity / intensityLevelClassification_Same);
        }
        else
        {
            for (int i = 0; i < intensityLevelClassification_Different.Count; i++)
            {
                if (battleIntensity - intensityLevelClassification_Different[i] > 0)
                {
                    battleIntensityLevel = i + 1;
                }
            }
        }
        if (battleIntensityLevel > maxIntensityLevel)
            battleIntensityLevel = maxIntensityLevel;
    }

    /// <summary>
    /// 根据当前的烈度等级，生成对应的烈度等级中可能出现的波次中的其中一波敌人
    /// </summary>
    public void CreateWaveEnemy()
    {
        //声明一个随机数，当前烈度等级对应的可能出现的波次的list的长度
        int x = Random.Range(0, wavesEveryIntensityLevel[battleIntensityLevel].possibleWaves.Count);
        //选择对应的波次并且生成
        wavesEveryIntensityLevel[battleIntensityLevel].possibleWaves[x].GenerateOneWave();
        Debug.Log("生成了");
        Debug.Log(wavesEveryIntensityLevel[battleIntensityLevel].possibleWaves[x]);
    }



}
/// <summary>
/// 每一个烈度等级所对应的可能出现的波次
/// </summary>
[System.Serializable]
public class WaveCorrespondingToIntensityLevel
{
    public string name;
    public List<Wave> possibleWaves;
}