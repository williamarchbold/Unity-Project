using System.Collections.Generic;

namespace Project.Scoreboards
{
    [System.Serializable] 
    public class ScoreboardSavedData
    {
        public List<ScoreboardEntryData> high_scores = new List<ScoreboardEntryData>();
    }

}
