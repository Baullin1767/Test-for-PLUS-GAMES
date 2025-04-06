using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CoutriesSpawner : MonoBehaviour
{
    public static CoutriesSpawner instance;

    public List<CountryItem> countryItems;

    [SerializeField]
    CountryItem countryItemPrefab;
    [SerializeField]
    RectTransform contentRT;
    [SerializeField]
    DropdownSettings dropdownSettings;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < dropdownSettings.coutriesData.Length; i++)
        {
            CountryItem item = Instantiate(countryItemPrefab, contentRT);

            item.init(i,
                dropdownSettings.coutriesData[i].localizedString,
                dropdownSettings.coutriesData[i].flagSprite);

            countryItems.Add(item);
        }
    }
}
