using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [Header("Настроения")]
    public GameObject Mood_sad;
    public GameObject Mood_good;
    public GameObject Mood_happy;

    [Header("Шкала настроения")]
    public Slider MoodBar;

    [Header("Кнопки действий")]
    public Button[] Buttons;

    [Header("Эффекты")]
    public GameObject Particles;

    [Header("Реакции")]
    public Text Reactions;


    private string _action;
    private string _reaction;
    private string _mood;
    private string _newMood;
    private bool _moodChanged;

    private void MakeReaction()
    {
        Reactions.rectTransform.sizeDelta = new Vector2(Reactions.rectTransform.rect.width, Reactions.rectTransform.rect.height + 500);
        Reactions.text += "\n" + "------------------" + "\n" + _action + " (настроение: " + _mood + ") -> " + _reaction;
        if (_moodChanged)
        {
            for (int i = 0; i < 4; i++)
                Buttons[i].interactable = false;
            _mood = _newMood;
            ChangeMood();
        }
    }
    private void ChangeMood()
    {
        Particles.SetActive(false);
        var position = new Vector2();
        if (_mood == "плохое")
        {
            Mood_good.transform.GetChild(1).gameObject.SetActive(true);
            Mood_good.transform.GetChild(0).gameObject.SetActive(false);
            Mood_happy.transform.GetChild(1).gameObject.SetActive(true);
            Mood_happy.transform.GetChild(0).gameObject.SetActive(false);

            Mood_sad.transform.GetChild(1).gameObject.SetActive(false);
            Mood_sad.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(ChangeMoodBar(0));
            position = new Vector2(-317, 0);
        }
        if (_mood == "хорошее")
        {
            Mood_sad.transform.GetChild(1).gameObject.SetActive(true);
            Mood_sad.transform.GetChild(0).gameObject.SetActive(false);
            Mood_happy.transform.GetChild(1).gameObject.SetActive(true);
            Mood_happy.transform.GetChild(0).gameObject.SetActive(false);

            Mood_good.transform.GetChild(1).gameObject.SetActive(false);
            Mood_good.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(ChangeMoodBar(0.5f));
            position = new Vector2(0, 0);
        }
        if (_mood == "отличное")
        {
            Mood_sad.transform.GetChild(1).gameObject.SetActive(true);
            Mood_sad.transform.GetChild(0).gameObject.SetActive(false);
            Mood_good.transform.GetChild(1).gameObject.SetActive(true);
            Mood_good.transform.GetChild(0).gameObject.SetActive(false);

            Mood_happy.transform.GetChild(1).gameObject.SetActive(false);
            Mood_happy.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(ChangeMoodBar(1f));
            position = new Vector2(317, 0);
        }
        Particles.GetComponent<RectTransform>().localPosition = position;
        Particles.SetActive(true);
        _moodChanged = false;
    }

    private IEnumerator ChangeMoodBar(float value)
    {
        Color handleColor = MoodBar.transform.GetChild(2).GetChild(0).GetComponent<Image>().color;
        Color fillAreaColor = MoodBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().color;
        if (value == 0)
        {
            fillAreaColor = Color.red;
            handleColor = Color.red;
        }
        if (value == 0.5f)
        {
            fillAreaColor = Color.yellow;
            handleColor = Color.yellow;
        }
        if (value == 1f)
        {
            fillAreaColor = Color.green;
            handleColor = Color.green;
        }
        MoodBar.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = handleColor;
        MoodBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = fillAreaColor;
        if (MoodBar.value < value)
            while (MoodBar.value < value)
            {
                MoodBar.value += 0.01f;
                yield return new WaitForFixedUpdate();
            }
        else
            while (MoodBar.value > value)
            {
                MoodBar.value -= 0.01f;
                yield return new WaitForFixedUpdate();
            }
        MoodBar.value = value;
        for (int i = 0; i < 4; i++)
            Buttons[i].interactable = true;
    }

    private void Start()
    {
        _mood = "хорошее";
    }


    //----------------------//
    //       BUTTONS        //
    //----------------------//
    public void Play()
    {
        _action = "Поиграть";
        if (_mood == "плохое")
        {
            _reaction = "сидит на месте.";
        }
        else
            if (_mood == "хорошее")
        {
            _reaction = "медленно бегает за мячиком.";
            _newMood = "отличное";
            _moodChanged = true;
        }
        else
            if (_mood == "отличное")
        {
            _reaction = "носится как угорелая.";
        }
        MakeReaction();
    }
    public void Feed()
    {
        _action = "Накормить";
        if (_mood == "плохое")
        {
            _reaction = "все съедает, но если в это время подойти - поцарапает.";
            _newMood = "хорошее";
            _moodChanged = true;
        }
        else
            if (_mood == "хорошее")
        {
            _reaction = "быстро все съедает.";
            _newMood = "отличное";
            _moodChanged = true;
        }
        else
            if (_mood == "отличное")
        {
            _reaction = "быстро все съедает.";
        }
        MakeReaction();
    }
    public void Pet()
    {
        _action = "Погладить";
        if (_mood == "плохое")
        {
            _reaction = "царапает.";
        }
        else
            if (_mood == "хорошее")
        {
            _reaction = "мурлычет.";
            _newMood = "отличное";
            _moodChanged = true;
        }
        else
            if (_mood == "отличное")
        {
            _reaction = "мурлычет и виляет хвостом.";
        }
        MakeReaction();
    }
    public void Kick()
    {
        _action = "Дать пинка";
        if (_mood == "плохое")
        {
            _reaction = "прыгает и кусает за правое ухо.";
        }
        else
            if (_mood == "хорошее")
        {
            _reaction = "убегает на ковер и писает.";
            _newMood = "плохое";
            _moodChanged = true;
        }
        else
            if (_mood == "отличное")
        {
            _reaction = "убегает в другую комнату.";
            _newMood = "плохое";
            _moodChanged = true;
        }
        MakeReaction();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
