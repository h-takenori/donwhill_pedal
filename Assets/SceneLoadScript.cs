using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoadScript : MonoBehaviour {

    public void ClickStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("hoge" , LoadSceneMode.Single);
    }
}
