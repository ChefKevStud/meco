using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    public float speed = 1;
    public float yPositionOffset = 2;

    void Start()
    {
        Vector3 startPosition = transform.position;
        transform.position = new Vector3(startPosition.x, startPosition.y + yPositionOffset, startPosition.z);
    }
    
    void Update()
    {

        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -speed * Time.deltaTime, 0);
        }
        else
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey("up"))
        {
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey("down"))
        {
            transform.Rotate(0, 0, -speed * Time.deltaTime);
        }
    }
}