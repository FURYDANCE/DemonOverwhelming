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
    float timer;
    void Start()
    {
        entity = GetComponent<Entity>();
        speed = entity.parameter.character.moveSpeed;
        manager = GetComponent<CharacterStateManager>();
        timer = 0.1f;
    }


    public void Moving()
    {


        //��ֱ���ƶ���ͬʱ�������Χ�ĵ��ˣ��Ƿ����׷�����˵�״̬
        //������ִ�е�ԭ�򣺵���Ҳٿ�����ʱ������Χ���е���Ҳ��Ӧ�ý���׷��״̬�������ڲ������κ��ж���ť��ʱ���ҷ�Χ���е��˲Ž���׷��״̬������Ӧ���ھ���Ľű���ִ��
        manager.CheckEnemy();
        manager.SetAttackTarget(manager.enemyCheckScript.EnemyCheck());

        if (entity.camp == Camp.demon)
            moveDir = Vector3.right.normalized;
        else
            moveDir = Vector3.left.normalized;
        if (timer < 0)
        {
            //Ϊ�˵�����ʱ����λ����Ч������Ҫֱ�ӵ���ʵ����ٶ�ֵ
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDir, entity.parameter.character.moveSpeed * Time.deltaTime);
            //Debug.Log("����");
        }
        timer -= Time.deltaTime;
    }



}
