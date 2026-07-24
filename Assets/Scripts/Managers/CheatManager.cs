using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatManager : MonoBehaviour
{
    private string victoryScene = "VictoryMenu";

    void Update()
    {
        //Cheat que acaba o jogo e leva para o ecrã de vitória
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Time.timeScale = 1f; 
            SceneManager.LoadScene(victoryScene);
        }

        //Cheat que avança para o próximo nível, se for o último nível, leva para o ecrã de vitória
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Time.timeScale = 1f;
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            SceneManager.LoadScene(nextSceneIndex);

            
        }
    }
}