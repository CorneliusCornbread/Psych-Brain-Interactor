using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private Transform target;
    
    [SerializeField]
    private Vector2 rotationXMinMax = new Vector2(-40, 40);

    [SerializeField]
    [Range(.1f, 6)]
    private float mouseSensitivity = 3;

    [SerializeField]
    [Range(.1f, 6)]
    private float scrollSensitivity = 3;
    
    private float _rotationY;
    private float _rotationX;
    
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * -mouseSensitivity;

            _rotationY += mouseX;
            _rotationX += mouseY;

            // Apply clamping for x rotation 
            _rotationX = Mathf.Clamp(_rotationX, rotationXMinMax.x, rotationXMinMax.y);

            transform.localEulerAngles = new Vector3(_rotationX, _rotationY);

            // Subtract forward vector of the GameObject to point its forward vector to the target
            transform.position = target.position - transform.forward * Vector3.Distance(transform.position, target.position);
        }

        float scrollDelta = Input.mouseScrollDelta.y;

        transform.position += transform.forward * scrollDelta * scrollSensitivity;
        
        transform.LookAt(target.position);
    }
    
}
