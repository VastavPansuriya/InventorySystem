using UnityEngine;

[CreateAssetMenu()]
public class ItemSO : ScriptableObject
{
    public ItemType itemType;
    public Sprite icon;
    public string itemDescription;
    public int buyingPrice;
    public int sellingPrice;
    public int weight;
    public Rarity rarity;
    public int Quantity;
}
