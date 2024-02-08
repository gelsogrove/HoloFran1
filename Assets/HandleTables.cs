using UnityEditor;
using UnityEngine;

public class HandleTables : MonoBehaviour
{
    public GameObject Cube;
    public GameObject CubeR;
    public GameObject Title;
    public GameObject Menu;
    public GameObject Container;



    void Update()
    {
        // Assicurati che l'oggetto Title esista prima di procedere
        if (Title != null)
        {
            // Ottenere la direzione dalla camera dell'utente all'oggetto Title
            Vector3 directionToTitle = Title.transform.position - Camera.main.transform.position;
            Vector3 directionToMenu = Menu.transform.position - Camera.main.transform.position;
            Vector3 directionToContainer = Container.transform.position - Camera.main.transform.position;


            // Calcolare la rotazione necessaria per far puntare l'oggetto Title verso la camera
            Quaternion lookRotationTitle = Quaternion.LookRotation(directionToTitle, Vector3.up);
            Quaternion lookRotationMenu = Quaternion.LookRotation(directionToMenu, Vector3.up);
            


            Title.transform.rotation = lookRotationTitle;
            Menu.transform.rotation = lookRotationMenu;


            CubeR.transform.Rotate(Vector3.up, 90f * Time.deltaTime);

            CubeR.transform.position  = Cube.transform.position;


        }


        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            this.OpenContainer();
        }

       

    }

    public void OpenMenu()
    {
        Cube.transform.Rotate(Vector3.left, 360f);

        GameObject menu = GameObject.FindGameObjectWithTag("Menu");
        menu?.SetActive(true);
    }

    public void OpenContainer()
    {
        GameObject container = GameObject.FindGameObjectWithTag("Container");
        container?.SetActive(true);
    }
}
