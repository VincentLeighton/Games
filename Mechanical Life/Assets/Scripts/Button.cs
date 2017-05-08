using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    private Button[] buttonArray;
    private Color grayedOut;
    public GameObject prefab;
    private Defender defender;
    public bool isSelected = false;
    public Level0 level;
    private bool isClickable = false;
    // Use this for initialization
    void Start()
    {
        buttonArray = GameObject.FindObjectsOfType<Button>();
        defender = prefab.GetComponent<Defender>();
        grayedOut = new Color(156, 157, 158, .5f);
        level = GameObject.Find("Canvas").GetComponent<Level0>();

		if(defender == null)
		{
			defender = prefab.GetComponentInChildren<Defender>();
		}
    }

    void Update()
    {
        CanAfford();
    }

    public Defender getDefender()
    {
        return defender;
    }

    private void CanAfford()
    {
		int currency = level.GetCurrency();
		Defender def = getDefender();
		if(def == null)
		{
			return;
		}
		
		if (def.Cost > currency)
		{
			isClickable = false;
			gameObject.GetComponentInChildren<SpriteRenderer>().color = grayedOut;
		}
		else
		{
			isClickable = true;
			gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
		}
    }

    void OnMouseDown()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.gray;
        isSelected = true;
        foreach (Button b in buttonArray)
        {
            if (b.name != name && b.isClickable)
            {
                b.GetComponentInChildren<SpriteRenderer>().color = grayedOut;
                b.isSelected = false;
            }
        }
    }

    public static GameObject SelectedDefenderPrefab()
    {
        Button[] temp = GameObject.FindObjectsOfType<Button>();
        GameObject selectedDefenderPrefab = null;
        foreach (Button b in temp)
        {
            if (b.isSelected)
            {
                selectedDefenderPrefab = b.prefab;
            }
        }
        return selectedDefenderPrefab;
    }
}
