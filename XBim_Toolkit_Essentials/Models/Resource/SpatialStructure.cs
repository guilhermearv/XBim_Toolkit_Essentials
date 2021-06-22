using System;
using System.Collections.Generic;
using System.Linq;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using XBim_Toolkit_Essentials.Models.View;

namespace XBim_Toolkit_Essentials.Models.Resource
{
    public class SpatialStructure
    {
        public List<SpatialStructureViewModel> ListSpatialStructureViewModel { get; set; } = new List<SpatialStructureViewModel>();
        public void Get()
        {
            const string file = "SampleHouse.ifc";

            using (var model = IfcStore.Open(file))
            {
                var project = model.Instances.FirstOrDefault<IIfcProject>();
                PrintHierarchy(project, 0);
            }
        }

        private void PrintHierarchy(IIfcObjectDefinition o, int level)
        {
            this.ListSpatialStructureViewModel.Add(new SpatialStructureViewModel()
            {
                Level = level,
                Name = o.Name,
                TypeName = o.GetType().Name
            });

            //only spatial elements can contain building elements
            var spatialElement = o as IIfcSpatialStructureElement;
            if (spatialElement != null)
            {
                //using IfcRelContainedInSpatialElement to get contained elements
                var containedElements = spatialElement.ContainsElements.SelectMany(rel => rel.RelatedElements);
                foreach (var element in containedElements)
                {                    
                    this.ListSpatialStructureViewModel.Add(new SpatialStructureViewModel()
                    {
                        Level = level,
                        Name = element.Name,
                        TypeName = element.GetType().Name
                    });
                }   
            }

            //using IfcRelAggregares to get spatial decomposition of spatial structure elements
            foreach (var item in o.IsDecomposedBy.SelectMany(r => r.RelatedObjects))
                PrintHierarchy(item, level + 1);

            
        }
    }
}
