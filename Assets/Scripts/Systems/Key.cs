using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [ SerializeField ] private Item itemData;

    public void interact( )
    {
        pickup( );
    }

    public string get_text( )
    {
        return $"[E] Подобрать {(itemData ? itemData.itemName : "Ключ")}";
    }

    void pickup( )
    {
        if ( Inventory.Instance.add( itemData ) )
        {
            // animation ?

            Destroy( gameObject );
        }
    }
}
