using System.Collections.Generic;
using DefaultNamespace.Models.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Handlers
{
    public class TimeHandler : ITickable, ITimeHandler, IFixedTickable
    {
        private float _accumulatedTime = 0f;
        private bool _isRunning = false;
        private readonly HashSet<ITimeUpdatable> _scheduledToAddUpdatables = new HashSet<ITimeUpdatable>();
        private readonly HashSet<ITimeUpdatable> _scheduledToRemoveUpdatables = new HashSet<ITimeUpdatable>();
        private readonly HashSet<ITimeUpdatable> _updatables;

        public TimeHandler(List<ITimeUpdatable> updatables)
        {
            _updatables = updatables.ToHashSet();
        }

        public void Tick()
        {
            foreach (var updatable in _updatables)
            {
                _isRunning = true;
                updatable.UpdateTime();
            }
        }

        public void FixedTick()
        {
            _accumulatedTime += Time.deltaTime;

            if (!(_accumulatedTime >= 1f)) return;

            foreach (var updatable in _updatables)
            {
                _isRunning = true;
                updatable.FixedUpdateTime();
            }

            _accumulatedTime = 0f;
            _isRunning = false;
            ProcessScheduledUpdates();
        }

        public void Subscribe(ITimeUpdatable updatable)
        {
            if (TryAddToScheduled(updatable, _scheduledToAddUpdatables))
                return;

            _updatables.Add(updatable);
            _scheduledToAddUpdatables.Remove(updatable);
        }

        public void Unsubscribe(ITimeUpdatable updatable)
        {
            if (TryAddToScheduled(updatable, _scheduledToRemoveUpdatables))
                return;

            _updatables.Remove(updatable);
            _scheduledToRemoveUpdatables.Remove(updatable);
        }
        
        private void ProcessScheduledUpdates()
        {
            if (_scheduledToAddUpdatables.Count > 0)
            {
                foreach (var updatable in _scheduledToAddUpdatables)
                {
                    if(updatable == null)
                        _updatables.Add(updatable);
                }

                _scheduledToAddUpdatables.Clear();
            }

            if (_scheduledToRemoveUpdatables.Count <= 0) return;

            foreach (var updatable in _scheduledToRemoveUpdatables)
            {
                _updatables.Remove(updatable);
            }

            _scheduledToRemoveUpdatables.Clear();
        }

        private bool TryAddToScheduled(ITimeUpdatable updatable, HashSet<ITimeUpdatable> scheduledSet)
        {
            if (!_isRunning) return false;

            if (!scheduledSet.Contains(updatable))
                scheduledSet.Add(updatable);
            return true;
        }
    }

}