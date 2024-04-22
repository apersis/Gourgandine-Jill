using UnityEngine;

public class Touche : MonoBehaviour
{
    public Camera mainCamera;
    public Camera rideCamera;
    public GameObject chevalPrincipal;
    public GameObject chevalCourse;

    private bool barrierActive = false;

    void Start()
    {
        // Désactiver la caméra "ride" au démarrage
        rideCamera.gameObject.SetActive(false);
    }

    void Update()
    {

        // Changer de caméra en fonction de la valeur de barrierActive
        if (barrierActive)
        {
        // Activer la caméra "ride" et désactiver la caméra principale
            mainCamera.gameObject.SetActive(false);
            rideCamera.gameObject.SetActive(true);
            chevalPrincipal.transform.localScale = new Vector3(0,0,0);
            chevalCourse.transform.localScale = new Vector3(0.7f,0.7f,0.7f);
        }
        else
        {
        // Activer la caméra principale et désactiver la caméra "ride"
            mainCamera.gameObject.SetActive(true);
            rideCamera.gameObject.SetActive(false);
            chevalPrincipal.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
            chevalCourse.transform.localScale = new Vector3(0,0,0);
        }
    }

    void OnMouseDown()
    {
        if (barrierActive == false)
        {
            barrierActive = true;
        }
        else
        {
            barrierActive = false;
        }
    }
}