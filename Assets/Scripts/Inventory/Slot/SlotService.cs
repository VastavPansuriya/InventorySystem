using System.Collections.Generic;
using UnityEngine;

public class SlotService
{
    [System.Serializable]
    public class SlotServiceData
    {
        [HideInInspector] public ItemListSO itemListSO;
        public SlotView slotPrefab;
        public Transform slotContainerShop;
        public Transform slotContainerInventory;
        public Transform playerContainerInventory;
        public int slotCount;
        public int playersSlotCount;
    }

    private List<SlotController> allInventorySlots = new List<SlotController>();
    private List<SlotController> allShopSlots = new List<SlotController>();
    private List<SlotController> allPlayerSlots = new List<SlotController>();


    private SlotServiceData slotServiceData;

    private TabService tabService;// not needed right know but it can be helpful
    private PlayerInventoryService playerInventoryService;// not needed right know but it can be helpful

    public SlotService(SlotServiceData slotServiceData, TabService tabService, PlayerInventoryService playerInventoryService)
    {
        this.slotServiceData = slotServiceData;
        this.tabService = tabService;
        this.playerInventoryService = playerInventoryService;

        CreateSlot(slotServiceData.slotCount);

        tabService.Init(new TabService.TabServiceRuntimeData()
        {
            allInventorySlots = allInventorySlots,
            allShopSlots = allShopSlots,
            itemListSO = slotServiceData.itemListSO
        });

        playerInventoryService.InitSlots(allPlayerSlots);
    }

    private void CreateSlot(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            allInventorySlots.Add(new SlotController(slotServiceData.slotPrefab, slotServiceData.slotContainerInventory));
        }

        foreach (ItemSO slotController in slotServiceData.itemListSO.allItemdata)
        {
            allShopSlots.Add(new SlotController(slotServiceData.slotPrefab, slotServiceData.slotContainerShop));
        }

        for (int i = 0; i < slotServiceData.playersSlotCount; i++)
        {
            allPlayerSlots.Add(new SlotController(slotServiceData.slotPrefab, slotServiceData.playerContainerInventory));
        }
    }
}
