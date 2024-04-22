using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NewBehaviourScript : MonoBehaviour
{
    
    
    // Coordonnées de destination
    public Vector3 targetPosition;

    // Vitesse de déplacement
    public float moveSpeed = 1f;

    // Booléen pour vérifier si l'objet doit se déplacer
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 allerA = new Vector3(9f,7.7f,0f);
        MoveTo(allerA);
    }


    void Update()
    {
        // Si isMoving est vrai, on déplace l'objet vers la position cible
        if (isMoving)
        {
            // Calcul de la direction vers la position cible
            Vector3 direction = targetPosition - transform.position;
            // Normalisation de la direction pour obtenir une vitesse constante
            direction.Normalize();
            // Déplacement de l'objet dans la direction spécifiée avec la vitesse spécifiée
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // Si l'objet est suffisamment proche de la position cible, on arrête le déplacement
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    // Fonction pour déplacer l'objet vers une nouvelle position
    public void MoveTo(Vector3 newPosition)
    {
        // Définir la nouvelle position cible
        targetPosition = newPosition;
        // Activer le déplacement de l'objet
        isMoving = true;
    }
}
