using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool isClicked = false;
    private bool isGrabbed = false;

    private void Update()
    {
        // Controlla se è stato cliccato il pulsante sinistro del mouse o il trigger del controller destro
        if (Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (!isClicked)
            {
                Debug.Log("L'oggetto è stato cliccato: " + gameObject.name);
                isClicked = true;
            }
        }

        // Controlla se il grab è attivo sul controller destro
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            if (!isGrabbed)
            {
                Debug.Log("Hai afferrato l'oggetto: " + gameObject.name);
                isGrabbed = true;
            }
        }
        else
        {
            if (isGrabbed)
            {
                Debug.Log("Hai rilasciato l'oggetto: " + gameObject.name);
                isGrabbed = false;
            }
        }
    }
}
