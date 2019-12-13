//based upon https://www.youtube.com/watch?v=FSEbPxf0kfs
//very simple so just struct not class


namespace Project.Scoreboards
{
    [System.Serializable]  //include this so that data in struct can be saved to computer   
    public struct ScoreboardEntryData 
    {
        public string entry_name;
        public int entry_score;

    }

}

