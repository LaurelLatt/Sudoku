using UnityEngine;

namespace States
{
    public interface IGameState
    {
        void Enter();      // What happens when entering this state
        void Exit();       // What happens when leaving this state
        void Update();     // Optional: update logic if needed
    }
}
