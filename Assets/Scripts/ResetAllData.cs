using UnityEngine;

public class ResetAllData : MonoBehaviour
{
    public void ResetShopAndBuffs()
    {

        string[] itemIDs = { "buff_click_1", "buff_click_2", "buff_click_3", "buff_click_4", "buff_click_5" };
        foreach (string id in itemIDs)
        {
            PlayerPrefs.DeleteKey($"shop_item_{id}_purchased");
        }


        PlayerPrefs.DeleteKey("coins");
        PlayerPrefs.DeleteKey("likes");
        PlayerPrefs.DeleteKey("followers");

        PlayerPrefs.Save();

        if (BuffManager.Instance != null)
        {
            BuffManager.Instance.ResetBuffs();
        }

 
        var clickManager = FindObjectOfType<ClickManagerForStream>();
        if (clickManager != null)
        {
            clickManager.ResetData();
        }

        Debug.Log("Всі покупки, баффи і ресурси скинуті!");

    }

}
