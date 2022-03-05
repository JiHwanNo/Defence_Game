using UnityEngine;

public abstract class UI_Base : MonoBehaviour
{
    public GAMESTATE m_GameState = GAMESTATE.None;

    public abstract void StartUI();

    public abstract void EndUI();

    public virtual void OnDestroy()
    {
        EndUI();
    }
}
