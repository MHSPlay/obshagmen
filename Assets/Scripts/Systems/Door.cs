using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable
{
    [ SerializeField ] private Item reqKey;
    [ SerializeField ] private string reqKeyID;
    [ SerializeField ] private bool isLocked = true;

    private bool isOpen = false;

    public UnityEvent onDoorOpened;
    public UnityEvent onDoorClosed;
    public UnityEvent onDoorUnlocked;
    public UnityEvent onAccessDenied;

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
                toggle( );
            }
            else
            {
                // locked door feedback ?
            }
        }
        else
        {
            toggle( );
        }
    }

    public string get_text( )
    {
        // @todo: fix text
        if ( isLocked )
            return $"[E] Открыть (Требуется: {(reqKey ? reqKey.itemName : "Ключ")})";
        else
            return isOpen ? "[E] Закрыть дверь" : "[E] Открыть дверь";

    }

    void unlock( )
    {
        isLocked = false;

        //PlaySound();

        Inventory.Instance.remove( reqKey );
        onDoorUnlocked?.Invoke( );
    }

    void toggle( )
    {
        if ( isOpen )
            close( );
        else
            open( );
    }

    public void open( )
    {
        if ( isOpen )
            return;

        isOpen = true;

        //PlaySound();

        onDoorOpened?.Invoke( );
    }

    public void close()
    {
        if ( !isOpen )
            return;

        isOpen = false;

        //PlaySound();

        onDoorClosed?.Invoke( );
    }

    public bool IsLocked( ) => isLocked;
    public bool IsOpen( ) => isOpen;

}
