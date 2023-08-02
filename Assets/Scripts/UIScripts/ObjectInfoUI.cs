using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// 当鼠标悬停在某种对象上显示相关信息的脚本
/// </summary>
public class ObjectInfoUI : MonoBehaviour
{
    [Header("内容的父对象")]
    public Transform content;
    [Header("文本内容")]
    public TextMeshProUGUI textObject;
    [Header("限制范围的UI")]
    public RectTransform container;
    [Header("当前显示的文本对象")]
    public List<TextMeshProUGUI> createdTexts;
    [Header("将摧毁的文本对象")]
    public List<TextMeshProUGUI> destoryingTexts;
    /// <summary>
    /// 摧毁计时（直接摧毁会因未知原因导致报错所以先隐藏之后过一段时间统一摧毁
    /// </summary>
    float timer = 2;
    /// <summary>
    /// 文本内容的rectTransform
    /// </summary>
    RectTransform rectTransform;
    /// <summary>
    /// 用于计算ui显示边界的变量
    /// </summary>
    float minX, maxX, minY, maxY;
    private void Start()
    {
        rectTransform = content.GetComponent<RectTransform>();
        if (container)
            SetDragRange();
        //在最开始时先隐藏自身，有需要时再激活
        gameObject.SetActive(false);
    }
    private void LateUpdate()
    {
        //计算内容的具体位置
        content.transform.position = Input.mousePosition + new Vector3(0, rectTransform.sizeDelta.y / 2+10);
        if (container)
            content.transform.position = DragRangeLimit(content.transform.position);
        //计算过时的文本内容的摧毁
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 2;
            foreach (TextMeshProUGUI t in destoryingTexts)
            {
                t.transform.DetachChildren();
                Destroy(t.gameObject);
            }
            destoryingTexts.Clear();
        }
    }
   /// <summary>
   /// 创造新的文本对象
   /// </summary>
   /// <param name="text"></param>
    public void CreateNewText(string text)
    {
        //创造新的textmeshpro对象，并且加进内容中
        GameObject newText = Instantiate(textObject.gameObject, content);
        TextMeshProUGUI t = newText.GetComponent<TextMeshProUGUI>();
        t.text = text;
        createdTexts.Add(t);
    }
    /// <summary>
    /// 设置未激活的文本，在index之后的文本会以灰色显示
    /// </summary>
    /// <param name="index"></param>
    public void SetTextColor(int index)
    {
        
        for (int i = 0; i < createdTexts.Count; i++)
        {
            if (i > index)
            {
                //想要将用标签打上的颜色也改变，但不知为何无法正常运作
                createdTexts[i].text.Replace("<color=red>", "<color=gray>");

                createdTexts[i].color = Color.gray;
            }
        }
    }
    /// <summary>
    /// 摧毁过时的文本对象
    /// </summary>
    public void DestoryAllText()
    {
        foreach (TextMeshProUGUI t in createdTexts)
        {
            destoryingTexts.Add(t);

            t.gameObject.SetActive(false);
        }
        createdTexts.Clear();
    }

    /// <summary>
    /// 设置最大、最小坐标
    /// </summary>
    void SetDragRange()
    {
        // 最小x坐标 = 容器当前x坐标 - 容器轴心距离左边界的距离 + UI轴心距离左边界的距离
        minX = container.position.x
            - container.pivot.x * container.rect.width
            + rectTransform.rect.width * rectTransform.pivot.x;

        // 最大x坐标 = 容器当前x坐标 + 容器轴心距离右边界的距离 - UI轴心距离右边界的距离
        maxX = container.position.x
            + (1 - container.pivot.x) * container.rect.width
            - rectTransform.rect.width * (1 - rectTransform.pivot.x);

        // 最小y坐标 = 容器当前y坐标 - 容器轴心距离底边的距离 + UI轴心距离底边的距离
        minY = container.position.y
            - container.pivot.y * container.rect.height
            + rectTransform.rect.height * rectTransform.pivot.y;

        // 最大y坐标 = 容器当前x坐标 + 容器轴心距离顶边的距离 - UI轴心距离顶边的距离
        maxY = container.position.y
            + (1 - container.pivot.y) * container.rect.height
            - rectTransform.rect.height * (1 - rectTransform.pivot.y);
    }
    /// <summary>
    ///  限制坐标范围
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    Vector3 DragRangeLimit(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        return pos;
    }
}

