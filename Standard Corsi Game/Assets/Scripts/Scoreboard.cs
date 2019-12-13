//class is derived from this tutorial : https://www.youtube.com/watch?v=FSEbPxf0kfs

using System;
using System.IO; //input output
using UnityEngine;

namespace Project.Scoreboards
{
    public class Scoreboard : MonoBehaviour
    {
        [SerializeField] private int max_scoreboard_entries = 5; //this was from the video. can be changed to whatever desired
        [SerializeField] private Transform high_scores_holder_transform = null;
        [SerializeField] private GameObject scoreboard_entry_object = null;

        [Header("Test")]
        [SerializeField] ScoreboardEntryData test_entry_data = new ScoreboardEntryData(); //allows developer to test entries from the Unity Engine 

        //make a safe path to save to my computer. Application.persistentDataPath is a dynamic means of saving to local machine for different computers
        //without having to change code. 
        private string save_path => $"{Application.persistentDataPath}/highscores.json";  //where the project is stored. can be .txt not necessarily json

        private void Start()
        {
            ScoreboardSavedData saved_scores = GetSavedScores();

            UpdateUI(saved_scores);

            SaveScores(saved_scores);
        }

        [ContextMenu("Add Test Entry")] //allows user to call test from within unity 
        public void AddTestEntry()
        {
            AddEntry(test_entry_data);
        }

        [ContextMenu("Delete Save File")]
        public void DeleteSaveFile() 
        {
            File.Delete(save_path);
        }

        public void AddEntry(ScoreboardEntryData scoreboardEntryData)
        {
            ScoreboardSavedData saved_scores = GetSavedScores();

            bool score_added = false;

            for (int i = 0; i < saved_scores.high_scores.Count; i++)
            {
                if (scoreboardEntryData.entry_score > saved_scores.high_scores[i].entry_score) //if score trying to add is greater than current score checking
                {
                    saved_scores.high_scores.Insert(i, scoreboardEntryData); //put into list at i 
                    score_added = true;
                    break;
                }
            }
            if (!score_added && saved_scores.high_scores.Count < max_scoreboard_entries) // still space left
            {
                saved_scores.high_scores.Add(scoreboardEntryData); //put new score in list 
            }
            if (saved_scores.high_scores.Count > max_scoreboard_entries) //too many entries 
            {
                saved_scores.high_scores.RemoveRange(max_scoreboard_entries, saved_scores.high_scores.Count - max_scoreboard_entries); //remove entries from max to count of scores
            }

            UpdateUI(saved_scores);

            SaveScores(saved_scores);
        }

        private void UpdateUI(ScoreboardSavedData saved_scores)
        {
            foreach (Transform child in high_scores_holder_transform)
            {
                Destroy(child.gameObject); //if new entry want to destroy old buttons and repopulate with new ones
            }
            foreach (ScoreboardEntryData high_score in saved_scores.high_scores) //loop through each score in high_scores list
            {
                Debug.Log("check if scoreboard entry object null; " + (scoreboard_entry_object == null));
                Debug.Log("check if high_scores_holder_transform object null; " + (high_scores_holder_transform == null));
                Instantiate(scoreboard_entry_object, high_scores_holder_transform). //instantiate means to "spawn in"
                    GetComponent<ScoreboardEntryUI>().Initialize(high_score); //create a new UI entry with this loop's high score info
            }
        }


        //This method is private because it's called earlier in Start and nowhere else
        private ScoreboardSavedData GetSavedScores() //this will retrieve saved scores
        {
            if (!File.Exists(save_path))
            {
                File.Create(save_path).Dispose(); //need to dispose or else get an error later saying file is already in use
                return new ScoreboardSavedData();
            }

            using (StreamReader stream = new StreamReader(save_path)) //using is a good way to stop leakages like leaving a file open
            {
                string json = stream.ReadToEnd();

                return JsonUtility.FromJson<ScoreboardSavedData>(json);
            }
        }

        private void SaveScores(ScoreboardSavedData scoreboardSavedData) //this will save scores to file
        {
            using (StreamWriter stream = new StreamWriter(save_path))
            {
                string json = JsonUtility.ToJson(scoreboardSavedData, true); //format to true makes it look nice
                stream.Write(json);
            }
        }

    }
}

