using System.Threading;
namespace GameServer.Models.EnemyStates
{
    abstract class StateChange
    {
        protected StateChange next = null;

        public StateChange setNext(StateChange stateChange)
        {
            StateChange last = this;

            while (last.next != null)
            {
                last = last.next;
            }

            last.next = stateChange;

            return this;
        }

        public void ChangeState(IEnemy enemy)
        {
            InitiateState(enemy);

            if (next != null)
            {
                Thread.Sleep(Config.ENEMYSTATECHANGETIME);
                next.ChangeState(enemy);
            }
        }

        protected abstract void InitiateState(IEnemy enemy);
    }
}
