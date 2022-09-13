using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Orienty_MapManager
{

    public class StateManager
    {
        public State state;

        public delegate void ShowInfoVertex(int v);
        public delegate void DeleteVertex(int v);
        public delegate void DrawVertex(Point p);
        public delegate void StopShowingNodeInfo();
        public delegate void StopDrawingNodes();
        public delegate void ReturnMovedNodeBackToWhereItWas();
        public delegate void StartDrawingFromVertex(int v);
        public delegate void DrawEdgeToVertex(int v);


        public ShowInfoVertex showInfoVertex;
        public DeleteVertex deleteVertex;
        public DrawVertex drawVertex;
        public StopShowingNodeInfo stopShowingNodeInfo;
        public StopDrawingNodes stopDrawingNodes;
        public ReturnMovedNodeBackToWhereItWas returnMovedNodeBackToWhereItWas;
        public StartDrawingFromVertex startDrawingFromVertex;
        public DrawEdgeToVertex drawEdgeToVertex;

        public void RV(int v)
        {
            switch (state)
            {
                case State.ReadyToSelect: 
                    deleteVertex(v);
                    break;
            }
        }

        public void RS()
        {
            switch (state)
            {
                case State.ShowingNodeInfo:
                    stopShowingNodeInfo();
                    state = State.ReadyToSelect;
                    break;
                case State.DrawingNodes:
                    stopDrawingNodes();
                    state = State.ReadyToSelect;
                    break;
                case State.MovingNode:
                    returnMovedNodeBackToWhereItWas();
                    state = State.ReadyToSelect;
                    break;
            }
        }

        public void L2V(int v)
        {
            switch (state)
            {
                case State.ReadyToSelect:
                    showInfoVertex(v);
                    state = State.ShowingNodeInfo;
                    break;
            }
        }

        public void LV_DownMouse(int v)
        {
            switch (state)
            {
                // TODO
            }
        }


        public enum State
        {
            ReadyToSelect,
            ShowingNodeInfo,
            DrawingNodes,
            MovingNode,
            WaitingMoveOrUpmouseAfterReadyToSelect,
            WaitingMoveOrUpmouseAfterDrawingNodes,
            WaitingMoveOrUpmouseShowingNodeInfo,
        }
    }
}
