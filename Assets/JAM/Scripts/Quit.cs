#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class Quit : MonoBehaviour
{
    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Close the game
            CloseGame();
        }
    }

    private void CloseGame()
    {
#if UNITY_EDITOR
        // If in the Unity Editor, stop play mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // For standalone builds or other platforms, use Application.Quit()
        Application.Quit();
#endif
    }
}