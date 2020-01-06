﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace MagazineWebService2
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;
    using Models;

    /// <summary>
    /// Extension methods for Unit.
    /// </summary>
    public static partial class UnitExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<UnitEntry> GetAllProducts(this IUnit operations)
            {
                return Task.Factory.StartNew(s => ((IUnit)s).GetAllProductsAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<UnitEntry>> GetAllProductsAsync(this IUnit operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetAllProductsWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static object GetProduct(this IUnit operations, int id)
            {
                return Task.Factory.StartNew(s => ((IUnit)s).GetProductAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetProductAsync(this IUnit operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetProductWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='name'>
            /// </param>
            /// <param name='count'>
            /// </param>
            /// <param name='price'>
            /// </param>
            public static object AddMagazineProduct(this IUnit operations, string name, int count, double price)
            {
                return Task.Factory.StartNew(s => ((IUnit)s).AddMagazineProductAsync(name, count, price), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='name'>
            /// </param>
            /// <param name='count'>
            /// </param>
            /// <param name='price'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> AddMagazineProductAsync(this IUnit operations, string name, int count, double price, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.AddMagazineProductWithHttpMessagesAsync(name, count, price, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='count'>
            /// </param>
            /// <param name='price'>
            /// </param>
            public static object ModifyMagazineProduct(this IUnit operations, int id, int count, double price)
            {
                return Task.Factory.StartNew(s => ((IUnit)s).ModifyMagazineProductAsync(id, count, price), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='count'>
            /// </param>
            /// <param name='price'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> ModifyMagazineProductAsync(this IUnit operations, int id, int count, double price, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ModifyMagazineProductWithHttpMessagesAsync(id, count, price, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static object RemoveMagazineProduct(this IUnit operations, int id)
            {
                return Task.Factory.StartNew(s => ((IUnit)s).RemoveMagazineProductAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> RemoveMagazineProductAsync(this IUnit operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.RemoveMagazineProductWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static object RemoveAllMagazineProducts(this IUnit operations)
            {
                return Task.Factory.StartNew(s => ((IUnit)s).RemoveAllMagazineProductsAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> RemoveAllMagazineProductsAsync(this IUnit operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.RemoveAllMagazineProductsWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
