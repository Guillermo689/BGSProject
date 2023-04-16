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
    internal InputAction _addMoney;
    internal InputAction _movement;
    internal InputAction _pause;
    internal Animator _animator;

    internal int _horizontalID;
    internal int _verticalID;

    [SerializeField] private GameObject _pauseMenu;
    private bool isPaused;
    public AudioSource PlayerAudio;

    // Start is called before the first frame update
    void Awake()
    {
        _playerInventory = GetComponent<PlayerInventory>();
        _playerInput = GetComponent<PlayerInput>();
        _playerOutfit = GetComponent<PlayerOutfit>();
        _playerMovement = GetComponent<PlayerMovement>();
        PlayerAudio = GetComponent<AudioSource>();
        _inventoryButton = _playerInput.actions["Inventory"];
        _addMoney = _playerInput.actions["AddMoney"];
        _movement = _playerInput.actions["Movement"];
        _pause = _playerInput.actions["Pause"];
        _horizontalID = Animator.StringToHash("Horizontal");
        _verticalID = Animator.StringToHash("Vertical");
    }

    private void Start()
    {
        _pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (_pause.WasPressedThisFrame())
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void QuitGame()
    {
        _playerInventory.ClearInventory();
        StartCoroutine(Quit());
    }
    IEnumerator Quit()
    {
        yield return new WaitForEndOfFrame();
        Application.Quit();
    }
}
