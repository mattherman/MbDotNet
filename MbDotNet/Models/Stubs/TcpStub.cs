﻿using System;
using System.Collections.Generic;
using MbDotNet.Enums;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;

namespace MbDotNet.Models.Stubs
{
    public class TcpStub : StubBase
    {

        /// <summary>
        /// Adds a predicate to the stub that will match when the request data equals the specified data.
        /// </summary>
        /// <param name="data">The data to match on</param>
        /// <returns>The stub that the predicate was added to</returns>
        public TcpStub OnDataEquals(string data)
        {
            var fields = new TcpPredicateFields
            {
                Data = data
            };

            var predicate = new EqualsPredicate<TcpPredicateFields>(fields);

            return On(predicate);
        }

        /// <summary>
        /// Adds a predicate to the stub
        /// </summary>
        /// <param name="predicate">The predicate object designating what the stub will match on</param>
        /// <returns>The stub that the predicate was added to</returns>
        public TcpStub On(PredicateBase predicate)
        {
            Predicates.Add(predicate);
            return this;
        }

        /// <summary>
        /// Adds a response to the stub that will return the specified data.
        /// </summary>
        /// <param name="data">The data to be returned</param>
        /// <param name="latencyInMilliseconds">The number of milliseconds to be waiting before response will be returned</param>
        /// <returns>The stub that the response was added to</returns>
        public TcpStub ReturnsData(string data, int? latencyInMilliseconds = null)
        {
            var fields = new TcpResponseFields
            {
                Data = data
            };

            var behavior = latencyInMilliseconds.HasValue
                ? new Behavior
                {
                    LatencyInMilliseconds = latencyInMilliseconds
                }
                : null;

            var response = new IsResponse<TcpResponseFields>(fields, behavior);

            return Returns(response);
        }

        /// <summary>
        /// Adds a response to the stub.
        /// </summary>
        /// <param name="response">The response object designating what the stub will return</param>
        /// <returns>The stub that the response was added to</returns>
        public TcpStub Returns(ResponseBase response)
        {
            Responses.Add(response);
            return this;
        }

        /// <summary>
        /// Adds a proxy to the stub with predicate generators containing specific values
        /// </summary>
        /// <param name="to">endpoint address to proxy to</param>
        /// <param name="proxyMode">proxyalways, proxyonce or proxytransparent</param>
        /// <param name="predicateGenerators">list of predicates that a proxy repsonse will be recorded for</param>
        /// <param name="latencyInMilliseconds">The number of milliseconds to be waiting before response will be returned</param>
        /// <returns>The stub that the response was added to</returns>
        public TcpStub ReturnsProxy(Uri to, ProxyMode proxyMode,
            IList<MatchesPredicate<TcpPredicateFields>> predicateGenerators, int? latencyInMilliseconds = null)
        {
            var fields = new ProxyResponseFields<TcpPredicateFields>
            {
                To = to,
                Mode = proxyMode,
                PredicateGenerators = predicateGenerators
            };

            var behavior = latencyInMilliseconds.HasValue
                ? new Behavior
                {
                    LatencyInMilliseconds = latencyInMilliseconds
                }
                : null;

            var response = new ProxyResponse<ProxyResponseFields<TcpPredicateFields>>(fields, behavior);

            return Returns(response);
        }

        /// <summary>
        /// Adds a proxy to the stub with predicate generators containing booleans
        /// </summary>
        /// <param name="to">endpoint address to proxy to</param>
        /// <param name="proxyMode">proxyalways, proxyonce or proxytransparent</param>
        /// <param name="predicateGenerators">list of predicates that a proxy repsonse will be recorded for</param>
        /// <returns>The stub that the response was added to</returns>
        public TcpStub ReturnsProxy(Uri to, ProxyMode proxyMode,
            IList<MatchesPredicate<TcpBooleanPredicateFields>> predicateGenerators, int? latencyInMilliseconds = null)
        {
            var fields = new ProxyResponseFields<TcpBooleanPredicateFields>
            {
                To = to,
                Mode = proxyMode,
                PredicateGenerators = predicateGenerators
            };

            var behavior = latencyInMilliseconds.HasValue
                ? new Behavior
                {
                    LatencyInMilliseconds = latencyInMilliseconds
                }
                : null;

            var response = new ProxyResponse<ProxyResponseFields<TcpBooleanPredicateFields>>(fields, behavior);

            return Returns(response);
        }
    }
}
