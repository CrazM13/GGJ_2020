using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum TurnStage
    {
        Invalid = 0,
        GetNewUnits = 1,
        IssueCommands = 2,
        EndTurn = 3,
        ActionsExecute = 4,
        CheckWin = 5,
        ProblemsSpread = 6,
        GetNewProblems = 7,
    }

    public TurnStage CurrentStage = TurnStage.Invalid;

    void ChangeStage(TurnStage newStage)
    {
        if(CurrentStage != newStage)
        {
            CurrentStage = newStage;
            OnStageStart();
        }
    }

    void OnStageStart()
    {
        switch(CurrentStage)
        {
            case TurnStage.GetNewUnits:
                GetNewUnits();
                break;
            case TurnStage.IssueCommands:
                IssueCommands();
                break;
            case TurnStage.EndTurn:
                EnterEndTurnPhase();
                break;
            case TurnStage.ActionsExecute:
                ActionsExecute();
                break;
            case TurnStage.CheckWin:
                CheckWin();
                break;
            case TurnStage.ProblemsSpread:
                ProblemsSpread();
                break;
            case TurnStage.GetNewProblems:
                GetNewProblems();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeStage(TurnStage.GetNewUnits);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetNewUnits()
    {
        Debug.Log("Turn Manager: Getting new units");
        ChangeStage(TurnStage.IssueCommands);
    }

    void IssueCommands()
    {
        Debug.Log("Turn Manager: Issuing Commands...");
    }

    void EnterEndTurnPhase()
    {
        Debug.Log("Turn Manager: Ending the turn");
        ChangeStage(TurnStage.ActionsExecute);
    }
    
    // Called from UI
    public void HandleEndTurnButton()
    {
        if(CurrentStage == TurnStage.IssueCommands)
        {
            ChangeStage(TurnStage.EndTurn);
        }
    }

    void ActionsExecute()
    {
        Debug.Log("Turn Manager: Actions execute");

        // todo

        ChangeStage(TurnStage.CheckWin);
    }

    void CheckWin()
    {
        Debug.Log("Turn Manager: Checking for win condition");

        // todo

        ChangeStage(TurnStage.ProblemsSpread);//only if you didn't win!
    }

    void ProblemsSpread()
    {
        Debug.Log("Turn Manager: Problems Spread");

        // todo

        ChangeStage(TurnStage.GetNewProblems);
    }

    void GetNewProblems()
    {
        Debug.Log("Turn Manager: Getting new problems");

        // todo


        ChangeStage(TurnStage.GetNewUnits);
    }
}
