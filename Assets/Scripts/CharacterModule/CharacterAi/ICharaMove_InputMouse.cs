using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace DemonOverwhelming
{

    /// <summary>
    /// 鼠标左键选定移动，但是还是有点问题（
    /// </summary>
    public class ICharaMove_InputMouse : MonoBehaviour, ICharacterMove
    {
        Entity entity;
        float speed;
        Vector3 moveDir;

        public void Moving()
        {
            if (moveDir != Vector3.zero)
                transform.position = Vector3.MoveTowards(transform.position, moveDir, speed * Time.deltaTime);
            if (transform.position == moveDir)
                moveDir = Vector3.zero;
        }


        void Start()
        {
            entity = GetComponent<Entity>();
            speed = entity.parameter.character.moveSpeed;
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector3 d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 f = new Vector3(d.x, d.y, transform.position.z);
                Debug.Log(d + "qqqqqqqqqq" + f);
                moveDir = (f - transform.position);
            }
        }
    }
}