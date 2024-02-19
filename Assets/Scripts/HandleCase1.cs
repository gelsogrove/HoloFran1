using System.IO;
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
    private string commonPath = "SceneObjects/Case1/Cube1/Canvas/";
    private string menuSelected;

    void Start()
    {
        TurnOff(commonPath + "Menu", includeChildren: true);
    }

    void Update()
    {

        RotateTitle();

        RotateCube();

        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.RTouch) && (!isMenuOpen || !isContainerOpen))
        {
            Ray ray = new Ray(controllerTransform.position, controllerTransform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                string objectHit = hit.collider.gameObject.name;
                debugTextMesh.text = "CASE 1 > Object Hit: " + objectHit;

                if (objectHit == "Cube1")
                {
                    ToggleMenu();
                }
                else if (objectHit == "General" || objectHit == "Fibers")
                {
                    ToggleContainer(objectHit);
                }
                else if (this.menuSelected == "General" && objectHit == "DM" || objectHit == "NDF" || objectHit == "dNDF")
                {
                    ToggleGraph(objectHit);
                }

                else if (this.menuSelected == "Fibers" &&  objectHit == "ADF" || objectHit == "NDF" || objectHit == "dNDF" || objectHit == "uNDF")
                {
                    ToggleGraph(objectHit);
                }

            }
        }
        else if (OVRInput.GetUp(OVRInput.Button.Any, OVRInput.Controller.RTouch))
        {
            isMenuOpen = false;
            isContainerOpen = false;            
        }
    }

    private void RotateCube()
    {
        CubeR.transform.Rotate(Vector3.up, 90f * Time.deltaTime);
        CubeR.transform.position = Cube.transform.position;
    }

    private void Toggle(string path, bool includeChildren = false)
    {
        GameObject obj = GameObject.Find(path);

        if (obj != null)
        {
            bool isActive = obj.activeSelf;
            obj.SetActive(!isActive);

            if (includeChildren)
            {
                foreach (Transform child in obj.transform)
                {
                    child.gameObject.SetActive(!isActive);
                }
            }
        }
        else
        {
            Debug.LogError("Oggetto non trovato al percorso: " + path);
        }
    }

    public void TurnOff(string path, bool includeChildren = false)
    {
        GameObject obj = GameObject.Find(path);

        if (obj != null)
        {
            obj.SetActive(false);

            if (includeChildren)
            {
                foreach (Transform child in obj.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError("Oggetto non trovato al percorso: " + path);
        }
    }


    private void TurnOn(string path, bool includeChildren = false)
    {
        GameObject obj = GameObject.Find(path);

        if (obj != null)
        {
            obj.SetActive(true);

            if (includeChildren)
            {
                foreach (Transform child in obj.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            Debug.LogError("Oggetto non trovato al percorso: " + path);
        }
    }


    private void RotateTitle() {
        Vector3 directionToTitle = Title.transform.position - Camera.main.transform.position;
        Vector3 directionToMenu = Menu.transform.position - Camera.main.transform.position;
        Quaternion lookRotationTitle = Quaternion.LookRotation(directionToTitle, Vector3.up);
        Quaternion lookRotationMenu = Quaternion.LookRotation(directionToMenu, Vector3.up);

        Title.transform.rotation = lookRotationTitle;
        Menu.transform.rotation = lookRotationMenu;
    }

    public void ToggleMenu()
    {
        isMenuOpen = true;

        Toggle(commonPath + "Menu", includeChildren: true);
        TurnOff(commonPath + "Menu/Container", includeChildren: true);
        TurnOff(commonPath + "Menu/Graph", includeChildren: true);

    }

    public void ToggleGraph(string objectHit)
    {

        TurnOn(commonPath + "Menu/Graph/");
        TurnOff(commonPath + "Menu/Graph/" + this.menuSelected,  includeChildren: true);
        
        TurnOn(commonPath + "Menu/Graph/" + this.menuSelected );
        Toggle(commonPath + "Menu/Graph/" + this.menuSelected + "/" + objectHit);

        debugTextMesh.text = commonPath + "Menu/Graph/" + this.menuSelected + "/" + objectHit;
    }

    private void ToggleContainer(string objectHit)
    {
        isContainerOpen = true;
        

        if (objectHit == "General") {
            TurnOn(commonPath + "Menu/Container/");
            TurnOn(commonPath + "Menu/Container/General", includeChildren: true);
            TurnOff(commonPath + "Menu/Container/Fibers", includeChildren: true);
            this.menuSelected = "General";
        }

        if (objectHit == "Fibers") {
            TurnOn(commonPath + "Menu/Container/");
            TurnOn(commonPath + "Menu/Container/Fibers" , includeChildren: true);
            TurnOff(commonPath + "Menu/Container/General" , includeChildren: true);
            this.menuSelected = "Fibers";
        }
  
        TurnOff(commonPath + "Menu/Graph", includeChildren: true);           
             
    }
}
