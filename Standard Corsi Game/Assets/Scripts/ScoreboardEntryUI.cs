using UnityEngine;
using TMPro;

namespace Project.Scoreboards
{

    public class ScoreboardEntryUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI entry_name_text = null;
        [SerializeField] private TextMeshProUGUI entry_score_text = null;

        public void Initialize(ScoreboardEntryData scoreboardEntryData)
        {
            entry_name_text.text = scoreboardEntryData.entry_name;
            entry_score_text.text = scoreboardEntryData.entry_score.ToString();

        }
    }



}