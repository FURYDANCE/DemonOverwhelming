using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    [Header("�����buff��Ϣ")]
    public BuffObject buffDatas;
    //public List<BuffInformation> buffDatas;
    /// <summary>
    /// buff����������
    /// </summary>
    public static BuffManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }
    /// <summary>
    /// ͨ��bufftypeid�ж�Ӧ����������buff����
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BuffBase SetBuffScript(string buffTypeId)
    {
        if (buffTypeId == "1")
            return new Buff_AttackStrengthen();
        else
            return null;
    }
    /// <summary>
    /// ͨ��id�ҵ�buff�����ݵķ���
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BuffInformation findBuffData(string id)
    {
        BuffInformation data = buffDatas.buffs.Find((BuffInformation data) => { return data.buffId == id; });
        if (data != null)
            return data;
        else
            return null;
    }
    /// <summary>
    /// ��ʵ����buff����Ҫ����ʵ����½�һ��buffinformation��
    /// </summary>
    /// <param name="e"></param>
    /// <param name="buff"></param>
    public void EntityAddBuff(Entity e, BuffInformation buff)
    {
        e.AddBuff(buff);
    }
    /// <summary>
    /// ��ʵ����buff������ʵ�����Ҫ��buffid���ɣ�ǰ�������buff�����ڱ����棩
    /// </summary>
    /// <param name="e"></param>
    /// <param name="buffId"></param>
    public void EntityAddBuff(Entity e, string buffId)
    {

        BuffInformation b = findBuffData(buffId);
        if (b == null)
            return;
        b.buff = SetBuffScript(b.buffTypeId);
        e.AddBuff(b);
    }
}