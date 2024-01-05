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
        if (backPlateRawImage.color == Color.red) {
            backPlateRawImage.color =   new Color(151f / 255f, 216f / 255f, 1f, 51f / 255f);
        }else {
            backPlateRawImage.color = Color.red;
        }
        backPlateRawImage.SetMaterialDirty(); 
    }



    

    public void OpenGraphToggle()
    {
        graphGriddObject.SetActive(!graphGriddObject.activeSelf);
    }


    public void OnTouch()
    {
        if (containerObject != null && cubeObject != null)
        {
          
            // Cube position
            Vector3 mainCubePosition = cubeObject.transform.position;
    
            // New position
            Vector3 desiredPosition = mainCubePosition + new Vector3(0.7f, -0.2f, 0f);
            Quaternion desiredRotation = Quaternion.identity;

            // Set values
            containerObject.transform.position = desiredPosition;
            containerObject.transform.rotation = desiredRotation;

            // Hide - Show
            containerObject.SetActive(!containerObject.activeSelf);
            detailsGridObject.SetActive(false);
            graphGriddObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Oggetto non trovato.");
        }
    }




    public void CloseMainSlate() 
    {
        containerObject.SetActive(false);
        detailsGridObject.SetActive(false);
        graphGriddObject.SetActive(false);
    }

    public void CloseDetailsSlate() 
    {    
        detailsGridObject.SetActive(false);
        graphGriddObject.SetActive(false);
    }

    public void CloseGraphSlate()
    {
        graphGriddObject.SetActive(false);
    }

}
