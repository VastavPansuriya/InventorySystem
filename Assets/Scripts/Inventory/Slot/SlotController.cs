using System;
using UnityEngine;

public class SlotController
{
    private SlotView slotView;
    private SlotModel slotModel;
    public SlotController(SlotView slotViewPrefab, Transform parent)
    {
        slotView = MonoBehaviour.Instantiate(slotViewPrefab, parent);
        slotModel = new SlotModel(SlotState.Empty);
        SetEmpty();
    }

    private void SetImage(Sprite sprite)
    {
        slotView.SetImage(sprite);
    }

    private void SetText(string text)
    {
        slotView.SetText(text);
    }

    public void ShowHideToggle(bool toggle)
    {
        Action action = toggle ? slotView.Show : slotView.Hide;
        action?.Invoke();
    }

    public bool IsSlotEmpty()
    {
        return slotModel.GetState() == SlotState.Empty;
    }

    public void SetEmpty()
    {
        slotView.GetItemUIButton().SetSlot(null);
        slotModel.SetState(SlotState.Empty);
        slotModel.SetItemSO(null);
        slotView.Hide();
    }

    public void SetFilled(ItemSO itemSO)
    {
        ItemUIButton itemUIButton = slotView.GetItemUIButton();
        itemUIButton.SetItemSo(itemSO);
        itemUIButton.SetSlot(this);
        slotView.Show();
        slotModel.SetItemSO(itemSO);
        slotModel.GetItemSO();
        SetText(itemSO.Quantity.ToString());
        SetImage(itemSO.icon);
        slotModel.SetState(SlotState.Filled);
    }

    public ItemSO GetItemSO() => slotModel.GetItemSO();
}