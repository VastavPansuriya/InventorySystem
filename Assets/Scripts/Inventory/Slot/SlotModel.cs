public class SlotModel
{
    private ItemSO itemSO;
    private SlotState currentState;
    public SlotModel(SlotState slotState)
    {
        currentState = slotState;
    }

    public void SetState(SlotState slotState)
    {
        currentState = slotState;
    }

    public void SetItemSO (ItemSO itemSO) => this.itemSO = itemSO;

    public SlotState GetState() { return  currentState; }

    internal ItemSO GetItemSO() => itemSO;
}