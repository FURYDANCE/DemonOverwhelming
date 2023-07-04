using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 实体的HPUI脚本
/// </summary>
public class HpShowUi : MonoBehaviour
{
    public GameObject barParent;
    public Image fillBar;
    public Image bufferBar;
    public float bufferTarget;
    public float bufferSpeed;

    float distance;
    float realBufferSpeed;
    CanvasGroup group;
    private void Start()
    {
        bufferSpeed /= 10;
        group = GetComponent<CanvasGroup>();
    }
    void Update()
    {
        distance = (bufferBar.fillAmount - fillBar.fillAmount) * 100;
        realBufferSpeed = Mathf.Clamp(distance * bufferSpeed, 0.1f, 999999999);
        if (bufferBar.fillAmount > bufferTarget)
        {
            bufferBar.fillAmount -= Time.deltaTime * realBufferSpeed;
            if (bufferBar.fillAmount <= bufferTarget)
            {
                bufferBar.fillAmount = bufferTarget;
            }
        }
        else
            bufferBar.fillAmount += Time.deltaTime * realBufferSpeed;

    }
    /// <summary>
    /// 刷新血量显示
    /// </summary>
    /// <param name="now"></param>
    /// <param name="max"></param>
    public void Refresh(float now, float max)
    {
        if (group && group.alpha < 1)
            StartCoroutine(ShowBar());
        fillBar.fillAmount = now / max;
        bufferTarget = now / max;
    }
    IEnumerator ShowBar()
    {
        while (group.alpha < 1)
        {
            group.alpha += Time.deltaTime * 2;
            yield return new WaitForEndOfFrame();
        }
    }
}
