using UnityEngine;

public class NPCShop : MonoBehaviour
{
    [SerializeField] private InventoryObject _inventory;
    private GameObject _selectedPlayer;
    [SerializeField] private GameObject _talkIcon;

    [SerializeField] private InventoryObject _itemLibrary;

    private void Start()
    {
        _talkIcon.SetActive(false);
        FillInventory();
    }

    private void FillInventory()
    {
        for (int i = 0; i < _itemLibrary.Items.Count; i++)
        {
            if(!_inventory.Items.Contains(_itemLibrary.Items[i])) _inventory.Items.Add(_itemLibrary.Items[i]);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _selectedPlayer = collision.gameObject;
            _talkIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CloseShop();
            _talkIcon.SetActive(false);
            _selectedPlayer = null;
        }
    }

    public void CloseShop()
    {
        ToggleShop(_selectedPlayer, false);
    }

    public void OpenShop()
    {
        ToggleShop(_selectedPlayer, true);
    }

    private void ToggleShop(GameObject player, bool open)       //Open and close the shop window, activate the bool on the player that is shopping to prevent equiping instead of sell
    {
        PlayerMain playerMain = player.GetComponent<PlayerMain>();
        playerMain._playerInventory._shopInventory = _inventory;
        playerMain._playerInventory.IsShop = open;
        playerMain._playerInventory.ToggleShop(open);
        if(open) playerMain._playerInventory.OpenInventory();
        else playerMain._playerInventory.CloseInventory();
    }
    private void OnApplicationQuit()
    {
        _inventory.Items.Clear();
    }


}
