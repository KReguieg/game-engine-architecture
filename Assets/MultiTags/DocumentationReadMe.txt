MULTI TAGS v1.2
*********************************************************
Base75
*********************************************************

DOCUMENTATION
*********************************************************
*********************************************************
*********************************************************


IN EDITOR 
You can assign add the component MultiTags To any GameObject.
There you will be able to add Tags easily to the GameObject Currently Selected.
When you Create a new Tag it gets added to the Local Assigned Tags List AND the Global List.

The Global Project Tags list is so you can see while jumping 
between many GameObjects in the Editor all of the tags Currently being Used.
By using the Global Project Tags list you can quickly add tags without entering new names.
---------------------------------

AT RUNTIME
You have 3 main methods. They all extend gameObject functionality.
HasTag, AddTag, RemoveTag

You have 3 other lookup methods. 

*********************************************************
//this is the standard tag check, it will return true or false.
HasTag(string);

//USE CASE

void OnTriggerEnter2D(Collider2D col) 
{ 

   if (col.gameObject.HasTag ("Enemy")) { 

	//Magic here 

    } 
} 

*********************************************************

*********************************************************
//Add Tag will FIRST add the MultiTags Component to the game object if it does not exist,
//then it will add the tag that you pass through as a string.
AddTag(string);

//USE CASE


public void Start()
{

  gameObject.AddTag("Hero");
  
} 

*********************************************************

*********************************************************
//Remove Tag will check to see if the MultiTags component exists then the list if the tag exists then removes the tag from the list
RemoveTag(string);

//USE CASE


public void Death()
{

  gameObject.RemoveTag("Alive");
  
} 

*********************************************************



One more thing. When the Component becomes Awake() it will add the unity tag to the list. 
That way it lets you continue to use the Unity tag system. 





LOOKUPS


GameObject go = MultiTags.FindWithMultiTag("ElderBeast");

GameObject[] gos = MultiTags.FindGameObjectsWithMultiTag("Beast");

int amount = MultiTags.FindGameObjectsWithMultiTagCount("Beast");






*********************************************************
*********************************************************
*********************************************************