using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// �������ͣ��ĳ�ֶ�������ʾ�����Ϣ�Ľű�
/// </summary>
public class ObjectInfoUI : MonoBehaviour
{
    [Header("���ݵĸ�����")]
    public Transform content;
    [Header("�ı�����")]
    public TextMeshProUGUI textObject;
    [Header("���Ʒ�Χ��UI")]
    public RectTransform container;
    [Header("��ǰ��ʾ���ı�����")]
    public List<TextMeshProUGUI> createdTexts;
    [Header("���ݻٵ��ı�����")]
    public List<TextMeshProUGUI> destoryingTexts;
    /// <summary>
    /// �ݻټ�ʱ��ֱ�Ӵݻٻ���δ֪ԭ���±�������������֮���һ��ʱ��ͳһ�ݻ�
    /// </summary>
    float timer = 2;
    /// <summary>
    /// �ı����ݵ�rectTransform
    /// </summary>
    RectTransform rectTransform;
    /// <summary>
    /// ���ڼ���ui��ʾ�߽�ı���
    /// </summary>
    float minX, maxX, minY, maxY;
    private void Start()
    {
        rectTransform = content.GetComponent<RectTransform>();
        if (container)
            SetDragRange();
        //���ʼʱ��������������Ҫʱ�ټ���
        gameObject.SetActive(false);
    }
    private void LateUpdate()
    {
        //�������ݵľ���λ��
        content.transform.position = Input.mousePosition + new Vector3(0, rectTransform.sizeDelta.y / 2+10);
        if (container)
            content.transform.position = DragRangeLimit(content.transform.position);
        //�����ʱ���ı����ݵĴݻ�
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
   /// �����µ��ı�����
   /// </summary>
   /// <param name="text"></param>
    public void CreateNewText(string text)
    {
        //�����µ�textmeshpro���󣬲��Ҽӽ�������
        GameObject newText = Instantiate(textObject.gameObject, content);
        TextMeshProUGUI t = newText.GetComponent<TextMeshProUGUI>();
        t.text = text;
        createdTexts.Add(t);
    }
    /// <summary>
    /// ����δ������ı�����index֮����ı����Ի�ɫ��ʾ
    /// </summary>
    /// <param name="index"></param>
    public void SetTextColor(int index)
    {
        
        for (int i = 0; i < createdTexts.Count; i++)
        {
            if (i > index)
            {
                //��Ҫ���ñ�ǩ���ϵ���ɫҲ�ı䣬����֪Ϊ���޷���������
                createdTexts[i].text.Replace("<color=red>", "<color=gray>");

                createdTexts[i].color = Color.gray;
            }
        }
    }
    /// <summary>
    /// �ݻٹ�ʱ���ı�����
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
    /// ���������С����
    /// </summary>
    void SetDragRange()
    {
        // ��Сx���� = ������ǰx���� - �������ľ�����߽�ľ��� + UI���ľ�����߽�ľ���
        minX = container.position.x
            - container.pivot.x * container.rect.width
            + rectTransform.rect.width * rectTransform.pivot.x;

        // ���x���� = ������ǰx���� + �������ľ����ұ߽�ľ��� - UI���ľ����ұ߽�ľ���
        maxX = container.position.x
            + (1 - container.pivot.x) * container.rect.width
            - rectTransform.rect.width * (1 - rectTransform.pivot.x);

        // ��Сy���� = ������ǰy���� - �������ľ���ױߵľ��� + UI���ľ���ױߵľ���
        minY = container.position.y
            - container.pivot.y * container.rect.height
            + rectTransform.rect.height * rectTransform.pivot.y;

        // ���y���� = ������ǰx���� + �������ľ��붥�ߵľ��� - UI���ľ��붥�ߵľ���
        maxY = container.position.y
            + (1 - container.pivot.y) * container.rect.height
            - rectTransform.rect.height * (1 - rectTransform.pivot.y);
    }
    /// <summary>
    ///  �������귶Χ
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

