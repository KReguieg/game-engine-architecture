//small script that can be used to rotate an element from the turrets as needed

public var rotationSpeed : float = 100;
public var direction : Vector3 = Vector3(0,0,1);

function Start () {

}

function Update () {
transform.Rotate(direction * rotationSpeed*Time.deltaTime);
}