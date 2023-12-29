using UnityEngine;

public class FollowMe : MonoBehaviour
{
    public float followSpeed = 5f; // Velocità di movimento del pannello
    public float distanceToFront = 5f; // Distanza iniziale davanti alla telecamera

    private bool isFollowing = false;
    private Transform slateTransform; // Riferimento al trasform dell'oggetto Slate

    void Start()
    {
        // Trova l'oggetto Slate nel GameObject corrente
        slateTransform = GameObject.Find("Slate").transform;
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
        Vector3 direction = Camera.main.transform.position - slateTransform.position;
        direction.y = 0f; // Mantieni la stessa altezza
        direction.Normalize(); // Normalizza la direzione
        Vector3 targetPosition = Camera.main.transform.position - direction * distanceToFront;
        targetPosition.y = slateTransform.position.y; // Mantieni la stessa coordinata Y
        slateTransform.position = Vector3.MoveTowards(slateTransform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    public void ToggleFollow()
    {
        isFollowing = !isFollowing;
    }

   
    public void ChangeDistance(float newDistance)
    {
        distanceToFront = newDistance;
    }
}
