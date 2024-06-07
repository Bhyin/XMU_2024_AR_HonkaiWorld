using System.Collections.Generic;
using UnityEngine;

public class PanelItems
{
    public string name;
    public string prefabPath;

    public static PanelItems[] items = new PanelItems[]
    {
        new PanelItems{name=start_StartPanel, prefabPath="UIPanelPrefabs/Start/StartPanel"},
        new PanelItems{name=game_SetAnchorPanel, prefabPath="UIPanelPrefabs/Game/SetAnchorPanel"},
        new PanelItems{name=game_SetMapPanel, prefabPath="UIPanelPrefabs/Game/SetMapPanel"},
        new PanelItems{name=game_GamePanel, prefabPath="UIPanelPrefabs/Game/GamePanel"},
        new PanelItems{name=game_PausePanel, prefabPath="UIPanelPrefabs/Game/PausePanel"},
        //new PanelIetm{name=game_SettingPanel, prefabPath="UIPanelPrefabs/Game/"}
    };

    public static string start_StartPanel => "start_StartPanel";
    public static string game_SetAnchorPanel => "game_SetAnchorPanel";
    public static string game_SetMapPanel => "game_SetMapPanel";
    public static string game_GamePanel => "game_GamePanel";
    public static string game_PausePanel => "game_PausePanel";
    public static string game_SettingPanel => "game_SettingPanel";
}

public class PanelManager
{
    static Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

    public static void LoadPanels()
    {
        if (prefabDict == null) prefabDict = new Dictionary<string, GameObject>();

        if (prefabDict.Count == PanelItems.items.Length) return;

        prefabDict.Clear();
        foreach (PanelItems item in PanelItems.items)
        {
            prefabDict.Add(item.name, Resources.Load<GameObject>(item.prefabPath));
        }
    }

    public static void ReleasePanels()
    {
        prefabDict?.Clear();
        prefabDict = null;
    }

    public static GameObject GetPanel(string name)
    {
        GameObject panel = null;
        prefabDict.TryGetValue(name, out panel);
        if(panel != null)panel.name = name;
        return panel;
    }

}
