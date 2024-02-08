using TMPro;
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

    void Update()
    {
        if (Title != null)
        {
            Vector3 directionToTitle = Title.transform.position - Camera.main.transform.position;
            Vector3 directionToMenu = Menu.transform.position - Camera.main.transform.position;

            Quaternion lookRotationTitle = Quaternion.LookRotation(directionToTitle, Vector3.up);
            Quaternion lookRotationMenu = Quaternion.LookRotation(directionToMenu, Vector3.up);

            Title.transform.rotation = lookRotationTitle;
            Menu.transform.rotation = lookRotationMenu;

            CubeR.transform.Rotate(Vector3.up, 90f * Time.deltaTime);
            CubeR.transform.position = Cube.transform.position;

            if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.RTouch) && !isMenuOpen)
            {
                Ray ray = new Ray(controllerTransform.position, controllerTransform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, raycastDistance))
                {
                    if (hit.collider.CompareTag("Cube"))
                    {
                        OpenMenu();
                        isMenuOpen = true;
                    }
                }
            }
            else if (OVRInput.GetUp(OVRInput.Button.Any, OVRInput.Controller.RTouch))
            {
                isMenuOpen = false;
            }
        }
    }

    public void OpenMenu()
    {
        GameObject menu = GameObject.Find("SceneObjects/Case1/Cube/Canvas/Menu");

        if (menu != null)
        {
            bool isActive = menu.activeSelf;
            menu.SetActive(!isActive);

            debugTextMesh.text = "OpenMenu " + !isActive;
        }
    }

    public void OpenContainer()
    {
        GameObject container = GameObject.FindGameObjectWithTag("Container");
        container?.SetActive(true);
    }
}
