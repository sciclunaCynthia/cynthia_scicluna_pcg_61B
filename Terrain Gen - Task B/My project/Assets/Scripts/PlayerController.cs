using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 50f;

    void Update()
    {
        float moveDirection = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float turnDirection = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;

        transform.Translate(0, 0, moveDirection);
        transform.Rotate(0, turnDirection, 0);
    }
}
