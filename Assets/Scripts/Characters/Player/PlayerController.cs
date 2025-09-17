using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _walkingSpeed = 5f;
    [SerializeField] private float _runningSpeed = 8f;

    [SerializeField] private float maxVelocityChange = 10f;

    [Header("Target direction")]    
    [SerializeField] private Transform _cameraTransform;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent< Rigidbody >( );
        _rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update( )
    {
        //GroundCheck( );
    }

    private void FixedUpdate( )
    {
        MoveCharacter( );
    }

    private void MoveCharacter( ) 
    {
        Vector3 move = _cameraTransform.forward * Input.GetAxisRaw( "Vertical" ) + _cameraTransform.right * Input.GetAxisRaw( "Horizontal" );
        move.y = 0f;

        Vector3 targetVelocity = move.normalized * _walkingSpeed;

        Vector3 velocity = _rb.linearVelocity;

        Vector3 velocityChange = targetVelocity - velocity;
        velocityChange.x = Mathf.Clamp( velocityChange.x, -maxVelocityChange, maxVelocityChange );
        velocityChange.z = Mathf.Clamp( velocityChange.z, -maxVelocityChange, maxVelocityChange );
        velocityChange.y = 0;

        _rb.AddForce( velocityChange, ForceMode.VelocityChange );
    }

}
