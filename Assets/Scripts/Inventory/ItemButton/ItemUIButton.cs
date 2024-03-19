using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIButton : MonoBehaviour
{
    [SerializeField] private Button button;
    public SlotController slotController;

    public static event Action<ItemUIButton, ItemSO> OnItemUIButtonPress;

    private ItemSO itemSO;

    public void SetItemSo(ItemSO itemSO)
    {
        this.itemSO = itemSO;
    }

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            OnItemUIButtonPress?.Invoke(this, itemSO);
        });
    }

    public void SetSlot(SlotController slotController)
    {
        this.slotController = slotController;
    }

    public SlotController GetSlotController()
    {
        return slotController;
    }
}
