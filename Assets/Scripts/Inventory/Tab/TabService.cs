using System;
using System.Collections.Generic;
using UnityEngine;

public class TabService
{
    private InventoryPanelType inventoryPanelType;

    public class TabServiceRuntimeData
    {
        public List<SlotController> allInventorySlots;
        public List<SlotController> allShopSlots;
        public ItemListSO itemListSO;
    }

    [Serializable]
    public class TabServiceEditModeData
    {
        public TabUIButton tabUIButtonPrefab;

        public Transform playerInventoryButtonParent;
        public Transform shopButtonParent;

        public InfoTab shopInfoTab;
        public InfoTab inventoryInfoTab;
    }

    private TabServiceRuntimeData tabServiceRuntimeData;
    private TabServiceEditModeData tabServiceEditModeData;
    private ShopService shopService;
    private PlayerInventoryService playerInventoryService;

    public TabService(TabServiceEditModeData tabServiceEditModeData, ShopService shopService, PlayerInventoryService playerInventoryService)
    {
        this.tabServiceEditModeData = tabServiceEditModeData;
        this.playerInventoryService = playerInventoryService;
        this.shopService = shopService;

        TabUIButton.OnTabChange += TabUIButton_OnTabChange;
        InputServiceManager.OnInventoryPanelOpen += InputServiceManager_OnInventoryPanelOpen;
        InputServiceManager.OnShopPanelOpen += InputServiceManager_OnShopPanelOpen;
    }

    private void InputServiceManager_OnShopPanelOpen()
    {
        HideInfoTab(inventoryPanelType);
    }

    private void InputServiceManager_OnInventoryPanelOpen()
    {
        HideInfoTab(inventoryPanelType);
    }

    private void TabUIButton_OnTabChange(ItemType itemType)
    {
        HideInfoTab(inventoryPanelType);
        ReInitSlots(itemType);
    }

    public void Init(TabServiceRuntimeData tabServiceData)
    {
        this.tabServiceRuntimeData = tabServiceData;
        ReInitSlots(ItemType.Materials);
        CreateButtons();
    }

    private void ReInitSlots(ItemType itemType)
    {
        HideItems();
        ShowItems(itemType);
    }

    private void CreateButtons()
    {
        foreach (ItemType item in Enum.GetValues(typeof(ItemType)))
        {
            TabUIButton tabUIButton = MonoBehaviour.Instantiate(tabServiceEditModeData.tabUIButtonPrefab, tabServiceEditModeData.playerInventoryButtonParent);
            tabUIButton.Init(item);

            tabUIButton = MonoBehaviour.Instantiate(tabServiceEditModeData.tabUIButtonPrefab, tabServiceEditModeData.shopButtonParent);
            tabUIButton.Init(item);
        }
    }

    private void HideItems()
    {
        shopService.EmptyItemSoSelection();
        playerInventoryService.EmptyItemSoSelection();

        foreach (SlotController item in GetUsingSlots())
        {
            item.SetEmpty();
        }
    }

    private void ShowItems(ItemType itemType)
    {
        List<ItemSO> allItemSO = new List<ItemSO>(tabServiceRuntimeData.itemListSO.allItemdata);

        List<ItemSO> allNeededItem = new List<ItemSO>(allItemSO.FindAll(item => item.itemType == itemType));

        foreach (SlotController slot in GetUsingSlots())
        {
            ItemSO itemSO = allNeededItem[0];
            if (!IsShopPanelSelected() && itemSO.Quantity ==  0)
            {
                continue;  
            }
            if (slot.IsSlotEmpty())
            {
                slot.SetFilled(itemSO);

                allNeededItem.Remove(itemSO);
            }

            if (allNeededItem.Count == 0) break;
        }
    }

    private List<SlotController> GetUsingSlots()
    {
        List<SlotController> currUsingSlots = null;
        switch (inventoryPanelType)
        {
            case InventoryPanelType.Inventory:
                currUsingSlots = tabServiceRuntimeData.allInventorySlots;
                break;
            case InventoryPanelType.Shop:
                currUsingSlots = tabServiceRuntimeData.allShopSlots;
                break;
        }

        return currUsingSlots;
    }
    public void SetShopPanel()
    {
        inventoryPanelType = InventoryPanelType.Shop;
        ReInitSlots(ItemType.Materials);
    }

    public void SetInventoryPanel()
    {
        inventoryPanelType = InventoryPanelType.Inventory;
        ReInitSlots(ItemType.Materials);
    }
    public void HideInfoTab(InventoryPanelType inventoryPanelType)
    {
        switch (inventoryPanelType)
        {
            case InventoryPanelType.Inventory:
                tabServiceEditModeData.inventoryInfoTab.gameObject.SetActive(false);
                tabServiceEditModeData.shopInfoTab.gameObject.SetActive(true);
                break;
            case InventoryPanelType.Shop:
                tabServiceEditModeData.shopInfoTab.gameObject.SetActive(false);
                tabServiceEditModeData.inventoryInfoTab.gameObject.SetActive(true);
                break;
        }
    }

    public bool IsShopPanelSelected()
    {
        return inventoryPanelType == InventoryPanelType.Shop;
    }

    ~TabService()
    {
        TabUIButton.OnTabChange -= TabUIButton_OnTabChange;
        InputServiceManager.OnInventoryPanelOpen -= InputServiceManager_OnInventoryPanelOpen;
        InputServiceManager.OnShopPanelOpen -= InputServiceManager_OnShopPanelOpen;
    }

    public enum InventoryPanelType
    {
        Inventory,
        Shop
    }
}