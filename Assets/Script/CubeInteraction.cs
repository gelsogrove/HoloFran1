using UnityEngine;
using UnityEngine.UI;


public class CubeInteraction : MonoBehaviour
{
    public GameObject cubeObject;
    public GameObject containerObject;
    public GameObject detailsGridObject;
    public GameObject graphGriddObject;

     
    private void Start()
    {
           containerObject.SetActive(false);
           graphGriddObject.SetActive(false);
    }

  public void OpenDetails(GameObject backPlate)
    {
        detailsGridObject.SetActive(!detailsGridObject.activeSelf);

        RawImage backPlateRawImage = backPlate.GetComponent<RawImage>();

        if (backPlateRawImage != null)
        {
            Debug.Log("RawImage trovata su: " + backPlate.name);
            
            // Imposta il colore con valori RGB convertiti
            if (backPlateRawImage.color == Color.red) {
                    backPlateRawImage.color =   new Color(151f / 255f, 216f / 255f, 1f, 51f / 255f);

            }else {
                backPlateRawImage.color = Color.red;
            }
            


            backPlateRawImage.SetMaterialDirty(); 
        }
        else
        {
            Debug.LogError("RawImage non trovata su: " + backPlate.name);
        }
    }



    

    public void OpenGraphToggle()
    {
        graphGriddObject.SetActive(!graphGriddObject.activeSelf);
    }
    


    public void OnTouch()
    {
      if (containerObject != null)
        {
            
            containerObject.SetActive(!containerObject.activeSelf);
            
            detailsGridObject.SetActive(false);
            graphGriddObject.SetActive(false);

            // Otteniamo la posizione del cubo principale
            Vector3 mainCubePosition = cubeObject.transform.position;

            // Calcoliamo la posizione desiderata  
            Vector3 desiredPosition = mainCubePosition + new Vector3(0.7f , -0.2f, 0f);

            // Impostiamo la rotazione desiderata
            Quaternion desiredRotation = Quaternion.identity;

            // Attiviamo un nuovo metodo per spostare l'oggetto dopo un breve ritardo
            SetContainerSolution(containerObject, desiredPosition, desiredRotation);
            
        }
        else
        {
            Debug.LogWarning("Oggetto non trovato.");
        }

    }

    private void SetContainerSolution(GameObject obj, Vector3 position, Quaternion rotation)
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

    public void CloseMainSlate() {
        containerObject.SetActive(false);
        detailsGridObject.SetActive(false);
        graphGriddObject.SetActive(false);
    }

    public void CloseDetailsSlate() {
        
        detailsGridObject.SetActive(false);
        graphGriddObject.SetActive(false);
    }

    public void CloseGraphSlate()
{
    // Assicurati che graphGriddObject sia stato inizializzato correttamente
    if (graphGriddObject != null)
    {
        graphGriddObject.SetActive(false);
    }
    else
    {
        Debug.LogWarning("graphGriddObject non valido.");
    }
}

}
