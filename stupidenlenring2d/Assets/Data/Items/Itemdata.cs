using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Itemdata : ScriptableObject
{
    public string itemID;
    public string itemName;
    public string itemType;
    [TextArea]
    public string itemDescription;
    public Sprite itemSprite;
    public GameObject consumeItem;
}
