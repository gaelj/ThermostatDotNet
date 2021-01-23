using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ThermostatDotNet.Client
{
    internal static class KnownValueHelper
    {
        public static bool Compare(string expected, string value)
            => expected.Replace(" ", "").Equals(value.Replace(" ", ""), StringComparison.InvariantCultureIgnoreCase);
    }

    public static class KnownActionClosureTypes
    {
        private static bool IsInitialized = false;
        /*public static int Positive { get; private set; }
        public static int Negative { get; private set; }*/

        internal static async Task Initialize(IThermostatDotNetService actionService,
                                              CancellationToken cancellationToken = default(CancellationToken))
        {
            if (IsInitialized)
                return;
            /*var knownTypes = await actionService.ActionTypes
                                                .GetAllActionClosureTypesAsync(true, cancellationToken);
            Positive = knownTypes.Single(t => KnownValueHelper.Compare("Positive", t.Name)).Id;
            Negative = knownTypes.Single(t => KnownValueHelper.Compare("Negative", t.Name)).Id;*/
        }
    }

    public class KnownActionStatuses
    {
        private static bool IsInitialized = false;
        public static int New { get; private set; }
        public static int InProgress { get; private set; }
        public static int WaitingForValidation { get; private set; }
        public static int Closed { get; private set; }
        public static int Cancelled { get; private set; }

        internal static async Task Initialize(IThermostatDotNetService actionService,
                                              CancellationToken cancellationToken = default(CancellationToken))
        {
            if (IsInitialized)
                return;
            /* var knownTypes = await actionService.ActionTypes
                                                .GetAllActionStatusesAsync(cancellationToken);
            New = knownTypes.Single(t => KnownValueHelper.Compare("New", t.Name)).Id;
            InProgress = knownTypes.Single(t => KnownValueHelper.Compare("InProgress", t.Name)).Id;
            WaitingForValidation = knownTypes.Single(t => KnownValueHelper.Compare("WaitingForValidation", t.Name)).Id;
            Closed = knownTypes.Single(t => KnownValueHelper.Compare("Closed", t.Name)).Id;
            Cancelled = knownTypes.Single(t => KnownValueHelper.Compare("Cancelled", t.Name)).Id; */
        }
    }

    /* public class KnownActionRelationshipTypes
    {
        private static bool IsInitialized = false;
        public static int Parent { get; private set; }
        public static int Duplicate { get; private set; }
        public static int RelatesTo { get; private set; }
        public static int Blocks { get; private set; }
        public static int Replaces { get; private set; }

        internal static async Task Initialize(IThermostatDotNetService actionService,
                                              CancellationToken cancellationToken = default(CancellationToken))
        {
            if (IsInitialized)
                return;
            var knownTypes = await actionService.ActionTypes
                                                .GetAllActionRelationshipTypesAsync(cancellationToken);
            Parent = knownTypes.Single(t => KnownValueHelper.Compare("Parent", t.Name)).Id;
            Duplicate = knownTypes.Single(t => KnownValueHelper.Compare("Duplicate", t.Name)).Id;
            RelatesTo = knownTypes.Single(t => KnownValueHelper.Compare("RelatesTo", t.Name)).Id;
            Blocks = knownTypes.Single(t => KnownValueHelper.Compare("Blocks", t.Name)).Id;
            Replaces = knownTypes.Single(t => KnownValueHelper.Compare("Replaces", t.Name)).Id;
        }
    } */
}