using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGrid : MonoBehaviour
{

    private GameObject detailsGridObject;

    // Start is called before the first frame update

     private void Start()
    {
        detailsGridObject = GameObject.Find("Details_1");
       
    }

    public void OpenDetails()
    {
       
        detailsGridObject.SetActive(!detailsGridObject.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
