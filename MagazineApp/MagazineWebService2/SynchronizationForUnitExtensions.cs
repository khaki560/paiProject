﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace MagazineApp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;
    using Models;

    /// <summary>
    /// Extension methods for SynchronizationForUnit.
    /// </summary>
    public static partial class SynchronizationForUnitExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static object IsSynchronize(this ISynchronizationForUnit operations)
            {
                return Task.Factory.StartNew(s => ((ISynchronizationForUnit)s).IsSynchronizeAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> IsSynchronizeAsync(this ISynchronizationForUnit operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.IsSynchronizeWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static object GetLocations(this ISynchronizationForUnit operations)
            {
                return Task.Factory.StartNew(s => ((ISynchronizationForUnit)s).GetLocationsAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetLocationsAsync(this ISynchronizationForUnit operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetLocationsWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static object Synchronize(this ISynchronizationForUnit operations)
            {
                return Task.Factory.StartNew(s => ((ISynchronizationForUnit)s).SynchronizeAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> SynchronizeAsync(this ISynchronizationForUnit operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SynchronizeWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
