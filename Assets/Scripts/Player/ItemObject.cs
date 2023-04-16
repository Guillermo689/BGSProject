using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class ItemObject : ScriptableObject
{
    public string Name;
    public int OutfitIndex;
    public int Price;
    public Sprite Icon;
    public GameObject Prefab;
   
}
