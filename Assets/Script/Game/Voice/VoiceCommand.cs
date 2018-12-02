using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine.Networking;

public class VoiceCommand : NetworkBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    private bool MusicOn = false;
    public GameObject Newobject;
    public GameObject Markerobject;

    [HideInInspector]
    public bool check = false;
    // Use this for initialization
    void Start()
    {
        //말할단어

        keywords.Add("Play", () =>
        {
            if (!check)
            {
                CustomMessage.Instance.SendCreatePhan(Newobject.transform.position);
                Newobject.SetActive(true);
                Newobject.transform.position = Camera.main.transform.position + (Camera.main.transform.forward+ new Vector3(0, -5f, 18f));//Camera.main.transform.forward;
                Newobject.transform.localEulerAngles = new Vector3(-25f, 0f, 0f);
                check = true;
            }
        }
      );


        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!check)
            {
                //CustomMessage.Instance.SendCreatePhan(Newobject.transform.position);
                Newobject.SetActive(true);
                Newobject.transform.position = Camera.main.transform.position + (Camera.main.transform.forward + new Vector3(0, -5f, 18f));//Camera.main.transform.forward;
                Newobject.transform.localEulerAngles = new Vector3(-25f, 0f, 0f);
                check = true;
            }
        }
    }
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    public void checkcall()
    {
        //keywords.Add("Play", () =>
        //{
        //    if (!check)
        //    {
        //        CustomMessage.Instance.SendCreatePhan(Newobject.transform.position);
        //        Newobject.SetActive(true);
        //        Newobject.transform.position = Markerobject.transform.position;
        //        Newobject.transform.localEulerAngles = new Vector3(-25f, 0f, 0f);
        //        check = true;
        //    }
        //}
      //);


        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();

    }
}