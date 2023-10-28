using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UQuit()
    {
        Application.Quit();
    }

    public void UPlay()
    {
        SceneManager.LoadScene("other scene");
    }
}
