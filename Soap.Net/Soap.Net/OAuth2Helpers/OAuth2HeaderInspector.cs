using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace NameBright.DomainApi.Soap.OAuth2Helpers
{
    /// <summary>
    /// Before sending a SOAP request this inspector will set the "Authorization" header with an OAuth2 Bearer token 
    /// retrieved from the namebright api's auth service:https://api.namebright.com/auth. 
    /// See: https://api.namebright.com/auth/Help for details on using the auth api.
    /// </summary>
    public class OAuth2HeaderInspector : IClientMessageInspector
    {
        public static string AccessToken { get; set; }

        public static string BearerString
        {
            get
            {
                return String.Format("Bearer {0}", AccessToken ?? String.Empty);
            }
        }

        public const string AUTHORIZATION_HEADER = "Authorization";

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            return;
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            HttpRequestMessageProperty httpRequestMessage;
            object httpRequestMessageObject;
            if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
            {
                httpRequestMessage = httpRequestMessageObject as HttpRequestMessageProperty;
                if (string.IsNullOrEmpty(httpRequestMessage.Headers[AUTHORIZATION_HEADER]))
                {
                    httpRequestMessage.Headers[AUTHORIZATION_HEADER] = BearerString;
                }
            }
            else
            {
                httpRequestMessage = new HttpRequestMessageProperty();
                httpRequestMessage.Headers.Add(AUTHORIZATION_HEADER, BearerString);
                request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);
            }
            return null;            
        }
    }
}
