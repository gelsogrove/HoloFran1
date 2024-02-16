using UnityEngine;
using OculusSampleFramework;

public class DisableGrabWhenButtonPressed : MonoBehaviour
{
    private OVRGrabbable grabbable;
    public OVRInput.Button disableGrabButton = OVRInput.Button.One; // Puoi modificare questo valore per il pulsante desiderato

    void Start()
    {
        // Otteniamo il componente OVRGrabbable attaccato all'oggetto
        grabbable = GetComponent<OVRGrabbable>();

        // Verifichiamo che il componente OVRGrabbable sia stato trovato
        if (grabbable == null)
        {
            Debug.LogError("Componente OVRGrabbable non trovato sull'oggetto.");
            return;
        }
    }

    void Update()
    {
        // Verifica se il pulsante specificato è stato premuto
        if (OVRInput.Get(disableGrabButton))
        {
            // Disabilita la capacità di afferrare la sfera
            grabbable.enabled = false;
        }
        else if (!OVRInput.Get(disableGrabButton) && !grabbable.enabled)
        {
            // Riabilita la capacità di afferrare la sfera se il pulsante non è più premuto e la sfera è stata precedentemente disabilitata
            grabbable.enabled = true;
        }
    }
}
