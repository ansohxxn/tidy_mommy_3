using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class ComboText : MonoBehaviour
{
    [SerializeField] AnimationClip animClip;
    TextMeshPro comboText;
    [HideInInspector] public Transform myTransform;
    [HideInInspector] public GameObject myGameObject;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        myGameObject = gameObject;
        comboText = GetComponent<TextMeshPro>();

        myGameObject.SetActive(false);
    }

    public void Show_ComboText(Vector2 pos)
    {
        comboText.text = Managers.Game.combo.ToString() + " Combo";
        myTransform.position = pos;

        StartCoroutine(Hide_ComboText());
    }

    IEnumerator Hide_ComboText()
    {
        yield return Managers.Co.WaitSeconds(animClip.length);
        myGameObject.SetActive(false);
    }
}
