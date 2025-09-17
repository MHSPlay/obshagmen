using UnityEngine;

[ CreateAssetMenu( fileName = "New Item", menuName = "Inventory/Item" ) ]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemID;
    public string description;
    public Sprite defaultSprite;
    public ItemType itemType;

    public enum ItemType
    {
        None,
        Key
    }

    public Item( string name, string id, string desc, ItemType type )
    {
        itemName = name;
        itemID = id;
        description = desc;
        itemType = type;
    }

}
