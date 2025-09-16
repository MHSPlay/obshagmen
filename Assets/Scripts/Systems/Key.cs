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
        return $"[E] ��������� {(itemData ? itemData.itemName : "����")}";
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
