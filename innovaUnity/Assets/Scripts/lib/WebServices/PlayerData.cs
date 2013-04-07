using System.Collections;
using Boomlagoon.JSON;

public class PlayerData {
    private static PlayerData instance;
    private JSONObject data;

    public static JSONObject Data
    {
        get {
            if (instance == null)
            {
                return null;
            }
            else
            {
                return instance.data;
            }
        }
        set {
            if (instance == null)
            {
                instance = new PlayerData(value);
            }
            else
            {
                instance.data = value;
            }
        }
    }

    public static bool hasInstance(){
        return instance != null;
    }

    public static void clearData() {
        instance = null;
    }

    public PlayerData(JSONObject data) {
        this.data = data;
    }
}
