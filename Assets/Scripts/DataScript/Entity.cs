using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
/// <summary>
/// 实体类，挂载在场景中所有的实体上（建筑，兵种，英雄）
/// 鼠标悬停在上面时通过改变material来切换显示
/// </summary>
public class Entity : MonoBehaviour
{

    /// <summary>
    /// 阵营
    /// </summary>
    public Camp camp;
    public bool spineObject;
    /// <summary>
    /// buff
    /// </summary>
    public List<BuffInformation> buffs;
    public List<Character_SpecialTagBase> character_SpecialTagBases;
    public UnitParameter parameter;
    public HpShowUi hpBar;
    SpriteRenderer sprite;
    bool settled;
    Material startMaterial;
    /// <summary>
    /// 所在的兵组
    /// </summary>
    public SoliderGroup parentSoldierGroup;
    [HideInInspector]
    public BoxCollider boxCollider;
    [Header("(调试)无敌状态")]
    public bool hpLock;
    public Animator animator;
    public AnimationsList animations;
    public SkeletonAnimation skAni;
    private void Start()
    {
        buffs = new List<BuffInformation>();
        if (parameter.type == EntityType.building && parameter.ID != "")
            SetEntityParameter();
        GenerateEntity();
    }
    private void FixedUpdate()
    {
        UpdateBuff();
    }
    #region 初始化相关
    public void SetEntityParameter()
    {
        parameter.SetValue(GameDataManager.instance.GetEntityDataById(parameter.ID));
    }
    public void SetParentSoldierGroup(SoliderGroup soliderGroup)
    {
        parentSoldierGroup = soliderGroup;
    }
    /// <summary>
    /// 根据id获取到的变量生成该实体
    /// </summary>
    public void GenerateEntity()
    {
        if (settled)
            return;
        sprite = GetComponent<SpriteRenderer>();
        //设置是否是spine动画
        if (parameter.name.Split("/").Length > 1)
        {
            spineObject = parameter.name.Split("/")[1].Contains("spine") ? true : false;
        }
        if (parameter.hpBarSize == 0) parameter.hpBarSize = 1;
        //生成血条
        if (!hpBar)
        {
            hpBar = Instantiate(SceneObjectsManager.instance.hpBarObject, transform).GetComponent<HpShowUi>();
            //设置血条的尺寸和偏移
            hpBar.barParent.transform.position += parameter.hpBarOffset;
            hpBar.transform.localScale = new Vector3(parameter.hpBarSize, parameter.hpBarSize);
            //刷新血量和血条显示
            parameter.nowHp = parameter.Hp;
            hpBar.Refresh(parameter.nowHp, parameter.Hp);
        }
        //当id不为空时根据id生成目标
        if (parameter.ID != "")
        {
            //设置尺寸
            transform.localScale = new Vector3(parameter.modleSize, parameter.modleSize);
            //设置面向相机
            gameObject.AddComponent<FaceToCamera>();
            //设置贴图或者spine动画
            //没有spine动画时设置贴图
            if (!spineObject && sprite == null)
            {
                sprite = gameObject.AddComponent<SpriteRenderer>();
                sprite.sprite = parameter.sprite;
                sprite.sortingLayerName = "Layer1";
                //设置材质
                sprite.material = /*new Material(*/GameDataManager.instance.materialObject.soldierMaterial/*)*/;
                //if (camp == Camp.demon)
                //    sprite.flipX = false;
                //else
                //    sprite.flipX = true;
            }
            //有spine动画时设置spine动画
            if (spineObject && skAni == null)
            {
                //string m_name = parameter.name.Split("/")[0];
                skAni = gameObject.GetComponent<SkeletonAnimation>();
                //skAni.skeletonDataAsset = Resources.Load<SkeletonDataAsset>("SpineAnimations/" + m_name + "/" + m_name + "_SkeletonData");
                //skAni.GetComponent<MeshRenderer>().sortingLayerName = "Layer1";
                //skAni.GetComponent<MeshRenderer>().sortingOrder = 0;
                //skAni.state = new Spine.AnimationState(new Spine.AnimationStateData(skAni.skeletonDataAsset.GetSkeletonData(true)));
                skAni.state.SetAnimation(0, "stand", true);
                //StartCoroutine(SkAniInstitate());
                //skAni.state = new Spine.AnimationState(new Spine.AnimationStateData(new Spine.SkeletonData()));
            }
            //设置状态管理器
            gameObject.AddComponent<CharacterStateManager>();
            //设置行动ai
            //if (parameter.character.aiType == AiType.warrior)
            //{
            //    gameObject.AddComponent<CharacterAi_Warrior>();
            //}
            //if (parameter.character.aiType == AiType.archer)
            //{
            //    //Debug.Log("Archer");
            //    gameObject.AddComponent<CharacterAi_Archer>();
            //}
            //设置碰撞体(用于处理未旋转碰撞体的数值）
            if (!gameObject.GetComponent<BoxCollider>())
                boxCollider = gameObject.AddComponent<BoxCollider>();
            else
                boxCollider = gameObject.GetComponent<BoxCollider>();
            if (sprite && sprite.material.name != "Sprites-Default")
                startMaterial = sprite.material;
            character_SpecialTagBases = new List<Character_SpecialTagBase>();
            //设置特殊词条
            SetTagEventScript();
            //测试中：设置行为脚本
        
        }
        //当子物体没有名为shadow的对象时，创建shadow影子对象
        if (!transform.Find("shadow"))
        {
            GameObject go = Instantiate(new GameObject(), transform);
            go.name = "shadow";
            go.transform.localPosition = new Vector3(0, parameter.shadowOffset, 0);
            go.transform.localScale = parameter.shadowSize;
            SpriteRenderer shadowSprite = go.AddComponent<SpriteRenderer>();
            shadowSprite.sprite = (Sprite)Resources.Load("Sprites/ModelSprites/Circle", typeof(Sprite)) as Sprite;
            shadowSprite.color = new Color(0.4f, 0.4f, 0.4f);
            shadowSprite.sortingLayerName = "Layer1";
            shadowSprite.sortingOrder = -1;
        }
        //创建动画脚本和获取动画
        animator = gameObject.AddComponent<Animator>();
        animations = GameDataManager.instance.GetAnimatinosDataById(parameter.ID);
        if (animations != null)
        {
            animator.runtimeAnimatorController = animations.animatorController;
        }
        gameObject.AddComponent<ICharaMove_InputKey>();  
        gameObject.AddComponent<ICharaEnemyCheck_Nearest>();
        gameObject.AddComponent<ICharaChase_TryCloser>();
        gameObject.AddComponent<ICharaAttack_Normal>();
        settled = true;
    }

