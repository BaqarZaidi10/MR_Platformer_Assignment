using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float force = 10f; // Adjust this value as needed
    private Vector3 hitDir;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            hitDir = hit.normal;
            var playerController = hit.controller;
            if (playerController != null)
            {
                ApplyBounceForce(playerController);
            }
        }
    }

    private void ApplyBounceForce(CharacterController controller)
    {
        // Calculate the bounce direction
        Vector3 bounceDirection = -hitDir * force;

        // Apply the bounce force
        StartCoroutine(BounceCoroutine(controller, bounceDirection));
    }

    private System.Collections.IEnumerator BounceCoroutine(CharacterController controller, Vector3 bounceForce)
    {
        float bounceTime = 0.5f; // Adjust this value to control the duration of the bounce
        float elapsedTime = 0f;

        while (elapsedTime < bounceTime)
        {
            controller.Move(bounceForce * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}