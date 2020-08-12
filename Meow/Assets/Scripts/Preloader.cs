using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Preloader : MonoBehaviour
{
    [Header("Прогресс загрузки")]
    public Text ProgressText;

    private void Start()
    {
        StartCoroutine(Preload());
    }

    private IEnumerator Preload()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(1);
        while (!load.isDone)
        {
            var progress = load.progress / 0.9f;
            GetComponent<Slider>().value = progress;
            if (progress <= 0.3f)
                ProgressText.color = Color.red;
            else
                if (progress <= 0.6f)
                ProgressText.color = Color.yellow;
            else
                if (progress <= 1f)
                ProgressText.color = Color.green;
            ProgressText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
        }
    }
}
