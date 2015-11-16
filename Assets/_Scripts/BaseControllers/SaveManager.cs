using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SaveManager {
    private static List<string> listLevel = new List<string>();
    static SaveManager()
    {
        listLevel = new List<string>();
        listLevel.Add("Level_01");
        listLevel.Add("Level_02");
        listLevel.Add("Level_03");
        listLevel.Add("Level_04");
        listLevel.Add("Level_05");
        listLevel.Add("Level_06");
        listLevel.Add("Level_07");
        listLevel.Add("Level_08");
        listLevel.Add("Level_09");
        listLevel.Add("Level_10");
    }

    public static void SaveStarLevel(int level,int star)
    {
        PlayerPrefs.SetInt(listLevel[level - 1], star);
    }

    public static int GetAllStar()
    {
        int total = 0;
        foreach (var item in listLevel)
            total += PlayerPrefs.GetInt(item,0);
        return total;
    }
	
}
