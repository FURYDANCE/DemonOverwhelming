using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 拖拽UI
/// </summary>
public class UiDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Vector3 relativeGap;
    //限制范围的UI
    [Header("限制范围的UI")]
    public RectTransform container;

    public Vector3 startPos;
    private Vector3 offset = new Vector3();
    // 最小、最大X、Y坐标
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
        //按下鼠标计算鼠标和自己轴心的偏移量
        offset = Input.mousePosition - transform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition - offset; //拖拽
        if (container)
            transform.position = DragRangeLimit(transform.position);
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

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
