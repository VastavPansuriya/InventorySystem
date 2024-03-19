using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopService
{
    public static event Action<ItemSO> OnBuySomething;
    public static event Action<ItemSO> OnSellSomething;

    private Button sellButton;
    private Button buyButton;

    private ItemSO currSelectedItem;
    private ItemUIButton currItemUIButton;
    private TabService tabService;

    public ShopService(Button sellButton, Button buyButton)
    {
        ItemUIButton.OnItemUIButtonPress += ItemUIButton_OnItemUIButtonPress;
        this.sellButton = sellButton;
        this.buyButton = buyButton;

        this.sellButton.onClick.AddListener(OnSell);
        this.buyButton.onClick.AddListener(OnBuy);
    }

    public void InitService(TabService tabService)
    {
        this.tabService = tabService;
    }

    private void ItemUIButton_OnItemUIButtonPress(ItemUIButton itemUIButton, ItemSO currSelectedItem)
    {
        if (tabService.IsShopPanelSelected())
        {
            this.currSelectedItem = currSelectedItem;
            this.currItemUIButton = itemUIButton;
        }
    }

    private void OnBuy()
    {
        if (currSelectedItem == null)
        {
            Debug.LogError("Something get wrong your currSelected Item is null");
        }

        if (GameService.Instance.CurrencyService.CanPurchaseInAmmountOf(currSelectedItem.buyingPrice))
        {
            currSelectedItem.Quantity++;
            currItemUIButton.GetSlotController().SetFilled(currSelectedItem);
            CurrencyService.OnWantRemoveCoin?.Invoke(currSelectedItem.buyingPrice);
            OnBuySomething?.Invoke(currSelectedItem);
        }
        else
        {
            Debug.Log("Not Enough Amount");
        }
    }

    private void OnSell()
    {
        if (currSelectedItem == null)
        {
            Debug.LogError("Something get wrong your currSelected Item is null");
        }

        if (currSelectedItem.Quantity > 0)
        {
            currSelectedItem.Quantity--;
            currItemUIButton.GetSlotController().SetFilled(currSelectedItem);
            CurrencyService.OnWantAddCoin?.Invoke(currSelectedItem.sellingPrice);
            OnSellSomething?.Invoke(currSelectedItem);
        }
    }

    public void EmptyItemSoSelection()
    {
        currSelectedItem = null;
    }

    ~ShopService()
    {
        ItemUIButton.OnItemUIButtonPress -= ItemUIButton_OnItemUIButtonPress;
    }
}