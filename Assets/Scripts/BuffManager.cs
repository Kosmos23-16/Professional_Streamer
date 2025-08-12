using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance;

    public int clickLikeBonus = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyBuff(string buffID)
    {
        if (buffID == "buff_click_1")
            clickLikeBonus += 1;
        else if (buffID == "buff_click_2")
            clickLikeBonus += 2;
        else if (buffID == "buff_click_3")
            clickLikeBonus += 3;
    }

    public void ResetBuffs()
    {
        clickLikeBonus = 0;
    }
}