    #endregion

    #region 材质设置相关
    private void OnMouseEnter()
    {
        BattleManager.instance.nowChooseingTarget = gameObject;
        if (BattleManager.instance.nowChoosedTarget != gameObject)
        {

            if (sprite)
            {
                sprite.material = GameDataManager.instance.materialObject.onMouseCoverMaterial;
            }
        }
    }
    private void OnMouseExit()
    {
        BattleManager.instance.nowChooseingTarget = null;
        if (BattleManager.instance.nowChoosedTarget != gameObject)
        {
            SetDefaultMaterial();
        }
    }
    public void SetDefaultMaterial()
    {
        if (sprite && !startMaterial)
            sprite.material = GameDataManager.instance.materialObject.defaultMaterial;
        if (sprite && startMaterial)
            sprite.material = startMaterial;
    }
    public void RefreshHp()
    {
        hpBar.Refresh(parameter.nowHp, parameter.Hp);
    }

    #endregion

    #region 受伤，死亡相关
    /// <summary>
    /// 实体受伤时调用的方法，传入伤害信息，伤害者，输出最终伤害
    /// </summary>
    /// <param name="伤害信息"></param>
    /// <param name="攻击者"></param>
    /// <param name="最终伤害"></param>
    public void TakeDamage(DamageData damageData, Entity attacker, out float finalDamage)
    {
        if (hpLock)
        {
            finalDamage = 0;
            return;
        }

        //根据防御计算伤害
        float realDamage_physic = Mathf.Clamp(damageData.physicDamage - parameter.character.defence, 0, 999999999);
        float realDamage_far = Mathf.Clamp(damageData.farDamage - parameter.character.defence_far, 0, 999999999);
        float realDamage_magic = Mathf.Clamp(damageData.magicDamage - parameter.character.defence_magic, 0, 999999999);
        float realDamage = realDamage_physic + realDamage_far + realDamage_magic;
        parameter.nowHp -= realDamage;



        finalDamage = realDamage;
        //伤害大于零的时候会闪烁一下
        if (realDamage > 0)
        {
            StartCoroutine(HitEffect());
            VfxManager.instance.CreateVfx(VfxManager.instance.vfx_Hit, transform.position + new Vector3(1.5f, 5, 0)/*+ new Vector3(0, boxCollider.size.y, 0)*/, new Vector3(2, 2, 2), 3);
        }
        //如果实体上有计算dps的组件则进行计算
        if (GetComponent<DpsCaculate>())
        {
            GetComponent<DpsCaculate>().AddDpsAmount(realDamage);
        }
        //刷新血条，计算是否死亡
        hpBar.Refresh(parameter.nowHp, parameter.Hp);
        if (parameter.nowHp <= 0)
        {
            //如果存在父兵组，则触发其中的士兵死亡方法
            if (parentSoldierGroup)
            {
                parentSoldierGroup.OnSoldierDie(this);
            }
            Die();
        }
    }
    public void Die()
    {
        //计算血液掉落
        BattleManager.instance.AddBlood(parameter.character.bloodDrop);

        if (BattleManager.instance.nowChoosedTarget == this.gameObject)
            BattleManager.instance.ReleaseChoosedEntity();
        Debug.Log("单位死亡：" + name);
        BattleManager.instance.allSoldiers.Remove(this);

        if (spineObject)
        {
            Destroy(gameObject);
            PlayAniamtion_Die();
            gameObject.AddComponent<LifeTime>().lifeTime = 50;
            Destroy(this.GetComponent<Entity>());
            return;
        }
        if (!spineObject)
            Destroy(gameObject);
    }
    /// <summary>
    /// 实体受伤闪烁红色
    /// </summary>
    /// <returns></returns>
    IEnumerator HitEffect()
    {
        if (!sprite)
            yield break;
        if (sprite.material)
            sprite.material.SetFloat("_HitEffectBlend", 1);
        while (sprite.material.GetFloat("_HitEffectBlend") > 0)
        {
            yield return new WaitForFixedUpdate();
            sprite.material.SetFloat("_HitEffectBlend", sprite.material.GetFloat("_HitEffectBlend") - Time.deltaTime * (parameter.type == EntityType.character ? 1 : 3));
        }
    }

