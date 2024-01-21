using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        var input = context.ReadValue<Vector2>();
        Vector3 position = player.transform.position;
        Vector3 targetPosition = position + new Vector3(input.x, 0, 0);
        
        position = Vector3.Lerp(position, targetPosition, speed * Time.fixedDeltaTime);
        player.transform.position = position;
    }
}
