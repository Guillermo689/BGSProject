using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorActivator : MonoBehaviour
{
    [SerializeField] private GameObject _interiors;
    void Start()
    {
        _interiors.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _interiors.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _interiors.SetActive(false);
        }
    }
}
