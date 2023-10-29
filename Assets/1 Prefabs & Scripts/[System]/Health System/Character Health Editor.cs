using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterHealth))]
public class CharacterHealthEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Отображение стандартного инспектора
        DrawDefaultInspector();

        // Получаем ссылку на объект CharacterHealth
        CharacterHealth characterHealth = (CharacterHealth)target;

        // Группировка кнопок в горизонтальную строку
        GUILayout.BeginHorizontal();

        // Кнопка для нанесения урона персонажу
        if (GUILayout.Button("Damage 10", GUILayout.ExpandWidth(true)))
        {
            characterHealth.healthSystem.Damage(10);
            Debug.Log("Damage 10. Now HP: " + characterHealth.healthSystem.GetHealth());
        }

        // Кнопка для восстановления здоровья персонажу
        if (GUILayout.Button("Heal 10", GUILayout.ExpandWidth(true)))
        {
            characterHealth.healthSystem.Heal(10);
            Debug.Log("Heal 10. Now HP: " + characterHealth.healthSystem.GetHealth());
        }

        // Завершаем горизонтальную группу
        GUILayout.EndHorizontal();
    }
}
