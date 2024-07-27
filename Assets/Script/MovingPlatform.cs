using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Platform Movement")]
    public Vector3[] waypoints;
    public float moveSpeed = 2f;
    public float waitTime = 1f;

    [Header("Platform Rotation")]
    public Vector3 rotationSpeed;
    public bool shouldRotate = false;

    private int currentWaypointIndex = 0;
    private List<CharacterController> playersOnPlatform = new List<CharacterController>();
    private Vector3 lastPosition;

    private void Start()
    {
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0];
        }
        lastPosition = transform.position;
        StartCoroutine(MovePlatform());
    }

    private void Update()
    {
        if (shouldRotate)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }

        Vector3 movement = transform.position - lastPosition;
        foreach (CharacterController player in playersOnPlatform)
        {
            player.Move(movement);
        }

        lastPosition = transform.position;
    }

    private IEnumerator MovePlatform()
    {
        while (true)
        {
            yield return StartCoroutine(MoveToNextWaypoint());
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator MoveToNextWaypoint()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = waypoints[currentWaypointIndex];
        float journeyLength = Vector3.Distance(startPosition, endPosition);
        float startTime = Time.time;

        while (transform.position != endPosition)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
            yield return null;
        }

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterController playerController = other.GetComponent<CharacterController>();
        if (playerController != null && !playersOnPlatform.Contains(playerController))
        {
            playersOnPlatform.Add(playerController);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterController playerController = other.GetComponent<CharacterController>();
        if (playerController != null)
        {
            playersOnPlatform.Remove(playerController);
        }
    }
}
