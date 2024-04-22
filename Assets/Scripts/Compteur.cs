using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Compteur : MonoBehaviour
{

    private Outline outline;
    public TextMesh textMesh; // Référence au composant TextMesh
    public GameObject choux;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Int32.Parse(textMesh.text)>9)
        {
            transform.position = new Vector3(-1.077f,9.627f,-0.129f);
        }
    }
}
