using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    // Nome dell'oggetto da nascondere o mostrare
    public string objectName = "GridButtons_1";

    private GameObject myObject;

    private void Start()
    {
        myObject = GameObject.Find(objectName);
    }

  
    public void OnTouch()
    {
     
        if (myObject != null)
        {
            myObject.SetActive(!myObject.activeSelf);
        }
        else
        {
            Debug.LogWarning("Oggetto non valido.");
        }
    }
}
