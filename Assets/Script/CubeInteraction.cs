using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    // Nome dell'oggetto da nascondere o mostrare
    public string objectName = "GridButtons_1";

    // Riferimento al renderer dell'oggetto
    private Renderer myObjectRenderer;

    // Flag per tenere traccia dello stato di visibilità
    private bool isVisible = true;

    private void Start()
    {
        // Ottieni il renderer dell'oggetto specificato
        GameObject myObject = GameObject.Find(objectName);

        if (myObject != null)
        {
            // Cerca sia il MeshRenderer che lo SkinnedMeshRenderer
            myObjectRenderer = myObject.GetComponent<MeshRenderer>();

            if (myObjectRenderer == null)
            {
                // Se MeshRenderer non è presente, cerca SkinnedMeshRenderer
                myObjectRenderer = myObject.GetComponent<SkinnedMeshRenderer>();
            }

            // Verifica se almeno uno dei due è presente
            if (myObjectRenderer == null)
            {
                Debug.LogError("Componente Renderer non trovato sull'oggetto.");
            }
        }
        else
        {
            Debug.LogError("Oggetto non trovato con il nome specificato.");
        }
    }

    // Metodo chiamato quando l'oggetto viene toccato
    public void OnTouch()
    {
        Debug.Log("Tocco!");

        // Verifica se il renderer è valido
        if (myObjectRenderer != null)
        {
            // Inverti la visibilità dell'oggetto
            ToggleVisibility();
        }
        else
        {
            Debug.LogWarning("Componente Renderer non trovato sull'oggetto.");
        }
    }

    // Metodo per invertire la visibilità dell'oggetto
    private void ToggleVisibility()
    {
        // Inverti lo stato di visibilità
        isVisible = !isVisible;

        // Imposta la visibilità attraverso la componente Renderer
        myObjectRenderer.enabled = isVisible;
    }
}
