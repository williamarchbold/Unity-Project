
namespace Project.Scoreboards
{
    [System.Serializable]  //include this so that data in struct can be saved to computer   
    public struct ScoreboardEntryData //very simple so just struct not class
    {
        public string entry_name;
        public int entry_score;

    }

}

