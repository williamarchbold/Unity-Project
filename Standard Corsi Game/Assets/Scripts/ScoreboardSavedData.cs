//based upon https://www.youtube.com/watch?v=FSEbPxf0kfs
//encapsulating everything that needs to be saved into one object apparently is a lot easier for JSON to handle
//this class encapsulates user names and scores which are components of ScoreboardEntryData
//

using System.Collections.Generic;

namespace Project.Scoreboards
{
    [System.Serializable] //needed for saving with JSON 
    public class ScoreboardSavedData
    {
        public List<ScoreboardEntryData> high_scores = new List<ScoreboardEntryData>();
    }

}
