using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class CarGameManager : MonoBehaviour
{
    public bool inPlay;

    public GameObject Player;
    public GameObject startingPosition;

    public PositionTween pt;
    public GameObject startbutton;

    public StringRTSet pickedUpParticles;
    public StringRTSet spawnedSet;
    public RectTransform trainTransform;
    public float elapsedTime;
    public float timeBeforeArrival;
    public GameObject RiderPrefab;
    public GameObjectRTSet spawnpoints;
    public float spawnCircleRadius;

    public InGameTrainController IGTC;

    public bool arrived;

    public int points;
    public int maxPoints;
    public TextMeshProUGUI pointsUI;

    public GameObject FailPanel;

    public GameObjectRTSet RidersSet;
    public GameEvent inPlayEvent;
    public GameEvent stopPlayEvent;

    public GameObject PausedText;

    public List<GameObject> Levels;
    void Start()
    {
        Player.transform.position = startingPosition.transform.position;
        GameEnd();
    }

    public void GameStart()
    {
        Player.transform.position = startingPosition.transform.position;
        Player.transform.rotation = Quaternion.Euler(0, -90, 0);
        pickedUpParticles.Clear();
        spawnedSet.Clear();
        arrived = false;
        inPlay = false;
        elapsedTime = 0;
        points = maxPoints;
    }

    private void ChooseLevel()
    {
        int rand = UnityEngine.Random.Range(0, Levels.Count - 1);
        foreach(GameObject l in Levels)
        {
            if (l == Levels[rand])
                l.SetActive(true);
            else
                l.SetActive(false);
        }
    }

    public void StartPlay()
    {
        inPlay = true;
        
    }

    public void PauseGame()
    {
        inPlay = false;
        stopPlayEvent.Raise();
    }

    public void ResumeGame()
    {
        inPlay = true;
        inPlayEvent.Raise();
    }

    public void GameEnd()
    {
        Player.transform.position = startingPosition.transform.position;
        Player.transform.rotation = Quaternion.Euler(0, -90, 0);
        inPlay = false;
        Vector3 pos = trainTransform.localPosition;
        trainTransform.localPosition = new Vector3(-500, pos.y, pos.z);
        pt.CheatToStart();
        startbutton.SetActive(true);
        startbutton.GetComponent<ScaleTween>().OnOpen();
        foreach(GameObject g in RidersSet.Items)
        {
            Destroy(g);
        }
    }

    public void ChooseLevelAndSpawnRiders(List<string> correct, List<string> wrong)
    {
        //CURSED LEVEL SELECTION HERE CAUSE I DONT WANT TO FIGURE OUT ORDER
        ChooseLevel();

        List<string> levelList = new List<string>();
        levelList.AddRange(correct);
        levelList.AddRange(wrong);
        foreach (string i in levelList)
        {
            int randCount = UnityEngine.Random.Range(0, spawnpoints.Items.Count - 1);
            Vector3 randPos = UnityEngine.Random.insideUnitSphere * spawnCircleRadius;
            Vector3 yNormalizedPos = new Vector3(randPos.x, 0, randPos.z);
            GameObject t = Instantiate(RiderPrefab);
            t.transform.position = spawnpoints.Items[randCount].transform.position + yNormalizedPos;
            t.GetComponent<Rider>().SetParticle(i);
            spawnedSet.Add(i);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inPlay)
        {
            PauseGame();
            PausedText.SetActive(true);
        }  
        else if (Input.GetKeyDown(KeyCode.Escape) && !inPlay)
        {
            ResumeGame();
            PausedText.SetActive(false);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!inPlay)
        {
            return;
        }
        
        CalculatePoints();
        StepTimer();
        MoveInGameTrain();

        IGTC.UpdatePosition(elapsedTime, timeBeforeArrival);
    }

    private void MoveInGameTrain()
    {
        Vector3 pos = trainTransform.localPosition;
        float nX = -500 + (500 * (elapsedTime / timeBeforeArrival));
        trainTransform.localPosition = new Vector3(nX, pos.y, pos.z);
    }

    private void StepTimer()
    {
        if (arrived != true)
            elapsedTime += Time.deltaTime;
        if (elapsedTime > timeBeforeArrival)
        {
            arrived = true;
        }

        if (arrived)
            TimesUp();
    }

    private void CalculatePoints()
    {
        points = maxPoints - Mathf.RoundToInt(maxPoints * (elapsedTime / timeBeforeArrival));
        pointsUI.text = "ポイント： " + points;
    }

    private void TimesUp()
    {
        inPlay = false;
        FailPanel.SetActive(true);
        FailPanel.GetComponent<ScaleTween>().OnOpen();
    }

    public void EndLevel()
    {
        Debug.Log("Level finish!");
    }
}
