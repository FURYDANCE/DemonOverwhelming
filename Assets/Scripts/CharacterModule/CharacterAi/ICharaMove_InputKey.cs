using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICharaMove_InputKey : MonoBehaviour, ICharacterMove
{
    Entity entity;
    float speed;
    public float moveDir_X;
    public float moveDir_Y;
    public Vector3 moveDir;
    CharacterStateManager manager;
    public void Moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveDir, speed * Time.deltaTime);

    }


    void Start()
    {
        manager = GetComponent<CharacterStateManager>();
        entity = GetComponent<Entity>();
        speed = entity.parameter.character.moveSpeed;
        manager.GetInterfaceScript();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            moveDir_X = -1;
        else if (Input.GetKey(KeyCode.D))
            moveDir_X = 1;
        else
            moveDir_X = 0;
        if (Input.GetKey(KeyCode.S))
            moveDir_Y = -1;
        else if (Input.GetKey(KeyCode.W))
            moveDir_Y = 1;
        else
            moveDir_Y = 0;
        moveDir = transform.position + new Vector3(moveDir_X, moveDir_Y, 0).normalized;
        //当有输入的时候不能进入追击状态，仅当无输入的时候可以进入
        if (moveDir_X == 0 && moveDir_Y == 0)
            manager.SetAttackTarget(manager.enemyCheckScript.EnemyCheck());
    }
}
