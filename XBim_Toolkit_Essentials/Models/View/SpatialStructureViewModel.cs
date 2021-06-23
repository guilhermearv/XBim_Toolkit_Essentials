using System.Collections.Generic;
using Xbim.Ifc4.MeasureResource;

namespace XBim_Toolkit_Essentials.Models.View
{
    public class SpatialStructureViewModel
    {
        public string GlobalId { get; set; }
        public int Level { get; set; }
        public IfcLabel? Name { get; set; }
        public string TypeName { get; set; }
        public List<SpatialStructureViewModel> ChildrenSpatialStructureViewModel { get; set; } = new List<SpatialStructureViewModel>();
    }
}
