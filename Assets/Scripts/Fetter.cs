using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectItem))]
public class Fetter : MonoBehaviour
{
    public List<FetterType> Types = new List<FetterType>();
    public MinionController CurrentMinion;
    public Transform TPTransform;

    
    public void AddMinion(MinionController minion)
    {
        if (CurrentMinion != minion)
            CurrentMinion = minion;
    }

    public void RemoveMinion(MinionController minion)
    {
        CurrentMinion = null;
    } 
}