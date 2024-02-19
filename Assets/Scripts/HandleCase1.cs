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
    private string caseNumber = "Case1";
    private string menuSelected = "";

    void Start()
    {
        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu");
        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Container/");        
        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Graph");
    }

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
    

            if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.RTouch) && (!isMenuOpen || !isContainerOpen  ))
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
                    else if (objectHit == "DM" || objectHit == "NDF"  || objectHit == "dNDF")
                    {
                        ToggleGraph(objectHit);                    
                    }

            }

        }
            else if (OVRInput.GetUp(OVRInput.Button.Any, OVRInput.Controller.RTouch))
            {
                this.isMenuOpen = false;
                this.isContainerOpen = false;
                this.menuSelected = null;
        }
        
    }


    private void ToggleContainer(string objectHit) {

        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Container/");
        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Graph/General");
        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Graph/General/DM");
        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Graph/General/NDF");
        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Graph/General/dNDF");

        if (objectHit == "General")
        {
            this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Container/Fibers");
            this.TurnOn("SceneObjects/Case1/Cube1/Canvas/Menu/Container/");
            this.TurnOn("SceneObjects/Case1/Cube1/Canvas/Menu/Container/General");
            
        }

        if (objectHit == "Fibers")
        {
            this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Container/General");                        
            this.TurnOn("SceneObjects/Case1/Cube1/Canvas/Menu/Container/");
            this.TurnOn("SceneObjects/Case1/Cube1/Canvas/Menu/Container/Fibers");            
        }


        isContainerOpen = true;
        this.menuSelected = objectHit;

    }

    private void Toggle(string path)
    {    
        GameObject obj = GameObject.Find(path);
     
        if (obj != null)
        {
            bool isActive = obj.activeSelf;
            obj.SetActive(!isActive);
        }
        else
        {
            Debug.LogError("Oggetto non trovato al percorso: " + path);
        }
    }


    public void TurnOff(string path)
    {        
        GameObject obj = GameObject.Find(path);
        
        if (obj != null)
        {                       
            obj.SetActive(false);
        }
        else
        {
            Debug.LogError("Oggetto non trovato al percorso: " + path);
        }
    }


    private void TurnOn(string path)
    {
        GameObject obj = GameObject.Find(path);

        if (obj != null)
        {

            obj.SetActive(true);
        }
        else
        {
            Debug.LogError("Oggetto non trovato al percorso: " + path);
        }
    }



    private void TurnOffRecorsive(string path)
    {
        GameObject obj = GameObject.Find(path);

        if (obj != null)
        {
            obj.SetActive(false);
            
            foreach (Transform child in obj.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("Oggetto non trovato al percorso: " + path);
        }
    }

    public void ToggleMenu()
    {       
        this.isMenuOpen = true;

        Toggle("SceneObjects/Case1/Cube1/Canvas/Menu/");

        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Container/");
        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Container/General");
        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Container/Fibers");

        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Graph");
        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Graph/General/DM");
        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Graph/General/NDF");
        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Graph/General/dNDF");

    }


    public void ToggleGraph(string objectHit)
    {


        debugTextMesh.text = "CASE 1 > Object Hit > ToggleGraph " + objectHit;


        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Graph/General");
        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Graph/General/DM");
        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Graph/General/NDF");
        this.TurnOff("SceneObjects/Case1/Cube1/Canvas/Menu/Graph/General/dNDF");         

        this.TurnOn("SceneObjects/Case1/Cube1/Canvas/Menu/Graph/");
        this.TurnOn("SceneObjects/Case1/Cube1/Canvas/Menu/Graph/General/");
        this.TurnOn("SceneObjects/Case1/Cube1/Canvas/Menu/Graph/General/" + objectHit);


    }


}
