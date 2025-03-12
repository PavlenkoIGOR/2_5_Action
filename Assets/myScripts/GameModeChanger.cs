using System;

public class GameModeChanger
{
    public string gameMode = "FreeWalkMode";

    public Action<string> changer;

    public void ChangeMode()
    {
        if (gameMode == "FreeWalkMode")
        {
            gameMode = "CombatMode";
            //Debug.Log($"mode {gameMode}");
        }
        else if (gameMode == "CombatMode")
        {
            //Debug.Log($"mode {gameMode}");
            gameMode = "FreeWalkMode";
        }

        
    }

    private void OnModeChange() 
    {
        changer?.Invoke(gameMode);
    }
}
