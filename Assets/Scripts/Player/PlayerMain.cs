using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMain : MonoBehaviour
{
    internal PlayerMovement _playerMovement;
    private PlayerInput _playerInput;
    internal PlayerOutfit _playerOutfit;
    internal PlayerInventory _playerInventory;
    internal InputAction _inventoryButton;
    internal InputAction _movement;
    internal Animator _animator;

    internal int _horizontalID;
    internal int _verticalID;

    // Start is called before the first frame update
    void Awake()
    {
        _playerInventory = GetComponent<PlayerInventory>();
        _playerInput = GetComponent<PlayerInput>();
        _playerOutfit = GetComponent<PlayerOutfit>();
        _playerMovement = GetComponent<PlayerMovement>();
        _inventoryButton = _playerInput.actions["Inventory"];
        _movement = _playerInput.actions["Movement"];
        _horizontalID = Animator.StringToHash("Horizontal");
        _verticalID = Animator.StringToHash("Vertical");
    }

}
