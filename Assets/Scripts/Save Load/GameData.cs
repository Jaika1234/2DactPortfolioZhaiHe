using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public int currency;
    public Dictionary<string, int> inventory;
    public GameData()
    {
        this.currency= 0;
        inventory = new Dictionary<string, int>();
    }

}
