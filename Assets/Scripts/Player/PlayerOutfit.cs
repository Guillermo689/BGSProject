using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutfit : MonoBehaviour
{
    private PlayerMain _playerMain;
    [SerializeField] private GameObject _selectedOutfit;
    [SerializeField] private List<GameObject> _outfits;

    void Start()
    {
        _playerMain = GetComponent<PlayerMain>();
        UpdateOutfit();
    }

    internal void UpdateOutfit()
    {
        if (_playerMain._playerInventory._equippedItem == null) ChangeOutfit(0);
        else ChangeOutfit(_playerMain._playerInventory._equippedItem.OutfitIndex);
    }
    private void ChangeOutfit(int outfitIndex)
    {
        //Deactivate all outfits and set the current outfit and activate it and select the animator
        foreach (GameObject outfit in _outfits) outfit.SetActive(false);
        _selectedOutfit = _outfits[outfitIndex];
        _selectedOutfit.SetActive(true);
        _playerMain._animator = _selectedOutfit.GetComponent<Animator>();

    }
}
