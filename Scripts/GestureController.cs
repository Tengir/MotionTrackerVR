using UnityEngine;

public class GestureController : MonoBehaviour
{
    [SerializeField] private GestureRecorder recorder;
    
    // Какое имя жеста мы применим при сохранении
    private string currentGestureName = "default";

    void Update()
    {   
        // Проверяем нажатие цифр (1..9)
        for (int i = (int)KeyCode.Alpha1; i <= (int)KeyCode.Alpha9; i++)
        {
            KeyCode key = (KeyCode)i;
            if (Input.GetKeyDown(key))
            {
                // Например, 'Alpha1' -> '1', 'Alpha2' -> '2' и т.д.
                // Можно сделать map, но проще:
                char digit = key.ToString()[key.ToString().Length - 1]; // последний символ: '1','2',...
                currentGestureName = digit.ToString();
                Debug.Log($"Current gesture name set to: {currentGestureName}");
            }
        }

        // S - начать запись
        if (Input.GetKeyDown(KeyCode.S))
        {   
            Debug.Log($"Press S");
            if (recorder != null)
            {   
                Debug.Log($"Start recording");
                recorder.BeginGestureRecord();
            }
        }

        // C - закончить, сохранить жест
        if (Input.GetKeyDown(KeyCode.C))
        {   
            Debug.Log($"Press C");
            if (recorder != null)
            {
                var gesture = recorder.EndGestureRecord(currentGestureName);
                if (gesture != null)
                {
                    // Для примера сохраним в файл "Gesture_имя.bin" в корне проекта
                    // Либо можно дать свою папку.
                    
                    string path = Application.dataPath + $"/MyPlugin/Gesture_{gesture.gestureName}.bin";
                    recorder.SaveGestureBinary(gesture, path);
                    Debug.Log($"Create new gesture " + path);
                }
                else
                {
                    Debug.Log($"Gesture us null");
                }
            }
        }

        // R - распознать жест (пока заглушка)
        if (Input.GetKeyDown(KeyCode.R))
        {   
            Debug.Log($"Press R");
            Debug.Log("Gesture Recognition is not implemented yet.");
        }
    }
}
