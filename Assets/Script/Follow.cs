using UnityEngine;

public class FollowMe : MonoBehaviour
{
    public float followSpeed = 5f; // Velocità di movimento del pannello
    public float distanceToFront = 3f; // Distanza iniziale davanti alla telecamera

    private bool isFollowing = false;
    private Transform slateTransform; // Riferimento al trasform dell'oggetto Slate

    void Start()
    {
        // Trova l'oggetto Slate nel GameObject corrente
        slateTransform = GameObject.Find("Slate_1").transform;
        if (slateTransform == null)
        {
            Debug.LogError("Oggetto Slate non trovato. Assicurati che il nome sia corretto.");
        }
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
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 targetPosition = cameraPosition + Camera.main.transform.forward * distanceToFront;

            // Calcoliamo il punto medio tra la posizione corrente e la posizione desiderata
            Vector3 intermediatePosition = Vector3.Lerp(slateTransform.position, targetPosition, Time.deltaTime * followSpeed);

            // Aggiorniamo la posizione dell'oggetto "slate"
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




    public void ChangeDistance(float newDistance)
    {
        distanceToFront = newDistance;
    }
}
