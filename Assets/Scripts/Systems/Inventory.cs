using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    private static Inventory instance;
    public static Inventory Instance
    {
        get
        {
            if ( instance == null )
                instance = FindFirstObjectByType< Inventory >( );
            return instance;
        }
    }

    [ SerializeField ] private List< Item > items = new List< Item >( );
    public UnityEvent< Item > onItemAdded;
    public UnityEvent< Item > onItemRemoved;
    public UnityEvent onInventoryChanged;

    [ SerializeField ] private TMPro.TextMeshProUGUI notificationText;
    private Coroutine resetCoroutine;

    void Awake()
    {
        if ( instance == null )
        {
            instance = this;
            DontDestroyOnLoad( gameObject );
        }
        else if ( instance != this )
            Destroy( gameObject );
        
    }

    public bool add( Item item )
    {
        items.Add( item );
        onItemAdded?.Invoke( item );
        onInventoryChanged?.Invoke();

        switch ( item.itemType )
        {
            case Item.ItemType.None: notificationText.text = $"Добавлен предмет: {item.itemName}."; break;
            case Item.ItemType.Key: notificationText.text = $"Подобран ключ: {item.itemName}."; break;
        }

        if ( resetCoroutine != null )
            StopCoroutine( resetCoroutine );

        resetCoroutine = StartCoroutine( reset_notify( 5f ) );

        return true;
    }

    public bool remove( Item item )
    {
        if ( items.Remove( item ) )
        {
            onItemRemoved?.Invoke( item );
            onInventoryChanged?.Invoke( );
            return true;
        }
        return false;
    }

    public bool has( string id ) => items.Exists( item => item.itemID == id );
    
    public Item get( string id ) => items.Find( item => item.itemID == id );

    public List< Item > get_items( ) => new List< Item >( items );

    public void clear( )
    {
        items.Clear( );
        onInventoryChanged?.Invoke( );
    }

    private IEnumerator reset_notify( float delay )
    {
        yield return new WaitForSeconds( delay );
        notificationText.text = "";
        resetCoroutine = null;
    }

}