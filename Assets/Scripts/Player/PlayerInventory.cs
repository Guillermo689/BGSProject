using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private PlayerMain _playerMain;
    [SerializeField] internal InventoryObject _inventory;
    [SerializeField] internal GameObject _panel;
    [Header("Item Variables")]
    [SerializeField] private GameObject _inventoryWindow;
    [SerializeField] internal ItemObject _equippedItem;
    [SerializeField] private TMP_Text _money;
    [SerializeField] private AudioClip _equipPop;
    [Header("Shop Variables")]
    [SerializeField] private GameObject _shopWindow;
    [SerializeField] internal GameObject _shopPanel;
    [SerializeField] internal InventoryObject _shopInventory;
    [SerializeField] private GameObject _unequipButton;
    public bool IsShop;

    void Start()
    {
        _playerMain = GetComponent<PlayerMain>();
        _inventoryWindow.SetActive(false);
        _shopWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerMain._inventoryButton.WasPressedThisFrame()) ToggleInventory(false);
        if (_playerMain._addMoney.WasPressedThisFrame()) AddMoney();
        MoneyText();
    }

    private void MoneyText()
    {
        _money.text = "$ " + _inventory.Money.ToString();
    }
    public void AddItem(ItemObject item)
    {
        _inventory.Items.Add(item);
    }
    public void EquipItem(ItemObject item)
    {
        if (_equippedItem != null) UnequipItem();
        _equippedItem = item;
        EquipPop();
        _playerMain._playerOutfit.UpdateOutfit();
    }
    public void UnequipItem()       //Unequip the item and set the outfit to 0
    {
        _equippedItem = null;
        _playerMain._playerOutfit.UpdateOutfit();
    }
    public void Removeitem(ItemObject item)
    {
        _inventory.Items.Remove(item);
    }

    #region Invnetory Window
    public void ToggleInventory(bool isShopPanel)        //Check if inventory is closed and open it, and vice versa, and check if is shopping, so it activate the shopping functions instead
    {
        if(_inventoryWindow.activeSelf) _inventoryWindow.SetActive(false);
        else
        {
            _inventoryWindow.SetActive(true);
            UpdateInventoryPanel(_inventory, _panel, isShopPanel);
            if (IsShop) _unequipButton.SetActive(false);
            else _unequipButton.SetActive(true);
            DestroyIcon(_inventory, _panel);
        }
    }

    public void OpenInventory()
    {
        _inventoryWindow.SetActive(true);
        UpdateInventoryPanel(_inventory, _panel, false);
        if (IsShop) _unequipButton.SetActive(false);
        else _unequipButton.SetActive(true);
        DestroyIcon(_inventory, _panel);
    }
    public void CloseInventory()
    {
        _inventoryWindow.SetActive(false);
    }
    private void DestroyIcon(InventoryObject inventory, GameObject panel)        //Check all items in the inventory window, if doesn't contain item, create the button
    {
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            EquipButton button = panel.transform.GetChild(i).GetComponent<EquipButton>();
            if (!ContainsItem(button.Item, inventory))
            {
                Destroy(button.gameObject);
                return;
            }
            
        }
    }
    public void UpdateInventoryPanel(InventoryObject inventory, GameObject panel, bool isShopPanel)      //Check all items in the inventory, if panel doesn't contain the item, create icon 
    {
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            if (!ContainsIcon(inventory.Items[i], panel))
            {
                GameObject icon = Instantiate(SetupIcon(inventory.Items[i]), panel.transform);
                EquipButton button = icon.GetComponent<EquipButton>();
                button.IsShopPanel = isShopPanel;
            }
        }
    }
    private bool ContainsIcon(ItemObject item, GameObject panel)      //Check all items in the invnetory Panel and return true if it contains the item icon
    {
        bool hasItem = false;
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            EquipButton button = panel.transform.GetChild(i).GetComponent<EquipButton>();
            if (item == button.Item)
            {
                hasItem = true;
            }
        }
        return hasItem;
    }
    private GameObject SetupIcon(ItemObject item)           //Get the prefab from the item and link the item to the prefab
    {
        GameObject itemPrefab = item.Prefab;
        EquipButton itemButton = itemPrefab.GetComponent<EquipButton>();
        itemButton.Item = item;
        return itemPrefab;
    }
    public bool ContainsItem(ItemObject item, InventoryObject inventory)          //Check all the items in the inventory return true if contains the item
    {
        bool hasItem = false;
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            if (inventory.Items[i] == item)
            {
                hasItem = true;
            }
        }
        return hasItem;
    }
    #endregion

    #region Shop Window

    public void ToggleShop(bool open)       //Open and close the shop window
    {
        if (!open)
        {
            for (int i = 0; i < _shopPanel.transform.childCount; i++)
            {
                Destroy(_shopPanel.transform.GetChild(i).gameObject);
            }
            _shopWindow.SetActive(false);
        }
        else
        {

            _shopWindow.SetActive(true);
            UpdateInventoryPanel(_inventory, _panel, false);
            UpdateInventoryPanel(_shopInventory, _shopPanel, true);
        }
       
    }

    public void AddShopItem(ItemObject item)        //Add item to shopper inventory
    {
        _shopInventory.Items.Add(item);
    }

    public void RemoveShopItem(ItemObject item)     //Remove item from shop inventory
    {
        _shopInventory.Items.Remove(item);

    }

    #endregion

    private void AddMoney()
    {
        _inventory.Money += 10;
    }
    public void ClearInventory()
    {
        _inventory.Items.Clear();
        _inventory.Money = 0;
    }

    public void EquipPop()
    {
        _playerMain.PlayerAudio.PlayOneShot(_equipPop);
    }
}
