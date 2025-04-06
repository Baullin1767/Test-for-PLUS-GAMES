using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class CountryItem : MonoBehaviour
{
    [HideInInspector]
    public int id;

    [Header("Data")]
    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    Image imageFlag;

    [Header("Actions")]
    [SerializeField]
    GameObject checkMark;
    [SerializeField]
    Button buttonChoise;
    public void init(int id, LocalizedString localizedString, Sprite sprite)
    {
        title.text = localizedString.GetLocalizedString();
        imageFlag.sprite = sprite;
        this.id = id;
        buttonChoise.onClick.AddListener(ChoiseCountry);
    }

    private void ChoiseCountry()
    {
        foreach (var item in CoutriesSpawner.instance.countryItems)
        {
            item.HideCheckMark();
        }
        checkMark.SetActive(true);
        MapManager.instance.OpenMiniMap(id);
        UiManager.instace.ChangeScreen(ScreenType.Flights);
    }

    public void HideCheckMark()
    {
        checkMark.SetActive(false);
    }
}
