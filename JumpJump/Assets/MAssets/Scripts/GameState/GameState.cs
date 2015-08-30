
public class GameState  {

	static GameState g_Instance;

	public static void Init(){
		Instance();
	}

	public static GameState Instance(){
		if(g_Instance==null) g_Instance=new GameState(); 
		return g_Instance;
	}


	private GameState(){
		M_PlayState=PlayState.EMPTY;
		M_GameResultState=GameResultState.EMPTY;
		M_SceneState=SceneState.MENUSCENE;
	}


	public PlayState M_PlayState{
		get ;
		set;
	}

	public GameResultState  M_GameResultState{
		get ;
		set;
	}

	public SceneState M_SceneState{
		get ;
		set;
	}



}


public enum PlayState  {
	EMPTY=-1,
	READY=0,
	PLAY=1,
	PAUSE=2,
	OVER=3
}

public enum GameResultState{
	EMPTY=-1,
	VICTORY=0,
	FAILED=1,
	OTHER=2
}


public enum SceneState{
	EMPTY=-1,
	MENUSCENE=0,
	PLAYSCENE=1
}
