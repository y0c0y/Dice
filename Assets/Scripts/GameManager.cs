using UnityEngine;
using System.Collections;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public DataManager dataManager;
    public UIManager uiManager;
    public DicePlay DicePlay;
    
    public void Play()
    {   
        StartCoroutine(Stop());
    }

    public void Quit()
    {
        dataManager.JsonSave(DicePlay.Player.Scores);
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    
    void Start()
    {
        DicePlay = new DicePlay
        {
            Npc = new CharacterInfo(),
            Player = new CharacterInfo()
        };
        
        DicePlay.Player.Scores = dataManager.JsonLoad();
        
        uiManager.UpdateRecordText(DicePlay.Player.Scores);
        uiManager.SetStartCanvas();
    }

    private string OneTime()
    {
        Random random = new Random();
        Enums.DiceState randomValue = (Enums.DiceState)random.Next(1, 6);

        uiManager.SetNpcChoice(randomValue);
        
        Enums.Score result = DicePlay.CheckResult();
        
        DicePlay.ScoreUpdate(result);
        
        return DicePlay.ScoreString(result);
    }

    private IEnumerator Count(int count)
    {
        yield return new WaitForSeconds(0.5f);
        uiManager.resultText.text = TextData.NpcString;
        for (int i = 3; i >= 0; i--)
        {
            uiManager.resultText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator Stop()
    {
        uiManager.SetDuringGameCanvas(); //게임 화면 셋팅
        
        yield return Count(3); //선택 시간
        
        uiManager.playerButtonCanvasOnOff(false); // 버튼 삭제
        string tmp = OneTime(); // 한 게임 과정
        
        uiManager.SetResultText(tmp); // 결과 출력
        
        yield return new WaitForSeconds(2.0f); //결과 확인 시간
        
        uiManager.SetStartCanvas(); // 다시 로비 화면
        
        uiManager.UpdateRecordText(DicePlay.Player.Scores);
    }

}
