using System.Collections.Generic;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.MeasureResource;
using Xbim.Ifc4.UtilityResource;

namespace XBim_Toolkit_Essentials.Models.View
{
    public class BasicModelOperationsViewModel
    {
        public string GlobalId { get; set; }
        public IfcLabel? Name { get; set; }
        public List<Properties> ListProperties { get; set; } = new List<Properties>();
    }

    public class Properties
    {
        public IfcIdentifier Name { get; set; }
        public IIfcValue NominalValue { get; set; }
    }
}
