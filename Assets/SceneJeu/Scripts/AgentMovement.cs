using UnityEngine;

public class AgentManualMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;
        transform.Translate(move * speed * Time.deltaTime, Space.World);
    }
}
