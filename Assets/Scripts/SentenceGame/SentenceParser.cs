using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParsedSentences
{
    public List<string> ParsedWords = new List<string>();
    public List<string> ParsedParticles = new List<string>();
    public List<string> WrongParticles;
    public string FullSentence;
    public int Difficulty;

    readonly public static List<string> ParticleList = new List<string>{ "‚Í", "‚É", "‚ª", "‚ð", "‚Ö", "‚Ì", "‚ð", "‚Æ" };

    public ParsedSentences(int difficulty, string unparsed)
    {
        this.Difficulty = difficulty;
        unparsed = unparsed.Substring(1, unparsed.Length - 1);
        unparsed = unparsed.Trim(' ');
        unparsed = unparsed.Trim('\n');
        unparsed = unparsed.Trim('\r');
        FullSentence = unparsed.Trim('%');
        string[] sps = unparsed.Split('%');
        List<string> spaceSplit = new List<string>(sps);

        foreach (string wordset in spaceSplit)
        {

            if (ParticleList.Contains(wordset.Split(',')[0]))
            {
                ParsedParticles.Add(wordset);
            }
            else
            {
                ParsedWords.Add(wordset);
            }
        }
    }

    public void SetWrongParticles(string unparsed)
    {
        unparsed = unparsed.Trim(' ');
        string[] sps = unparsed.Split(',');
        WrongParticles = new List<string>(sps);
    }

}


public class SentenceParser : MonoBehaviour
{
    public SentenceGameManager sgm;
    public List<ParsedSentences> SentenceData = new List<ParsedSentences>();

    public int lastDifficulty = 1;
    public DifficultyHelper dh;

    public TextAsset sentenceTextData;
    public void ReadFile()
    {
        int count = 0;
        int x = 0;
        ParsedSentences p;

        List<string> ListLines = new List<string>(sentenceTextData.text.Split('\n'));
        foreach (string line in ListLines)
        {
            string cleanedline = line.Trim('\r', '\n');
            if (int.TryParse(line.Substring(0, 1), out x))
            {
                p = new ParsedSentences(x, cleanedline);
                SentenceData.Add(p);
            }
            else if(cleanedline != "")
            {
                SentenceData[SentenceData.Count - 1].SetWrongParticles(cleanedline);
                count++;
            }
        }
    }

    public void Start()
    {
        ReadFile();
        InitializeGame(dh.difficutly);
    }

    public void InitializeGame()
    {
        System.Random rng = new System.Random();
        List<ParsedSentences> d = GetSentenceWithDifficulty(lastDifficulty);
        sgm.setNewSentence(d[rng.Next(0, d.Count)]);
        sgm.setUpCarGameSentence();
        sgm.setupSentence();
    }

    public void InitializeGame(int difficulty)
    {
        System.Random rng = new System.Random();
        List<ParsedSentences> d = GetSentenceWithDifficulty(difficulty);
        sgm.setNewSentence(d[rng.Next(0, d.Count)]);
        sgm.setUpCarGameSentence();
        sgm.setupSentence();
        lastDifficulty = difficulty;
    }

    private List<ParsedSentences> GetSentenceWithDifficulty(int difficulty)
    {
        List<ParsedSentences> d = new List<ParsedSentences>();
        foreach(ParsedSentences x in SentenceData)
        {
            if(x.Difficulty == difficulty)
            {
                d.Add(x);
            }
        }
        return d;
    }
}
