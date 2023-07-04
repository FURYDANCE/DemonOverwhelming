using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ͷ�����Ŀ��λ�ƣ�λ�Ƶ�Ŀ��λ��ʱĿ��ִ�г����˺��ķ������ݻٸ�Ͷ����
/// </summary>
public class Missile : MonoBehaviour
{
    public Camp camp;
    public string id;
    public Entity creater;
    public UnitParameter_Missile parameter;


    bool set;
    int nowAoeAmount;
    Vector3 targetV3;
    Vector3 nearPos;
    //����battlemanager�е�����Ͷ���﷽���д������transform����v3����������Ŀ���transform�ƶ�����v3�ƶ�
    Transform targetTransform;
    BoxCollider targetCollider;
    Entity targetEntity;

    void Start()
    {
        Initialization();

        //    StartCoroutine(Parabola());
    }
    void Update()
    {
        //λ��
        Translate();
        //����Ƿ��ƶ����յ�
        MoveEndCheck();
        //�������ƶ���ʽ��Ϊ��ʱ���ж�Ŀ���������ݻٸ���Ŀ���Ͷ����
        if (targetTransform == null && targetV3 == Vector3.zero)
            Destroy(gameObject);
        //�����Ƕ�
        if (targetTransform != null)
        {
            RotationCaculate(nearPos);
        }
    }

    #region ��ʼ�����

    /// <summary>
    /// ��ʼ��
    /// </summary>
    public void Initialization()
    {
        //��ʼʱͨ��id��ȡ����Ͷ����Ӧ�еı���
        parameter = new UnitParameter_Missile();
        Debug.Log(id);
        SetParameter(id);
        //���ʹ���������ڣ���ʼ��ʱ
        if (parameter.useLifeTime)
            StartCoroutine(LifeTimeCaculate());
        //���ʹ��aoe��������ʼִ��aoe����
        if (parameter.useAoe)
            StartCoroutine(AoeAttack());
        //������λ�ƵĿ�ʼ
        if (parameter.moveType == MissileMoveType.parabola)
        {
            if (targetTransform != null)
                gameObject.AddComponent<ArcMovement>().SetTarget(targetTransform, parameter.arcMoveTime, parameter.arcMoveHeight);
            else
                gameObject.AddComponent<ArcMovement>().SetTarget(targetV3, parameter.arcMoveTime, parameter.arcMoveHeight);
        }
        //��⿪ʼʱ���ɵĶ���
        if (parameter.startObjectId != "0")
        {
            VfxManager.instance.CreateVfx(VfxManager.instance.GetVfxByIdOrName(parameter.startObjectId).vfx, transform.position, new Vector3(4, 4, 4), 1.5f);
        }
    }

