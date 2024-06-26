using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

public enum ItemTpye
{
    Marterial,
    Equipment
}
[CreateAssetMenu(fileName ="New Item Data",menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemTpye itemType;
    public string itemName;
    public Sprite icon;
    public string itemId;

    [Range(0,100)]
    public float dropChance;

    protected StringBuilder sb =new StringBuilder();

    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }
    public virtual string GetDiscription()
    {
        return "";
    }

}
