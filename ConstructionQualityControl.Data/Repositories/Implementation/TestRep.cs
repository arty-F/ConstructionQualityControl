using ConstructionQualityControl.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace ConstructionQualityControl.Data.Repositories.Implementation
{
    public class TestRep : ITestRep
    {
        private readonly QualityControlContext context;

        public TestRep(QualityControlContext context) => this.context = context;
        
        public void Get()
        {
            var a = context.Cities.FirstOrDefault();
        }
    }
}
