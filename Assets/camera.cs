using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Start()
    {
        // Assicurati di avere il pacchetto di integrazione Oculus installato nel tuo progetto.
        OVRManager.boundary.SetVisible(true);
    }
}
