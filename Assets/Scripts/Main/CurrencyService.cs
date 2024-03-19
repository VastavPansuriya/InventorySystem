using System;

public class CurrencyService
{
    public static Action<int> OnWantAddCoin;
    public static Action<int> OnWantRemoveCoin;

    public CurrencyService()
    {
        OnWantAddCoin += CurrencyService_OnWantAddCoin;
        OnWantRemoveCoin += CurrencyService_OnWantRemoveCoin;
    }

    private void CurrencyService_OnWantRemoveCoin(int value)
    {
        GlobalData.TotalCurrency -= value;
    }

    private void CurrencyService_OnWantAddCoin(int value)
    {
        GlobalData.TotalCurrency += value;
    }

    ~CurrencyService()
    {
        OnWantAddCoin -= CurrencyService_OnWantAddCoin;
        OnWantRemoveCoin -= CurrencyService_OnWantRemoveCoin;
    }

    public bool CanPurchaseInAmmountOf(int ammount)
    {
        return GlobalData.TotalCurrency >= ammount;
    }
}