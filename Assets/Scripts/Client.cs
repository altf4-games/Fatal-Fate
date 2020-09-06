using UnityEngine;
using SimpleFirebaseUnity;
using System.Text.RegularExpressions;

public class Client : MonoBehaviour
{
    private protected int OPTION_A;
    private protected int OPTION_B;
    public static Client instance;
    Firebase firebase = Firebase.CreateNew("https://game-analytics-ab746.firebaseio.com/");

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance != null)
            instance = this;
        firebase.OnGetSuccess += GetOKHandler;
        firebase.OnGetFailed += GetFailHandler;
    }

    private void Start()
    {
        RetriveData();
    }

    public void PostToDatabase(string optionType)
    {
        ++OPTION_A; ++OPTION_B; //Both are incremented but only one is sent to server
        string data = (optionType == "Option-A") ? OPTION_A.ToString() : OPTION_B.ToString();
        firebase.Child(optionType).SetValue(data, optionType);
    }

    public void RetriveData()
    {
        firebase.GetValue("https://game-analytics-ab746.firebaseio.com/");
    }

    private void GetOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        string data = snapshot.RawJson;
        data = data.Replace("\"", "");
        data = Regex.Replace(data, "[A-Z a-z {}:-]", "");
        string[] parsedData = data.Split(',');
        OPTION_A = int.Parse(parsedData[0]);
        OPTION_B = int.Parse(parsedData[1]);
    }

    private void GetFailHandler(Firebase sender, FirebaseError err)
    {
        Debug.LogError("[ERR] Get from key: <" + sender.FullKey + ">,  " + err.Message + " (" + (int)err.Status + ")");
    }
}
