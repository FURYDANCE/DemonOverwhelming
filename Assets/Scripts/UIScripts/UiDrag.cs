using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// ��קUI
/// </summary>
public class UiDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Vector3 relativeGap;
    //���Ʒ�Χ��UI
    [Header("���Ʒ�Χ��UI")]
    public RectTransform container;

    public Vector3 startPos;
    private Vector3 offset = new Vector3();
    // ��С�����X��Y����
    public float minX, maxX, minY, maxY;
    RectTransform rectTransform;
    private void Start()
    {
        if (!GetComponent<FormatCard>())
            startPos = transform.position;
        rectTransform = GetComponent<RectTransform>();
        if (container)
            SetDragRange();
    }
    private void Update()
    {
        relativeGap = transform.position - startPos;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //���������������Լ����ĵ�ƫ����
        offset = Input.mousePosition - transform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition - offset; //��ק
        if (container)
            transform.position = DragRangeLimit(transform.position);
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

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
