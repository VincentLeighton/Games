using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGame : MonoBehaviour
{

    public Camera Camera;
    public Level0 level;
	GameObject defenders;
    // Use this for initialization
    void Start()
    {
		//max x675 y500
		//min x135 y135
    }

	void Awake(){
		defenders = GameObject.Find("Defenders");
    }

    void OnMouseDown()
    {
        GameObject defenderPrefab = Button.SelectedDefenderPrefab();

        if (defenderPrefab != null)
        {
            Vector2 gridCoords = CalcWorldpointofMouseClick();
			Vector2 worldCoords = ConvertCoordstoWorldPosition(gridCoords);
            GameObject positioner = Instantiate(defenderPrefab, worldCoords, Quaternion.identity);
            Defender btnDef = positioner.GetComponent<Defender>();
            if(btnDef == null)
            {
                btnDef = positioner.GetComponentInChildren<Defender>();
            }
            level.SpendCurrency(btnDef.Cost);
            positioner.transform.SetParent(defenders.transform);
        }
        //Debug.Log(Camera.WorldToScreenPoint(Input.mousePosition));
        //Debug.Log(Camera.WorldToViewportPoint(Input.mousePosition));
        //Debug.Log(Camera.ScreenToViewportPoint(Input.mousePosition));
    }

    Vector2 CalcWorldpointofMouseClick()
    {
        float xWidth = (705 - 108) / 10;
        float yHeight = (529 - 80) / 5;

        Vector2 temp = Camera.ScreenToWorldPoint(Input.mousePosition);
        temp = new Vector2(temp.x - 108, temp.y - 80);

        float tempx = temp.x / xWidth;
        float tempy = temp.y / yHeight;

        int xfloor = Mathf.FloorToInt(tempx);
        int yfloor = Mathf.FloorToInt(tempy);

        temp = new Vector2(Mathf.Clamp(xfloor, 0, 9), Mathf.Clamp(yfloor, 0, 4));

        return temp;
    }

	Vector2 ConvertCoordstoWorldPosition(Vector2 gridCoords){
		float magicX = (675 - 135) / 9;
		float magicY = (500 - 135) / 4;
		return new Vector2(135 + (gridCoords.x * magicX), 135 + (gridCoords.y * magicY));
	}
}
