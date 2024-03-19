using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameService : GenericMonoSingleton<GameService>
{
    [Header(nameof(SlotService))]
    [SerializeField] private SlotService.SlotServiceData slotServiceData;

    [Header(nameof(TabService))]
    [SerializeField] private TabService.TabServiceEditModeData tabServiceEditModeData;

    [Header(nameof(UIService))]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TMP_Text coinText;

    [Header(nameof(PlayerInventoryService))]
    [SerializeField] private PlayerInventoryService.PlayerInventoryServiceData playerInventoryServiceData;

    [Header(nameof(ShopService))]
    [SerializeField] private Button sellButton;
    [SerializeField] private Button buyButton;

    [Header("Globle")]
    [SerializeField] private ItemListSO itemListSO;
    [SerializeField] private int startingCoinAmount;

    public PlayerInventoryService PlayerInventoryService { get; private set; }
    public SlotService SlotService { get; private set; }
    public TabService TabService { get; private set; }
    public UIService UIService { get; private set; }
    public InputServiceManager InputServiceManager { get; private set; }
    public ShopService ShopService { get; private set; }
    public CurrencyService CurrencyService { get; internal set; }

    protected override void Awake()
    {
        base.Awake();

        slotServiceData.itemListSO = itemListSO;

        CurrencyService = new CurrencyService();

        PlayerInventoryService = new PlayerInventoryService(playerInventoryServiceData);

        ShopService = new ShopService(sellButton, buyButton);

        TabService = new TabService(tabServiceEditModeData, ShopService,PlayerInventoryService);

        SlotService = new SlotService(slotServiceData, TabService, PlayerInventoryService);

        UIService = new UIService(inventoryPanel, shopPanel, coinText, TabService);

        InputServiceManager = new InputServiceManager(UIService);

        ShopService.InitService(TabService);
    }

    private void Start()
    {
        CurrencyService.OnWantAddCoin?.Invoke(startingCoinAmount);
    }
}
