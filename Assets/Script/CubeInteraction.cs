using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    private GameObject myObject;
    private GameObject slateObject;
    private GameObject cubeObject;
    private GameObject myDetailsObject;
    private float offsetDistance = 0.5f; // Offset di 0.5 metro

    private void Start()
    {
        myObject = GameObject.Find("Grid_1");
        myDetailsObject = GameObject.Find("Details_1");
        slateObject = GameObject.Find("Slate_1");
        cubeObject = GameObject.Find("Cube_1");

        // Chiamiamo il metodo DeactivateObject dopo 300 millisecondi (0.3 secondi)
        Invoke("DeactivateObject", 0.3f);
    }

    private void PerformCommonOperations(bool toggleMyObject)
    {
        if (myObject != null)
        {
            if (toggleMyObject)
            {
                myObject.SetActive(!myObject.activeSelf);
            }

            myDetailsObject.SetActive(false);

            // Otteniamo la posizione del cubo principale
            Vector3 mainCubePosition = cubeObject.transform.position;

            // Calcoliamo la posizione desiderata aggiungendo un offset di 1 metro a destra
            Vector3 desiredPosition = mainCubePosition + new Vector3(offsetDistance, 0f, 0f);

            // Impostiamo la rotazione desiderata
            Quaternion desiredRotation = Quaternion.identity;

            // Attiviamo un nuovo metodo per spostare l'oggetto dopo un breve ritardo
            MoveSlateWithDelay(desiredPosition, desiredRotation, 0.1f);
        }
        else
        {
            Debug.LogWarning("Oggetto non trovato.");
        }
    }

    private async void MoveSlateWithDelay(Vector3 position, Quaternion rotation, float delay)
    {
        await System.Threading.Tasks.Task.Delay((int)(delay * 1000));

        // Posizioniamo e ruotiamo l'oggetto nella posizione desiderata
        SetObjectTransform(slateObject, position, rotation);
    }

    private void DeactivateObject()
    {
        // Chiamiamo PerformCommonOperations con toggleMyObject a false
        PerformCommonOperations(false);
    }

    public void OnTouch()
    {
        // Chiamiamo PerformCommonOperations con toggleMyObject a true
        PerformCommonOperations(true);
    }

    // Metodo per impostare la posizione e la rotazione di un oggetto
    private void SetObjectTransform(GameObject obj, Vector3 position, Quaternion rotation)
    {
        if (obj != null)
        {
            obj.transform.position = position;
            obj.transform.rotation = rotation;
        }
        else
        {
            Debug.LogWarning("Oggetto non valido.");
        }
    }
}