    /// <summary>
    /// ͨ��id��ȡ����
    /// </summary>
    /// <param name="id"></param>
    public void SetParameter(string id)
    {

        if (set == true) return;
        set = true;
        //Debug.Log("id" + id);
        UnitParameter_Missile parameter = GameDataManager.instance.GetMissileDataById(id);
        if (parameter == null)
            return;
        //Debug.Log(GameDataManager.instance.GetMissileDataById(id));
        this.parameter.damageData = parameter.damageData;
        this.parameter.speed = parameter.speed;
        this.parameter.name = parameter.name;
        this.parameter.sprite = parameter.sprite;
        GetComponent<SpriteRenderer>().sprite = this.parameter.sprite;
        this.parameter.moveType = parameter.moveType;
        this.parameter.useLifeTime = parameter.useLifeTime;
        this.parameter.lifeTime = parameter.lifeTime;
        this.parameter.useAoe = parameter.useAoe;
        this.parameter.aoeArea = parameter.aoeArea;
        this.parameter.aoeWaitTime = parameter.aoeWaitTime;
        this.parameter.aoeAmount = parameter.aoeAmount;
        this.parameter.createNewObjectWhenDie = parameter.createNewObjectWhenDie;
        this.parameter.objectCreatedWhenDieId = parameter.objectCreatedWhenDieId;
        this.parameter.startObjectId = parameter.startObjectId;
        this.parameter.endObjectId = parameter.endObjectId;
        this.parameter.trailId = parameter.trailId;
        this.parameter.arcMoveTime = parameter.arcMoveTime;
        this.parameter.arcMoveHeight = parameter.arcMoveHeight;
        name = "Missile_" + parameter.name + "_" + id;
        foreach (BuffInformation buff in creater.buffs)
        {
            buff.buff.OnAddbuff_Missile(this, buff.buffLevel);
        }
        this.parameter.damageData = GameDataManager.instance.GetDamageDataById(parameter.damageDataId);
    }
    /// <summary>
    /// ���ù���Ŀ��
    /// </summary>
    /// <param name="e"></param>
    public void SetTarget(Transform e)
    {
        targetTransform = e;
        targetCollider = e.GetComponent<BoxCollider>();
        targetEntity = e.GetComponent<Entity>();
    }
    public void SetTarget(Vector3 v)
    {
        targetV3 = v;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, parameter.aoeArea);
    }

    #endregion

    #region �ƶ�����ת���

    //λ��
    public void Translate()
    {
        if (targetTransform != null && set)
        {
            //�����ж�Ͷ�����ƶ����յ�ĵ㣨��Ŀ������transfʱ������ײ��ѡ����ײ������㣬û��ʱȡĿ�����꣬û��transfʱѡȡĿ��v3���꣩
            if (targetCollider)
            {
                nearPos = targetCollider != null ? targetCollider.ClosestPointOnBounds(transform.position) : targetTransform.position;
            }
            else
            {
                nearPos = targetV3;
            }
            //ֱ��λ��
            if (parameter.moveType == MissileMoveType.direct)
            {
                //Ŀ������ײ��ʱ������ײ�����λ��λ�ƣ�����ײ��ʱ����Ŀ������λ��
                if (targetCollider)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(nearPos.x, nearPos.y /*+ targetEntity.GetComponent<BoxCollider>().size.y*/ , transform.position.z), parameter.speed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetTransform != null ? targetTransform.position : targetV3, parameter.speed * Time.deltaTime);
                }
            }
            //������λ��
            //�����ƶ���start��
            //ֱ��˲�Ƶ�Ŀ���
            if (parameter.moveType == MissileMoveType.teleport)
            {
                transform.position = targetTransform != null ? targetTransform.position : targetV3;
            }
        }
    }
    /// <summary>
    /// ����Ƿ��ƶ����յ㣬�Լ��ƶ����յ�ʱִ�еķ���
    /// </summary>
    public void MoveEndCheck()
    {
        //�ƶ���Ŀ��λ�ã���Ŀ��Ϊʵ�壬���˺�Ŀ�꣬��û��,����������������ʱ����,������buff�����Ŀ����buff
        if (Vector3.Distance(transform.position, nearPos) < 2)
        {
            if (targetEntity)
            {
                BattleManager.instance.CreateDamage(creater, parameter.damageData, targetEntity);
            }
            targetTransform = null;
            if (!parameter.useLifeTime)
                Die();
        }
    }
    //�����Ƕ�
    public void RotationCaculate(Vector3 target)
    {
        if (parameter.moveType == MissileMoveType.parabola)
            return;
        Vector3 v = target - transform.position; //���Ȼ��Ŀ�귽��
        v.z = 0; //����һ��Ҫ��z����Ϊ0
        float angle = Vector3.SignedAngle(Vector3.up, v, Vector3.forward); //�õ�Χ��z����ת�ĽǶ�
        Quaternion rotation = Quaternion.Euler(0, 0, angle); //��ŷ����ת��Ϊ��Ԫ��
        transform.rotation = rotation;
    }

    #endregion


    #region ������buff���������
    /// <summary>
    /// ÿ��һ��ʱ�����һ��aoe��Э�̣�������aoe���ֵʱ��ִ�д����������ֵ��ݻ�
    /// </summary>
    /// <returns></returns>
    IEnumerator AoeAttack()
    {
        Debug.Log("��ʼAoe");
        while (true)
        {
            yield return new WaitForSeconds(parameter.aoeWaitTime);
            //����aoe����
            BattleManager.instance.CreateAoeHurtArea(transform.position, parameter.aoeArea, creater, camp, parameter.damageData);
            if (parameter.aoeAmount != 0)
            {
                //����������aoe�������򵽴���������ʱ��ݻ�
                nowAoeAmount++;
                if (nowAoeAmount >= parameter.aoeAmount)
                    Die();
            }
        }
    }


    /// <summary>
    /// �������ʱ�������
    /// </summary>
    /// <returns></returns>
    IEnumerator LifeTimeCaculate()
    {
        yield return new WaitForSeconds(parameter.lifeTime);
        Die();
    }
    /// <summary>
    /// ����
    /// </summary>
    public void Die()
    {
        //�������ʱ�����¶���
        if (parameter.endObjectId != "0")
        {
            VfxManager.instance.CreateVfx(VfxManager.instance.GetVfxByIdOrName(parameter.endObjectId).vfx, transform.position, new Vector3(4, 4, 4), 1.5f);
        }
        if (parameter.createNewObjectWhenDie)
        {
            BattleManager.instance.GenerateOneMissle(transform.position, parameter.objectCreatedWhenDieId, camp, creater);
        }
        StopAllCoroutines();
        Destroy(gameObject);
    }

    #endregion



}
