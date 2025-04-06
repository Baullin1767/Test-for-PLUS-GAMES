using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instace;

    [Header("Screens")]
    public float animationDuration = 0.3f;
    [SerializeField]
    CanvasGroup loadingScreen;
    [SerializeField]
    CanvasGroup mainScreen;
    [SerializeField]
    CanvasGroup flightsScreen;


    [Header("Elements")]

    [SerializeField]
    Button backButton;
    [SerializeField]
    Button openMapButton;
    [SerializeField]
    MapDragger mapDragger;
    [SerializeField]
    GameObject MiniMap;

    private void Awake()
    {
        instace = this;
        loadingScreen.alpha = 1;
        mainScreen.alpha = 0;
        mainScreen.interactable = false;
        flightsScreen.alpha = 0;
        flightsScreen.interactable = false;
        Screen.orientation = ScreenOrientation.Portrait;
        StartCoroutine(StartGame());
    }

    private void Start()
    {
        backButton.onClick.AddListener(() => ChangeScreen(ScreenType.Main));
        openMapButton.onClick.AddListener(OpenBigMap);
        mapDragger.enabled = false;
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        ChangeScreen(ScreenType.Main);
    }

    public void ChangeScreen(ScreenType screenType)
    {
        switch (screenType)
        {
            case ScreenType.Main:
                ChangeScreen(() => {
                    mainScreen.DOFade(1f, animationDuration);
                    mainScreen.interactable = true;
                    mainScreen.blocksRaycasts = true;
                });
                break;
            case ScreenType.Flights:
                ChangeScreen(() => {
                    flightsScreen.DOFade(1f, animationDuration);
                    flightsScreen.interactable = true;
                    flightsScreen.blocksRaycasts = true;
                });
                break;
            default:
                break;
        }
    }

    private void ChangeScreen(TweenCallback action)
    {
        if (loadingScreen.alpha == 1)
            loadingScreen.DOFade(0f, animationDuration)
                .OnComplete(() => {
                    action();
                    loadingScreen.gameObject.SetActive(false);
                });
        if(mainScreen.alpha == 1)
            mainScreen.DOFade(0f, animationDuration)
                .OnComplete(() => {
                    action();
                    mainScreen.interactable = false;
                    mainScreen.blocksRaycasts = false;
                });
        if (flightsScreen.alpha == 1)
            flightsScreen.DOFade(0f, animationDuration)
                .OnComplete(() => {
                    action();
                    flightsScreen.interactable = false;
                    flightsScreen.blocksRaycasts = false;
                });
    }

    private void OpenBigMap()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        MapManager.instance.OpenMap();
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(CloseBigMap);

        mapDragger.enabled = true;
        MiniMap.SetActive(false);
    }
    private void CloseBigMap()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        MapManager.instance.СloseMap();
        backButton.onClick.AddListener(() => ChangeScreen(ScreenType.Main));

        mapDragger.enabled = false;
        MiniMap.SetActive(true);
    }
}


public enum ScreenType
{
    Loading,
    Main,
    Flights
}