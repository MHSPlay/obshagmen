using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [ SerializeField ] private Item reqKey;
    [ SerializeField ] private string reqKeyID;
    [ SerializeField ] private bool isLocked = true;

    void Start( )
    {
        if ( reqKey != null && reqKey.itemType == Item.ItemType.Key )
            reqKeyID = reqKey.itemID;
        
    }

    public void interact( )
    {
        if ( isLocked )
        {
            if ( Inventory.Instance.has( reqKey.itemID ) )
            {
                unlock( );
            }
            else
            {
                // locked door feedback ?
            }
        }
        else
        {
            // open
        }
    }

    public string get_text( )
    {
        if ( isLocked )
            return $"[E] Открыть (Требуется: { ( reqKey ? reqKey.itemName : "Ключ" ) })";
        else
            return "[E] Взаимодействие";

    }

    void unlock( )
    {
        isLocked = false;
        
        // play sound ?

        Inventory.Instance.remove( reqKey );
       
    }

    public bool IsLocked( ) => isLocked;

}
