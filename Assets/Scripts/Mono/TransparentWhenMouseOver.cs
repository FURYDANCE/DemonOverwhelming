using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    /// <summary>
    /// 鼠标移上去时变得透明的脚本
    /// </summary>
    public class TransparentWhenMouseOver : MonoBehaviour
    {
        SpriteRenderer sprite;
        public bool overing;
        // Start is called before the first frame update
        void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            if (!GetComponent<BoxCollider>())
                gameObject.AddComponent<BoxCollider>();
        }
        private void Update()
        {
            if (overing)
            {
                if (sprite.color.a > 0.5f)
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - Time.deltaTime);
            }
            else
            {
                if (sprite.color.a < 1f)
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + Time.deltaTime);
            }
        }
        private void OnMouseOver()
        {
            overing = true;
        }

        private void OnMouseExit()
        {
            overing = false;
        }
    }
}