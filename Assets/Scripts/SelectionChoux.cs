using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectionChoux : MonoBehaviour
{
    private Outline outline;
    public TextMesh textMesh; // Référence au composant TextMesh
    public Camera selectionCamera;
    public GameObject cheval;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Detect mouse right-click
        if (Input.GetMouseButtonDown(0))
        {
            // Convertir la position du clic de la souris en rayon dans la scène
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Effectuer un raycast pour détecter les collisions avec les objets
            if (Physics.Raycast(ray, out hit))
            {
                // Vérifier si l'objet cliqué est différent de celui attaché à ce script
                if (hit.collider.gameObject != gameObject && hit.collider.gameObject != cheval)
                {
                    // Le clic a eu lieu en dehors de l'objet attaché à ce script
                    
                    outline.enabled = false;
                }
            }
        }
        if (Int32.Parse(textMesh.text)<1)
        {
            outline = gameObject.GetComponent<Outline>();
            outline.enabled = false;
        }
    }

    void OnMouseDown()
    {
    
        if (Int32.Parse(textMesh.text)>0)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }

    
}
