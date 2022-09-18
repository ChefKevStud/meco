using TMPro;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    public float speed = 1;
    public Vector3 rotationDirection = new Vector3();
    private Vector3 formerRotationDirection = new Vector3();
    
    public float yPositionOffset = 2;

    public GameObject mainCamera;

    private float lastMousePosY;
    private float mousePosDeltaY;
    private float lastMousePosX;
    private float mousePosDeltaX;

    private bool mouseControl = false;
    private float formerSpeed = 1;

    private float rotationConstant = 1f;
    
    public GameObject speedLabel;
    private TextMeshProUGUI speedLabelText;

    void Start()
    {
        Vector3 startPosition = transform.position;
        transform.position = new Vector3(startPosition.x, startPosition.y + yPositionOffset, startPosition.z);
        
        rotationDirection = new Vector3(0, rotationConstant, 0);

        mainCamera = this.transform.Find("Main Camera").gameObject;
        
        speedLabelText = speedLabel.GetComponent<TextMeshProUGUI>();

    }
    
    void Update()
    {
        if (mouseControl)
        {
            if (Input.GetMouseButton(0))
            {
                mousePosDeltaX = lastMousePosX - Input.mousePosition.x;
                mousePosDeltaY = lastMousePosY - Input.mousePosition.y;

                rotationDirection = new Vector3(0, -mousePosDeltaX * Time.deltaTime, -mousePosDeltaY * Time.deltaTime);

                lastMousePosX = Input.mousePosition.x;
                lastMousePosY = Input.mousePosition.y;
            }
            else
            {
                lastMousePosX = 0;
                lastMousePosY = 0;
            }
        }
        else
        {
            if (Input.GetKey("right"))
            {
                rotationDirection = new Vector3(0, -rotationConstant, 0);
            }
            if (Input.GetKey("left"))
            {
                rotationDirection = new Vector3(0, rotationConstant, 0);
            }
            if (Input.GetKey("down"))
            {
                rotationDirection = new Vector3(0, 0, rotationConstant);
            }
            if (Input.GetKey("up"))
            {
                rotationDirection = new Vector3(0, 0, -rotationConstant);
            }
            if (Input.GetKey("space"))
            {
                rotationDirection = new Vector3();
            }
            
            if (speed != formerSpeed) 
            {
                if (formerSpeed == 0) formerSpeed = 1;

                float rotationSum = (rotationDirection.x + rotationDirection.y + rotationDirection.z);
                if (rotationSum == 0) rotationSum = 1;

                rotationDirection /= rotationSum;
                rotationDirection *= speed;

                formerSpeed = speed;
                
                SetSpeedText(speed);
            }
            else if (!(rotationDirection.Equals(formerRotationDirection)))
            {
                rotationDirection *= speed;
            }
            
        }

        mainCamera.transform.Translate(0, 0, Input.mouseScrollDelta.y);

        transform.Rotate(rotationDirection);
        formerRotationDirection = rotationDirection;

    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void moveByMouse(bool active)
    {
        mouseControl = active;
    }
    
    public void SetSpeedText(float speed)
    {
        speedLabelText.text = $"Speed: {speed}";
    }
    
}