
using System;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "DropdownSettings", menuName = "UI/Dropdown Settings")]
public class DropdownSettings : ScriptableObject
{
    public CoutryData[] coutriesData;
    [Serializable]
    public struct CoutryData
    {
        public LocalizedString localizedString;
        public Sprite flagSprite;
    }
}