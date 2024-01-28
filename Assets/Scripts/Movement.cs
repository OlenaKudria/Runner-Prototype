using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject playerHolder;
    [SerializeField] private GameObject playerCube;
    [SerializeField] private GameObject platform;
    [SerializeField] private float speed;
    private Collider _collider;
    private Renderer _renderer;
    private float _colliderExtent;
    private float _rendererExtent;

    private void Start()
    {
        if (playerCube != null)
        {
            _renderer = playerCube.GetComponentInChildren<Renderer>();
            if (_renderer != null)
                _rendererExtent = _renderer.bounds.extents.x;
            else
                throw new NullReferenceException("_renderer is null");
        }
        
        if (platform != null)
        {
            _collider = platform.GetComponent<Collider>();

            if (_collider != null && _renderer != null)
                _colliderExtent = _collider.bounds.extents.x - _rendererExtent;
            else 
                throw new NullReferenceException("_collider is null");
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        float inputX = context.ReadValue<Vector2>().x;
        Vector3 playerPosition = playerHolder.transform.position;

        Vector3 platformPosition = platform.transform.position;
        float rightEdge = platformPosition.x + _colliderExtent;
        float leftEdge = platformPosition.x - _colliderExtent;
        
        float clampedX = Mathf.Clamp(playerPosition.x + inputX, leftEdge, rightEdge);

        Vector3 targetPosition = new Vector3(clampedX, playerPosition.y, playerPosition.z);

        playerHolder.transform.position = Vector3.Lerp(playerPosition, targetPosition, speed * Time.fixedDeltaTime);
    }
}