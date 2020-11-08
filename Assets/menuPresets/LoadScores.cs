using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace Resources.Score
{
    public class LoadScores : MonoBehaviour
    {
        private Transform entryContainer;
        private Transform entryTemplate;
        private static string scoreFilePath;
        private static string absoluteScoreFilePath = "/Resources/someScore.txt";
        private bool generated = false;

        public void Awake()
        {
            scoreFilePath = Application.dataPath + absoluteScoreFilePath;
        }


        public void LeaderboardLoad()
        {
            if (generated) return;
            generated = true;
            entryContainer = transform.Find("HighscoreEntryContainer");
            entryTemplate = entryContainer.Find("HighscoreEntryTemplate");
            entryTemplate.gameObject.SetActive(false);

            int firstElmentHeight = 96;
            int templateHeight = 22;

            //TODO MOCK DELETE AFTER IMPLEMENTING ADDING TO SCORE
            // List<Player> mockList = new List<Player>();
            // CreatePlayer(mockList);
            // WriteScoreToAFile(mockList);

            for (int i = 0; i < 10; i++)
            {
                Transform entryTransform = Instantiate(entryTemplate, entryContainer);
                RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
                entryRectTransform.anchoredPosition = new Vector2(0, firstElmentHeight - templateHeight * i);
                entryTransform.gameObject.SetActive(true);

                int rank = i + 1;
                string rankString;
                switch (rank)
                {
                    default:
                        rankString = rank + "th";
                        break;
                    case 1:
                        rankString = "1st";
                        break;
                    case 2:
                        rankString = "2nd";
                        break;
                    case 3:
                        rankString = "3rd";
                        break;
                }

                entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rankString;
                List<Player> players = GetScoreList();
                if (players.Count > i)
                {
                    entryTransform.Find("nickText").GetComponent<TextMeshProUGUI>().text = players[i].NickName;
                    entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = "" + players[i].Score;
                    continue;
                }

                entryTransform.Find("nickText").GetComponent<TextMeshProUGUI>().text = "NO_ONE";
                entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = "---";
            }
        }

        //TODO MOCK DELETE AFTER IMPLEMENTING ADDING TO SCORE
        private static void CreatePlayer(List<Player> players)
        {
            Random rnd = new Random();
            string[] nicks = new[]
            {
                "Sanya", "Doomer", "CoolGuy", "Pietia", "Jack", "Lilly", "Eren", "Mikasa", "Roma", "Severyn"
            };
            for (int i = 0; i < nicks.Length; i++)
            {
                int randScore = rnd.Next(1, 1000);
                players.Add(new Player(nicks[i], randScore));
            }
        }

        private static List<Player> GetScoreList()
        {
            if (!File.Exists(scoreFilePath))
            {
                Debug.Log(scoreFilePath);
                CreateScoreFile(scoreFilePath);
                return new List<Player>();
            }
            else if (new FileInfo(scoreFilePath).Length == 0) return new List<Player>();

            return DesirealizeScoreFile();
        }

        private static List<Player> DesirealizeScoreFile()
        {
            // Open the file containing the data that you want to deserialize.
            FileStream fs = new FileStream(scoreFilePath, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                List<Player> players = (List<Player>) formatter.Deserialize(fs);
                SortAndNormalizeScoreList(players);
                return players;
            }
            catch (SerializationException e)
            {
                Debug.Log(e.Message + "\n Cannot deserialize, creating a new score list.");
                fs.Close();
                return new List<Player>();
            }
            finally
            {
                fs.Close();
            }
        }

        private static void WriteScoreToAFile(List<Player> players)
        {
            try
            {
                //writing into the file
                FileStream fs = new FileStream(scoreFilePath, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, players);
                }
                catch (SerializationException e)
                {
                    Debug.Log("Failed to serialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
            catch (IOException e)
            {
                Debug.Log(e.Message + "\n Cannot write to file.");
            }
        }

        private static void CreateScoreFile(string scoreFile)
        {
            BinaryWriter bw = null;
            try
            {
                bw = new BinaryWriter(new FileStream(scoreFile, FileMode.Create));
            }
            catch (IOException e)
            {
                Debug.Log(e.Message + "\n Cannot create file.");
            }
            finally
            {
                if (bw != null) bw.Close();
            }
        }

        private static void SortAndNormalizeScoreList(List<Player> players)
        {
            if (players.Count != 1)
            {
                players.Sort((p1, p2) => p2.Score.CompareTo(p1.Score));
                while (players.Count > 10)
                {
                    players.RemoveAt(10);
                }
            }

            //WriteScoreToAFile(players);
        }


        public TextMeshProUGUI entryText;
        public TextMeshProUGUI entryPlaceholder;

        public int score = 0;

        public void ReceiveNickname()
        {
            string nickname = entryText.text;
            if (string.IsNullOrEmpty(nickname) || nickname.Length > 15 || nickname.Length == 1)
            {
                entryPlaceholder.text = "Try Again...";
                return;
            }

            List<Player> scoreList = GetScoreList();
            scoreList.Add(new Player(nickname, score));
            WriteScoreToAFile(scoreList);
            SceneManager.LoadScene(0);
        }
    }
}