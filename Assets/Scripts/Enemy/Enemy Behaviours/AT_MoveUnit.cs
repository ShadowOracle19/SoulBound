using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions{

	public class AT_MoveUnit : ActionTask{

        private bool activeOnce = false;
        private Blackboard _blackboard;
        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit(){
            _blackboard = agent.GetComponent<Blackboard>();

            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute(){
            
            if (agent.GetComponent<Enemy>().CheckIfAgentInRange() != null)
            {
                _blackboard.SetVariableValue("TargetedAgent", agent.GetComponent<Enemy>().CheckIfAgentInRange());

                CombatManager.Instance.EndTurn(agent.GetComponent<Token>());
                EndAction(true);

            }
            else if (agent.GetComponent<Enemy>().CheckIfAgentInRange() == null)
            {
                StartCoroutine(Move());
            }

        }

        IEnumerator Move()
        {

            _blackboard.SetVariableValue("TargetedAgent", agent.GetComponent<Enemy>().FindClosestAgent());


            for (int i = 0; i < 4; i++)
            {
                Vector3 dir = (agent.transform.position - _blackboard.GetVariableValue<Agent>("TargetedAgent").transform.position).normalized;
                int x = Mathf.RoundToInt(dir.x);
                int z = Mathf.RoundToInt(dir.z);

                if (Vector3.Distance(agent.transform.position, _blackboard.GetVariableValue<Agent>("TargetedAgent").transform.position) <= 1)//if the agent is within 1 cube of the target they will stop
                {
                    CombatManager.Instance.EndTurn(agent.GetComponent<Token>());
                    Debug.Log("arrived at agent");
                    yield return null;
                    EndAction(true);
                    break;
                    
                }

                if(GridManager.Instance.GetTileAtPosition(new Vector3(agent.transform.position.x - x, (int)agent.transform.position.z - z)).objectOnTile)//if the tile they are about to step on as an object select another
                {

                }

                if(x != 0)//this will allow the bots to favor going on the x axis before the z axis
                {
                    z = 0;
                }
                agent.transform.position = new Vector3(agent.transform.position.x - x, agent.transform.position.y, (int)agent.transform.position.z - z);

                yield return new WaitForSeconds(0.5f);
            }


            yield return null;

            //agent.transform.position = Vector3.MoveTowards(agent.transform.position, _blackboard.GetVariableValue<Agent>("TargetedAgent").transform.position, 4);
            //agent.transform.position = new Vector3((int)agent.transform.position.x, agent.transform.position.y, (int)agent.transform.position.z);

            CombatManager.Instance.EndTurn(agent.GetComponent<Token>());
            EndAction(true);
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate(){
            

        }

		//Called when the task is disabled.
		protected override void OnStop(){
			
		}

		//Called when the task is paused.
		protected override void OnPause(){
			
		}
	}
}