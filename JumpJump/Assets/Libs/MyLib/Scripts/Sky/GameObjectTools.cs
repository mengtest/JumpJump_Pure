using UnityEngine;
using System.Collections;

static public class GameObjectTools
{
	///////////////////////////////////////////////////////////
	// Essentially a reimplementation of 
	// GameObject.GetComponentInChildren< T >()
	// Major difference is that this DOES NOT skip deactivated 
	// game objects
	///////////////////////////////////////////////////////////
	static public TType GetComponentInChildren< TType  >( GameObject objRoot ) where TType : Component
	{
		// if we don't find the component in this object 
		// recursively iterate children until we do
		TType tRetComponent = objRoot.GetComponent< TType >();                
		
		if( null == tRetComponent )
		{
			// transform is what makes the hierarchy of GameObjects, so 
			// need to access it to iterate children
			Transform     trnsRoot        = objRoot.transform;
			int         iNumChildren     = trnsRoot.childCount;
			
			// could have used foreach(), but it causes GC churn
			for( int iChild = 0; iChild < iNumChildren; ++iChild )
			{
				// recursive call to this function for each child
				// break out of the loop and return as soon as we find 
				// a component of the specified type
				tRetComponent = GetComponentInChildren< TType >( trnsRoot.GetChild( iChild ).gameObject );
				if( null != tRetComponent )
				{
					break;
				}
			}
		}
		
		return tRetComponent;
	}
}
