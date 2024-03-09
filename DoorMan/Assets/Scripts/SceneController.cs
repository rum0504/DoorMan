using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotoMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void GotoTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void RestartMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
