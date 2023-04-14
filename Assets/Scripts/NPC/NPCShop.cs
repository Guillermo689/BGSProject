using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCShop : MonoBehaviour
{
    [SerializeField] private InventoryObject _inventory;
    private GameObject _selectedPlayer;
    [SerializeField] private GameObject _talkIcon;

    private void Start()
    {
        _talkIcon.SetActive(false);
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
        playerMain._playerInventory.ToggleInventory(false);
    }

  

}
