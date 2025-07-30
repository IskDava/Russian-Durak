using UnityEngine;
using UnityEngine.SceneManagement;
public class ResetButton : MonoBehaviour
{
    public void OnMouseDown()
    {
        Player.players = new();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
