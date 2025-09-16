using UnityEngine;
using UnityEngine.UIElements;

public class Interaction : MonoBehaviour
{
    [ SerializeField ] private float interactionRange = 3f;
    [ SerializeField ] private LayerMask interactionLayer = -1;
    [ SerializeField ] private Camera playerCamera;

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

            Debug.DrawRay( playerCamera.transform.position, playerCamera.transform.forward * interactionRange, Color.red );

            if ( interactable != null )
            {
                if ( current != interactable)
                {
                    current = interactable;
                    // some ui ?
                }
            }
            else
            {
                // clear ui ?
            }
        }
        else
        {
            // clear ui ?
        }
    }


}
