using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enterdoor : MonoBehaviour

{
    private bool enterAllowed;
    private string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<door1>())
        {
            sceneToLoad = "Lv.2";
            enterAllowed = true;
        }
        else if (collision.GetComponent<door2>())
            {
                sceneToLoad = "win";
                enterAllowed = true;
            }


    }
    private void OnTriggerExit2D(Collider2D collision)
        {
            enterAllowed = false;
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (enterAllowed)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    
}