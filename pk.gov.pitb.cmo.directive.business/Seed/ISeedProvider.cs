using System;
using System.Collections.Generic;
using System.Text;

namespace pk.gov.pitb.cmo.directive.business.Seed
{
    public interface ISeedProvider
    {
        void InitProduction();
        void InitDevelopment();
    }
}
