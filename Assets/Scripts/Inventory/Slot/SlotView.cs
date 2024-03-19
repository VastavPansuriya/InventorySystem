using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotView : MonoBehaviour
{
    [SerializeField] private Image itemIconImage;
    [SerializeField] private TMP_Text itemCountText;    
    [SerializeField] private ItemUIButton itemUIButton;

    public ItemUIButton GetItemUIButton()
    {
        return itemUIButton;
    }
    public void SetImage(Sprite sprite)
    {
        itemIconImage.sprite = sprite;
    }

    public void SetText(string text)
    {
        itemCountText.text = text;
    }

    public void Show()
    {
        itemIconImage.gameObject.SetActive(true);
        itemCountText.transform.parent.gameObject.SetActive(true);
    }

    public void Hide()
    {
        itemIconImage.gameObject.SetActive(false); 
        itemCountText.transform.parent.gameObject.SetActive(false);
    }
}
