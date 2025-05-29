using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private float elapsedTime;
    private bool isTiming = true;
     void Start()
    {
        timerText.text = "PIZDEC";
    }

    void Update()
    {
        if (!isTiming) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        isTiming = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        isTiming = true;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}


