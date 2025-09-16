using UnityEngine;
using UnityEngine.UIElements;

public class Interaction : MonoBehaviour
{
    [ SerializeField ] private float interactionRange = 3f;
    [ SerializeField ] private LayerMask interactionLayer = -1;
    [ SerializeField ] private Camera playerCamera;

    [ SerializeField ] private Image interactionIcon;
    [ SerializeField ] private TMPro.TextMeshProUGUI interactionText;

    private IInteractable current;

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
                    current = interactable;
                    showUI( interactable.get_text( ) );
                }
            }
            else
                clearUI( );
            
        }
        else
            clearUI( );
        
    }

    void showUI( string text )
    {
        if ( current != null )
        {
            interactionText.gameObject.SetActive( true );
            interactionText.text = text;
        }
        
    }
    
    void clearUI( )
    {
        if ( current != null )
        {
            current = null;
            
            if ( interactionText != null )
                interactionText.gameObject.SetActive( false );

        }
    }

}
