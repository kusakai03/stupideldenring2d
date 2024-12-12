using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquipmentForge:ScriptableObject{
    public string rid;
    public string eid;
    public List<ItemDrop> materials;
    public int cost;
}
