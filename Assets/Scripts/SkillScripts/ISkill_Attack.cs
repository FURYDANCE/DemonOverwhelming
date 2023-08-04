using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISkill_Attack : SkillBase
{
    public void OnEnd()
    {

    }

    public void OnStart(Entity thisEntity)
    {

    }

    public void OnUpdate()
    {

    }

    public void OnUse(Entity thisEntity, Skill thisSkill)
    {

        thisEntity.GetComponent<CharacterStateManager>().ChangeState(new CharacterSkillingState());
        thisSkill.waitTimer = 0;
        thisEntity.StartCoroutine(Skill_Attack(thisEntity));
    }

    public void OnUsing()
    {

    }

    IEnumerator Skill_Attack(Entity thisEntity)
    {
        Debug.Log("ִ���˼���");
        yield return new WaitForSeconds(0.5f);
        Collider[] colliders = Physics.OverlapSphere(thisEntity.transform.position + (thisEntity.transform.rotation.y == 0 ? 3 * Vector3.right : 3 * Vector3.left), 3);
        DamageData damage = new DamageData();
        //�����˺�
        damage.SetDamageData(15, 0, 0);
        //������Ч
        VfxManager.instance.CreateVfx(VfxManager.instance.GetVfxByIdOrName("7").vfx, thisEntity.transform.position + (thisEntity.transform.rotation.y == 0 ? 3 * Vector3.right : 3 * Vector3.left) + 5 * Vector3.up, new Vector3(10, 10, 10), 3);
        foreach (Collider c in colliders)
        {
            //�Լ�ⷶΧ�ڵ�ÿ����������˺���ÿ�λظ��˺���10%��Ѫ��
            Entity e = c.GetComponent<Entity>();
            if (e && e.camp != thisEntity.camp)
            {
                thisEntity.Heal(0.1f * BattleManager.instance.CreateDamage(thisEntity, damage, e));
            }
        }
        yield return new WaitForSeconds(0.5f);
        thisEntity.GetComponent<CharacterStateManager>().ChangeState(new CharacterWalkingState());
    }
}
