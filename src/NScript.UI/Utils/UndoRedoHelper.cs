using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Utils
{
    public interface IUndoRedoHost<TState> where TState : struct, IEquatable<TState>
    {
        TState UndoRedoState { get; set; }
    }

    class UndoRedoHelper<TState> : WeakTimer.IWeakTimerSubscriber where TState : struct, IEquatable<TState>
    {
        private readonly IUndoRedoHost<TState> _host;

        private readonly LinkedList<TState> _states = new LinkedList<TState>();

        private LinkedListNode<TState> _currentNode;

        public int Limit { get; set; } = 10;

        public UndoRedoHelper(IUndoRedoHost<TState> host)
        {
            _host = host;
            WeakTimer.StartWeakTimer(this, TimeSpan.FromSeconds(1));
        }

        public void Undo()
        {
            if (_currentNode?.Previous != null)
            {
                _currentNode = _currentNode.Previous;
                _host.UndoRedoState = _currentNode.Value;
            }
        }

        public bool IsLastState => _currentNode != null && _currentNode.Next == null;

        public bool TryGetLastState(out TState _state)
        {
            _state = default(TState);
            if (!IsLastState)
                return false;

            _state = _currentNode.Value;
            return true;
        }

        public bool HasState => _currentNode != null;
        public void UpdateLastState(TState state)
        {
            _states.Last.Value = state;
        }

        public void UpdateLastState()
        {
            UpdateLastState(_host.UndoRedoState);
        }

        public void DiscardRedo()
        {
            while (_currentNode?.Next != null)
                _states.Remove(_currentNode.Next);
        }

        public void Redo()
        {
            if (_currentNode?.Next != null)
            {
                _currentNode = _currentNode.Next;
                _host.UndoRedoState = _currentNode.Value;
            }
        }

        public void Snapshot()
        {
            var current = _host.UndoRedoState;
            if (_currentNode == null || !_currentNode.Value.Equals(current))
            {
                if (_currentNode?.Next != null)
                    DiscardRedo();
                _states.AddLast(current);
                _currentNode = _states.Last;
                if (_states.Count > Limit)
                    _states.RemoveFirst();
            }
        }

        public void Clear()
        {
            _states.Clear();
            _currentNode = null;
        }

        public bool Tick()
        {
            Snapshot();
            return true;
        }
    }
}
