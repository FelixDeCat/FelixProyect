
namespace AI.BTTest
{
    using UnityEngine;

    public class EnemyExample : MonoBehaviour
    {
        public Transform target;

        FieldOfView fov;
        Seek seek;
        PatrolWp patrol;

        Node root;

        void Start()
        {
            fov = GetComponent<FieldOfView>();
            seek = GetComponent<Seek>();
            patrol = GetComponent<PatrolWp>();

            root = new Selector
                (
                    new Sequence
                    (
                        new Leaf(_action: CanSeePlayerNode),
                        new Leaf(_action: () => Wait(2f), _onReset: () => timer = 0),
                        new Leaf(_action: FollowPlayerNode)
                    ),
                    new Leaf(PatrolNode)
                );
        }
        void Update()
        {
            root.Evaluate();

            //if (CanSeePlayer())
            //{
            //    FollowPlayer();
            //}
            //else
            //{
            //    Vector3 next = patrol.GetWp(transform.position);
            //    seek.DoSeek(next);
            //}
        }

        #region Acciones
        public bool CanSeePlayer()
        {
            return fov.FOV(target);
        }
        public void FollowPlayer()
        {
            seek.DoSeek(target);
        }

        public void Patrol()
        {
            Vector3 next = patrol.GetWp(transform.position);
            seek.DoSeek(next);
        }
        #endregion

        #region Acciones con Status
        public Status CanSeePlayerNode()
        {
            return fov.FOV(target) ? Status.sucess : Status.failure;
        }
        public Status FollowPlayerNode()
        {
            if (CanSeePlayer())
            {
                seek.DoSeek(target);
                return Status.running;
            }
            else
            {
                return Status.failure;
            }
        }
        public Status PatrolNode()
        {
            if (CanSeePlayer())
            {
                return Status.failure;
            }
            else
            {
                Vector3 next = patrol.GetWp(transform.position);
                seek.DoSeek(next);
                return Status.running;
            }
        }

        float timer = 0;
        public Status Wait(float toWait)
        {
            timer += Time.deltaTime;

            if (timer < toWait)
            {
                return Status.running;
            }
            return Status.sucess;
        }
        #endregion
    }

}
