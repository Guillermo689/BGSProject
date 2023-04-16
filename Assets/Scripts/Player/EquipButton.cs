using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PlayerMain _playerMain;
    public ItemObject Item;
    private Image _image;
    public bool IsShopPanel;
    [SerializeField] private TMP_Text _tooltip;
    [SerializeField] private AudioClip _buyPop;
    [SerializeField] private AudioClip _buyNo;

    // Start is called before the first frame update
    void Start()
    {
        _playerMain = GetComponentInParent<PlayerMain>();
        _image = GetComponentInParent<Image>();
        _image.sprite = Item.Icon;
        FillItemText();
        HideTooltip();
    }

    private void FillItemText()
    {
        if (IsShopPanel)
        {
            _tooltip.text = Item.Name + "/ Price: " + Item.Price.ToString() + " $";
        }
        else
        {
            _tooltip.text = Item.Name;
        }
      
    }
    public void ItemButton()        //This will decide if either buy, sell or equip, and is called directly by the button icon
    {
        if (_playerMain._playerInventory.IsShop)
        {
           if(IsShopPanel) BuyItem();
            else SellItem();
        }
        else
        {
            EquipOutfit();
        }
    }
    private void EquipOutfit()      //Call the EquipItem function on the inventory
    {
       if(!_playerMain._playerInventory.IsShop) _playerMain._playerInventory.EquipItem(Item);
    }

    private void BuyItem()      //Check if player can afford the price, and if it doesn't already have the item, then buy it and remove it from the store
    {
        if (_playerMain._playerInventory._inventory.Money > Item.Price)
        {
            if (!_playerMain._playerInventory.ContainsItem(Item, _playerMain._playerInventory._inventory))
            {
                _playerMain.PlayerAudio.PlayOneShot(_buyPop);
                _playerMain._playerInventory.AddItem(Item);
                _playerMain._playerInventory._inventory.Money -= Item.Price;
                _playerMain._playerInventory.RemoveShopItem(Item);
                _playerMain._playerInventory.UpdateInventoryPanel(_playerMain._playerInventory._inventory, _playerMain._playerInventory._panel, false);
                _playerMain._playerInventory.UpdateInventoryPanel(_playerMain._playerInventory._shopInventory, _playerMain._playerInventory._shopPanel, true);
                Destroy(gameObject);
            }
            else
            {
                _playerMain.PlayerAudio.PlayOneShot(_buyNo);
            }
        }
        else
        {
           _playerMain.PlayerAudio.PlayOneShot(_buyNo);
        }
        
    }
    private void SellItem()     //Remove item form the inventory and add the monuey, if the shop doesn't contain the item, add it, so the inventory won't clutter with the same item
    {

        _playerMain._playerInventory.Removeitem(Item);
        _playerMain._playerInventory._inventory.Money += Item.Price;
        if (!_playerMain._playerInventory.ContainsItem(Item, _playerMain._playerInventory._shopInventory)) _playerMain._playerInventory.AddShopItem(Item);
        _playerMain._playerInventory.UpdateInventoryPanel(_playerMain._playerInventory._inventory, _playerMain._playerInventory._panel, false);
        _playerMain._playerInventory.UpdateInventoryPanel(_playerMain._playerInventory._shopInventory, _playerMain._playerInventory._shopPanel, true);
        Debug.Log("Selling item");
        Destroy(gameObject);
    }

    private void ShowTooltip()
    {
        _tooltip.transform.parent.gameObject.SetActive(true);
    }
    private void HideTooltip()
    {
        _tooltip.transform.parent.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }
}
