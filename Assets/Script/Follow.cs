using UnityEngine;

public class FollowMe : MonoBehaviour
{
    public float followSpeed = 5f; // Velocità di movimento del pannello
    public float distanceToFront = 3f; // Distanza iniziale davanti alla telecamera
    public float horizontalFollowDistance;  // Distanza in altezza orizzontale
    public float lateralDistance;  // Distanza laterale
    private bool isFollowing = false;
    public Transform slateTransform; // Riferimento al trasform dell'oggetto Slate

    void Start()
    {
        slateTransform = slateTransform.transform;
    }

    void Update()
    {
        if (isFollowing && slateTransform != null)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        if (Camera.main != null)
        {
            // Posizione della telecamera
            Vector3 cameraPosition = Camera.main.transform.position;

            // Posizione dalla telecamera
            Vector3 targetPosition = cameraPosition + (Camera.main.transform.forward * distanceToFront) / 2;

            // Modifica l'asse Y della targetPosition in base a horizontalDistance
            targetPosition.y += horizontalFollowDistance;

            // Modifica l'asse X della targetPosition in base a lateralDistance
            targetPosition += Camera.main.transform.right * lateralDistance;

            // Situazione intermedia
            Vector3 intermediatePosition = Vector3.Lerp(slateTransform.position, targetPosition, Time.deltaTime * followSpeed);

            slateTransform.position = intermediatePosition;
        }
        else
        {
            Debug.LogError("Telecamera principale non trovata.");
        }
    }

    public void ToggleFollow()
    {
        isFollowing = !isFollowing;
    }

    public void StopFollow()
    {
        isFollowing = false;
    }
}
