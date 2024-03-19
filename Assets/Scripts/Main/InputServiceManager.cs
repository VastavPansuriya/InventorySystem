using System;

public class InputServiceManager 
{
    private InputService inputService;
    private UIService uiService;

    public static event Action OnShopPanelOpen;
    public static event Action OnInventoryPanelOpen;

    public InputServiceManager(UIService uiService)
    {
        inputService = new InputService();
        inputService.UIInput.Enable();
        this.uiService = uiService;

        inputService.UIInput.InventoryOpen.performed += InventoryOpen_performed;
        inputService.UIInput.ShopOpen.performed += ShopOpen_performed;
    }

    private void ShopOpen_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        uiService.ShowShop();
        OnShopPanelOpen?.Invoke();
    }

    private void InventoryOpen_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        uiService.ShowInventory();
        OnInventoryPanelOpen?.Invoke();
    }


    ~InputServiceManager() 
    {
        inputService.UIInput.ShopOpen.performed -= ShopOpen_performed;
        inputService.UIInput.InventoryOpen.performed -= InventoryOpen_performed;
    }
}
