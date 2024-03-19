using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryService
{

    [System.Serializable]
    public class PlayerInventoryServiceData
    {
        public int maxCarriyingWaight;
        public int itemCount = -1;
        private Button addItemButton;
    }

    private ItemSO currSelectedItem;

    private PlayerInventoryServiceData playerInventoryServiceData;
    private List<SlotController> allPlayerSlots;

    public int MaxCarriyingWaight { get => playerInventoryServiceData.maxCarriyingWaight; private set => playerInventoryServiceData.maxCarriyingWaight = value; }

    public PlayerInventoryService(PlayerInventoryServiceData playerInventoryServiceData)
    {
        this.playerInventoryServiceData = playerInventoryServiceData;
        ItemUIButton.OnItemUIButtonPress += ItemUIButton_OnItemUIButtonPress;

    }

    private void ItemUIButton_OnItemUIButtonPress(ItemUIButton arg1, ItemSO currSelectedItem)
    {
        this.currSelectedItem = currSelectedItem;
    }

    public void InitSlots(List<SlotController> playerSlots)
    {
        allPlayerSlots = playerSlots;
    }

    public void AddItem(ItemSO itemSO)
    {
        if (itemSO.weight > playerInventoryServiceData.maxCarriyingWaight)
        {
            Debug.Log("Player Waight is full you can't add item");
            return;
        }
        allPlayerSlots[playerInventoryServiceData.itemCount].SetFilled(itemSO);
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
                playerInventoryServiceData.itemCount--;
            }

            if (i > selectedObjIndex)
            {
                if (i == playerInventoryServiceData.itemCount)
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
    }
}
