#region Usings
using System.Collections.Generic;
using System.Collections;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Describe your class quickly here.
/// </summary>
/// <remarks>
/// Author: Khaled Reguieg E-Mail: Khaled.Reguieg@artcom.de
/// </remarks>
#endregion
public class BuildButtonMenu : MonoBehaviour
{
	private List<Button> buildMenuItems  = new List<Button>();
	private Vector2[] menuItemsTargetPositions;
	private Vector2[] menuItemStartPositions;
	public GameObject Buttons;

	[Range(-200.0f, .0f)]
	public float radius = -125.0f;
    private bool expand = false;
	public AnimationCurve AnimationCurve;
    private float startColor;
    private float endColor;
    private bool expandAnimation;
	private Color c;
	private float timer = 0;

	[SerializeField]
	private GameObject towerPlacer;

    // Use this for initialization
    private void Start ()
	{
		buildMenuItems = Buttons.GetComponentsInChildren<Button>(true)
							.Where(x => x.gameObject.transform.parent != transform.parent).ToList();

		menuItemsTargetPositions = new Vector2[buildMenuItems.Count];
		menuItemStartPositions = new Vector2[buildMenuItems.Count];
		GetComponent<Button>().onClick.AddListener( () => {ClickHandler();} );
		GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
	}

    private void ClickHandler()
    {
		towerPlacer.GetComponent<TowerPlacer>().HandleNewObjectHotkey();
        ExpandMenu();
    }

    private void Update()
    {
        ButtonAnimation();
    }

    private void ButtonAnimation()
    {
        
        timer += Time.deltaTime;
        if (timer <= AnimationCurve.keys[1].time)
		{
			int i = 0;
            foreach (Button menuItem in buildMenuItems)
            {
                menuItem.gameObject.transform.localPosition = Vector2.Lerp(menuItemStartPositions[i], menuItemsTargetPositions[i], AnimationCurve.Evaluate(timer));
                c.a = Mathf.Lerp(startColor, endColor, AnimationCurve.Evaluate(timer));
                menuItem.gameObject.GetComponent<Image>().color = c;
                i++;
            }
        }
        else
        {
            if (!expand)
                buildMenuItems.ForEach(x => { x.gameObject.SetActive(expand); });
        }
    }

    public void ExpandMenu() 
	{
		timer = Mathf.Clamp01(1-timer);
		expand = !expand;
		
		startColor = expand?0:1;
		endColor = expand?1:0;
		c = new Color(1, 1, 1, startColor);
		
		for (int i = 0; i < buildMenuItems.Count; i++)
		{
			menuItemStartPositions[i] = buildMenuItems[i].transform.localPosition;	
		}
		float angle = -90.0f /(buildMenuItems.Count - 1) * Mathf.Deg2Rad;
		for (int i = 0; i < buildMenuItems.Count; i++)
		{	
			if(expand)
			{
					float xPos = Mathf.Cos(angle * i) * radius;
					float yPos = Mathf.Sin(angle * i) * radius;
					menuItemsTargetPositions[i] = new Vector2(xPos, yPos);
					buildMenuItems[i].gameObject.SetActive(expand);		
			}
			else 
				menuItemsTargetPositions[i] = Vector2.zero;
		}
	}
}
