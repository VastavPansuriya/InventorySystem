using TMPro;
using UnityEngine;

public class UIService
{
    public GameObject inventoryPanel;
    public GameObject shopPanel;
    public TMP_Text coinText;

    private TabService tabService;

    public UIService(GameObject inventoryPanel, GameObject shopPanel, TMP_Text coinText, TabService tabService)
    {
        this.inventoryPanel = inventoryPanel;
        this.shopPanel = shopPanel;
        this.tabService = tabService;
        this.coinText = coinText;

        CurrencyService.OnWantAddCoin += SetCoinText;
        CurrencyService.OnWantRemoveCoin += SetCoinText;
    }

    private void SetCoinText(int amount)
    {
        coinText.text = GlobleData.TotalCurrency.ToString();
    }

    public void ShowInventory()
    {
        if (inventoryPanel.activeSelf) return;
        inventoryPanel.SetActive(true);
        shopPanel.SetActive(false);
        tabService.SetInventoryPanel();
    }

    public void ShowShop()
    {
        if (shopPanel.activeSelf) return;
        shopPanel.SetActive(true);
        inventoryPanel.SetActive(false);
        tabService.SetShopPanel();
    }

    ~UIService()
    {
        CurrencyService.OnWantAddCoin -= SetCoinText;
        CurrencyService.OnWantRemoveCoin -= SetCoinText;
    }
}