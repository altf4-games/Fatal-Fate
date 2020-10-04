using UnityEngine;
using SimpleFirebaseUnity;
using System.Text.RegularExpressions;

public class Client : MonoBehaviour
{
    private protected int OPTION_A;
    private protected int OPTION_B;
    private protected string type;
    public static Client instance;
    protected Firebase firebase = Firebase.CreateNew("https://game-analytics-ab746.firebaseio.com/");

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
        type = optionType[optionType.Length - 1].ToString();
        bool canPost = (int.Parse(SaveData.data.ReadData().Item2) == 0) ? true : false;
        if(canPost)
        {
            ++OPTION_A; ++OPTION_B; //Both are incremented but only one is sent to server
            string data = (optionType == "Option-A") ? OPTION_A.ToString() : OPTION_B.ToString();
            firebase.Child(optionType).SetValue(data, optionType);
            SaveData.data.WriteData(type[0], 2147483647);
        }
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
        if(GetComponent<EndScreen>().isActive) {
            GetComponent<EndScreen>().GetFinalPercentage(OPTION_A,OPTION_B, type[0]);
        }
    }

    private void GetFailHandler(Firebase sender, FirebaseError err)
    {
        Debug.LogError("[ERR] Get from key: <" + sender.FullKey + ">,  " + err.Message + " (" + (int)err.Status + ")");
    }
}
