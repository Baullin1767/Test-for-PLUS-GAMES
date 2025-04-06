using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    [SerializeField]
    GameObject[] PointsOnMap;
    [SerializeField]
    Vector2[] MapPositions;


    [SerializeField] 
    float animationDuration = 0.3f;

    [SerializeField]
    RectTransform mapView;
    [SerializeField]
    RectTransform mapViewMask;
    Vector2 mapViewMaskOriginalSize;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
        mapViewMaskOriginalSize = mapViewMask.sizeDelta;
    }

    public void OpenMiniMap(int inCurrentCounry)
    {
        foreach (var item in PointsOnMap)
        {
            item.SetActive(false);
        }
        PointsOnMap[inCurrentCounry].SetActive(true);
        gameObject.SetActive(true);

        mapView.DOAnchorPos(MapPositions[inCurrentCounry], animationDuration).SetEase(Ease.OutCubic);
    }
    public void OpenMap()
    {
        Vector2 screenSize = new Vector2(Screen.height, Screen.width);

        mapViewMask
            .DOSizeDelta(screenSize, animationDuration)
            .SetEase(Ease.OutCubic);
    }

    public void СloseMap()
    {
        mapViewMask
            .DOSizeDelta(mapViewMaskOriginalSize, animationDuration)
            .SetEase(Ease.OutCubic);
    }
}
