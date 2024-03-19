using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TabUIButton : MonoBehaviour
{
    public static event Action<ItemType> OnTabChange;

    [SerializeField] private Button tabButton;
    [SerializeField] private TMP_Text buttonText;
    public ItemType itemType { get; private set; }

    private void Awake()
    {
        tabButton.onClick.AddListener(() => { OnTabChange?.Invoke(itemType); });
    }

    public void Init(ItemType itemType)
    {
        this.itemType = itemType;
        buttonText.text = itemType.ToString();
    }
}