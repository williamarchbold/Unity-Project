//this class is based on https://www.youtube.com/watch?v=OmynDREHO_8&t=1987s
//the ShuffleCubes method was not part of the video 
// added CreateScoreEntry(), CreatePlayerStatsEntry(), 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Project.Scoreboards;
using TMPro;
using System; 

public class RobotLogic : MonoBehaviour
{
    public CursorTracker cursorTracker;
    public ClickCursorTracker clickCursorTracker;
    public CubesPositionRandomizer cubeRandomizer;
    public LogIn login;
    public ClickButton[] cubes;
    public List<int> color_list; //this will hold the colors for the current level of the game so 2 colors for first round

    public float show_time = 0.5f;
    public float pause_time = 0.5f;

    public int level = 2; //starting number of boxes in play
    private int player_level = 0; //used to compare to level. resets at end of every level.

    public bool player = false;

    private int my_random;

    public Button start_button;
    public Text game_over_text;
    public Text score_text;
    public Text orderPrompt;
    public Scoreboard scoreboard;
    public PlayerScoreboard playerScoreboard;
    public GameObject scoreboardsParent;

    public GameObject namePrompt;
    public TMP_InputField inputField;
    private int score;

    
    void Start()// Start is called before the first frame update so before player even logs in
    {
        Debug.Log(Application.persistentDataPath);

        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].onClick += ButtonClicked;
            cubes[i].my_number = i; //set each button's number 
        }
    }

    void ButtonClicked(int _number) //This is called when user clicks on a cube
    {
        if (player) //protects game from user clicking on cube prematurely 
        {
            clickCursorTracker.CubeClicked(Input.mousePosition);

            if (_number == color_list[player_level]) //if the number for cube matches 
            {
                player_level += 1;
                score += 1;
                score_text.text = score.ToString();
                Debug.Log("Correct answer");
            }
            else
            {
                Debug.Log("Wrong answer");
                GameOver();
            }

            if (player_level == level) //no more buttons to press
            {
                level += 1; //set level for next time 1 higher
                player_level = 0; //reset player level for next round
                player = false;
                StartCoroutine(Robot()); //execute code over intervals of time
            }
        }
    }

    //Robot() is the true starting point for every level 
    //wait one seconds, get random value, change color of button @ random value,
    //wait for show_time seconds, change color back, wait pause_time seconds
    private IEnumerator Robot() //IEnumerator allows for yield returns and delays 
    {
        clickCursorTracker.StopTrackingAndSave();
        orderPrompt.text = ""; //empty out order to click squares
        yield return new WaitForSeconds(1f); //1 sec
        cubeRandomizer.ShuffleCubes(); 
        yield return new WaitForSeconds(1f); //1 sec
        for (int i = 0; i < level; i++)
        {
            if (color_list.Count < level)
            {
                my_random = UnityEngine.Random.Range(0, cubes.Length);
                color_list.Add(my_random); //store to save color order for later comparison
            }
            
            cubes[color_list[i]].ClickedColor();
            yield return new WaitForSeconds(show_time);
            cubes[color_list[i]].UnClickedColor();
            yield return new WaitForSeconds(pause_time);           
        }

        if (UnityEngine.Random.Range(0f, 1f) < 0.5f) //
        {
            color_list.Reverse();
            orderPrompt.text = "Reverse";
        }        
        else
            orderPrompt.text = "Normal";

        var sequence = SequenceString() + " : " + orderPrompt.text;
        clickCursorTracker.WriteSequence(sequence);

        player = true;
    }

    private string SequenceString() 
    {
        var sequence = "";
        foreach (int i in color_list)
        {
            sequence += 
                cubes[i].GetComponent<MeshRenderer>().material.name + ", ";
        }
        return sequence;
    }

    public void StartGame()//this starts the very first game only
    {
        cursorTracker.StartTracking();
        StartCoroutine(Robot()); //execute code over intervals of time
        score = 0;
        player_level = 0;
        level = 2;
        game_over_text.text = "";
        score_text.text = score.ToString();
        start_button.interactable = false;        
    }

    void GameOver()
    {
        clickCursorTracker.StopTrackingAndSave();
        cursorTracker.StopTrackingAndSave();
        game_over_text.text = "Game Over";
        start_button.interactable = true;
        player = false;
        // namePrompt.SetActive(true);
        CreateScoreEntry(login.Username);
        CreatePlayerStatsEntry();
        scoreboardsParent.SetActive(true);
    }

    public void CreateScoreEntry(string userName) //this manipuates the scoreboard on the left
    {
        namePrompt.SetActive(false);
        var enteredName = userName; //enteredName was old variable used from video which had user input name after game over. customized to use login name
        scoreboard.AddEntry(new ScoreboardEntryData() { entry_name = enteredName, entry_score = score });
    }

    public void CreatePlayerStatsEntry() //this manipulates the scoreboard on the right
    {
        namePrompt.SetActive(false);
        playerScoreboard.AddEntry(new ScoreboardEntryData() { entry_name = DateTime.Now.ToString(), entry_score = score });
    }


    public void ConfirmName() 
    {
        CreateScoreEntry(inputField.text);
    }

}
