using UnityEngine;

namespace Platformer
{
    public class Goblin : Monster
    {
        private float timer;
        private float idleTime = 3f;
        private float patrolTime = 5f;
        private float attackTime;

        private float traceDistance = 5f;
        private float attackDistance = 2f;

        void Start()
        {
            Init();
            attackTime = monsterData.attackTime;
        }

        #region 대기 기능
        protected override void Idle()
        {
            timer += Time.deltaTime;
            if (timer >= idleTime)
            {
                timer = 0f;
                patrolTime = Random.Range(0f, 5f);

                int ranNumber = Random.Range(0, 2); // 0 , 1

                if (ranNumber == 0)
                    moveDir = -1;
                else if (ranNumber == 1)
                    moveDir = 1;

                // moveDir = Random.Range(0, 2) == 0 ? -1 : 1;

                transform.localScale = new Vector3(moveDir, 1, 1);
                hpSlider.transform.parent.localScale = new Vector3(moveDir, 1, 1);
                
                anim.SetBool("IsRun", true);
                ChangeState(MonsterState.Patrol);
            }

            if (distance <= traceDistance)
            {
                Vector2 targetDir = (target.position - transform.position).normalized;

                Vector2 lookDir = new Vector2(moveDir, 0);
                float angle = Vector2.Angle(targetDir, lookDir);

                if (angle >= 0f && angle <= 40f)
                {
                    anim.SetBool("IsRun", true);
                    ChangeState(MonsterState.Trace);
                }
            }
        }
        #endregion


        #region 정찰 기능
        protected override void Patrol()
        {
            transform.position += Vector3.right * moveDir * moveSpeed * Time.deltaTime;

            timer += Time.deltaTime;
            if (timer >= patrolTime)
            {
                timer = 0f;
                idleTime = Random.Range(0f, 5f);

                anim.SetBool("IsRun", false);
                ChangeState(MonsterState.Idle);
            }

            if (distance <= traceDistance)
            {
                Vector2 targetDir = (target.position - transform.position).normalized;

                Vector2 lookDir = new Vector2(moveDir, 0);
                float angle = Vector2.Angle(targetDir, lookDir);

                if (angle >= 0f && angle <= 40f)
                {
                    anim.SetBool("IsRun", true);
                    ChangeState(MonsterState.Trace);
                }
            }
        }
        #endregion


        #region 추격 기능
        protected override void Trace()
        {
            Vector2 targetDir = (target.position - transform.position).normalized;

            transform.position += Vector3.right * targetDir.x * moveSpeed * Time.deltaTime;

            if (targetDir.x > 0)
                moveDir = 1;
            else if (targetDir.x < 0)
                moveDir = -1;

            transform.localScale = new Vector3(moveDir, 1, 1);
            hpSlider.transform.parent.localScale = new Vector3(moveDir, 1, 1);

            
            if (distance <= attackDistance)
            {
                timer = attackTime;
                anim.SetBool("IsRun", false);
                ChangeState(MonsterState.Attack);
            }

            if (distance > traceDistance)
            {
                timer = 0f;
                idleTime = Random.Range(0f, 5f);

                anim.SetBool("IsRun", false);
                ChangeState(MonsterState.Idle);
            }
        }
        #endregion


        #region 공격 기능
        protected override void Attack()
        {
            timer += Time.deltaTime;
            if (timer >= attackTime)
            {
                timer = 0f;
                anim.SetTrigger("Attack");
            }

            if (distance > attackDistance)
            {
                anim.SetBool("IsRun", true);
                ChangeState(MonsterState.Trace);
            }
        }
        #endregion
    }
}