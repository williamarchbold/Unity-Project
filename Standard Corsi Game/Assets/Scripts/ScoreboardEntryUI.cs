//based upon https://www.youtube.com/watch?v=FSEbPxf0kfs



using UnityEngine;
using TMPro;

namespace Project.Scoreboards
{

    public class ScoreboardEntryUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI entry_name_text = null; 
        [SerializeField] private TextMeshProUGUI entry_score_text = null;

        //This method links the usernames and scores to the GUI interface for display to screen
        public void Initialize(ScoreboardEntryData scoreboardEntryData)
        {
            entry_name_text.text = scoreboardEntryData.entry_name;
            entry_score_text.text = scoreboardEntryData.entry_score.ToString();

        }
    }



}