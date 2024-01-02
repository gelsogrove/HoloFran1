using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    public GameObject cubeObject;
    public GameObject gridObject;
    public GameObject slateObject;
    public GameObject detailsGridObject;

    private float offsetDistance = 0.5f;  
    private void Start()
    {
           gridObject.SetActive(false);
    }



    public void OpenDetails()
    {

        detailsGridObject.SetActive(!detailsGridObject.activeSelf);
    }
    
 

    private async void MoveSlateWithDelay(Vector3 position, Quaternion rotation, float delay)
    {
        await System.Threading.Tasks.Task.Delay((int)(delay * 1000));

        // Posizioniamo e ruotiamo l'oggetto nella posizione desiderata
        SetObjectTransform(slateObject, position, rotation);
    }


    public void OnTouch()
    {
      if (gridObject != null)
        {
            
            gridObject.SetActive(!gridObject.activeSelf);
            

            detailsGridObject.SetActive(false);

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
