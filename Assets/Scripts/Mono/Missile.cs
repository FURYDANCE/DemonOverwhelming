using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 投射物，向目标位移，位移到目标位置时目标执行承受伤害的方法并摧毁该投射物
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
    //根据battlemanager中的生成投射物方法中传入的是transform还是v3，决定是向目标的transform移动还是v3移动
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
        //位移
        Translate();
        //检测是否移动到终点
        MoveEndCheck();
        //当两种移动方式都为空时，判定目标死亡，摧毁跟随目标的投射物
        if (targetTransform == null && targetV3 == Vector3.zero)
            Destroy(gameObject);
        //调整角度
        if (targetTransform != null)
        {
            RotationCaculate(nearPos);
        }
    }

    #region 初始化相关

    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialization()
    {
        //开始时通过id获取到该投射物应有的变量
        parameter = new UnitParameter_Missile();
        Debug.Log(id);
        SetParameter(id);
        //如果使用生命周期，则开始计时
        if (parameter.useLifeTime)
            StartCoroutine(LifeTimeCaculate());
        //如果使用aoe攻击，则开始执行aoe攻击
        if (parameter.useAoe)
            StartCoroutine(AoeAttack());
        //抛物线位移的开始
        if (parameter.moveType == MissileMoveType.parabola)
        {
            if (targetTransform != null)
                gameObject.AddComponent<ArcMovement>().SetTarget(targetTransform, parameter.arcMoveTime, parameter.arcMoveHeight);
            else
                gameObject.AddComponent<ArcMovement>().SetTarget(targetV3, parameter.arcMoveTime, parameter.arcMoveHeight);
        }
        //检测开始时生成的对象
        if (parameter.startObjectId != "0")
        {
            VfxManager.instance.CreateVfx(VfxManager.instance.GetVfxByIdOrName(parameter.startObjectId).vfx, transform.position, new Vector3(4, 4, 4), 1.5f);
        }
    }

    /// <summary>
    /// 通过id获取变量
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
    /// 设置攻击目标
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

    #region 移动、旋转相关

    //位移
    public void Translate()
    {
        if (targetTransform != null && set)
        {
            //设置判断投射物移动到终点的点（有目标对象的transf时，有碰撞体选择碰撞体最近点，没有时取目标坐标，没有transf时选取目标v3坐标）
            if (targetCollider)
            {
                nearPos = targetCollider != null ? targetCollider.ClosestPointOnBounds(transform.position) : targetTransform.position;
            }
            else
            {
                nearPos = targetV3;
            }
            //直线位移
            if (parameter.moveType == MissileMoveType.direct)
            {
                //目标有碰撞体时，向碰撞体最近位置位移，无碰撞体时，向目标坐标位移
                if (targetCollider)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(nearPos.x, nearPos.y /*+ targetEntity.GetComponent<BoxCollider>().size.y*/ , transform.position.z), parameter.speed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetTransform != null ? targetTransform.position : targetV3, parameter.speed * Time.deltaTime);
                }
            }
            //抛物线位移
            //具体移动在start中
            //直接瞬移到目标点
            if (parameter.moveType == MissileMoveType.teleport)
            {
                transform.position = targetTransform != null ? targetTransform.position : targetV3;
            }
        }
    }
    /// <summary>
    /// 检测是否移动到终点，以及移动到终点时执行的方法
    /// </summary>
    public void MoveEndCheck()
    {
        //移动到目标位置，若目标为实体，则伤害目标，若没有,当不作用生命周期时消亡,若作用buff，则给目标上buff
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
    //调整角度
    public void RotationCaculate(Vector3 target)
    {
        if (parameter.moveType == MissileMoveType.parabola)
            return;
        Vector3 v = target - transform.position; //首先获得目标方向
        v.z = 0; //这里一定要将z设置为0
        float angle = Vector3.SignedAngle(Vector3.up, v, Vector3.forward); //得到围绕z轴旋转的角度
        Quaternion rotation = Quaternion.Euler(0, 0, angle); //将欧拉角转换为四元数
        transform.rotation = rotation;
    }

    #endregion


    #region 攻击、buff、死亡相关
    /// <summary>
    /// 每隔一段时间进行一次aoe的协程，当设置aoe最大值时，执行次数超过最大值则摧毁
    /// </summary>
    /// <returns></returns>
    IEnumerator AoeAttack()
    {
        Debug.Log("开始Aoe");
        while (true)
        {
            yield return new WaitForSeconds(parameter.aoeWaitTime);
            //生成aoe区域
            BattleManager.instance.CreateAoeHurtArea(transform.position, parameter.aoeArea, creater, camp, parameter.damageData);
            if (parameter.aoeAmount != 0)
            {
                //如果作用最大aoe次数，则到达最大次数的时候摧毁
                nowAoeAmount++;
                if (nowAoeAmount >= parameter.aoeAmount)
                    Die();
            }
        }
    }


    /// <summary>
    /// 到达存在时间后消除
    /// </summary>
    /// <returns></returns>
    IEnumerator LifeTimeCaculate()
    {
        yield return new WaitForSeconds(parameter.lifeTime);
        Die();
    }
    /// <summary>
    /// 死亡
    /// </summary>
    public void Die()
    {
        //检测销毁时生成新对象
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