    #endregion

    #region buff相关

    public void AddBuff(BuffInformation buff)
    {
        StartCoroutine(AddBuffIenumerator(buff));
    }
    public void UpdateBuff()
    {
        if (buffs.Count != 0)
        {
            foreach (BuffInformation buff in buffs)
            {
                buff.buff.OnUpdateBuff(this, buff.buffLevel);
            }
        }
    }
    IEnumerator AddBuffIenumerator(BuffInformation buff)
    {
        //遍历已有buff，若有同名buff则不添加新buff
        foreach (BuffInformation b in buffs)
        {
            if (b.buffName == buff.buffName)
            {
                Debug.Log("已有相同buff，不执行该buff添加");
                yield break;
            }
        }
        //添加buff
        buffs.Add(buff);
        Debug.Log("buff开始");
        VfxManager.instance.CreateVfx(VfxManager.instance.vfx_Buff, transform, parameter.type == EntityType.building ? new Vector3(5, 5, 5) : new Vector3(1, 1, 1), buff.buffTime);
        buff.buff.OnAddBuff(this, buff.buffLevel);
        yield return new WaitForSeconds(buff.buffTime);
        Debug.Log("buff结束");
        buff.buff.OnRemoveBuff(this, buff.buffLevel);

        buffs.Remove(buff);
    }

    #endregion

    #region 词条事件

    /// <summary>
    /// 根据数据中的特殊词条，设置词条事件的子类
    /// </summary>
    public void SetTagEventScript()
    {
        //设置特殊事件子类
        foreach (string tag in parameter.character.specialTags)
        {
            if (tag.Contains("荆棘"))
            {
                float level = float.Parse(System.Text.RegularExpressions.Regex.Replace(tag, @"[^0-9]+", ""));
                Debug.Log(level);
                character_SpecialTagBases.Add(new Character_SpecialTag_Thorns(level));
            }
            if (tag.Contains("吸血"))
            {
                float level = float.Parse(System.Text.RegularExpressions.Regex.Replace(tag, @"[^0-9]+", ""));
                Debug.Log(level);
                character_SpecialTagBases.Add(new Character_SpecialTag_BloodAbsorb(level));
            }
            if (tag.Contains("咒死"))
            {
                character_SpecialTagBases.Add(new Character_SpecialTag_Curse());
            }
            if (tag.Contains("格挡"))
            {
                float level = float.Parse(System.Text.RegularExpressions.Regex.Replace(tag, @"[^0-9]+", ""));
                Debug.Log(level);
                character_SpecialTagBases.Add(new Character_SpecialTag_Parry(level));
            }
        }
    }


