using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Input
{
    /// <summary>
    /// A raw input event.
    /// </summary>
    /// <remarks>
    /// Raw input events are sent from the windowing subsystem to the <see cref="InputManager"/>
    /// for processing: this gives an application the opportunity to pre-process the event. After
    /// pre-processing they are consumed by the relevant <see cref="Device"/> and turned into
    /// standard Avalonia events.
    /// </remarks>
    public class RawInputEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawInputEventArgs"/> class.
        /// </summary>
        /// <param name="device">The associated device.</param>
        /// <param name="timestamp">The event timestamp.</param>
        public RawInputEventArgs(IInputDevice device, uint timestamp)
        {
            Device = device;
            Timestamp = timestamp;
        }

        /// <summary>
        /// Gets the associated device.
        /// </summary>
        public IInputDevice Device { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the event was handled.
        /// </summary>
        /// <remarks>
        /// If an event is not marked handled after processing via the
        /// <see cref="InputManager"/>, then it will be passed on to the underlying OS for
        /// handling.
        /// </remarks>
        public bool Handled { get; set; }

        /// <summary>
        /// Gets the timestamp associated with the event.
        /// </summary>
        public uint Timestamp { get; private set; }
    }

    public interface IInputDevice
    {
        /// <summary>
        /// Processes raw event. Is called after preprocessing by InputManager
        /// </summary>
        /// <param name="ev"></param>
        void ProcessRawEvent(RawInputEventArgs ev);
    }

    [Flags]
    public enum InputModifiers
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8,
        LeftMouseButton = 16,
        RightMouseButton = 32,
        MiddleMouseButton = 64
    }

    [Flags]
    public enum KeyStates
    {
        None = 0,
        Down = 1,
        Toggled = 2,
    }

    public class KeyEventArgs : BaseEventArgs
    {
        public Key Key { get; set; }

        public InputModifiers Modifiers { get; set; }
    }
}
