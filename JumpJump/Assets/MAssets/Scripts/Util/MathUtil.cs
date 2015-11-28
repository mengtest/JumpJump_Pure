using UnityEngine;
using System.Collections;

public class MathUtil
{
	
	#region 3d random
	
	public static Vector3 GetRandomDirInCone(Vector3 startPot,Vector3 dir,float h,float r){
		return GetRandomDirInCone(startPot,startPot+dir*h,r);
	}
	
	public static Vector3 GetRandomDirInCone(Vector3 startPot,Vector3 endPot,float r){
		Vector3 n=endPot-startPot;
		n.Normalize();
		float d=Vector3.Dot(n,endPot);
		Vector3 randomPot;
		if(n.x==0 && n.y==0 && n.z==0) return (Vector3.zero-startPot).normalized;
		
		if(n.y==0 && n.z==0) {
			randomPot.x=d/n.x;
			randomPot.y=1;
			randomPot.z=1;
		}else if(n.x==0 && n.z==0){
			randomPot.x=1;
			randomPot.y=d/n.y;
			randomPot.z=1;
		}else if(n.x==0 && n.y==0){
			randomPot.x=1;
			randomPot.y=1;
			randomPot.z=d/n.z;
		}else {
			randomPot.x=1;
			randomPot.y=1;
			randomPot.z=(d-randomPot.x*n.x-randomPot.y*n.y)/n.z;
		}
		
		Vector3 axisX=(randomPot-endPot).normalized;
		Vector3 axisY=Vector3.Cross(n,axisX);
		
		float randomAngle=Random.Range(0,360)*Mathf.Deg2Rad;
		float randomR=Random.Range(0,r);
		
		Vector3 offset=randomR*Mathf.Cos(randomAngle)*axisX+randomR*Mathf.Sin (randomR)*axisY;
		
		Vector3  result=endPot+offset;
		return (result-startPot).normalized;	
	}
	
	
	#endregion 
	
	#region tangle
	
	public static float ClampAngleInPI(float angle){
		angle%=360;
		if(angle>180) angle=-360+angle;
		if(angle<-180) angle=360+angle;
		return angle;
	}
	#endregion
	
	#region swap
	//swap
	public static void Swap(ref float a,ref float b){
		float tmp=a;
		a=b;
		b=tmp;
	}
	#endregion
	
	#region rotate
	// rotate
	public static Vector3 RotateOnUp (Vector3 original, float angle)
	{
		angle *= Mathf.Deg2Rad;
		Vector3 r; 
		r.x = original.x * Mathf.Cos (angle) - original.z * Mathf.Sin (angle);
		r.y = original.y;
		r.z = original.x * Mathf.Sin (angle) + original.z * Mathf.Cos (angle);
		r.Normalize ();
		return r;
	}
	
	public static float AngleAroundAxis (Vector3 dirA, Vector3 dirB, Vector3 axis)
	{
		dirA = dirA - Vector3.Project (dirA, axis);
		dirB = dirB - Vector3.Project (dirB, axis);
		float angle = Vector3.Angle (dirA, dirB);
		return angle * (Vector3.Dot (axis, Vector3.Cross (dirA, dirB)) < 0 ? -1 : 1);
	}
	#endregion
	
	
	#region probability
	//probability
	public static float EPSILON=0.00001f;
	public static int getRandomByProbability (float[] probabilityList)
	{
		int length = probabilityList.Length;
		float sumR = 0;
		float r=Random.Range (0, 1f);
		for (int i=0; i<length; i++) {
			sumR += probabilityList [i];
			if (sumR > r)
				return i;
		}
		if(Mathf.Abs(sumR-1)>EPSILON) return -1; // check the sum is 1
		return length-1;
	}
	
	public static bool IndependentProbability(float p){ 
		return Random.Range(0f,1f)<p;
	}
	#endregion
	
	
	#region line intersection
	// discard the parallel 
	public static  bool LineIntersection2D(Vector2 p0,Vector2 p1,Vector2 p2,Vector2 p3,ref Vector2 intersectPoint){

		if(Cross(p1.x-p0.x,p1.y-p0.y,p3.x-p2.x,p3.y-p2.y)==0) return false; 
		
		float area_p012=area(p0,p1,p2);
		float area_p013=area(p0,p1,p3);
		if(area_p012*area_p013>0) return false;
		
		float area_p023=area(p0,p2,p3);
		float area_p123=area(p1,p2,p3);
		if(area_p023*area_p123>0) return false;
		
		
		float t=Mathf.Abs(area_p023)/(Mathf.Abs(area_p023)+Mathf.Abs(area_p123));
		intersectPoint=Vector2.Lerp(p0,p1,t);
		
		return true;
	}
	
	public static bool LineIntersect2D(Vector2 p0,Vector2 p1,Vector2 p2,Vector2 p3){
		if(Cross(p1.x-p0.x,p1.y-p0.y,p3.x-p2.x,p3.y-p2.y)==0) return false; 
		
		float area_p012=area(p0,p1,p2);
		float area_p013=area(p0,p1,p3);
		if(area_p012*area_p013>0) return false;
		
		float area_p023=area(p0,p2,p3);
		float area_p123=area(p1,p2,p3);
		if(area_p023*area_p123>0) return false;
		return true;
	}

	
	public static  float area(Vector2 p0,Vector2 p1,Vector2 p2){
		return Cross(p1.x-p0.x,p1.y-p0.y,p2.x-p0.x,p2.y-p0.y);
	}


	public static  float Cross(float x1,float y1,float x2,float y2){
		return x1*y2-x2*y1;
	}
	#endregion
	
	
	
	#region In Quadrangle
	
	public static bool pInQuadrangle(Vector2 [] pots,Vector2 p){
		return pInQuadrangle(pots[0],pots[1],pots[2],pots[3],p);
	}
	
    public static bool pInQuadrangle(Vector2 a, Vector2 b, Vector2 c, Vector2 d,  
            Vector2 p) {  
        float dTriangle = triangleArea(a, b, p) + triangleArea(b, c, p)  
                + triangleArea(c, d, p) + triangleArea(d, a, p);  
        float dQuadrangle = triangleArea(a, b, c) + triangleArea(c, d, a);  
        return dTriangle == dQuadrangle;  
    }  
  
    private static float triangleArea(Vector2 a, Vector2 b, Vector2 c) {  
        return Mathf.Abs((a.x * b.y + b.x * c.y + c.x * a.y - b.x * a.y  
                - c.x * b.y - a.x * c.y) *0.5f);  
    }  
	#endregion
	
	

	
	
	
}
