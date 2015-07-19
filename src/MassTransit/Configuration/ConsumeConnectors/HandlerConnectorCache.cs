// Copyright 2007-2014 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.ConsumeConnectors
{
    using System;
    using System.Threading;


    public class HandlerConnectorCache<TMessage> :
        IHandlerConnectorCache<TMessage>
        where TMessage : class
    {
        readonly Lazy<HandlerConnector<TMessage>> _connector;

        HandlerConnectorCache()
        {
            _connector = new Lazy<HandlerConnector<TMessage>>(() => new HandlerConnector<TMessage>());
        }

        public static IHandlerConnector<TMessage> Connector
        {
            get { return InstanceCache.Cached.Value.Connector; }
        }

        IHandlerConnector<TMessage> IHandlerConnectorCache<TMessage>.Connector
        {
            get { return _connector.Value; }
        }


        static class InstanceCache
        {
            internal static readonly Lazy<IHandlerConnectorCache<TMessage>> Cached = new Lazy<IHandlerConnectorCache<TMessage>>(
                () => new HandlerConnectorCache<TMessage>(), LazyThreadSafetyMode.PublicationOnly);
        }
    }
}