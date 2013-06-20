using System.ServiceModel.Description;

namespace NameBright.DomainApi.Soap.OAuth2Helpers
{
    /// <summary>
    /// Fluff for WCF to wire up the OAuth2HeaderInspector
    /// </summary>
    public class OAuth2HeaderBehavior : IEndpointBehavior
    {

        public OAuth2HeaderBehavior() {}

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters) {}

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            OAuth2HeaderInspector inspector = new OAuth2HeaderInspector();
            clientRuntime.MessageInspectors.Add(inspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher) {}

        public void Validate(ServiceEndpoint endpoint) {}

        #endregion
    }
}
