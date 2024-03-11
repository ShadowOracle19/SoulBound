using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions{

	public class AT_UseAbility : ActionTask{
		private Blackboard _blackboard;
		public int numberOfAbilityUses = 0;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit()
        {
            _blackboard = agent.GetComponent<Blackboard>();
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute(){
			
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate(){
            if (_blackboard.GetVariableValue<Agent>("TargetedAgent") != null && agent.GetComponent<Enemy>().CheckIfAgentInRange() != null)
            {
                if (numberOfAbilityUses == 3)
                {
                    //use bigger melee
                    for (int i = 0; i < agent.GetComponent<Enemy>().stats.ability2.numberOfHits; i++)
                    {
                        Debug.Log("Attacking agent");
                        _blackboard.GetVariableValue<Agent>("TargetedAgent").currentHealth -= agent.GetComponent<Enemy>().stats.ability2.damage * agent.GetComponent<Enemy>().stats.attack;
                    }
                    Debug.Log("Attacking finished");
                    numberOfAbilityUses = 0;
                    CombatManager.Instance.EndTurn(agent.GetComponent<Token>());
                    EndAction(true);
                }

                numberOfAbilityUses += 1;
                for (int i = 0; i < agent.GetComponent<Enemy>().stats.ability1.numberOfHits; i++)
                {

                    Debug.Log("Attacking agent");
                    _blackboard.GetVariableValue<Agent>("TargetedAgent").currentHealth -= agent.GetComponent<Enemy>().stats.ability1.damage * agent.GetComponent<Enemy>().stats.attack;
                }
                Debug.Log("Attacking finished");
                CombatManager.Instance.EndTurn(agent.GetComponent<Token>());
                EndAction(true);


            }
        }

		//Called when the task is disabled.
		protected override void OnStop(){
			
		}

		//Called when the task is paused.
		protected override void OnPause(){
			
		}
	}
}