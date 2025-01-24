using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.TitleScreen
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("Scenes/HighStreet");
        }
    }
}
