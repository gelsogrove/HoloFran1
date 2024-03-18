using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class HandleCase2 : MonoBehaviour
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
    private string commonPath = "SceneObjects/Case2/Cube2/Canvas/";
    private string menuSelected;

    void Start()
    {
        TurnOff(commonPath + "Menu", includeChildren: true);
    }

    void Update()
    {

        RotateTitle();

        RotateCube();

        Rollover();

        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.RTouch) && (!isMenuOpen || !isContainerOpen))
        {
            Ray ray = new Ray(controllerTransform.position, controllerTransform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                string objectHit = hit.collider.gameObject.name;
                debugTextMesh.text = "CASE 2 > Object Hit: " + objectHit;

                if (objectHit == "Cube2")
                {
                    ToggleMenu();
                }
                else if (objectHit == "General2" || objectHit == "Fibers2" || objectHit == "Proteins")
                {
                    ToggleContainer(objectHit);
                }
                else if (this.menuSelected == "General2" && objectHit == "DM2" || objectHit == "NDF2" || objectHit == "dNDF2")
                {
                    ToggleGraph(objectHit);
                }

                else if (this.menuSelected == "Fibers2" && objectHit == "ADF2" || objectHit == "NDF2" || objectHit == "dNDF2" || objectHit == "uNDF2")
                {
                    ToggleGraph(objectHit);
                }

                else if (this.menuSelected == "Proteins" && objectHit == "CP2" || objectHit == "SP2" || objectHit == "ADIC_CP" )
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

    private void Rollover()
    {


    }

    private void RotateTitle()
    {
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
        TurnOff(commonPath + "Container", includeChildren: true);
        TurnOff(commonPath + "Graph", includeChildren: true);

    }

    public void ToggleGraph(string objectHit)
    {

        TurnOn(commonPath + "Graph/");
        TurnOff(commonPath + "Graph/" + this.menuSelected, includeChildren: true);

        TurnOn(commonPath + "Graph/" + this.menuSelected);
        Toggle(commonPath + "Graph/" + this.menuSelected + "/" + objectHit);

    }

    private void ToggleContainer(string objectHit)
    {
        isContainerOpen = true;

        TurnOn(commonPath + "Container/");

        if (objectHit == "General2")
        {

            TurnOn(commonPath + "Container/General2", includeChildren: true);
            TurnOff(commonPath + "Container/Fibers2", includeChildren: true);
            TurnOff(commonPath + "Container/Proteins", includeChildren: true);
            this.menuSelected = "General2";
        }

        if (objectHit == "Fibers2")
        {

            TurnOn(commonPath + "Container/Fibers2", includeChildren: true);
            TurnOff(commonPath + "Container/General2", includeChildren: true);
            TurnOff(commonPath + "Container/Proteins", includeChildren: true);
            this.menuSelected = "Fibers";
        }

        if (objectHit == "Proteins")
        {

            TurnOn(commonPath + "Container/Proteins", includeChildren: true);
            TurnOff(commonPath + "Container/Fibers2", includeChildren: true);
            TurnOff(commonPath + "Container/General2", includeChildren: true);
            this.menuSelected = "Proteins";
        }


        TurnOff(commonPath + "Graph", includeChildren: true);

    }
}
