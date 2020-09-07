using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExportValidationChecker.Tests
{
    public class BaseTest
    {
        public virtual string TestTitle => "";

        public virtual bool Execute(SearchAttributeDetails value, List<Entity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
