using UnityEngine;
namespace DemonOverwhelming
{

    /// <summary>
    /// 生命周期脚本，计时结束时摧毁自身
    /// </summary>
    public class LifeTime : MonoBehaviour
    {
        public float lifeTime;

        void Update()
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}