using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateText : MonoBehaviour
{
    [SerializeField] AnimationClip animClip;
    TextMeshPro stateText;
    [HideInInspector] public GameObject myGameObject;

    void Start()
    {
        myGameObject = gameObject;
        stateText = GetComponent<TextMeshPro>();

        myGameObject.SetActive(false);
    }

    public void Show_StateText(Define.GameState state)
    {
        switch (state)
        {
            case Define.GameState.Fever:
                stateText.text = "Fever!(x 2)";
                break;
            case Define.GameState.SuperFever:
                stateText.text = "Super Fever!(x 4)";
                break;
        }
        StartCoroutine(Hide_StateText());
    }

    IEnumerator Hide_StateText()
    {
        yield return Managers.Co.WaitSeconds(animClip.length);
        myGameObject.SetActive(false);
    }
}
