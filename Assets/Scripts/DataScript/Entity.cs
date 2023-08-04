using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Animations;
/// <summary>
/// ʵ���࣬�����ڳ��������е�ʵ���ϣ����������֣�Ӣ�ۣ�
/// �����ͣ������ʱͨ���ı�material���л���ʾ
/// </summary>
public class Entity : MonoBehaviour
{

    /// <summary>
    /// ��Ӫ
    /// </summary>
    public Camp camp;
    public bool spineObject;
    /// <summary>
    /// buff
    /// </summary>
    public List<BuffInformation> buffs;
    public List<Character_SpecialTagBase> character_SpecialTagBases;
    /// <summary>
    /// ʵ��ļ���s
    /// </summary>
    public List<Skill> skills;
    public UnitParameter parameter;
    public HpShowUi hpBar;
    SpriteRenderer sprite;
    bool settled;
    Material startMaterial;
    /// <summary>
    /// ���ڵı���
    /// </summary>
    public SoliderGroup parentSoldierGroup;
    [HideInInspector]
    public BoxCollider boxCollider;
    [Header("(����)�޵�״̬")]
    public bool hpLock;
    public Animator animator;
    public AnimationsList animations;
    public SkeletonAnimation skAni;
    [SerializeField]
    int[] skillLevels;
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
        SkillWaitTimeCaculate();
    }

    #region ��ʼ�����

    public void SetEntityParameter()
    {
        parameter.SetValue(GameDataManager.instance.GetEntityDataById(parameter.ID));
    }
    public void SetParentSoldierGroup(SoliderGroup soliderGroup)
    {
        parentSoldierGroup = soliderGroup;
    }
    /// <summary>
    /// ����id��ȡ���ı������ɸ�ʵ��
    /// </summary>
    public void GenerateEntity()
    {
        if (settled)
            return;
        sprite = GetComponent<SpriteRenderer>();
        //�����Ƿ���spine����
        if (parameter.name.Split("/").Length > 1)
        {
            spineObject = parameter.name.Split("/")[1].Contains("spine") ? true : false;
        }
        if (parameter.hpBarSize == 0) parameter.hpBarSize = 1;
        //����Ѫ��
        if (!hpBar)
        {
            hpBar = Instantiate(SceneObjectsManager.instance.hpBarObject, transform).GetComponent<HpShowUi>();
            //����Ѫ���ĳߴ��ƫ��
            hpBar.barParent.transform.position += parameter.hpBarOffset;
            hpBar.transform.localScale = new Vector3(parameter.hpBarSize, parameter.hpBarSize);
            //ˢ��Ѫ����Ѫ����ʾ
            parameter.nowHp = parameter.Hp;
            hpBar.Refresh(parameter.nowHp, parameter.Hp);
        }
        //��id��Ϊ��ʱ����id����Ŀ��
        if (parameter.ID != "")
        {
            //���óߴ�
            transform.localScale = new Vector3(parameter.modleSize, parameter.modleSize);
            //�����������
            gameObject.AddComponent<FaceToCamera>();
            //������ͼ����spine����
            //û��spine����ʱ������ͼ
            if (!spineObject && sprite == null)
            {
                sprite = gameObject.AddComponent<SpriteRenderer>();
                sprite.sprite = parameter.sprite;
                sprite.sortingLayerName = "Layer1";
                //���ò���
                sprite.material = /*new Material(*/GameDataManager.instance.materialObject.soldierMaterial/*)*/;
                //if (camp == Camp.demon)
                //    sprite.flipX = false;
                //else
                //    sprite.flipX = true;
            }
            //��spine����ʱ����spine����
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
            //����״̬������
            gameObject.AddComponent<CharacterStateManager>();


            //������ײ��(���ڴ���δ��ת��ײ�����ֵ��
            if (!gameObject.GetComponent<BoxCollider>())
                boxCollider = gameObject.AddComponent<BoxCollider>();
            else
                boxCollider = gameObject.GetComponent<BoxCollider>();
            if (sprite && sprite.material.name != "Sprites-Default")
                startMaterial = sprite.material;
            character_SpecialTagBases = new List<Character_SpecialTagBase>();
            //�����������
            SetTagEventScript();
            //�����У�������Ϊ�ű�

        }
        //��������û����Ϊshadow�Ķ���ʱ������shadowӰ�Ӷ���
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
        //���������ű��ͻ�ȡ����
        animator = gameObject.AddComponent<Animator>();
        animations = GameDataManager.instance.GetAnimatinosDataById(parameter.ID);
        if (animations != null)
        {
            animator.runtimeAnimatorController = animations.animatorController;
        }
        //���ü��ܺͼ��ܵȼ�
        parameter.character.skills = new List<Skill>();
        for (int i = 0; i < parameter.character.skillIds.Length; i++)
        {
            if (GameDataManager.instance.GetSkillById(parameter.character.skillIds[i]) != null)
            {
                parameter.character.skills.Add(GameDataManager.instance.GetSkillById(parameter.character.skillIds[i]));
            }
        }
        skills = parameter.character.skills;
        //���ü��ܵȼ�
        skillLevels = new int[skills.Count];
        for (int i = 0; i < skills.Count; i++)
        {
            skillLevels[i] = int.Parse(parameter.character.skillLevels[i]);
        }
        for (int i = 0; i < skills.Count; i++)
        {
            skills[i].skillLevel = skillLevels[i];
        }
        //�����ж�ai
        if (parameter.aiMode_Move == Ai_MoveType.directMove)
            gameObject.AddComponent<ICharaMove_Direct>();
        if (parameter.aiMode_Move == Ai_MoveType.playerInputMove)
        {
            gameObject.AddComponent<ICharaMove_InputKey>();
            //��Ҳٿصĵ�λ�����Ǯ���ļ��
            gameObject.AddComponent<MoneyBagCheck>();
            //��Ҳٿصĵ�λҲ����UI����ʾ������ʽ����ս��������������Ӣ��
            BattleManager.instance.SetHero(this);
        }
        gameObject.AddComponent<ICharaEnemyCheck_Nearest>();
        gameObject.AddComponent<ICharaChase_TryCloser>();
        gameObject.AddComponent<ICharaAttack_Normal>();
        //���þ���ļ���ִ�нű�����ʼ�¼�
        Skill_StartEvent();
        settled = true;
    }

    #endregion

    #region �����������
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

    #region ���ˣ��������
    /// <summary>
    /// ʵ������ʱ���õķ����������˺���Ϣ���˺��ߣ���������˺�
    /// </summary>
    /// <param name="�˺���Ϣ"></param>
    /// <param name="������"></param>
    /// <param name="�����˺�"></param>
    public void TakeDamage(DamageData damageData, Entity attacker, out float finalDamage)
    {
        if (hpLock)
        {
            finalDamage = 0;
            return;
        }

        //���ݷ��������˺�
        float realDamage_physic = Mathf.Clamp(damageData.physicDamage - parameter.character.defence, 0, 999999999);
        float realDamage_far = Mathf.Clamp(damageData.farDamage - parameter.character.defence_far, 0, 999999999);
        float realDamage_magic = Mathf.Clamp(damageData.magicDamage - parameter.character.defence_magic, 0, 999999999);
        float realDamage = realDamage_physic + realDamage_far + realDamage_magic;
        parameter.nowHp -= realDamage;



        finalDamage = realDamage;
        //�˺��������ʱ�����˸һ��
        if (realDamage > 0)
        {
            StartCoroutine(HitEffect());
            VfxManager.instance.CreateVfx(VfxManager.instance.vfx_Hit, transform.position + new Vector3(1.5f, 5, 0)/*+ new Vector3(0, boxCollider.size.y, 0)*/, new Vector3(2, 2, 2), 3);
        }
        //���ʵ�����м���dps���������м���
        if (GetComponent<DpsCaculate>())
        {
            GetComponent<DpsCaculate>().AddDpsAmount(realDamage);
        }
        //ˢ��Ѫ���������Ƿ�����
        hpBar.Refresh(parameter.nowHp, parameter.Hp);
        if (parameter.nowHp <= 0)
        {
            //������ڸ����飬�򴥷����е�ʿ����������
            if (parentSoldierGroup)
            {
                parentSoldierGroup.OnSoldierDie(this);
            }
            Die();
        }
    }
    public void Die()
    {
        //����ѪҺ����
        BattleManager.instance.AddBlood(parameter.character.bloodDrop);
        if (parameter.character.bloodDrop != 0)
        {
            VfxManager.instance.CreateVfx(VfxManager.instance.vfx_Hit, transform.position, new Vector3(4, 4, 4), 2);
            BattleManager.instance.CreateSceneInformation(gameObject, GameDataManager.instance.bloodSprite, "" + parameter.character.bloodDrop, false, Color.red);
        }
        if (BattleManager.instance.nowChoosedTarget == this.gameObject)
            BattleManager.instance.ReleaseChoosedEntity();
        Debug.Log("��λ������" + name);
        BattleManager.instance.allSoldiers.Remove(this);
        //���������Ǯ������
        foreach (string t in parameter.character.specialTags)
        {
            if (t.Contains("Ǯ��"))
            {
                float level = float.Parse(System.Text.RegularExpressions.Regex.Replace(t, @"[^0-9]+", ""));
                Debug.Log(level);
                BattleManager.instance.CreateMoneyBag(transform.position, level);
            }
        }
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
    /// ʵ��������˸��ɫ
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

    #region buff���

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
        //��������buff������ͬ��buff�������buff
        foreach (BuffInformation b in buffs)
        {
            if (b.buffName == buff.buffName)
            {
                Debug.Log("������ͬbuff����ִ�и�buff���");
                yield break;
            }
        }
        //���buff
        buffs.Add(buff);
        Debug.Log("buff��ʼ");
        VfxManager.instance.CreateVfx(VfxManager.instance.vfx_Buff, transform, parameter.type == EntityType.building ? new Vector3(5, 5, 5) : new Vector3(1, 1, 1), buff.buffTime);
        buff.buff.OnAddBuff(this, buff.buffLevel);
        yield return new WaitForSeconds(buff.buffTime);
        Debug.Log("buff����");
        buff.buff.OnRemoveBuff(this, buff.buffLevel);

        buffs.Remove(buff);
    }

    #endregion

    #region �����¼�

    /// <summary>
    /// ���������е�������������ô����¼�������
    /// </summary>
    public void SetTagEventScript()
    {
        //���������¼�����
        foreach (string tag in parameter.character.specialTags)
        {
            if (tag.Contains("����"))
            {
                float level = float.Parse(System.Text.RegularExpressions.Regex.Replace(tag, @"[^0-9]+", ""));
                Debug.Log(level);
                character_SpecialTagBases.Add(new Character_SpecialTag_Thorns(level));
            }
            if (tag.Contains("��Ѫ"))
            {
                float level = float.Parse(System.Text.RegularExpressions.Regex.Replace(tag, @"[^0-9]+", ""));
                Debug.Log(level);
                character_SpecialTagBases.Add(new Character_SpecialTag_BloodAbsorb(level));
            }
            if (tag.Contains("����"))
            {
                character_SpecialTagBases.Add(new Character_SpecialTag_Curse());
            }
            if (tag.Contains("��"))
            {
                float level = float.Parse(System.Text.RegularExpressions.Regex.Replace(tag, @"[^0-9]+", ""));
                Debug.Log(level);
                character_SpecialTagBases.Add(new Character_SpecialTag_Parry(level));
            }
        }
    }


    /// <summary>
    /// �������˺��󴥷��Ĵ����¼������羣�������뱾ʵ�壬����ɵ��˺����������˺�������ߣ��˺���Ϣ
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
    /// �����˺�Ǯ�����Ĵ����¼�������񵲣����ˣ�������ʵ�壬�˺���Ϣ�������ı����µ��˺���Ϣ
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
    /// ���˺�����󴥷��Ĵ����¼���������Ѫ�����뱾ʵ�壬��ɵ������˺�����������ʵ��͹�����Ϣ
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
    /// ���˺����Ǯ�����Ĵ����¼������������ж�������ʵ�壬�˺���Ϣ�������ı����µ��˺���Ϣ
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

    #region �������
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

    #region �������

    /// <summary>
    /// ���㼼�ܵ���ȴʱ��
    /// </summary>
    public void SkillWaitTimeCaculate()
    {
        foreach (Skill skill in parameter.character.skills)
        {
            if (skill.waitTimer < skill.waitTime)
                skill.waitTimer += Time.deltaTime;
        }
    }
    /// <summary>
    /// �������еļ��ܣ�������ִ�нű�����ʼ�¼�
    /// </summary>
    public void Skill_StartEvent()
    {
        foreach (Skill s in parameter.character.skills)
        {
            if (s.skillName == "��ڰ��")
            {
                s.skillBase = new ISkill_Defence();
            }
            if (s.skillName == "���֮��")
            {
                s.skillBase = new ISkill_Attack();
            }
            if (s.skillBase != null)
                s.skillBase.OnStart(this);
        }
    }
    public void UseSkill(int index)
    {
        Debug.Log("ʹ�ü���");
        if (index >= parameter.character.skills.Count) 
        {
            Debug.Log(index);

            return;

        }
        Debug.Log("ʹ�ü���1");

        Skill skill = parameter.character.skills[index];
        if (skill.waitTimer < skill.waitTime)
            return;
        Debug.Log("ʹ�ü���2");

        skill.skillBase.OnUse(this, skill);
    }
    #endregion

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="healAmount"></param>
    public void Heal(float healAmount)
    {
        parameter.nowHp += healAmount;
        if (parameter.nowHp > parameter.Hp)
            parameter.nowHp = parameter.Hp;
    }
}
