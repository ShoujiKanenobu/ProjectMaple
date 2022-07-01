using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SentenceGameManager : MonoBehaviour
{
    public GameObject uiParent;
    public GameObject uiprefab;
    public GameObject uiParticleSlotPrefab;
    public GameObject uiParticleCardPrefab;
    public GameObject uiParticleCardPanel;
    public GameObject levelComplete;
    public TextMeshProUGUI levelCompletePoints;
    public DifficultyUI DUI;
    public TextMeshProUGUI carGameSentenceUI;

    public Image uiFlashPanel;
    public ParsedSentences currentSentence;
    private string fullSentence;
    private List<GameObject> particleslots = new List<GameObject>();

    public StringRTSet PickedUpSet;

    public CarGameManager cgm;
    public SentenceParser spm;

    public GameObject sgpanel;

    private Coroutine _currentFlashRoutine;

    public GameEvent completedLevel;

    public DifficultyHelper difficulty;

    private Dictionary<int, int> difficultyDict = new Dictionary<int, int>()
    { [1] = 150,
        [2] = 125,
        [3] = 100,
        [4] = 75,
        [5] = 50
        };

    public void setNewSentence(ParsedSentences x)
    {
        particleslots.Clear();

        currentSentence = x;
        fullSentence = x.FullSentence;
    }

    public void setUpCarGameSentence()
    {
        string t = "";


        foreach(string i in currentSentence.ParsedWords)
        {
            t += i.Trim(' ', '　');
            t += " � ";
        }

        string t2 = t.Remove(t.Length - 3, 3);
        carGameSentenceUI.text = t2;

        List<string> correctedParticles = new List<string>();
        foreach(string i in currentSentence.ParsedParticles)
        {
            if (i.Contains(","))
            {
                correctedParticles.AddRange(i.Split(','));
            }  
            else
                correctedParticles.Add(i);
        }
        cgm.ChooseLevelAndSpawnRiders(correctedParticles, currentSentence.WrongParticles);
        cgm.timeBeforeArrival = difficultyDict[difficulty.difficutly];
    }

    public void setupSentence()
    {
        
        int count = 0;
        foreach (string i in currentSentence.ParsedWords)
        {
            GameObject reference = Instantiate(uiprefab, uiParent.transform);
            reference.GetComponent<TextMeshProUGUI>().text = i.Trim(' ', '　');
            if (i != currentSentence.ParsedWords[currentSentence.ParsedWords.Count - 1])
            {
                GameObject particleSlot = Instantiate(uiParticleSlotPrefab, uiParent.transform);
                string particleHandling = currentSentence.ParsedParticles[count];
                if(particleHandling.Contains(","))
                {
                    List<string> plist = new List<string>(particleHandling.Split(','));
                    particleSlot.GetComponent<ParticleSlotController>().setParticle(plist);
                }
                else
                    particleSlot.GetComponent<ParticleSlotController>().setParticle(particleHandling);
                particleslots.Add(particleSlot);
            }
            count++;

        }
    }

    public void setupParticleCards()
    {
        foreach (string i in PickedUpSet.Items)
        {
            GameObject particleCard = Instantiate(uiParticleCardPrefab, uiParticleCardPanel.transform);
            particleCard.GetComponent<ParticleCardController>().setParticle(i);
        }

        PickedUpSet.Clear();
    }

    public void CheckAll()
    {
        bool correct = true;
        foreach (GameObject g in particleslots)
        {
            if(!g.GetComponent<ParticleSlotController>().checkSlotChild())
            {
                correct = false;
                break;
            }
        }
        CompleteLevel(correct);
    }

    public void ClearUI()
    {
        foreach (Transform x in uiParticleCardPanel.transform)
        {
            GameObject.Destroy(x.gameObject);
        }
        foreach (Transform x in uiParent.transform)
        {
            GameObject.Destroy(x.gameObject);
        }
    }

    public void CompleteLevel(bool condition)
    {
        if (condition)
        {
            AudioManager.instance.Play("success");
            StartFlash(.25f, .5f, Color.green);
            levelComplete.SetActive(true);
            levelComplete.transform.localScale = new Vector3(0, 0, 0);
            levelComplete.GetComponent<ScaleTween>().OnOpen();
            levelCompletePoints.text = "ポイント： " + cgm.points;
            DUI.ChangeText();
            cgm.inPlay = false;
        }
        else
        {
            AudioManager.instance.Play("wrong");
            StartFlash(.25f, .5f, Color.red);
        }
    }
    

    public void RestartGame()
    {
        completedLevel.Raise();
        ClearUI();
        sgpanel.SetActive(false);
        spm.InitializeGame();
        cgm.GameEnd();
        cgm.GameStart();
    }

    public void StartFlash(float secondsForFlash, float maxAlpha, Color newColor)
    {
        uiFlashPanel.color = newColor;
        maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);

        if(_currentFlashRoutine != null)
            StopCoroutine(_currentFlashRoutine);
        _currentFlashRoutine = StartCoroutine(Flash(secondsForFlash, maxAlpha));
    }

    IEnumerator Flash(float secondsForFlash, float MaxAlpha)
    {
        float flashInDuration = secondsForFlash / 2;
        for (float t = 0; t <= flashInDuration; t += Time.deltaTime)
        {
            Color currentFrameColor = uiFlashPanel.color;
            currentFrameColor.a = Mathf.Lerp(0, MaxAlpha, t / flashInDuration);
            uiFlashPanel.color = currentFrameColor;
            yield return null;
        }

        float flashOutDuration = secondsForFlash / 2;
        for (float t = 0; t <= flashInDuration; t += Time.deltaTime)
        {
            Color currentFrameColor = uiFlashPanel.color;
            currentFrameColor.a = Mathf.Lerp(MaxAlpha, 0, t / flashInDuration);
            uiFlashPanel.color = currentFrameColor;
            yield return null;
        }
    }
}
