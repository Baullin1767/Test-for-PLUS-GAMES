using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MapDragger : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform mapRect;
    public RectTransform viewRect;

    private Vector2 dragStartPosition;
    private Vector2 mapStartPosition;
    private Tweener currentTween;

    public float springDuration = 0.3f;
    public Ease springEase = Ease.OutBack;

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartPosition = eventData.position;
        mapStartPosition = mapRect.anchoredPosition;

        currentTween?.Kill();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 difference = eventData.position - dragStartPosition;
        Vector2 targetPosition = mapStartPosition + difference;

        mapRect.anchoredPosition = targetPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 clamped = ClampToScreenBounds(mapRect.anchoredPosition);
        if (clamped != mapRect.anchoredPosition)
        {
            currentTween = mapRect.DOAnchorPos(clamped, springDuration)
                .SetEase(springEase);
        }
    }

    private Vector2 ClampToScreenBounds(Vector2 targetPosition)
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        Vector2 mapSize = mapRect.rect.size;

        Vector2 min = screenSize - mapSize;

        float clampedX = Mathf.Clamp(targetPosition.x, min.x / 2f, min.x < 0 ? -min.x / 2f : 0f);
        float clampedY = Mathf.Clamp(targetPosition.y, min.y / 2f, min.y < 0 ? -min.y / 2f : 0f);

        return new Vector2(clampedX, clampedY);
    }

}
