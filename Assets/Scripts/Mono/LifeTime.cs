using UnityEngine;
namespace DemonOverwhelming
{

    /// <summary>
    /// �������ڽű�����ʱ����ʱ�ݻ�����
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