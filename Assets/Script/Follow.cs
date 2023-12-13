using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform telecamera; // Assegna la telecamera dall'Editor Unity

    void Start()
    {
        // Controlla se la telecamera è assegnata
        if (telecamera != null)
        {
            // Rendi l'oggetto un figlio della telecamera
            transform.parent = telecamera;
        }
    }
}
