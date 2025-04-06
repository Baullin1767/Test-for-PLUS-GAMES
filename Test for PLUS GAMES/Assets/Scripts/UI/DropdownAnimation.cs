using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DropdownAnimation : MonoBehaviour
{
    [SerializeField]
    Button buttonToggle;

    public float animationDuration = 0.3f;
    public float hiddenYPosition = 100f;
    public float targetHeight = 1000f;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransformDropdown;
    private GameObject dropdownGameObject;
    private RectTransform rectTransformParent;
    private Vector2 originalPosition;
    private float originalHeight;
    private bool isOpen = false;

    void Start()
    {
        dropdownGameObject = transform.Find("Dropdown").gameObject;
        canvasGroup = dropdownGameObject.GetComponent<CanvasGroup>();
        rectTransformDropdown = dropdownGameObject.GetComponent<RectTransform>();
        rectTransformParent = GetComponent<RectTransform>();
        originalPosition = rectTransformDropdown.anchoredPosition;
        originalHeight = rectTransformParent.sizeDelta.y;
        HideInstant();
        buttonToggle.onClick.AddListener(Toggle);
    }

    public void Toggle()
    {
        if (isOpen)
            Hide();
        else
            Show();
    }

    public void Show()
    {
        isOpen = true;
        rectTransformDropdown.anchoredPosition = originalPosition + new Vector2(0, hiddenYPosition);
        canvasGroup.alpha = 0f;
        dropdownGameObject.SetActive(true);

        rectTransformDropdown.DOAnchorPos(originalPosition, animationDuration).SetEase(Ease.OutCubic);
        canvasGroup.DOFade(1f, animationDuration);
        rectTransformParent.DOSizeDelta(
            new Vector2(rectTransformParent.sizeDelta.x, targetHeight),
            animationDuration * 0.5f
        ).SetEase(Ease.OutCubic);
    }

    public void Hide()
    {
        isOpen = false;
        rectTransformDropdown.DOAnchorPos(originalPosition + new Vector2(0, hiddenYPosition), animationDuration).SetEase(Ease.InCubic);
        canvasGroup.DOFade(0f, animationDuration)
            .OnComplete(() => {
                dropdownGameObject.SetActive(false);
                rectTransformParent.DOSizeDelta(
                            new Vector2(rectTransformParent.sizeDelta.x, originalHeight),
                            animationDuration
                            ).SetEase(Ease.OutCubic);
            }); 

        
    }

    private void HideInstant()
    {
        canvasGroup.alpha = 0f;
        rectTransformDropdown.anchoredPosition = originalPosition + new Vector2(0, hiddenYPosition);
        dropdownGameObject.SetActive(false);
    }
}
