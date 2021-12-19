using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string nextScene;

    void OnTriggerEnter2D (Collider2D col) {
        if (col.gameObject.tag == "Player") {
            SceneManager.LoadScene(nextScene);
        }
    }
}
