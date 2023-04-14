using UnityEngine;
using UnityEngine.UI;

public class EquipButton : MonoBehaviour
{
    private PlayerMain _playerMain;
    public ItemObject Item;
    private Image _image;
    public bool IsShopPanel;
    // Start is called before the first frame update
    void Start()
    {
        _playerMain = GetComponentInParent<PlayerMain>();
        _image = GetComponentInParent<Image>();
        _image.sprite = Item.Icon;
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
                _playerMain._playerInventory.AddItem(Item);
                _playerMain._playerInventory._inventory.Money -= Item.Price;
                _playerMain._playerInventory.RemoveShopItem(Item);
                _playerMain._playerInventory.UpdateInventoryPanel(_playerMain._playerInventory._inventory, _playerMain._playerInventory._panel, false);
                _playerMain._playerInventory.UpdateInventoryPanel(_playerMain._playerInventory._shopInventory, _playerMain._playerInventory._shopPanel, true);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Already contains item");
            }
        }
        else
        {
            Debug.Log("Can't buy ");
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

}
