using TMPro;
using UnityEditor;
using UnityEngine;

public class HandleCase1 : MonoBehaviour
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
    private string caseNumber = "Case1";
    private string menuSelected = "";

    void Update()
    {
        
            Vector3 directionToTitle = Title.transform.position - Camera.main.transform.position;
            Vector3 directionToMenu = Menu.transform.position - Camera.main.transform.position;
            Vector3 directionToContainer = Container.transform.position - Camera.main.transform.position;

            Quaternion lookRotationTitle = Quaternion.LookRotation(directionToTitle, Vector3.up);
            Quaternion lookRotationMenu = Quaternion.LookRotation(directionToMenu, Vector3.up);
            //Quaternion lookRotationContainer = Quaternion.LookRotation(directionToContainer, Vector3.up);

                
            Title.transform.rotation = lookRotationTitle;
            Menu.transform.rotation = lookRotationMenu;
            //Container.transform.rotation = lookRotationContainer;

            CubeR.transform.Rotate(Vector3.up, 90f * Time.deltaTime);
            CubeR.transform.position = Cube.transform.position;



    

            if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.RTouch) && (!isMenuOpen || !isContainerOpen  ))
            {
                Ray ray = new Ray(controllerTransform.position, controllerTransform.forward);
                RaycastHit hit;                                 


                if (Physics.Raycast(ray, out hit, raycastDistance))
                {
                    string objectHit = hit.collider.gameObject.name; // Ottieni il nome dell'oggetto colpito
                  

                        if (hit.collider.CompareTag("Cube1"))
                        {
                            debugTextMesh.text = "CASE 1 > OpenMenu 1";
                            OpenMenu();
                            isMenuOpen = true;
                        }

                        if (objectHit == "General")
                        {                           
                            OpenContainer();
                            isContainerOpen = true;
                            this.menuSelected = "General";
                        }

                        if (objectHit == "DM" && this.menuSelected == "General")
                        {
                         Toggle("SceneObjects/Case1/Cube1/Canvas/Menu/Graph/General/DM"); 
                        }
            }
    
            }
            else if (OVRInput.GetUp(OVRInput.Button.Any, OVRInput.Controller.RTouch))
            {
                isMenuOpen = false;
                isContainerOpen = false;                            
            }
        
    }



    public void Toggle(string path)
    {
        // Trova l'oggetto corrispondente al percorso specificato
        GameObject obj = GameObject.Find(path);

        // Verifica se l'oggetto è stato trovato
        if (obj != null)
        {
            // Ottieni lo stato attuale dell'oggetto
            bool isActive = obj.activeSelf;

            // Cambia lo stato dell'oggetto (attivo o disattivo)
            obj.SetActive(!isActive);
        }
        else
        {
            Debug.LogError("Oggetto non trovato al percorso: " + path);
        }
    }


    public void OpenMenu()
    {
        GameObject menu = GameObject.Find("SceneObjects/Case1/Cube1/Canvas/Menu");        

        if (menu != null)
        {

            bool isActive = menu.activeSelf;            

            menu.SetActive(!isActive);

            if (!isContainerOpen) {
                GameObject.Find("SceneObjects/Case1/Cube1/Canvas/Menu/Container").SetActive(false);
            }

            
        }else {
            debugTextMesh.text = "CASE 1 > OpenMenu 1 > menu KO";
        }
    }

    public void OpenContainer()
    {
        GameObject container = GameObject.Find("SceneObjects/Case1/Cube1/Canvas/Menu/Container");
        

        if (container != null)
        {
            bool isActive = container.activeSelf;            
            container?.SetActive(!isActive);       
        }
    }
}
