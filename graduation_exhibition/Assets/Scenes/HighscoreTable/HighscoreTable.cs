using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class HighscoreTable : MonoBehaviour {

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<HighscoreEntry> tenList;
    private List<Transform> highscoreEntryTransformList;
    private List<int> scores = new List<int>();
    private List<string> names = new List<string>();
    private int length = 0;

    private void Awake() {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        HttpRequestManager HTM = new HttpRequestManager(); 
        LikeScoreList data = HTM.GetLikeScoreList();

        if (data.status == 200)
        {
            List<LikeScoreListNode> list = data.list;
            Debug.Log(list.Count);

            string studentId = list[0].u_id;
            string name = list[0].name;
            string title = list[0].title;
            int score = list[0].like_score;

            length = list.Count;

            Debug.Log(studentId);
            Debug.Log(name);
            Debug.Log(title);
            Debug.Log(score);
            
            for(int i=0; i<list.Count; i++)
            {
                scores.Add(list[i].like_score);
                names.Add(list[i].title);
            }

            for(int i=0; i<list.Count; i++)
            {
                Debug.Log(scores[i]);
                Debug.Log(names[i]);
            }

            for(int i=0; i<list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (scores[j] > scores[i])
                    {
                        // Swap
                        int tmp = scores[i];
                        scores[i] = scores[j];
                        scores[j] = tmp;

                        string tmp2 = names[i];
                        names[i] = names[j];
                        names[j] = tmp2;
                    }
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                Debug.Log(scores[i]);
                Debug.Log(names[i]);
            }
        }

        if(length == 1)
        {
            highscoreEntryList = new List<HighscoreEntry>()
            {
                new HighscoreEntry { score = scores[0], name = names[0] },
            };
        }
        else if (length == 2)
        {
            highscoreEntryList = new List<HighscoreEntry>()
            {
                new HighscoreEntry { score = scores[0], name = names[0] },
                new HighscoreEntry { score = scores[1], name = names[1] },
            };
        }
        else if (length == 3)
        {
            highscoreEntryList = new List<HighscoreEntry>()
            {
                new HighscoreEntry { score = scores[0], name = names[0] },
                new HighscoreEntry { score = scores[1], name = names[1] },
                new HighscoreEntry { score = scores[2], name = names[2] },
            };
        }
        else if (length == 4)
        {
            highscoreEntryList = new List<HighscoreEntry>()
            {
                new HighscoreEntry { score = scores[0], name = names[0] },
                new HighscoreEntry { score = scores[1], name = names[1] },
                new HighscoreEntry { score = scores[2], name = names[2] },
                new HighscoreEntry { score = scores[3], name = names[3] },
            };
        }
        else if (length == 5)
        {
            highscoreEntryList = new List<HighscoreEntry>()
            {
                new HighscoreEntry { score = scores[0], name = names[0] },
                new HighscoreEntry { score = scores[1], name = names[1] },
                new HighscoreEntry { score = scores[2], name = names[2] },
                new HighscoreEntry { score = scores[3], name = names[3] },
                new HighscoreEntry { score = scores[4], name = names[4] },
            };
        }
        else if (length == 6)
        {
            highscoreEntryList = new List<HighscoreEntry>()
            {
                new HighscoreEntry { score = scores[0], name = names[0] },
                new HighscoreEntry { score = scores[1], name = names[1] },
                new HighscoreEntry { score = scores[2], name = names[2] },
                new HighscoreEntry { score = scores[3], name = names[3] },
                new HighscoreEntry { score = scores[4], name = names[4] },
                new HighscoreEntry { score = scores[5], name = names[5] },
            };
        }
        else if (length == 7)
        {
            highscoreEntryList = new List<HighscoreEntry>()
            {
                new HighscoreEntry { score = scores[0], name = names[0] },
                new HighscoreEntry { score = scores[1], name = names[1] },
                new HighscoreEntry { score = scores[2], name = names[2] },
                new HighscoreEntry { score = scores[3], name = names[3] },
                new HighscoreEntry { score = scores[4], name = names[4] },
                new HighscoreEntry { score = scores[5], name = names[5] },
                new HighscoreEntry { score = scores[6], name = names[6] },
            };
        }
        else if (length == 8)
        {
            highscoreEntryList = new List<HighscoreEntry>()
            {
                new HighscoreEntry { score = scores[0], name = names[0] },
                new HighscoreEntry { score = scores[1], name = names[1] },
                new HighscoreEntry { score = scores[2], name = names[2] },
                new HighscoreEntry { score = scores[3], name = names[3] },
                new HighscoreEntry { score = scores[4], name = names[4] },
                new HighscoreEntry { score = scores[5], name = names[5] },
                new HighscoreEntry { score = scores[6], name = names[6] },
                new HighscoreEntry { score = scores[7], name = names[7] },
            };
        }
        else if (length == 9)
        {
            highscoreEntryList = new List<HighscoreEntry>()
            {
                new HighscoreEntry { score = scores[0], name = names[0] },
                new HighscoreEntry { score = scores[1], name = names[1] },
                new HighscoreEntry { score = scores[2], name = names[2] },
                new HighscoreEntry { score = scores[3], name = names[3] },
                new HighscoreEntry { score = scores[4], name = names[4] },
                new HighscoreEntry { score = scores[5], name = names[5] },
                new HighscoreEntry { score = scores[6], name = names[6] },
                new HighscoreEntry { score = scores[7], name = names[7] },
                new HighscoreEntry { score = scores[8], name = names[8] },
            };
        }
        else if (length >= 10)
        {
            highscoreEntryList = new List<HighscoreEntry>()
            {
                new HighscoreEntry { score = scores[0], name = names[0] },
                new HighscoreEntry { score = scores[1], name = names[1] },
                new HighscoreEntry { score = scores[2], name = names[2] },
                new HighscoreEntry { score = scores[3], name = names[3] },
                new HighscoreEntry { score = scores[4], name = names[4] },
                new HighscoreEntry { score = scores[5], name = names[5] },
                new HighscoreEntry { score = scores[6], name = names[6] },
                new HighscoreEntry { score = scores[7], name = names[7] },
                new HighscoreEntry { score = scores[8], name = names[8] },
                new HighscoreEntry { score = scores[9], name = names[9] },
            };
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList) {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 31f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank) {
        default:
            rankString = rank + "TH"; break;

        case 1: rankString = "1ST"; break;
        case 2: rankString = "2ND"; break;
        case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        // Set background visible odds and evens, easier to read
        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);
        
        // Highlight First
        if (rank == 1) {
            entryTransform.Find("posText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<Text>().color = Color.green;
        }

        // Set tropy
        switch (rank) {
        default:
            entryTransform.Find("trophy").gameObject.SetActive(false);
            break;
        case 1:
            entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("FFD200");
            break;
        case 2:
            entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("C6C6C6");
            break;
        case 3:
            entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("B76F56");
            break;

        }

        transformList.Add(entryTransform);
    }

    /* single High score entry */
    [System.Serializable] 
    private class HighscoreEntry {
        public int score;
        public string name;
    }
}
