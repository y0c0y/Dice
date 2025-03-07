using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public GameManager gameManager;
    
    public Text resultText;
    public Text recordText;
    
    public Canvas playerButtonCanvas;
    public Canvas lobbyCanvas;
    public Canvas playTextCanvas;
    
    public Texture rockTexture;
    public Texture paperTexture;
    public Texture scissorsTexture;
    
    public RawImage npcImage;
    public RawImage playerImage;
    
    public TMP_Dropdown playerDropdown;
    
    public void OnClickPlayButton()
    {
        gameManager.Play();
    }

    public void UpdateRecordText(UserData userData)
    {
        int[] tmpScore = userData.userScores;
        recordText.text = $"Win : {tmpScore[0]} Loss : {tmpScore[1]}, Draw : {tmpScore[2]}";
    }

    public void OnClickQuitButton()
    {
        gameManager.Quit();
    }
    
    public void SetNpcChoice(Enums.DiceState state)
    {
        // SetImage(npcImage,state);
        gameManager.DicePlay.Npc.State = state;
        Debug.Log($"NPC : {state}");
    }

    public void OnValueChanged()
    {
        Enums.DiceState tmp = (Enums.DiceState)playerDropdown.value;
        // SetImage(npcImage,tmp);
        gameManager.DicePlay.Player.State = tmp;
        Debug.Log($"Player : {tmp}");

    }
    
    public void SetResultText(string text)
    {
        resultText.text = text;
    }

    public void playerButtonCanvasOnOff(bool isOn)
    {
        playerButtonCanvas.enabled = isOn;
    }

    public void SetStartCanvas()
    {
        if(playerImage && npcImage) ResetImage();
        
        playerButtonCanvasOnOff(false);
        PlayTextCanvasOnOff(false);
        lobbyCanvasOnOff(true);
    }
    
    public void SetDuringGameCanvas()
    {
        lobbyCanvasOnOff(false);
        PlayTextCanvasOnOff(true);
        playerButtonCanvasOnOff(true);
    }
    
    private void lobbyCanvasOnOff(bool isOn)
    {
        lobbyCanvas.enabled = isOn;
    }

    private void PlayTextCanvasOnOff(bool isOn)
    {
        resultText.text = "";
        playTextCanvas.enabled = isOn;
    }
    
    private void ResetImage()
    {
        npcImage.texture = null;
        playerImage.texture = null;
        
        playerDropdown.value = 0;

        npcImage.enabled = false;
        playerImage.enabled = false;
    }
}
