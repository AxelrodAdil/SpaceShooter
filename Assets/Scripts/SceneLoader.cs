using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
