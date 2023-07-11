using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ֱ���ƶ��ű�������ӪΪ��ħʱ����ֱ���ƶ�����֮����
/// </summary>
public class ICharaMove_Direct : MonoBehaviour, ICharacterMove
{
    Entity entity;
    float speed;
    Vector3 moveDir;
    CharacterStateManager manager;

    void Start()
    {
        entity = GetComponent<Entity>();
        speed = entity.parameter.character.moveSpeed;
        manager = GetComponent<CharacterStateManager>();
    }


    public void Moving()
    {
        if (entity.camp == Camp.demon)
            moveDir = Vector3.right.normalized;
        else
            moveDir = Vector3.left.normalized;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDir, speed * Time.deltaTime);

        //��ֱ���ƶ���ͬʱ�������Χ�ĵ��ˣ��Ƿ����׷�����˵�״̬
        //������ִ�е�ԭ�򣺵���Ҳٿ�����ʱ������Χ���е���Ҳ��Ӧ�ý���׷��״̬�������ڲ������κ��ж���ť��ʱ���ҷ�Χ���е��˲Ž���׷��״̬������Ӧ���ھ���Ľű���ִ��
        manager.CheckEnemy();
        manager.SetAttackTarget(manager.enemyCheckScript.EnemyCheck());

    }



}
