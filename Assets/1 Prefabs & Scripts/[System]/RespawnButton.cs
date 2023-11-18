using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartSceneButton : MonoBehaviour
{
    private Button restartButton;

    private void Start()
    {
        // Находим компонент Button на кнопке
        restartButton = GetComponent<Button>();

        // Добавляем обработчик события нажатия на кнопку
        restartButton.onClick.AddListener(RestartScene);
    }

    public void RestartScene()
    {
        // Получаем имя текущей сцены
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Перезагружаем текущую сцену
        SceneManager.LoadScene(currentSceneName);
    }
}
