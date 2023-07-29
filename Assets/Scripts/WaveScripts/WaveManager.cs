using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region ս���Ҷȼ�����ر���

    /// <summary>
    /// ս���Ҷȣ��ܣ�
    /// </summary>
    [Header("ս���Ҷȣ��ܣ�")]
    public float battleIntensity;

    /// <summary>
    /// ��ǰ��ս���Ҷȵȼ�
    /// </summary>
    [Header("��ǰ��ս���Ҷȵȼ��������ȼ�")]
    public int battleIntensityLevel;
    /// <summary>
    /// ����ս���Ҷȵȼ�
    /// </summary>
    public int maxIntensityLevel;

    /// <summary>
    /// ����һ��ʿ���ļ��ʱ��
    /// </summary>
    [Header("����һ��ʿ���ļ��ʱ��")]
    public float createWaitTime;
    [SerializeField]
    private float createWaitTimer;

    /// <summary>
    /// true����ʹ��ͬ�����Ҷȱ仯�̶ȣ�false�����ʹ�ò�ͬ����
    /// </summary>
    [Header("��ѡ��ʹ��ͬ�����Ҷȱ仯�̶ȣ�����ѡ��ʹ�ò�ͬ����")]
    [Header("��ʹ�ò�ͬ���ģ�����Ҫ���Ҷȵȼ���������ö�Ӧ�ĵȼ�������Ҷ�")]
    public bool usingSameLevelCaculation;

    //������ÿ��ս���Ҷȵĵȼ���Ҫ���Ҷȱ仯����ͬ��
    /// <summary>
    /// ս���Ҷȵȼ����֣�ս���Ҷȣ��ܣ����Եȼ����ֵõ���ǰ���Ҷȵȼ�
    /// </summary>
    [Header("ս���Ҷȵȼ����֣�ս���Ҷȣ��ܣ����Եȼ����ֵõ���ǰ���Ҷȵȼ�")]
    public int intensityLevelClassification_Same;

    //������ÿ��ս���Ҷȵĵȼ���Ҫ��ͬ���Ҷ�
    /// <summary>
    /// ս���Ҷȵȼ����֣���ս���Ҷȣ��ܣ���ȥ���еĵȼ�����ʱ�����㣬��ս���Ҷȵȼ�Ϊ��Ӧ�ĵȼ�
    /// </summary>
    [Header("ս���Ҷȵȼ����֣���ս���Ҷȣ��ܣ���ȥ���еĵȼ�����ʱ�����㣬��ս���Ҷȵȼ�Ϊ��Ӧ�ĵȼ�")]
    public List<int> intensityLevelClassification_Different;

    /// <summary>
    /// ս���Ҷȵ���Ȼ�����ٶ�
    /// </summary>
    [Header("ս���Ҷȵ���Ȼ�����ٶ�")]
    public float intensityIncreaseSpeed;

    #endregion
    /// <summary>
    /// ÿ���Ҷȵȼ���Ӧ�Ŀ��ܳ��ֵĲ���s
    /// ��Ҫ������Ҷȵȼ�������һ����
    /// </summary>
    [Header("ÿ���Ҷȵȼ���Ӧ�Ŀ��ܳ��ֵĲ���s��Ҫ������Ҷȵȼ�������һ����")]
    public WaveCorrespondingToIntensityLevel[] wavesEveryIntensityLevel;



    [Header("���ԣ���ѡ����Ȼ���ɲ���")]
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
    /// ս���Ҷȵ���ؼ���
    /// </summary>
    public void BattleIntensityCaculate()
    {
        //ÿ�����ӵ�ս���Ҷ�
        battleIntensity += Time.deltaTime * intensityIncreaseSpeed;
        //���㵱ǰ���Ҷȵȼ�
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
    /// ���ݵ�ǰ���Ҷȵȼ������ɶ�Ӧ���Ҷȵȼ��п��ܳ��ֵĲ����е�����һ������
    /// </summary>
    public void CreateWaveEnemy()
    {
        //����һ�����������ǰ�Ҷȵȼ���Ӧ�Ŀ��ܳ��ֵĲ��ε�list�ĳ���
        int x = Random.Range(0, wavesEveryIntensityLevel[battleIntensityLevel].possibleWaves.Count);
        //ѡ���Ӧ�Ĳ��β�������
        wavesEveryIntensityLevel[battleIntensityLevel].possibleWaves[x].GenerateOneWave();
        Debug.Log("������");
        Debug.Log(wavesEveryIntensityLevel[battleIntensityLevel].possibleWaves[x]);
    }



}
/// <summary>
/// ÿһ���Ҷȵȼ�����Ӧ�Ŀ��ܳ��ֵĲ���
/// </summary>
[System.Serializable]
public class WaveCorrespondingToIntensityLevel
{
    public string name;
    public List<Wave> possibleWaves;
}