using System;
using System.ServiceModel.Configuration;

namespace NameBright.DomainApi.Soap.OAuth2Helpers
{
    /// <summary>
    /// Fluff for WCF to wire up the OAuth2HeaderInspector
    /// </summary>
    public class OAuth2HeaderBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(OAuth2HeaderBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new OAuth2HeaderBehavior();
        }
    }
}
