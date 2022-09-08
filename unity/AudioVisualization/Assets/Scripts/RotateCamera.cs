using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    public float speed = 1;
    private Vector3 rotationDirection = new Vector3();
    
    public float yPositionOffset = 2;

    void Start()
    {
        Vector3 startPosition = transform.position;
        transform.position = new Vector3(startPosition.x, startPosition.y + yPositionOffset, startPosition.z);
        
        rotationDirection = new Vector3(0, speed * Time.deltaTime, 0);
        
    }
    
    void Update()
    {

        if (Input.GetKey("right"))
        {
            rotationDirection = new Vector3(0, -speed * Time.deltaTime, 0);
        }
        if (Input.GetKey("left"))
        {
            rotationDirection = new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey("down"))
        {
            rotationDirection = new Vector3(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey("up"))
        {
            rotationDirection = new Vector3(0, 0, -speed * Time.deltaTime);
        }
        
        transform.Rotate(rotationDirection);
        
    }
}