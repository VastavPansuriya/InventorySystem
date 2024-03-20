using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryService
{

    [System.Serializable]
    public class PlayerInventoryServiceData
    {
        public int maxCarriyingWaight;
        public Button addItemButton;
    }

    public int itemCount = -1;
    private ItemSO currSelectedItem;
    private PlayerInventoryServiceData playerInventoryServiceData;
    private TabService tabService;
    private List<SlotController> allPlayerSlots;
    private int currWaight = 0;

    public int MaxCarriyingWaight { get => playerInventoryServiceData.maxCarriyingWaight; private set => playerInventoryServiceData.maxCarriyingWaight = value; }

    public PlayerInventoryService(PlayerInventoryServiceData playerInventoryServiceData)
    {
        this.playerInventoryServiceData = playerInventoryServiceData;
        this.playerInventoryServiceData.addItemButton.onClick.AddListener(AddItem);

        ItemUIButton.OnItemUIButtonPress += ItemUIButton_OnItemUIButtonPress;
        ShopService.OnBuySomething += ShopService_OnBuySomething;
        ShopService.OnSellSomething += ShopService_OnSellSomething;
    }

    private void ShopService_OnSellSomething(ItemSO obj)
    {
        SlotController slotController = allPlayerSlots.Find(item => item.GetItemSO().Equals(obj));
        slotController.SetFilled(obj);
    }

    private void ShopService_OnBuySomething(ItemSO obj)
    {
        SlotController slotController = null;
        foreach (var item in allPlayerSlots)
        {
            if(item.GetItemSO() == obj)
            {
                slotController = item;
                break;
            }
        }

        if (slotController == null)
        {
            return;
        }
        slotController.SetFilled(obj);
    }

    public void InitService(TabService tabService)
    {
        this.tabService = tabService;
    }

    private void ItemUIButton_OnItemUIButtonPress(ItemUIButton arg1, ItemSO currSelectedItem)
    {

        if (!tabService.IsShopPanelSelected())
            this.currSelectedItem = currSelectedItem;
    }

    public void InitSlots(List<SlotController> playerSlots)
    {
        allPlayerSlots = playerSlots;
    }

    public void AddItem()
    {
        if (currWaight > playerInventoryServiceData.maxCarriyingWaight)
        {
            Debug.Log("Out of waight");
            return;
        }

        foreach (SlotController slotController in allPlayerSlots)
        {
            if (slotController.GetItemSO() == currSelectedItem)
            {
                return;
            }
        }
        currWaight += currSelectedItem.weight;
        itemCount++;

        allPlayerSlots[itemCount].SetFilled(currSelectedItem);
    }

    public void RemoveItem(ItemSO itemSO)
    {
        int selectedObjIndex = -1;
        ItemSO curItemSO = null;
        for (int i = 0; i < allPlayerSlots.Count; i++)
        {
            SlotController item = allPlayerSlots[i];

            if (item.IsSlotEmpty())
            {
                return;
            }

            if (item.GetItemSO() == itemSO)
            {
                selectedObjIndex = i;
                item.SetEmpty();
                itemCount--;
            }

            if (i > selectedObjIndex)
            {
                if (i == itemCount)
                {
                    return;
                }

                curItemSO = allPlayerSlots[i].GetItemSO();
                allPlayerSlots[i - 1].SetFilled(curItemSO);
            }
        }
    }

    public void EmptyItemSoSelection()
    {
        currSelectedItem = null;
    }

    ~PlayerInventoryService()
    {
        ItemUIButton.OnItemUIButtonPress -= ItemUIButton_OnItemUIButtonPress;
        ShopService.OnBuySomething -= ShopService_OnBuySomething;
        ShopService.OnSellSomething -= ShopService_OnSellSomething;
    }
}
