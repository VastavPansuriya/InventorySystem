using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoTab : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text info;
    [SerializeField] private TMP_Text sellPrice;
    [SerializeField] private TMP_Text buyPrice;

    private void Awake()
    {
        ItemUIButton.OnItemUIButtonPress += ItemUIButton_OnItemUIButtonPress;
        Hide();
    }

    private void ItemUIButton_OnItemUIButtonPress(ItemUIButton itemUIButton, ItemSO obj)
    {
        Show(obj);
    }

    public void Show(ItemSO itemSO)
    {
        gameObject.SetActive(true);
        iconImage.sprite = itemSO.icon;
        info.text = itemSO.itemDescription;
        sellPrice.text = itemSO.sellingPrice.ToString();
        buyPrice.text = itemSO.buyingPrice.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        ItemUIButton.OnItemUIButtonPress -= ItemUIButton_OnItemUIButtonPress;
    }
}