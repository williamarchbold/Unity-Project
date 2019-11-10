using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotLogic : MonoBehaviour
{
    public ClickButton[] my_buttons;
    public List<int> color_list; 

    public float show_time = 0.5f;
    public float pause_time = 0.5f;

    public int level = 2;
    private int player_level = 0;

    public bool player = false;

    private int my_random;

    public Button start_button;
    public Text game_over_text;
    public Text score_text;
    public Text orderPrompt;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < my_buttons.Length; i++)
        {
            my_buttons[i].onClick += ButtonClicked;
            my_buttons[i].my_number = i; //set 
        }
    }

    void ButtonClicked(int _number) 
    {
        if (player)
        {
            if (_number == color_list[player_level])
            {
                player_level += 1;
                score += 1;
                score_text.text = score.ToString();
                Debug.Log("Correnct answer");
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


    //wait one seconds, get random value, change color of button @ random value,
    //wait for show_time seconds, change color back, wait pause_time seconds
    private IEnumerator Robot()
    {
        orderPrompt.text = "";
        yield return new WaitForSeconds(1f); //1 sec
        for (int i = 0; i < level; i++)
        {
            if (color_list.Count < level)
            {
                my_random = Random.Range(0, my_buttons.Length);
                color_list.Add(my_random); //store to save color order for later comparison
            }
            
            my_buttons[color_list[i]].ClickedColor();
            yield return new WaitForSeconds(show_time);
            my_buttons[color_list[i]].UnClickedColor();
            yield return new WaitForSeconds(pause_time);           
        }

        if (Random.Range(0f, 1f) < 0.5f)
        {
            color_list.Reverse();
            orderPrompt.text = "Reverse";
        }        
        else
            orderPrompt.text = "Normal";

        player = true;
    }

    public void StartGame()
    {
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
        game_over_text.text = "Game Over";
        start_button.interactable = true;
        player = false;
    }
}
