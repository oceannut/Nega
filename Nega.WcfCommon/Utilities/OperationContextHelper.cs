﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Nega.WcfCommon
{

    public static class OperationContextHelper
    {

        public static string GetIP(OperationContext operationContext = null)
        {
            RemoteEndpointMessageProperty endpointProperty = GetRemoteEndpoint(operationContext);
            return endpointProperty.Address;
        }

        public static IPEndPoint GetIPEndPoint(OperationContext operationContext = null)
        {
            RemoteEndpointMessageProperty endpointProperty = GetRemoteEndpoint(operationContext);
            return new IPEndPoint(IPAddress.Parse(endpointProperty.Address), endpointProperty.Port);
        }

        private static RemoteEndpointMessageProperty GetRemoteEndpoint(OperationContext operationContext = null)
        {
            var context = operationContext;
            if (context == null)
            {
                context = OperationContext.Current;
            }

            MessageProperties messageProperties = context.IncomingMessageProperties;
            return messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
        }

    }

}
