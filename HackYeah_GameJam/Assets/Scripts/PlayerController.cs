using System.Collections;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [Header("General")]
    [SerializeField] private Rigidbody rb;
    [Header("Human Stats")]
    [SerializeField] private Vector2 MoveInput = Vector2.zero;
    [SerializeField] private float speedMultiplayer = 3;
    [SerializeField] private bool isMoving = false;
    [Header("Wurm stats")]
    [SerializeField] private bool canInteract = true;
    [SerializeField] private float timeout = 3;
    [SerializeField] private float forceMultiplayer = 20;
    [SerializeField] private float velocityMax = 3;


    Player map;

    private void OnEnable() {
        map = new Player();
        map.Default.Enable();

        map.Default.Move.performed += SetMove;
        map.Default.Move.canceled += StopMove;

        map.Default.Rotate.performed += Rotate;
        map.Default.Rotate.canceled += Rotate;

        map.Default.InteractWithPlayer.performed += InteractWithPlayer;
        map.Default.InteractWithPlayer.canceled += InteractWithPlayer;
    }

    private void OnDisable() {
        map.Default.Move.performed -= SetMove;
        map.Default.Move.canceled -= SetMove;

        map.Default.Rotate.performed -= Rotate;
        map.Default.Rotate.canceled -= Rotate;

        map.Default.InteractWithPlayer.performed -= InteractWithPlayer;
        map.Default.InteractWithPlayer.canceled -= InteractWithPlayer;

        map.Default.Disable();
    }

    private void FixedUpdate() {
        if (isMoving && rb.velocity.magnitude < velocityMax) {
            rb.AddRelativeForce(new Vector3(-MoveInput.y, 0, MoveInput.x) * speedMultiplayer, ForceMode.Force);
        }
    }

    private void SetMove(InputAction.CallbackContext ctx) {
        isMoving = true;
        MoveInput = ctx.ReadValue<Vector2>();
    }

    private void StopMove(InputAction.CallbackContext ctx) {
        isMoving = false;
        MoveInput = ctx.ReadValue<Vector2>();
    }

    private void Rotate(InputAction.CallbackContext ctx) {

    }

    private void InteractWithPlayer(InputAction.CallbackContext ctx) {
        if (canInteract) {
            StartCoroutine(SetTimeout());
            rb.AddForce(new Vector3(-ctx.ReadValue<Vector2>().y, 0, ctx.ReadValue<Vector2>().x) * forceMultiplayer, ForceMode.Impulse);
            //rb.AddRelativeForce(new Vector3(-ctx.ReadValue<Vector2>().y, 0, ctx.ReadValue<Vector2>().x) * forceMultiplayer, ForceMode.Impulse);
        }
    }

    private IEnumerator SetTimeout() {
        canInteract = false;
        yield return (new WaitForSeconds(timeout));
        canInteract = true;
    }
}