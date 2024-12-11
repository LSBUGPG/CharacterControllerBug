using System.IO;
using System.Text;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    StringBuilder data = new();

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        data.AppendLine("Time,Height");
    }

    void OnDestroy()
    {
        File.WriteAllText("Jump.csv", data.ToString());
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Makes the player jump
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        data.AppendLine($"{Time.time}, {transform.position.y}");
    }
}
