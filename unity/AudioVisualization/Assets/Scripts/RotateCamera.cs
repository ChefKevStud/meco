using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    private float speed = 4;
    private Vector3 rotationDirection = new Vector3();
    
    public float yPositionOffset = 2;

    public GameObject mainCamera;

    private float lastMousePosY;
    private float mousePosDeltaY;
    private float lastMousePosX;
    private float mousePosDeltaX;

    void Start()
    {
        Vector3 startPosition = transform.position;
        transform.position = new Vector3(startPosition.x, startPosition.y + yPositionOffset, startPosition.z);
        
        rotationDirection = new Vector3(0, speed * Time.deltaTime, 0);

        mainCamera = this.transform.Find("Main Camera").gameObject;

    }
    
    void Update()
    {

        if (Input.GetKey("f") && speed < 64)
        {
            speed += 2;
            rotationDirection = new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey("s") && speed > 0)
        {
            speed -= 2;
            rotationDirection = new Vector3(0, speed * Time.deltaTime, 0);
        }

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
        if (Input.GetKey("space"))
        {
            rotationDirection = new Vector3();
        }
        if (Input.GetMouseButton(0))
        {
            mousePosDeltaX = lastMousePosX - Input.mousePosition.x;
            mousePosDeltaY = lastMousePosY - Input.mousePosition.y;

            rotationDirection = new Vector3(0, -mousePosDeltaX * Time.deltaTime, 0);
            
            lastMousePosX = Input.mousePosition.x;
            lastMousePosY = Input.mousePosition.y;
        }
        else
        {
            lastMousePosX = 0;
            lastMousePosY = 0;
        }

        mainCamera.transform.Translate(0, 0, Input.mouseScrollDelta.y);

        transform.Rotate(rotationDirection);
        
    }
}