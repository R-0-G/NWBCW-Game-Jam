using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
	public List<Resource> resources; //todo this has to be a scriptable object because for smoe reason the LTS version of unity doesnt support lists in inspector? first element is always broken (not just custom drawers for me ...)
									 // see https://issuetracker.unity3d.com/issues/first-array-element-expansion-is-broken-for-arrays-that-use-custom-property-drawer
}
