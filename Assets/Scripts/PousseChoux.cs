using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
public class PousseChoux : MonoBehaviour
{
    // Nouvelle échelle de l'objet
    private Vector3 newScale = new Vector3(1,1,1);
    
    // Durée de la transformation en secondes
    private float duration = 0f;

    // Temps écoulé depuis le début de la transformation
    private float elapsedTime = 0f;

    // Booléen pour vérifier si la transformation est en cours
    private bool isResizing = false;

    // Position de départ
    private Vector3 startScale;
    
    public int delais;

    private int nbrChoux;

    public TextMesh textMesh; // Référence au composant TextMesh
    private Outline outline;
    public GameObject personnage;

    // Start is called before the first frame update
    void Start()
    {
        Thread.Sleep(delais);
        // Créer une instance de Random
        System.Random random = new System.Random();
        int randomInt = random.Next(20, 100);
        duration = randomInt;
        transform.localScale = new Vector3(0,0,0);
        Resize();
    }

    // Update is called once per frame
    void Update()
    {
        // Si la transformation est en cours
        if (isResizing)
        {
            // Incrémenter le temps écoulé
            elapsedTime += Time.deltaTime;

            // Calculer le pourcentage d'avancement de la transformation
            float t = Mathf.Clamp01(elapsedTime / duration);

            // Interpolation linéaire entre la position de départ et la nouvelle position
            transform.localScale = Vector3.Lerp(startScale, newScale, t);

            // Si la transformation est terminée
            if (t >= 1.0f)
            {
                isResizing = false; // Désactiver la transformation
            }
        }else
        {
            outline = gameObject.GetComponent<Outline>();
            outline.enabled = true;
        }
        
    }

    // Déclenché lorsque le bouton de la souris est enfoncé sur le collider de l'objet
    void OnMouseDown()
    {
        if(!isResizing){
            float distance = Vector3.Distance(personnage.transform.position, gameObject.transform.position);
            // Agrandir l'objet lorsqu'il est cliqué
            outline.enabled = false;
            transform.localScale = new Vector3(0,0,0);
            nbrChoux = Int32.Parse(textMesh.text) + 1;
            textMesh.text = nbrChoux.ToString();
            isResizing = true;
            elapsedTime = 0f;
        }
    }

    // Fonction pour changer la taille de l'objet
    public void Resize()
    {
        // Définir la position de départ
        startScale = transform.localScale;
        // Réinitialiser le temps écoulé
        elapsedTime = 0f;
        // Activer la transformation
        isResizing = true;
    }
}