    /// <summary>
    /// 当承受伤害后触发的词条事件，比如荆棘，传入本实体，被造成的伤害（物理），伤害的输出者，伤害信息
    /// </summary>
    /// <param name="thisEntity"></param>
    /// <param name="damage"></param>
    /// <param name="damageCreater"></param>
    /// <param name="damageData"></param>
    public void TagEvent_AfterHurt(Entity thisEntity, float damage, Entity damageCreater, DamageData damageData)
    {
        foreach (Character_SpecialTagBase tagBase in character_SpecialTagBases)
        {
            tagBase.AfterHurt(thisEntity, damage, damageCreater, damageData);
        }
    }
    /// <summary>
    /// 承受伤害钱触发的词条事件，比如格挡（减伤），传入实体，伤害信息，传出改变后的新的伤害信息
    /// </summary>
    /// <param name="thisEntity"></param>
    /// <param name="damageCreater"></param>
    /// <param name="damageData"></param>
    /// <param name="data"></param>
    public void TagEvent_BeforeHurt(Entity thisEntity, Entity damageCreater, DamageData damageData, DamageData data)
    {
        foreach (Character_SpecialTagBase tagBase in character_SpecialTagBases)
        {
            tagBase.BeforeHurt(thisEntity, damageCreater, damageData, out data);
        }
    }
    /// <summary>
    /// 当伤害输出后触发的词条事件，比如吸血，传入本实体，造成的最终伤害，被攻击的实体和攻击信息
    /// </summary>
    /// <param name="thisEntity"></param>
    /// <param name="damage"></param>
    /// <param name="damageTarget"></param>
    /// <param name="damageData"></param>
    public void TagEvent_AfterAttack(Entity thisEntity, float damage, Entity damageTarget, DamageData damageData)
    {
        foreach (Character_SpecialTagBase tagBase in character_SpecialTagBases)
        {
            tagBase.AfterAttack(thisEntity, damage, damageTarget, damageData);
        }
    }
    /// <summary>
    /// 当伤害输出钱触发的词条事件，比如咒死判定，传入实体，伤害信息，传出改变后的新的伤害信息
    /// </summary>
    /// <param name="thisEntity"></param>
    /// <param name="damageTarget"></param>
    /// <param name="damageData"></param>
    /// <param name="data"></param>
    public void TagEvent_BeforeAttack(Entity thisEntity, Entity damageTarget, DamageData damageData, DamageData data)
    {
        foreach (Character_SpecialTagBase tagBase in character_SpecialTagBases)
        {
            tagBase.BeforeAttack(thisEntity, damageTarget, damageData, out data);
        }
    }
    #endregion

    #region 动画相关
    public void PlayAnimation_Idle()
    {
        if (animations != null && animations.animation_Idle != null)
            animator.Play(animations.animation_Idle.name);
        if (skAni != null)
            skAni.state.SetAnimation(0, "stand", true);
    }
    public void PlayAniamtion_Attack()
    {
        if (animations != null && animations.animation_Attack != null)
            animator.Play(animations.animation_Attack.name);
        if (skAni != null)
            skAni.state.SetAnimation(0, "attack", false);
    }
    public void PlayAniamtion_Walk()
    {
        if (animations != null && animations.animation_Walk != null)
            animator.Play(animations.animation_Walk.name);
        if (skAni != null)
            skAni.state.SetAnimation(0, "move", true);
    }
    public void PlayAniamtion_Die()
    {
        if (animations != null && animations.animation_Die != null)
            animator.Play(animations.animation_Die.name);
        if (skAni != null)
            skAni.state.SetAnimation(0, "die", false);
    }
    IEnumerator SkAniInstitate()
    {
        yield return new WaitForSeconds(0.1f);
        skAni.state.SetAnimation(0, "attack", true);

    }
    #endregion
}
