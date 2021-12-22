﻿using MasterServerToolkit.Utils;
using System;
using System.Collections.Generic;

namespace MasterServerToolkit.Networking
{
    /// <summary>
    /// This is an object which gets spawned into game once.
    /// It's main purpose is to call update methods
    /// </summary>
    public class MstUpdateRunner : DynamicSingletonBehaviour<MstUpdateRunner>
    {
        /// <summary>
        /// List of <see cref="IUpdatable"/>
        /// </summary>
        private List<IUpdatable> _updatebles = new List<IUpdatable>();

        public int? Count => _updatebles?.Count;

        private void Update()
        {
            for (int i = 0; i < _updatebles.Count; i++)
            {
                IUpdatable runnable = _updatebles[i];
                runnable?.Update();
            }
        }

        /// <summary>
        /// Adds <see cref="IUpdatable"/> to list of updates that are running in main Unity thread
        /// </summary>
        /// <param name="updatable"></param>
        public void Add(IUpdatable updatable)
        {
            if (!_updatebles.Contains(updatable))
            {
                _updatebles.Add(updatable);
            }
        }

        /// <summary>
        /// Removes <see cref="IUpdatable"/> from list of updates that are running in main Unity thread
        /// </summary>
        /// <param name="updatable"></param>
        public void Remove(IUpdatable updatable)
        {
            _updatebles.Remove(updatable);
        }
    }
}