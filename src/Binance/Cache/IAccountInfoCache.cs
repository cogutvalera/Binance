﻿using Binance.Account;
using Binance.Api;
using Binance.Api.WebSocket;
using Binance.Cache.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Cache
{
    public interface IAccountInfoCache : IDisposable
    {
        #region Public Events

        /// <summary>
        /// Account cache update event.
        /// </summary>
        event EventHandler<AccountCacheEventArgs> Update;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// The account. Can be null if not yet synchronized or out-of-sync.
        /// </summary>
        AccountInfo Account { get; }

        /// <summary>
        /// The client that provides account synchronization.
        /// </summary>
        IUserDataWebSocketClient Client { get; }
        
        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task SubscribeAsync(IBinanceApiUser user, CancellationToken token = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="callback"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task SubscribeAsync(IBinanceApiUser user, Action<AccountCacheEventArgs> callback, CancellationToken token = default);

        /// <summary>
        /// Link to a subscribed <see cref="IUserDataWebSocketClient"/>.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="callback"></param>
        /// <param name="leaveClientOpen"></param>
        /// <returns></returns>
        void LinkTo(IUserDataWebSocketClient client, Action<AccountCacheEventArgs> callback = null, bool leaveClientOpen = true);

        #endregion Public Methods
    }
}