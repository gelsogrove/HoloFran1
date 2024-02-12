using TMPro;
using UnityEditor;
using UnityEngine;

public class MyClickHandler : MonoBehaviour
{
    public GameObject Cube;
    public GameObject CubeR;
    public GameObject Title;
    public GameObject Menu;
    public GameObject Container;
    public TextMeshPro debugTextMesh;
    public Transform controllerTransform;
    public float raycastDistance = 10f;

    private bool isMenuOpen = false;
    private bool isContainerOpen = false;

    void Update()
    {
        
            Vector3 directionToTitle = Title.transform.position - Camera.main.transform.position;
            Vector3 directionToMenu = Menu.transform.position - Camera.main.transform.position;

            Quaternion lookRotationTitle = Quaternion.LookRotation(directionToTitle, Vector3.up);
            Quaternion lookRotationMenu = Quaternion.LookRotation(directionToMenu, Vector3.up);

            Title.transform.rotation = lookRotationTitle;
            Menu.transform.rotation = lookRotationMenu;

            CubeR.transform.Rotate(Vector3.up, 90f * Time.deltaTime);
            CubeR.transform.position = Cube.transform.position;


            //Aggiorno canvas
            CubeR.transform.Rotate(Vector3.up, 90f * Time.deltaTime);
            CubeR.transform.position = Cube.transform.position;


            if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.RTouch) && (!isMenuOpen || !isContainerOpen  ))
            {
                Ray ray = new Ray(controllerTransform.position, controllerTransform.forward);
                RaycastHit hit;                                 


                if (Physics.Raycast(ray, out hit, raycastDistance))
                {
                    string objectHit = hit.collider.gameObject.name; // Ottieni il nome dell'oggetto colpito
                    debugTextMesh.text = "Object Hit: " + objectHit; // Imposta il testo con il nome dell'oggetto colpito


                        if (hit.collider.CompareTag("Cube"))
                        {
                            OpenMenu();
                            isMenuOpen = true;
                        }

                        if (objectHit == "Example1")
                        {                           
                            OpenContainer();
                            isContainerOpen = true;
                        }
                }
    
            }
            else if (OVRInput.GetUp(OVRInput.Button.Any, OVRInput.Controller.RTouch))
            {
                isMenuOpen = false;
                isContainerOpen = false;                            
            }
        
    }


   


    public void OpenMenu()
    {
        GameObject menu = GameObject.Find("SceneObjects/Case1/Cube/Canvas/Menu");

        if (menu != null)
        {
            bool isActive = menu.activeSelf;            

            menu.SetActive(!isActive);

            if (!isContainerOpen) {
                GameObject.Find("SceneObjects/Case1/Cube/Canvas/Container").SetActive(false);
            }

            debugTextMesh.text = "isContainerOpen " + isContainerOpen + "isMenuOpen" + isMenuOpen;
        }
    }

    public void OpenContainer()
    {
        GameObject container = GameObject.Find("SceneObjects/Case1/Cube/Canvas/Container");
        

        if (container != null)
        {
            bool isActive = container.activeSelf;            
            container?.SetActive(!isActive);
            debugTextMesh.text = "OpenContainer."  + !isActive;
        }
    }
}
