using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [ SerializeField ] private float interactionRange = 3f;
    [ SerializeField ] private LayerMask interactionLayer = -1;
    [ SerializeField ] private Camera playerCamera;

    [ SerializeField ] private Image interactionIcon;
    [ SerializeField ] private TMPro.TextMeshProUGUI interactionText;

    [ SerializeField ] private Sprite transparentIcon;

    private IInteractable current;
    private Door currentDoor;

    void Start( )
    {

        if ( playerCamera == null )
            playerCamera = Camera.main;

    }

    void Update( )
    {
        check( );

        if ( current != null && Input.GetKeyDown( KeyCode.E ) )
            current.interact( );
        
    }

    void check( )
    {
        Ray ray = new Ray( playerCamera.transform.position, playerCamera.transform.forward );
        RaycastHit hit;
        
        if ( Physics.Raycast( ray, out hit, interactionRange, interactionLayer ) )
        {
            IInteractable interactable = hit.collider.GetComponent< IInteractable >( );

            //Debug.DrawRay( playerCamera.transform.position, playerCamera.transform.forward * interactionRange, Color.red );

            if ( interactable != null )
            {
                if ( current != interactable )
                {

                    if ( currentDoor != null )
                    {
                        currentDoor.onDoorStateChanged.RemoveListener( UpdateUI );
                        currentDoor = null;
                    }

                    current = interactable;

                    Door door = hit.collider.GetComponent< Door >( );
                    if ( door != null )
                    {
                        currentDoor = door;
                        currentDoor.onDoorStateChanged.AddListener( UpdateUI );
                    }

                    showUI( interactable.get_text( ), interactable.get_icon( ) );
                }
            }
            else
                clearUI( );
            
        }
        else
            clearUI( );
        
    }

    void showUI( string text, Sprite icon )
    {
        if ( current != null )
        {
            interactionText.gameObject.SetActive( true );
            interactionText.text = text;

            if ( interactionIcon != null )
                interactionIcon.sprite = icon != null ? icon : transparentIcon;
            

        }
        
    }
    
    void clearUI( )
    {
        if ( current != null )
        {
            current = null;
            
            if ( interactionText != null )
                interactionText.gameObject.SetActive( false );

            if ( interactionIcon != null )
                interactionIcon.sprite = transparentIcon;

        }
    }

    void UpdateUI( )
    {
        if ( current != null )
            showUI( current.get_text( ), current.get_icon( ) );
    }

}
