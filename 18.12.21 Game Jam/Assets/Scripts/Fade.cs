using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public GameObject CanvasObject;

    private float alphaLevel;

    private string sceneName;

    void Start () {
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanvasObject.active == true) {
            alphaLevel += 0.001f;
            GetComponent<Image>().color = new Color (0f, 0f, 0f, alphaLevel);
        }
        if (alphaLevel >= 1f) {
            SceneManager.LoadScene(sceneName);
        }
    }
}
