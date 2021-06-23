using System;
using System.Linq;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using XBim_Toolkit_Essentials.Models.View;

namespace XBim_Toolkit_Essentials.Models.Resource
{
    public class BasicModelOperationsResource
    {
        public string GlobalId { get; set; }
        public BasicModelOperationsViewModel BasicModelOperationsViewModel { get; set; } = new BasicModelOperationsViewModel();
        public void Retrieve()
        {
            var filePath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\AppData\Local\Temp\XBim_Toolkit_Essentials\");
            string fileName = filePath + "SampleHouse.ifc";
            using (var model = IfcStore.Open(fileName))
            {
                //get all doors in the model (using IFC4 interface of IfcDoor this will work both for IFC2x3 and IFC4)
                var allDoors = model.Instances.OfType<IIfcDoor>();

                //get only doors with defined IIfcTypeObject
                var someDoors = model.Instances.Where<IIfcDoor>(d => d.IsTypedBy.Any());

                //get one single door 
                var theDoor = model.Instances.FirstOrDefault<IIfcDoor>(d => d.GlobalId == this.GlobalId);
                this.BasicModelOperationsViewModel.GlobalId = theDoor.GlobalId;
                this.BasicModelOperationsViewModel.Name = theDoor.Name;

                //get all single-value properties of the door
                var properties = theDoor.IsDefinedBy
                    .Where(r => r.RelatingPropertyDefinition is IIfcPropertySet)
                    .SelectMany(r => ((IIfcPropertySet)r.RelatingPropertyDefinition).HasProperties)
                    .OfType<IIfcPropertySingleValue>();

                foreach (var property in properties)
                {
                    this.BasicModelOperationsViewModel.ListProperties.Add(new Properties
                    {
                        Name = property.Name,
                        NominalValue = property.NominalValue
                    });
                }
                    
            }
        }
    }
}
