#region Usings
	using System.Collections.Generic;
	using System.Collections;
	using System.Linq;

	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// Describe your class quickly here.
	/// </summary>
	/// <remarks>
	/// Author: Khaled Reguieg E-Mail: Khaled.Reguieg@artcom.de
	/// </remarks>
#endregion
public class BuildButtonMenu : MonoBehaviour
{
	List<Button> buildMenuItems  = new List<Button>();
	Vector2[] buildMenuItemsPositions;

	[Range(-200.0f, .0f)]
	public float radius = -125.0f;
    private bool expand = false;
	public AnimationCurve AnimationCurve;

	[Range(0.1f, 100.0f)]
    public float speed;

    // Use this for initialization
    private void Start ()
	{
		buildMenuItems = GetComponentsInChildren<Button>(true)
							.Where(x => x.gameObject.transform.parent != transform.parent).ToList();

		buildMenuItemsPositions = new Vector2[buildMenuItems.Count];

		GetComponent<Button>().onClick.AddListener( () => {ExpandMenu();} );
		GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

	}

	public void ExpandMenu() 
	{
		expand = !expand;
		float angle = -90.0f /(buildMenuItems.Count - 1) * Mathf.Deg2Rad;
		for (int i = 0; i < buildMenuItems.Count; i++)
		{
			if(expand) {
					float xPos = Mathf.Cos(angle * i) * radius;
					float yPos = Mathf.Sin(angle * i) * radius;
					buildMenuItemsPositions[i] = new Vector2(transform.position.x + xPos, transform.position.y + yPos);
					//buildMenuItems[i].transform.position = buildMenuItemsPositions[i];
			}
			else
			{
				buildMenuItemsPositions[i] = transform.position;
				//buildMenuItems[i].transform.position = buildMenuItemsPositions[i];
			}
		}

		StartCoroutine(AnimateBuildMenuItems());
	}

	private IEnumerator AnimateBuildMenuItems() 
	{
		float timer = 0;
		while(timer <= AnimationCurve.length){
			int i = 0;
			timer += Time.deltaTime;
			foreach (Button menuItem in buildMenuItems) {
				menuItem.gameObject.transform.position = Vector2.Lerp(menuItem.gameObject.transform.position, buildMenuItemsPositions[i], AnimationCurve.Evaluate(timer));
				i++;
			}
			yield return new WaitForSeconds(0.01f);			
		}
	}
}
